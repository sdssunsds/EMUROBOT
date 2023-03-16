#pragma once
#include"basic_struct.h"
#include "common_func.h"
class scratch
{

public:
	static void analy_res(cv::Mat inputimg, std::vector<box_info_str>& res,
		basic_yolo* infer, std::vector<int>& task_id_com);
};
