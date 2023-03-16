#pragma once
#include"basic_struct.h"
#include "common_func.h"

class oil_leakage
{

public:
	static void analy_res(cv::Mat inputimg,
		std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
	static void analy_res_oil(cv::Mat inputimg, std::vector<inf_res> target_box, basic_yolo* infer, std::vector<box_info_str>& res);
}; 
