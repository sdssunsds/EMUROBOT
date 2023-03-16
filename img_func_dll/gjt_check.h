#pragma once
#include"basic_struct.h"
#include "common_func.h"
class gjt_check
{

public:
	static void analy_res(cv::Mat inputimg, std::vector<box_info_str>& res,
		std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com);
	static void gjt_check::analy_res(cv::Mat& inputimg, std::vector<inf_res> target_box,std::vector<box_info_str>& res);
}; 
