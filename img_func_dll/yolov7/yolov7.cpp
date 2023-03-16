#include "yolov7.h"
#define MAX_IMAGE_INPUT_SIZE_THRESH 5000 * 5000
#define CHECK(status) \
    do\
    {\
        auto ret = (status);\
        if (ret != 0)\
        {\
            std::cerr << "Cuda failure: " << ret << std::endl;\
            abort();\
        }\
    } while (0)

using namespace nvinfer1;
static Logger gLogger;

cv::Mat YOLO7::static_resize(cv::Mat& img) {

    float r = std::min(basic_config.INPUT_W / (img.cols * 1.0), basic_config.INPUT_H / (img.rows * 1.0));
    cv::Mat out(basic_config.INPUT_W, basic_config.INPUT_H, CV_8UC3, cv::Scalar(114, 114, 114));
    if (r == 1.0)
    {
        img.copyTo(out);
    }
    else
    {
        int unpad_w = r * img.cols;
        int unpad_h = r * img.rows;
        cv::Mat re(unpad_h, unpad_w, CV_8UC3);
        cv::resize(img, re, re.size());
        re.copyTo(out(cv::Rect(0, 0, re.cols, re.rows)));
    }
    
    return out;
}
float* YOLO7::blobFromImage(cv::Mat& img) {
    cv::cvtColor(img, img, cv::COLOR_BGR2RGB);

    float* blob = new float[img.total() * 3];
    int channels = 3;
    int img_h = img.rows;
    int img_w = img.cols;
    for (size_t c = 0; c < channels; c++)
    {
        for (size_t h = 0; h < img_h; h++)
        {
            for (size_t w = 0; w < img_w; w++)
            {
                blob[c * img_w * img_h + h * img_w + w] =
                    (((float)img.at<cv::Vec3b>(h, w)[c]) / 255.0f);
            }
        }
    }
    return blob;
}

void YOLO7::qsort_descent_inplace(std::vector<boxinfo>& faceobjects, int left, int right)
{
    int i = left;
    int j = right;
    float p = faceobjects[(left + right) / 2].prob;

    while (i <= j)
    {
        while (faceobjects[i].prob > p)
            i++;

        while (faceobjects[j].prob < p)
            j--;

        if (i <= j)
        {
            // swap
            std::swap(faceobjects[i], faceobjects[j]);

            i++;
            j--;
        }
    }

#pragma omp parallel sections
    {
#pragma omp section
        {
            if (left < j) qsort_descent_inplace(faceobjects, left, j);
        }
#pragma omp section
        {
            if (i < right) qsort_descent_inplace(faceobjects, i, right);
        }
    }
}

static inline float intersection_area(const boxinfo& a, const boxinfo& b)
{
    cv::Rect_<float> inter = a.rect & b.rect;
    return inter.area();
}

void YOLO7::qsort_descent_inplace(std::vector<boxinfo>& objects)
{
    if (objects.empty())
        return;

    qsort_descent_inplace(objects, 0, objects.size() - 1);
}

std::vector<boxinfo> YOLO7::nms(std::vector<boxinfo>& res,float nms_thresh = 0.5) {
    std::vector<std::vector<boxinfo>> classed(num_class);
    std::vector<boxinfo> final_res;
    for(auto s: res)
    {
        classed[s.label].push_back(s);
    }
    for (auto s : classed)
    {
        std::sort(s.begin(),s.end(), sort_conf);
        std::vector<int> del(s.size(),1);
        for (int i=0;i< s.size();i++)
        {
            if (del[i] == 0)
            {
                continue;
            }
            if (i + 1 >= s.size())
            {
                break;
            }
            for(int j=i+1;j< s.size();j++)
            {
                float inter_area = intersection_area(s[i], s[j]);
                float union_area = s[i].rect.area() + s[j].rect.area() - inter_area;
                float IoU = inter_area / union_area;
                if (IoU > nms_thresh)
                {
                    del[j] = 0;
                }
            }
            final_res.push_back(s[i]);
        }        
    }
    return final_res;
}
void YOLO7::generate_yolo_proposals(float* feat_blob, int output_size, float prob_threshold, std::vector<boxinfo>& objects)
{
    auto dets = output_size / (num_class + 5);
    for (int boxs_idx = 0; boxs_idx < dets; boxs_idx++)
    {
        const int basic_pos = boxs_idx * (num_class + 5);
        float x_center = feat_blob[basic_pos + 0];
        float y_center = feat_blob[basic_pos + 1];
        float w = feat_blob[basic_pos + 2];
        float h = feat_blob[basic_pos + 3];
        float x0 = x_center - w * 0.5f;
        float y0 = y_center - h * 0.5f;
        float box_objectness = feat_blob[basic_pos + 4];
        // std::cout<<*feat_blob<<std::endl;
        for (int class_idx = 0; class_idx < num_class; class_idx++)
        {
            float box_cls_score = feat_blob[basic_pos + 5 + class_idx];
            float box_prob = box_objectness * box_cls_score;
            if (box_prob > prob_threshold)
            {
                boxinfo obj;
                obj.rect.x = x0;
                obj.rect.y = y0;
                obj.rect.width = w;
                obj.rect.height = h;
                obj.label = class_idx;
                obj.prob = box_prob;

                objects.push_back(obj);
            }

        } // class loop
    }

}

