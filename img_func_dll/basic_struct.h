#pragma once
#include <string>
#include "opencv2/opencv.hpp"
#include <vector>
#include <map>
#include <mutex>
const bool show_test=true;
struct pt
{
	int x;
	int y;
	friend bool operator < (const struct pt& k1, const struct pt& k2);
};
struct trt_basic_config
{
	std::string modeltype;
	std::string modelstyle;
	float CONF_THRESH;
	float NMS_THRESH;
	int INPUT_H;
	int INPUT_W;
	std::string engine_name;
	std::vector<std::string> classname;
};
struct inf_res
{
	cv::Rect box;
	std::string box_name;
	float conf;
};
struct infer_class_config
{
	int num_classes;
	int input_batch;
	int input_channel;
	int input_width;
	int input_height;	
	std::vector<float> meanf;
	std::vector<float> stdf;
	std::string engine_name;
	std::vector<std::string> classname;
};
struct infer_class_info
{
	float conf;
	std::string class_name;
};
inline bool operator < (const struct pt& a, const struct pt& b)
{
	return (a.x < b.x) || (a.x == b.x && a.y < b.y);
}

struct box_info
{
	char name[50];//box的名字如螺丝
	int state;//box的状态
	int x;
	int y;
	int w;
	int h;
};
struct box_info_str
{
	std::string name;//box的名字如螺丝
	int state;//box的状态
	cv::Rect box;
	float conf =1.0;
};
struct input_task
{
	int task_list[10];
	int imgNO;//车厢号
	char location_str[50];//区域编号
	char part_location_str[50];//部件位置编号
	char part_str[50];//部件编号
	char only_str[50];//唯一编号
	int x;
	int y;
	int w;
	int h;
};
struct input_struct
{
	int task_list[10];//任务列表
	int imgNO;//列车编号
	char location_str[50];//box的状态
	char part_location_str[50];//部件位置编号
	char part_str[50];//部件编号
	char only_str[50];//唯一编码
	char img_path[200];//图像路径
};
struct model_struct
{
	char class_name[50];//种类名称
	int x;
	int y;
	int w;
	int h;
};
struct model_struct_box
{
	std::string class_name;
	cv::Rect box;
	float conf = 1;
};
enum state_enum
{
	//无法检测
	UnSafecheck = -1,
	//正常
	Normal = 0,
	//异常（通用）
	Abnormity = 1,
	//螺丝丢失
	Screw_missing = 2,
	//螺丝松动
	Screw_loose = 3,
	//铁丝断裂
	Broken_wire = 4,
	//划痕
	Scratch_marks = 5,
	//异物
	Foreign_body =6,
	//油位异常
	Abnormal_oil_level =7,
	//油液浑浊
	Oil_turbidity =8,
	//油污
	YW=9,
	//部件丢失
	BJDS=10,
	//车头中缝过大
	Chink_superthreshold=11,
	//管接头松脱
	Gjt_loose=12
};