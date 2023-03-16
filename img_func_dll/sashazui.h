#pragma once
#include"basic_struct.h"
#include "common_func.h"
class sashazui
{
public:
	static void analy_res(cv::Mat inputimg, std::map<std::string, basic_yolo*> infermap, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
};