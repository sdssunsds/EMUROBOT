#pragma once
#include"basic_struct.h"
#include "common_func.h"
#include "logger.h"
//ÂÖÖá»®ºÛ¼ì²â
class axis_check
{

public:
	static void analy_res(cv::Mat inputimg, std::vector<inf_res> target_box, basic_yolo* infer, std::vector<box_info_str>& res);
};