void YOLO7::decode_outputs(float* prob, int output_size, std::vector<inf_res>& objects, float scale, int img_w, int img_h) {
    std::vector<boxinfo> proposals;
    generate_yolo_proposals(prob, output_size, basic_config.CONF_THRESH, proposals);
    std::vector<boxinfo> res = nms(proposals, basic_config.NMS_THRESH);
    for (auto s:res)
    {
        inf_res trans_info;
        trans_info.box = s.rect;
        trans_info.box_name = basic_config.classname[s.label];
        trans_info.conf = s.prob;

        // adjust offset to original unpadded
        float x0 = (trans_info.box.x) / scale;
        float y0 = (trans_info.box.y) / scale;
        float x1 = (trans_info.box.x + trans_info.box.width) / scale;
        float y1 = (trans_info.box.y + trans_info.box.height) / scale;

        // clip
        x0 = std::max(std::min(x0, (float)(img_w - 1)), 0.f);
        y0 = std::max(std::min(y0, (float)(img_h - 1)), 0.f);
        x1 = std::max(std::min(x1, (float)(img_w - 1)), 0.f);
        y1 = std::max(std::min(y1, (float)(img_h - 1)), 0.f);

        trans_info.box.x = x0;
        trans_info.box.y = y0;
        trans_info.box.width = x1 - x0;
        trans_info.box.height = y1 - y0;
        objects.push_back(trans_info);
    }
}



int YOLO7::init(trt_basic_config input_config, std::string modelpath)
{
    basic_config = input_config;
    size_t size{ 0 };
    v7_dec.trtModelStream = nullptr;
    num_class = input_config.classname.size();
    std::ifstream file(modelpath+basic_config.engine_name+".engine", std::ios::binary);
    if (!file.good()) {
        std::cerr << "¶ÁÈ¡ " << input_config.engine_name << " ´íÎó!" << std::endl;

        return -1;
    }
    file.seekg(0, file.end);
    size = file.tellg();
    file.seekg(0, file.beg);
    v7_dec.trtModelStream = new char[size];
    assert(v7_dec.trtModelStream);
    file.read(v7_dec.trtModelStream, size);
    file.close();
    v7_dec.runtime = createInferRuntime(gLogger);
    assert(v7_dec.runtime != nullptr);
    v7_dec.engine = v7_dec.runtime->deserializeCudaEngine(v7_dec.trtModelStream, size);
    assert(v7_dec.engine != nullptr);
    v7_dec.context = v7_dec.engine->createExecutionContext();
    assert(v7_dec.context != nullptr);
    delete[] v7_dec.trtModelStream;
    auto out_dims = v7_dec.engine->getBindingDimensions(1);
    for (int j = 0; j < out_dims.nbDims; j++) {
        output_size *= out_dims.d[j];
    }
    v7_dec.output = new float[output_size];
    assert(v7_dec.engine->getNbBindings() == 2);
    void* buffers[2];
    v7_dec.inputIndex = v7_dec.engine->getBindingIndex(INPUT_BLOB_NAME);

    assert(v7_dec.engine->getBindingDataType(v7_dec.inputIndex) == nvinfer1::DataType::kFLOAT);
    v7_dec.outputIndex = v7_dec.engine->getBindingIndex(OUTPUT_BLOB_NAME);
    assert(v7_dec.engine->getBindingDataType(v7_dec.outputIndex) == nvinfer1::DataType::kFLOAT);
    assert(v7_dec.inputIndex == 0);
    assert(v7_dec.outputIndex == 1);
    int mBatchSize = v7_dec.engine->getMaxBatchSize();
    CHECK(cudaMalloc((void**)&v7_dec.buffers[v7_dec.inputIndex], 3 * basic_config.INPUT_H * basic_config.INPUT_W * sizeof(float)));
    CHECK(cudaMalloc((void**)&v7_dec.buffers[v7_dec.outputIndex], output_size * sizeof(float)));
    CHECK(cudaStreamCreate(&v7_dec.stream)); \
        cudaMallocHost(&v7_dec.img_host, MAX_IMAGE_INPUT_SIZE_THRESH * 3);
    // prepare input data cache in device memory
    cudaMalloc(&v7_dec.img_device, MAX_IMAGE_INPUT_SIZE_THRESH * 3);
    return 0;
}
YOLO7::~YOLO7()
{
    std::cout << "YOLO7 destroy" << std::endl;
    cudaStreamDestroy(v7_dec.stream);
    CHECK(cudaFree(v7_dec.buffers[v7_dec.inputIndex]));
    CHECK(cudaFree(v7_dec.buffers[v7_dec.outputIndex]));
    v7_dec.context->destroy();
    v7_dec.engine->destroy();
    v7_dec.runtime->destroy();

}
void YOLO7::doInference(IExecutionContext& context, cudaStream_t& stream, void** buffers, float* output, int batchSize) {

    context.enqueue(batchSize, buffers, stream, nullptr);
    CHECK(cudaMemcpyAsync(output, buffers[1], output_size * sizeof(float), cudaMemcpyDeviceToHost, stream));
    cudaStreamSynchronize(stream);
}
std::vector<inf_res> YOLO7::do_infer(cv::Mat input)
{
    cv::Mat img;
    input.copyTo(img);
    int img_w = img.cols;
    int img_h = img.rows;
    cv::Mat pr_img = static_resize(img);
    float* blob;
    blob = blobFromImage(pr_img);
    float scale = std::min(basic_config.INPUT_W / (img.cols * 1.0), basic_config.INPUT_H / (img.rows * 1.0));

    CHECK(cudaMemcpyAsync(v7_dec.buffers[v7_dec.inputIndex], blob, 3 * basic_config.INPUT_H * basic_config.INPUT_W * sizeof(float), cudaMemcpyHostToDevice, v7_dec.stream));
    delete[] blob;
    doInference(*v7_dec.context, v7_dec.stream, (void**)v7_dec.buffers, v7_dec.output, 1);
    std::vector<inf_res> objects;
    //std::vector<boxinfo> objects;
    decode_outputs(v7_dec.output, output_size, objects, scale, img_w, img_h);
    return objects;

}

