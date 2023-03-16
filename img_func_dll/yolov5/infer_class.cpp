#include "infer_class.h"

wchar_t* multiByteToWideChar(const string& pKey)
{
	const char* pCStrKey = pKey.c_str();
	//第一次调用返回转换后的字符串长度，用于确认为wchar_t*开辟多大的内存空间
	int pSize = MultiByteToWideChar(CP_OEMCP, 0, pCStrKey, strlen(pCStrKey) + 1, NULL, 0);
	wchar_t* pWCStrKey = new wchar_t[pSize];
	//第二次调用将单字节字符串转换成双字节字符串
	MultiByteToWideChar(CP_OEMCP, 0, pCStrKey, strlen(pCStrKey) + 1, pWCStrKey, pSize);
	return pWCStrKey;
}
void Classifier::init(infer_class_config info) {
	labels = info.classname;
	class_num = info.num_classes;
	auto allocator_info = Ort::MemoryInfo::CreateCpu(OrtDeviceAllocator, OrtMemTypeDefault);
	session_option.SetInterOpNumThreads(5);
	OrtSessionOptionsAppendExecutionProvider_CUDA(session_option, 0);
	session_option.SetGraphOptimizationLevel(GraphOptimizationLevel::ORT_ENABLE_ALL);
	session = Ort::Session(env, multiByteToWideChar(info.engine_name), session_option);
	Ort::AllocatorWithDefaultOptions allocator;
	input_names.push_back(session.GetInputName(0, allocator));
	output_names.push_back(session.GetOutputName(0, allocator));
	input_dims = session.GetInputTypeInfo(0).GetTensorTypeAndShapeInfo().GetShape();
	int size = 1;
	for (int i = 0; i < input_dims.size(); i++)
	{
		size *= input_dims[i];
	}
	input_ = new float[size];
	input_tensor_ = Ort::Value::CreateTensor<float>(allocator_info, input_, size, input_dims.data(), input_dims.size());
}

void Classifier::set_input(Mat img) {

	Mat dst;
	resize(img, dst, Size(input_dims[2], input_dims[3]));
	cvtColor(dst, dst, COLOR_BGR2RGB);
	float* input_prt = input_;
	for (int c = 0; c < input_dims[1]; c++) {
		for (int i = 0; i < input_dims[2]; i++) {
			for (int j = 0; j < input_dims[3]; j++) {
				float tmp = dst.ptr<uchar>(i)[j * 3 + c];
				input_prt[c * input_dims[2] * input_dims[3] + i * input_dims[2] + j] = ((tmp) / 255.0 - mean_[c]) / std_[c];
			}
		}
	}
}

infer_class_info Classifier::infer(Mat img) {
	set_input(img);
	auto output_tensors=session.Run(Ort::RunOptions{ nullptr }, input_names.data(), &input_tensor_, input_names.size(), output_names.data(), output_names.size());
	assert(output_tensors.size() == 1 && output_tensors.front().IsTensor());
	float* floatarr = output_tensors[0].GetTensorMutableData<float>();     // 也可以使用output_tensors.front(); 获取list中的第一个元素变量  list.pop_front(); 删除list中的第一个位置的元素
	// 得到最可能分类输出
	Mat newarr = Mat_<double>(1, class_num); //定义一个1*class_num的矩阵
	for (int i = 0; i < newarr.rows; i++)
	{
		for (int j = 0; j < newarr.cols; j++) //矩阵列数循环
		{
			newarr.at<double>(i, j) = floatarr[j];
		}
	}
	Point classNumber;
	double classProb;
	Mat probMat = newarr(Rect(0, 0, class_num, 1)).clone();
	Mat result = probMat.reshape(1, 1);
	minMaxLoc(result, NULL, &classProb, NULL, &classNumber);
	infer_class_info res;
	res.class_name= labels[classNumber.x];
	res.conf = classProb;
	return res;
}