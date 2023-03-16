#include <Windows.h>
#include"mainclass_dll.h"
#include "model_config.h"
#include "common_func.h"
#include "v5_trt.h"
#include "trt_dll.h"
#pragma comment(lib, "../lib/onnx_trans.lib")
#pragma comment(lib, "../lib/v5_trt.lib")
extern std::map<std::string, trt_basic_config> init_list_info;
extern std::map<std::string, int> label_map;
main_class::main_class()
{
}
main_class::~main_class()
{
	//for (auto i : infer_map_basic_yolo)
	//{
	//	i.second->~infer_map_basic_yolo();
	//}
}
int main_class::init(char* log)
{
	std::vector<std::string> model_list{"superglue_indoor_sim_int32.engine","superglue_outdoor_sim_int32.engine","superpoint_v1_sim_int32.engine"}, total_model_list, old_model_list;
	std::string model_basic_path = "../model/";
	std::string old_model_path = model_basic_path+ "old_model";
	std::string basic_model_path = model_basic_path + "basic_model";
	std::cout << "正在初始化图像校正模型" <<"\n";
	correct_img.init(basic_model_path, model_basic_path);
	if (init_list.empty())
	{
		init_list = init_list_info;
	}
	for (auto i : init_list)
	{
		if (i.second.modeltype == "v5")
		{
			std::cout << "正在初始化目标检测模型：" << i.first << "》》》";
			infer_map_basic_yolo[i.first] = new yolov5_v6_inf;
		v5ini:if (infer_map_basic_yolo[i.first]->init(i.second, model_basic_path) == -1)
			{
				std::cout << "正在重新转换检测模型：" << i.first << "\n";
				int stat = v5_trans(basic_model_path+i.second.engine_name+".wts", basic_model_path+i.second.engine_name + ".engine", i.second.modelstyle, i.second.INPUT_H, i.second.classname.size());
				goto v5ini;
			};
			std::cout << "检测模型：" << i.first << "初始话成功。" << "\n";
		}
		else if (i.second.modeltype == "v7")
		{
			std::cout << "正在初始化目标检测模型：" << i.first << "》》》";
			infer_map_basic_yolo[i.first] = new YOLO7;
		v7ini:	if (infer_map_basic_yolo[i.first]->init(i.second, model_basic_path)==-1)
			{
				std::cout << "正在重新转换检测模型：" << i.first << "\n";
				int stat=onnx_trans(basic_model_path + i.second.engine_name + ".onnx", basic_model_path + i.second.engine_name + ".engine", "--fp16");
				goto v7ini;
			};
			std::cout << "检测模型：" << i.first <<"初始话成功。" << "\n";
		}
		model_list.push_back(i.second.engine_name);
	}
	for (auto i : init_class_info)
	{
		
		infer_class_map[i.first] = new Classifier;
		infer_class_map[i.first]->init(i.second);
		model_list.push_back(i.second.engine_name);
	}
	std::cout << "正在移除不用或者旧的模型"<<std::endl;
	common_func::getFiles(model_basic_path, total_model_list);
	if (_access(old_model_path.c_str(),0)!=0)
	{
		int rere = _mkdir(old_model_path.c_str());
	}
	for (auto s: total_model_list)
	{
		bool exist = false;

		for (auto ss: model_list)
		{
			if (s== ss+".engine"|| s == ss + ".onnx")
			{
				exist = true;
				continue;
			}
		}
		if (!exist)
		{
			MoveFile((model_basic_path + "/" + s).c_str(), (old_model_path + "/" + s).c_str());
		}
		
	}
	
	std::cout << "模型初始化完毕。" << std::endl;
	return 0;
}
//判断是否为面阵图片
bool main_class::color_judge(cv::Mat& input)
{
	std::vector<cv::Mat> sp;
	cv::split(input, sp);
	cv::Mat temp_m, temp_sd, temp_m1, temp_sd1;
	meanStdDev(sp[0], temp_m, temp_sd);
	meanStdDev(sp[1], temp_m1, temp_sd1);
	double m = temp_m.at<double>(0, 0);
	double m1 = temp_m1.at<double>(0, 0);
	if (m!=m1)
	{
		return true;
	}
	else
	{
		return false;
	}
}

