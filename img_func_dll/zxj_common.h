#pragma once
#include"basic_struct.h"
#include "common_func.h"
#include"axis_check.h"
#include"gjt_check.h"
#include"oil_leakage.h"
#include "foreign_body.h"
class zxj_common
{

public:
	static void analy_res(cv::Mat inputimg,
		std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com,std::vector<box_info_str>& res_s);
};