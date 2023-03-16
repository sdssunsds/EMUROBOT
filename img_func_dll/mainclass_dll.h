#pragma once

#include <string>
#include <vector>
#include <map>
#include "DLL.h"
#include "common_class.h"
#include "yolov5/infer_class.h"
#include "yolov5/v6/yolov5_v6.h"
#include "yolov7/yolov7.h"
#include "basic_yolo.h"
#include "spsg.h"
class main_class :public IExport
{
public:
	main_class();
	~main_class();
	void get_modelconfig(std::string func_name, float nms, float CONF_THRESH, int INPUT_H, int INPUT_W, std::string engine_name, std::string classname);
	int init(char* log);//初始化。包括读取配置，读取模型	
	vector<box_info> get_imgres_img_common(cv::Mat& inputimg, cv::Mat& modelimg, vector<int> task_list, vector<model_struct> mode_box);//返回分析结果
	vector<box_info> get_imgres_img(cv::Mat inputimg, vector<int> task_list, vector<model_struct> mode_box);
	vector<box_info> get_imgres_img(cv::Mat inputimg, cv::Mat modelimg, vector<int> task_list, vector<model_struct> mode_box, std::string history_path);
	std::string log_str;
private:
	bool color_judge(cv::Mat& input);
	float standart=2048.0;
	float radio = 1.0;
	std::map<string, trt_basic_config> init_list;
	std::map<std::string, basic_yolo*> infer_map_basic_yolo;
	std::map<std::string, Classifier*> infer_class_map;
	spsg correct_img;
};