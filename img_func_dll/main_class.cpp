#include"main_class.h"
const v6_basic_config screw_config = { 0.45,0.25 ,320,320,"../model/yolov5m_screw.engine",{"001","002","003","004"} };
const v6_basic_config screw_config1 = { 0.45,0.25 ,544,544,"../model/yolov5m_screw_544.engine",{"001","002","003","004"} };
const v6_basic_config foreign_body_config = { 0.45,0.25 ,640,640,"../model/yolov5m_foreign_body.engine",{"foreign_body"} };
const v6_basic_config locking_wire_config = { 0.45,0.25 ,640,640,"../model/yolov5s_locking_wire.engine",{"locking_wire","locking_wire_break"} };
const v6_basic_config scratch_config = { 0.45,0.25 ,608,608,"../model/yolov5_scratch.engine",{"scratch"} };
const v6_basic_config oil_config = { 0.6,0.2 ,1024,1024,"../model/yolov5n_oil.engine",{"yellow","scale","oil_guage","liquid_level"} };
const std::map<string, v6_basic_config> init_list_info = { { "screw",screw_config },{"screw1",screw_config1},
	{ "locking_wire",locking_wire_config },{ "oil_level",oil_config } ,{ "foreign_body",foreign_body_config },{ "scratch",scratch_config } };//
std::map<int, std::string> sf_class_map =
{
	{0,"screw"},{1,"foreign_body"},{2,"locking_wire"},{3,"oil_level"},{4,"scratch"}
};
float IOU_4(box_info_str& A, box_info_str& B)
{
	// 左上右下坐标(x1,y1,x2,y2)
	int Ax2 = A.x + A.w;
	int Bx2 = B.x + B.w;
	int Ay2 = A.y + A.h;
	int By2 = B.y + B.h;
	float w = max(0.0f, min(Ax2, Bx2) - max(A.x, B.x) + 1);
	float h = max(0.0f, min(Ay2, By2) - max(A.y, B.y) + 1);
	float area1 = (A.w + 1) * (A.h + 1);
	float area2 = (B.w + 1) * (B.h + 1);
	float inter = w * h;
	float iou = inter / (area1 + area2 - inter);
	if (inter / area1 > 0.8 || inter / area2 > 0.8)
	{
		iou = 0.8;
	}
	return iou;
}
float IOU_4(box_info& A, box_info& B)
{
	// 左上右下坐标(x1,y1,x2,y2)
	int Ax2 = A.x + A.w;
	int Bx2 = B.x + B.w;
	int Ay2 = A.y + A.h;
	int By2 = B.y + B.h;
	float w = max(0.0f, min(Ax2, Bx2) - max(A.x, B.x) + 1);
	float h = max(0.0f, min(Ay2, By2) - max(A.y, B.y) + 1);
	float area1 = (A.w + 1) * (A.h + 1);
	float area2 = (B.w + 1) * (B.h + 1);
	float inter = w * h;
	float iou = inter / (area1 + area2 - inter);
	if (inter / area1 > 0.8 || inter / area2 > 0.8)
	{
		iou = 0.8;
	}
	return iou;
}
vector < box_info> trans(vector < box_info_str> input)
{
	vector < box_info> out;
	for (auto s : input)
	{
		box_info ss;
		ss.x = s.x;
		ss.y = s.y;
		ss.w = s.w;
		ss.h = s.h;
		memset(ss.name, '\0', 50 * sizeof(char));
		strcpy_s(ss.name, strlen(s.name.c_str()) + 1, s.name.c_str());
		ss.state = s.state;
		out.push_back(ss);
	}
	return out;
}
void delet_repeat(std::vector<box_info>& input, std::vector<box_info>& output)
{
	std::map<std::string, std::vector<box_info>> inputmap;
	for (auto s : input)
	{
		if (inputmap.find(s.name) == inputmap.end())
		{

			inputmap[s.name] = std::vector<box_info>{ s };
		}
		else
		{
			inputmap[s.name].push_back(s);
		}
	}

	for (auto s : inputmap)
	{
		std::vector<int> throw_box;
		for (int i = 0; i < s.second.size(); i++)
		{
			for (int j = 0; j < s.second.size(); j++)
			{
				if (j == i)
				{
					continue;
				}
				else {
					if (IOU_4(s.second[i], s.second[j]) > 0.5)
					{
						throw_box.push_back(j);
					}
				}
			}
		}
		std::vector<box_info> only_e;
		for (int i = 0; i < s.second.size(); i++)
		{
			int t = 0;
			for (auto s : throw_box)
			{
				if (s == i)
				{
					t = 1;
					break;
				}
			}
			if (t == 0)
			{
				output.push_back(s.second[i]);
			}
		}
	}
}
main_class::main_class()
{
}
main_class::~main_class()
{
	for (auto i : infer_map)
	{
		i.second->~yolov5_v6_inf();
	}
}
int main_class::init(char* log)
{
	if (init_list.empty())
	{
		init_list = init_list_info;
	}
	for (auto i : init_list)
	{
		std::cout << "正在初始化模型：" << i.first << std::endl;
		infer_map[i.first] = new yolov5_v6_inf;
		infer_map[i.first]->init(i.second);

	}
	std::cout << "模型初始化完毕。" << std::endl;
	return 0;
}
vector<string> SplitString(const string& s, const string& c)
{
	vector<string> v;
	string::size_type pos1, pos2;
	pos2 = s.find(c);
	pos1 = 0;
	while (string::npos != pos2)
	{
		v.push_back(s.substr(pos1, pos2 - pos1));

		pos1 = pos2 + c.size();
		pos2 = s.find(c, pos1);
	}
	if (pos1 != s.length())
		v.push_back(s.substr(pos1));
	return v;
}