void main_class::get_modelconfig(std::string func_name, float nms, float CONF_THRESH, int INPUT_H, int INPUT_W, std::string engine_name, std::string classname)
{

	trt_basic_config add_config;
	add_config.NMS_THRESH = nms;
	add_config.CONF_THRESH = CONF_THRESH;
	add_config.INPUT_H = INPUT_H;
	add_config.INPUT_W = INPUT_W;
	add_config.engine_name = engine_name;
	add_config.classname = common_func::SplitString(classname, ",");
	std::cout << func_name << std::endl;
	init_list[func_name] = add_config;
}
std::map<int, std::vector<model_struct_box>> model_class(vector<model_struct_box> mode_box,std::vector<int> task)
{
	std::map<int, std::vector<model_struct_box>> output;
	for (auto s: task)
	{
		if (s==0)
		{
			std::vector<model_struct_box> model_box;
			for (auto ss : mode_box)
			{
				if (label_map0.find(ss.class_name) != label_map0.end())
				{
					model_box.push_back(ss);
				}
			}
			output[0] = model_box;
		}
		else if (s==6)
		{
			std::vector<model_struct_box> model_box;
			for (auto ss : mode_box)
			{
				if (label_map6.find(ss.class_name) != label_map6.end())
				{
					model_box.push_back(ss);
				}
			}
			output[6] = model_box;
		}
	}
	return output;
}
//返回分析结果
vector<box_info> main_class::get_imgres_img_common(cv::Mat& inputimg, cv::Mat& modelimg, vector<int> task_list, vector<model_struct> mode_box)
{
	bool correct = std::count(task_list.begin(), task_list.end(), 100) != 0;
	std::vector<model_struct_box> mode_box_tran;
	for (auto s : mode_box)
	{
		model_struct_box box;
		box.box = cv::Rect{ s.x,s.y,s.w,s.h };
		box.class_name = s.class_name;
		mode_box_tran.push_back(box);
	}
	std::map<int, std::vector<model_struct_box>> task_mode_box;
	if (!mode_box_tran.empty())
	{
		task_mode_box = model_class(mode_box_tran, task_list);
	}

	//判断是否彩色图片
	bool color = color_judge(inputimg);
	std::vector<int> task_num;
	std::vector<box_info_str> res_all_str;;
	for (auto task : task_list)
	{
		if (color && task == 0)
		{
			task = 6;
		}
		std::vector<box_info_str> res_s;
		if (task == 0)
		{
			std::cout << "开始进行螺丝检测任务》》》" << std::endl;
			std::cout<<"模板总数为："<< task_mode_box[task].size()<< std::endl;
			screw::analy_res(inputimg, task_mode_box[task],infer_map_basic_yolo, color, task_num, res_s);
		}
		else if (task == 1)
		{
			std::cout << "--------开始进行异物检测任务》》》" << std::endl;
			foreign_body::analy_res(inputimg, infer_map_basic_yolo, color, task_num, res_s);
		}
		else if (task == 2)
		{
			std::cout << "--------开始进行防松铁丝检测任务》》》" << std::endl;
			locking_wire::analy_res(inputimg,infer_map_basic_yolo["locking_wire"], infer_class_map["locking_wire_class"],task_num, res_s);
		}
		else if (task == 3)
		{
			std::cout << "--------开始进行油位表检测任务》》》" << std::endl;
			oil_level::analy_res(inputimg, infer_map_basic_yolo, color,task_num, res_s);
			
		}
		else if (task == 4)
		{
			std::cout << "--------开始进行线阵转向架综合检测任务》》》" << std::endl;
			zxj_common::analy_res(inputimg,  infer_map_basic_yolo, color, task_num, res_s);
		}
		else if (task == 5)
		{
			if (!color)
			{
				std::cout << "不是面阵相机图片，无法进行任务，请检查" << std::endl;
			}
			else
			{
				std::cout << "--------开始进行面阵撒沙管检测任务》》》" << std::endl;
				sashazui::analy_res(inputimg, infer_map_basic_yolo, task_num, res_s);
			}
			
		}
		else if (task == 6)
		{
			std::cout << "--------开始进行面阵部件丢失检测任务》》》" << std::endl;
			if (!color)
			{
				std::cout << "不是面阵相机图片，无法进行任务，请检查" << std::endl;
			}
			else
			{
				if (correct&& !modelimg.empty())
				{
					get_loss::analy_res(inputimg, modelimg, task_mode_box[task], infer_map_basic_yolo, correct_img,task_num, res_s);
				}
				else
				{
					get_loss::analy_res(inputimg, task_mode_box[task], infer_map_basic_yolo, task_num, res_s);
				}
				
			}
		}
		else if (task == 7)
		{
			std::cout << "--------开始进行面阵特定位置螺栓松动检测任务》》》" << std::endl;
			if (!color)
			{
				std::cout << "不是面阵相机图片，无法进行任务，请检查" << std::endl;
			}
			else
			{			
				screw_loose_check::analy_res(inputimg, mode_box_tran, infer_map_basic_yolo, color, infer_class_map["bdscrew"], task_num, res_s);
			}
		}
		else if (task == 8)
		{
			std::cout << "--------开始进行油污检测任务》》》" << std::endl;
			oil_leakage::analy_res(inputimg,  infer_map_basic_yolo, color, task_num, res_s);
		}
		else if (task == 9)
		{
			std::cout << "--------开始进行车头检测任务》》》" << std::endl;
			//ct_check::analy_res(inputimg, res_s, infer_map_basic_yolo,color, task_num,res_s);

		}
		if (res_s.size() > 0)
		{
			res_all_str.insert(res_all_str.end(), res_s.begin(), res_s.end());
		}

	}
	//std::cout << "分析完成" << std::endl;
	return common_func::trans(res_all_str);

}
vector<box_info> main_class::get_imgres_img(cv::Mat inputimg,  vector<int> task_list, vector<model_struct> mode_box)
{

	vector<box_info> res = get_imgres_img_common(inputimg, cv::Mat(),task_list, mode_box);
	return res;
}
vector<box_info> main_class::get_imgres_img(cv::Mat inputimg, cv::Mat modelimg, vector<int> task_list, vector<model_struct> mode_box,std::string history_path="")
{
		vector<box_info> res = get_imgres_img_common(inputimg, modelimg, task_list, mode_box);
		return res;	
}