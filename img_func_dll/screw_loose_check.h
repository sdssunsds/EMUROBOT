#pragma once
#include"basic_struct.h"
#include "common_func.h"
#include "yolov5/infer_class.h"
class screw_loose_check
{
public:
	static void analy_res(cv::Mat& inputimg, std::vector<model_struct_box> mode_box,  std::map<std::string, basic_yolo*> infer, bool color, Classifier* Ts, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
private:
	static bool loose_judge(cv::Mat& inputimg, cv::Rect box, cv::Rect box1);
	static float getangel(cv::Mat& input,std::vector<cv::Point> contour, cv::Point center);
	static float getangel_basic(cv::Mat& input, std::vector<cv::Point> contour, cv::Point center);
};