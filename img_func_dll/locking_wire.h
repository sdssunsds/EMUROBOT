#pragma once
#include"basic_struct.h"
#include"yolov5/infer_class.h"
#include "common_func.h"
class locking_wire
{

public:
	const int id = 2;
	static void analy_res(cv::Mat inputimg,
		basic_yolo* infer, Classifier* Ts, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
};
