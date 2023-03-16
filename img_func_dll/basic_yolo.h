#pragma once
#include "basic_struct.h"
#include "NvInfer.h"
#include "NvInferRuntime.h"
#include <cuda_runtime.h>
#define BATCH_SIZE 1
using namespace nvinfer1;
static constexpr int MAX_OUTPUT_BBOX = 1000;
static constexpr int LOCATIONS = 4;
struct alignas(float) Detection {
	//center_x center_y w h
	float bbox[LOCATIONS];
	float conf;  // bbox_conf * cls_conf
	float class_id;
};
static const int OUTPUT_SIZE = MAX_OUTPUT_BBOX * sizeof(Detection) / sizeof(float) + 1;
struct yolodetect
{
	char* trtModelStream;
	ICudaEngine* engine;
	IRuntime* runtime;
	IExecutionContext* context;
	cudaStream_t stream;
	float* buffers[2];
	int inputIndex;
	int outputIndex;
	float prob[BATCH_SIZE * OUTPUT_SIZE];
	float* output;
	uint8_t* img_host;
	uint8_t* img_device;
};

class basic_yolo
{
public:

	trt_basic_config basic_config;
	virtual int init(trt_basic_config input_config, std::string modelpath);
	virtual std::vector<inf_res> do_infer(cv::Mat input);
};