#pragma once
#include "basic_yolo.h"
#include "common_func.h"
class slide_window_infer
{
public:
	static std::vector<inf_res> infer(cv::Mat input, basic_yolo* model);
};