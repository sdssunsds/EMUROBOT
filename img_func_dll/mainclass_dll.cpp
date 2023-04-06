#include <Windows.h>
#include"mainclass_dll.h"
#include "model_config.h"
#include "common_func.h"
#include "v5_trt.h"
#include "trt_dll.h"
#include "logger.h"
#pragma comment(lib, "../lib/onnx_trans.lib")
#pragma comment(lib, "../x64/Debug/yolov5_Trt.lib")
extern std::map<std::string, trt_basic_config> init_list_info;
extern std::map<std::string, int> label_map;
using namespace LOGGER;
CLogger logger(LogLevel_Info,CLogger::GetAppPathA().append("log\\"));
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
	std::vector<std::string> model_list{"superglue_indoor_sim_int32","superglue_outdoor_sim_int32","superpoint_v1_sim_int32"}, total_model_list, old_model_list;
	std::string model_basic_path = "../model/";
	logger.TraceInfo("ģ�ͻ���·��:"+ model_basic_path);
	std::string old_model_path = model_basic_path+ "old_model";
	logger.TraceInfo("��ʱģ��·��:" + old_model_path);
	std::string basic_model_path = model_basic_path + "basic_model/";
	logger.TraceInfo("ԭʼģ��·��:" + basic_model_path);
	
	std::cout << "���ڳ�ʼ��ͼ��У��ģ��" << std::endl;
	logger.TraceInfo("���ڳ�ʼ��ͼ��У��ģ��");
	correct_img.init(basic_model_path, model_basic_path);
	if (init_list.empty())
	{
		init_list = init_list_info;
	}
	for (auto i : init_list)
	{
		if (i.second.modeltype == "v5")
		{
			logger.TraceInfo("���ڳ�ʼ��Ŀ����ģ��:", i.first);
			std::cout << "���ڳ�ʼ��Ŀ����ģ��:" << i.first << "������";
			infer_map_basic_yolo[i.first] = new yolov5_v6_inf;
		v5ini:if (infer_map_basic_yolo[i.first]->init(i.second, model_basic_path) == -1)
			{
				std::cout << "��������ת�����ģ��:" << i.first << std::endl;
				logger.TraceInfo("��������ת�����ģ��:" + i.first);
				ifstream f((basic_model_path + i.second.engine_name + ".wts").c_str());
				if (f.good())
				{
					int stat = v5_trans(basic_model_path + i.second.engine_name + ".wts", model_basic_path + i.second.engine_name + ".engine", i.second.modelstyle, i.second.INPUT_H, i.second.classname.size());
					goto v5ini;
				}
				else
				{
					logger.TraceInfo("δ�ҵ��������ģ��:" + i.first);
				}
			};
			std::cout << "���ģ�ͣ�" << i.first << "��ʼ���ɹ���" << std::endl;
		}
		else if (i.second.modeltype == "v7")
		{
			std::cout << "���ڳ�ʼ��Ŀ����ģ�ͣ�" << i.first << "������";
			infer_map_basic_yolo[i.first] = new YOLO7;
			ifstream f((basic_model_path + i.second.engine_name + ".wts").c_str());
		v7ini:if (infer_map_basic_yolo[i.first]->init(i.second, model_basic_path)==-1||f.good())
			{
				std::cout << "��������ת�����ģ�ͣ�" << i.first << std::endl;
				logger.TraceInfo("��������ת�����ģ��:" + i.first);
				int stat=onnx_trans(basic_model_path + i.second.engine_name, model_basic_path + i.second.engine_name, "--int8");
				if (stat)
				{
					goto v7ini;
				}
			}
			else
			{
				logger.TraceInfo("δ�ҵ��������ģ��:" + i.first);
			}
			std::cout << "���ģ�ͣ�" << i.first <<"��ʼ���ɹ���" << std::endl;
			logger.TraceInfo("���ģ�ͣ�"+i.first+"��ʼ���ɹ���");
		}
		model_list.push_back(i.second.engine_name);
	}
	for (auto i : init_class_info)
	{
		i.second.engine_name = model_basic_path + i.second.engine_name;
		infer_class_map[i.first] = new Classifier;
		infer_class_map[i.first]->init(model_basic_path,i.second);
		model_list.push_back(i.second.engine_name);
	}
	logger.TraceInfo("�����Ƴ����û��߾ɵ�ģ��");
	std::cout << "�����Ƴ����û��߾ɵ�ģ��"<<std::endl;
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
			logger.TraceInfo("�Ƴ����ģ��:"+ s);
			MoveFile((model_basic_path + "/" + s).c_str(), (old_model_path + "/" + s).c_str());
		}
		
	}
	
	std::cout << "ģ�ͳ�ʼ����ϡ�" << std::endl;
	logger.TraceInfo("ģ�ͳ�ʼ����ϡ�");
	return 0;
}
//�ж��Ƿ�Ϊ����ͼƬ
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
//���ط������
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
	
	//�ж��Ƿ��ɫͼƬ
	bool color = color_judge(inputimg);
	std::vector<int> task_num;
	std::vector<box_info_str> res_all_str;;
	for (auto task : task_list)
	{
		if (color&& task==0)
		{
			task = 6;
		}
		std::vector<box_info_str> res_s;

		if (task == 0)
		{
			logger.TraceInfo("--------��ʼ������˿������񡷡���");
			std::cout << "--------��ʼ������˿������񡷡���" << std::endl;
			std::cout<<"ģ������Ϊ��"<< task_mode_box[task].size()<< std::endl;
			logger.TraceInfo("ģ������Ϊ��" +to_string( task_mode_box[task].size()));
			screw::analy_res(inputimg, task_mode_box[task],infer_map_basic_yolo, color, task_num, res_s);
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 1)
		{
			logger.TraceInfo("--------��ʼ�������������񡷡���");
			std::cout << "--------��ʼ�������������񡷡���";
			foreign_body::analy_res(inputimg, infer_map_basic_yolo, color, task_num, res_s);
			std::cout << "���ɹ���" << std::endl;
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 2)
		{
			logger.TraceInfo("--------��ʼ���з�����˿������񡷡���");
			std::cout << "--------��ʼ���з�����˿������񡷡���" << std::endl;
			locking_wire::analy_res(inputimg,infer_map_basic_yolo["locking_wire"], infer_class_map["locking_wire_class"],task_num, res_s);
			std::cout << "���ɹ���" << std::endl;
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 3)
		{
			logger.TraceInfo("--------��ʼ������λ��������񡷡���");
			std::cout << "--------��ʼ������λ��������񡷡���" << std::endl;
			oil_level::analy_res(inputimg, infer_map_basic_yolo, color,task_num, res_s);
			std::cout << "���ɹ���" << std::endl;
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 4)
		{
			logger.TraceInfo("--------��ʼ��������ת����ۺϼ�����񡷡���");
			std::cout << "--------��ʼ��������ת����ۺϼ�����񡷡���" << std::endl;
			zxj_common::analy_res(inputimg,  infer_map_basic_yolo, color, task_num, res_s);
			std::cout << "���ɹ���" << std::endl;
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 5)
		{
			std::cout << "--------��ʼ����������ɳ�ܼ�����񡷡���" << std::endl;
			logger.TraceInfo("--------��ʼ����������ɳ�ܼ�����񡷡���");
			if (!color)
			{
				logger.TraceInfo("�����������ͼƬ���޷�������������");
				std::cout << "�����������ͼƬ���޷�������������" << std::endl;
			}
			else
			{
				
				sashazui::analy_res(inputimg, infer_map_basic_yolo, task_num, res_s);
				std::cout << "���ɹ���" << std::endl;
				logger.TraceInfo("���ɹ�");
			}
			
		}
		else if (task == 6)
		{
			std::cout << "--------��ʼ�������󲿼���ʧ������񡷡���" << std::endl;
			logger.TraceInfo("--------��ʼ�������󲿼���ʧ������񡷡���");
			if (!color)
			{
				logger.TraceInfo("�����������ͼƬ���޷�������������");
				std::cout << "�����������ͼƬ���޷�������������" << std::endl;
			}
			else
			{
				if (correct&& !modelimg.empty())
				{
					logger.TraceInfo("������ģ��ģʽ��");
					get_loss::analy_res(inputimg, modelimg, task_mode_box[task], infer_map_basic_yolo, correct_img,task_num, res_s);
				}
				else
				{
					logger.TraceInfo("������ģ��ģʽ��");
					get_loss::analy_res(inputimg, task_mode_box[task], infer_map_basic_yolo, task_num, res_s);
				}
				
			}
			std::cout << "���ɹ���" << std::endl;
			logger.TraceInfo("���ɹ�");
		}
		else if (task == 7)
		{
			std::cout << "--------��ʼ���������ض�λ����˨�ɶ�������񡷡���" << std::endl;
			logger.TraceInfo("--------��ʼ���������ض�λ����˨�ɶ�������񡷡���");
			if (!color)
			{
				std::cout << "�����������ͼƬ���޷�������������" << std::endl;
			}
			else
			{			
				screw_loose_check::analy_res(inputimg, mode_box_tran, infer_map_basic_yolo, color, infer_class_map["bdscrew"], task_num, res_s);
			}
			std::cout << "���ɹ���" << std::endl;
		}
		else if (task == 8)
		{
			std::cout << "--------��ʼ�������ۼ�����񡷡���" << std::endl;
			logger.TraceInfo("--------��ʼ�������ۼ�����񡷡���");
			oil_leakage::analy_res(inputimg,  infer_map_basic_yolo, color, task_num, res_s);
			std::cout << "���ɹ���" << std::endl;
		}
		else if (task == 9)
		{
			logger.TraceInfo("--------��ʼ���г�ͷ������񡷡���");
			std::cout << "--------��ʼ���г�ͷ������񡷡���" << std::endl;
			//ct_check::analy_res(inputimg, res_s, infer_map_basic_yolo,color, task_num,res_s);

		}
		else
		{
			continue;
		}
		if (res_s.size() > 0)
		{
			res_all_str.insert(res_all_str.end(), res_s.begin(), res_s.end());
		}

	}
	//std::cout << "�������" << std::endl;
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