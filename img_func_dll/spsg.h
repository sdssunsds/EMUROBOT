#pragma once
#include <memory>
#include <chrono>
#include "utils.h"
#include "./correct/super_glue.h"
#include "./correct/super_point.h"
#include "./correct/read_config.h"
class spsg
{
public:
	void init(std::string basic_path,std::string model_path);
	cv::Mat change(cv::Mat image0, cv::Mat image1, cv::Mat& invert);
private:
	int width;
	int height;
	int input_width=1294;
	int input_height=964;
	float radio_w;
	float radio_h;
	SuperPointPtr superpoint;
	SuperGluePtr superglue;
};