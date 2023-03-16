#pragma once
#include <string>
#include <vector>
#include <map>
//#include "ClassFactory.h"
//#include "register.h"
#include"basic_struct.h"
#include "screw.h"
#include "oil_level.h"
#include "locking_wire.h"
#include "foreign_body.h"
#include "loss_check.h"
#include "scratch.h"
using namespace std;
class main_class
{
public:
	main_class();
	~main_class();
	void get_modelconfig(std::string func_name, float NMS_THRESH, float CONF_THRESH, int INPUT_H_v6, int INPUT_W_v6, std::string engine_name, std::string classname);
	int init(char* log);//初始化。包括读取配置，读取模型	
	vector<box_info> get_imgres_img_common(cv::Mat& inputimg, vector<int> task_list, vector<model_struct> mode_box, int model);//返回分析结果
	vector<box_info> get_imgres_img(cv::Mat inputimg, vector<int> task_list, vector<model_struct> mode_box, int& state_num);
	box_info* get_imgres(input_struct input_info, vector <model_struct> mode_box, int& len, vector<int> task_list, int& state_num, char* log);
	std::string log_str;
	box_info* new_get_imgres(std::string file_path, vector<input_task> input_info, vector <model_struct> mode_box, int& len, int& state_num, char* log);
private:
	std::map<string, v6_basic_config> init_list;
	std::map<std::string, yolov5_v6_inf*> infer_map;
};