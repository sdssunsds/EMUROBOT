#pragma once
#include"basic_struct.h"
#include "common_func.h"
#include "loss_check.h"
class screw:public loss_check
{

public:
	static void analy_res(cv::Mat& inputimg, vector<model_struct_box> mode_box,  std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com, std::vector<box_info_str>& res_ss);
//private:
//	std::vector<inf_res> compete_nms(std::vector<inf_res>& get_res0, std::vector<inf_res>& get_res1);
}; 