#pragma once
#include <fstream>
#include <iostream>
#include <sstream>
#include <numeric>
#include <chrono>
#include <vector>
#include <opencv2/opencv.hpp>
#include "NvInfer.h"
#include "cuda_runtime.h"
#include "../yolov5/logging.h"
//#include "yolov5/v6/yolov5_v6.h"
#include "../basic_struct.h"
#include "../basic_yolo.h"
struct boxinfo
{
    cv::Rect_<float> rect;
    int label;
    float prob;
};
static bool sort_conf(boxinfo& box1, boxinfo& box2)
{
    return (box1.prob > box2.prob);
}
class YOLO7:public basic_yolo
{
public:
    //trt_basic_config basic_config;
    int init(trt_basic_config input_config, std::string modelpath);
    ~YOLO7();
    std::vector<inf_res> do_infer(cv::Mat input);
    

private:
    yolodetect v7_dec;
    int output_size = 1;
    int num_class = 1;
    float* blob;
    const char* INPUT_BLOB_NAME = "data";
    const char* OUTPUT_BLOB_NAME = "prob";
    void decode_outputs(float* prob, int output_size, std::vector<inf_res>& objects, float scale, const int img_w, const int img_h);
    void qsort_descent_inplace(std::vector<boxinfo>& faceobjects, int left, int right);
    void qsort_descent_inplace(std::vector<boxinfo>& objects);
    void nms_sorted_bboxes(const std::vector<boxinfo>& faceobjects, std::vector<int>& picked, float nms_threshold);
    void generate_yolo_proposals(float* feat_blob, int output_size, float prob_threshold, std::vector<boxinfo>& objects);
    cv::Mat static_resize(cv::Mat& img);
    void blobFromImage(cv::Mat& img);
    void doInference(IExecutionContext& context, cudaStream_t& stream, void** buffers, float* output, int batchSize);
    std::vector<boxinfo> nms(std::vector<boxinfo>& res,float nms_thresh);
};


