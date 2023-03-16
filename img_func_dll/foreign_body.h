#pragma once
#include"basic_struct.h"
#include "common_func.h"
class foreign_body
{

public:
	static void analy_res(cv::Mat inputimg, 
		std::map<std::string, basic_yolo*> infer, bool color,std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
	static void analy_res(cv::Mat inputimg, basic_yolo* infer, std::vector<box_info_str>& res);
};