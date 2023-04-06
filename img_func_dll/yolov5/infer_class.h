#pragma once
#include <windows.h>
#include <onnxruntime_cxx_api.h>
#include <cmath>
#include <algorithm>
#include <fstream>
#include <opencv2/opencv.hpp>
#include "../basic_struct.h"
using namespace cv;
using namespace std;
class Classifier {
public:
	void init(std::string model_basic_path, infer_class_config info);
	void set_input(Mat img);
	infer_class_info infer(Mat img);
private:

	vector<const char*> input_names;
	vector<const char*> output_names;
	float* input_;
	std::vector<int64_t> input_dims;
	std::vector<int64_t> output_dims;
	Ort::Value input_tensor_{ nullptr };
	Ort::Value output_tensor_{ nullptr };
	Ort::SessionOptions session_option;
	Ort::Env env{ ORT_LOGGING_LEVEL_WARNING, "OnnxModel" };
	Ort::Session session{ nullptr };
	std::vector<float> mean_{ 0.485, 0.456, 0.406 };
	std::vector<float> std_{ 0.229, 0.224, 0.225 };
	vector<string> labels;
	int class_num;
};