void main_class::get_modelconfig(std::string func_name, float NMS_THRESH, float CONF_THRESH, int INPUT_H_v6, int INPUT_W_v6, std::string engine_name, std::string classname)
{

	v6_basic_config add_config;
	add_config.NMS_THRESH = NMS_THRESH;
	add_config.CONF_THRESH = CONF_THRESH;
	add_config.INPUT_H_v6 = INPUT_H_v6;
	add_config.INPUT_W_v6 = INPUT_W_v6;
	add_config.engine_name = engine_name;
	add_config.classname = SplitString(classname, ",");
	std::cout << func_name << std::endl;
	init_list[func_name] = add_config;
}
//返回分析结果
vector<box_info> main_class::get_imgres_img_common(cv::Mat& inputimg, vector<int> task_list, vector<model_struct> mode_box, int model = 0)
{
	std::vector<box_info_str> res_all_str;
	for (auto task : task_list)
	{
		std::vector<box_info_str> res_s;
		if (task == 0)
		{
			std::cout << "开始进行螺丝检测任务》》》" << std::endl;
			screw getscrew;
			getscrew.analy_res(inputimg, res_s, infer_map);
			std::cout << "任务完成。" << std::endl;
		}
		else if (task == 1)
		{
			std::cout << "开始进行异物检测任务》》》" << std::endl;
			foreign_body getforeign_body;
			getforeign_body.analy_res(inputimg, res_s, infer_map[sf_class_map[task]], model);
			std::cout << "任务完成。" << std::endl;
		}
		else if (task == 2)
		{
			std::cout << "开始进行防松铁丝检测任务》》》" << std::endl;
			locking_wire getlocking_wire;
			getlocking_wire.analy_res(inputimg, res_s, infer_map[sf_class_map[task]]);
			std::cout << "任务完成。" << std::endl;
		}
		else if (task == 3)
		{
			std::cout << "开始进行油位表检测任务》》》" << std::endl;
			oil_level getoil_level;
			getoil_level.analy_res(inputimg, res_s, infer_map);
			std::cout << "任务完成。" << std::endl;
		}
		else if (task == 4)
		{
			std::cout << "开始进行划痕检测任务》》》" << std::endl;
			scratch getscratch;
			getscratch.analy_res(inputimg, res_s, infer_map[sf_class_map[task]]);
			std::cout << "任务完成。" << std::endl;
		}
		if (res_s.size() > 0)
		{
			res_all_str.insert(res_all_str.end(), res_s.begin(), res_s.end());
		}

	}
	//std::cout << "分析完成" << std::endl;
	vector<box_info> res_all;
	if (!mode_box.empty())
	{
		loss_check getloss;
		std::vector<box_info_str> res_all_str_loss = getloss.getloss_res(inputimg, res_all_str, mode_box);

		std::cout << "缺失查找完成" << std::endl;
		return trans(res_all_str_loss);
	}
	else
	{
		for (auto s : res_all_str)
		{
			box_info ss;
			ss.x = s.x;
			ss.y = s.y;
			ss.w = s.w;
			ss.h = s.h;
			strcpy(ss.name, s.name.c_str());
			ss.state = s.state;
			res_all.push_back(ss);
		}
		return trans(res_all_str);
	}

}
vector<box_info> main_class::get_imgres_img(cv::Mat inputimg, vector<int> task_list, vector<model_struct> mode_box, int& state_num)
{
	vector<box_info> res = get_imgres_img_common(inputimg, task_list, mode_box, state_num);
	for (auto s : res)
	{
		if (string(s.name) == "locking_wire" || string(s.name) == "locking_wire_break")
		{
			if (string(s.name) == "locking_wire_break")
			{
				s.state = 301;
			}
			else if (s.state == 2)
			{
				s.state = 302;
			}
			else
			{
				s.state = 0;
			}
		}
		else if (string(s.name) == "scratch")
		{
			s.state = 702;
		}
		else if (string(s.name) == "foreign_body")
		{
			s.state = 107;
		}
		else if (string(s.name) == "oil_guage")
		{
			if (s.state == 7)
			{
				s.state = 801;
			}
			else if (s.state == 8)
			{
				s.state = 802;
			}
			else
			{
				s.state = 0;
			}
		}
		else
		{
			if (s.state == 2)
			{
				s.state = 402;
			}
		}
	}
	return res;
}
//输入信息
//input_info：包含图像信息int task_list[10];任务列表img_path[200];//图像路径
//model_struct：包含模板的信息
//
box_info* main_class::get_imgres(input_struct input_info, vector <model_struct> mode_box, int& len, vector<int> task_list, int& state_num, char* log)
{
	cv::Mat inputimg = cv::imread(input_info.img_path);
	std::cout << "读取图像：" << input_info.img_path << std::endl;

	std::vector<box_info> res_all = get_imgres_img_common(inputimg, task_list, mode_box, state_num);
	for (auto s : task_list)
	{
		if (s == 3)
		{
			cv::imwrite(input_info.img_path, cv::imread("../store.jpg"));
		}
	}
	std::cout << "计算完毕" << std::endl;
	len = res_all.size();
	box_info* res = new box_info[res_all.size()];
	copy(res_all.begin(), res_all.end(), res);
	log_str = "";
	memcpy(log, log_str.c_str(), sizeof(log_str.c_str()));
	return res;
}
box_info* main_class::new_get_imgres(std::string file_path, vector<input_task> input_info, vector <model_struct> mode_box, int& len, int& state_num, char* log)
{
	state_num = 0;
	std::cout << "读取图像：" << file_path << std::endl;
	cv::Mat inputimg = cv::imread(file_path);
	if (inputimg.empty())
	{
		std::cout << "读取图像：" << file_path << "失败。请检查图像路径是否存在！！！" << std::endl;
		return nullptr;
	}
	std::vector<box_info> res_all, res_other, res_other_res;
	std::cout << "开始进行整节车厢检测任务：" << file_path << std::endl;
	res_all = get_imgres_img_common(inputimg, vector<int>{0, 1}, mode_box, 1);
	std::cout << "开始进行整节车厢检测任务完成。" << file_path << std::endl;
	for (auto s : input_info)
	{
		cv::Mat cutMat;
		std::cout << "开始进行部件任务检测，部件唯一编号为:" << s.only_str << std::endl;
		inputimg(cv::Range(s.y, s.y + s.h), cv::Range(s.x, s.x + s.w)).copyTo(cutMat);
		std::vector<int> task_list_vec;
		for (int i = 0; i < 10; i++)
		{
			if (s.task_list[i] != -1 && s.task_list[i] != 0 && s.task_list[i] != 1)
			{
				task_list_vec.push_back(s.task_list[i]);
			}
			else
			{
				continue;
			}
		}
		if (!task_list_vec.empty())
		{
			std::vector<box_info> res_all_s = get_imgres_img_common(cutMat, task_list_vec, mode_box, 0);
			for (auto ss : res_all_s)
			{
				ss.x = s.x + ss.x;
				ss.y = s.y + ss.y;
			}
			res_other.insert(res_other.end(), res_all_s.begin(), res_all_s.end());
			state_num++;
		}
		else
		{
			continue;
		}

	}
	delet_repeat(res_other, res_other_res);
	res_all.insert(res_all.end(), res_other_res.begin(), res_other_res.end());
	std::cout << "计算完毕" << std::endl;
	len = res_all.size();
	box_info* res = new box_info[res_all.size()];
	copy(res_all.begin(), res_all.end(), res);
	log_str = "";
	memcpy(log, log_str.c_str(), sizeof(log_str.c_str()));
	return res;
}