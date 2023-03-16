#pragma once
#include"basic_struct.h"
#include "spsg.h"
#include "common_func.h"
class get_loss
{
public:
	static void analy_res(cv::Mat inputimg, std::vector<model_struct_box> mode_box,
		std::map<std::string, basic_yolo*> infer, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
	static void analy_res(cv::Mat inputimg, cv::Mat modelimg, std::vector<model_struct_box> mode_box,
		std::map<std::string, basic_yolo*> infer,spsg& correct_img, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s);
};
