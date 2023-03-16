#pragma once
#include"basic_struct.h"
#include "common_func.h"
class oil_level
{

public:
	static void analy_res(cv::Mat inputimg, std::map<std::string, basic_yolo*> infermap, bool color,std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
private:
	bool in_or_out(cv::Point point, cv::Rect area);
};