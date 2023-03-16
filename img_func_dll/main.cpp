#include "mainclass_dll.h"
#include<io.h>
#include<iostream>
#include<fstream>
#define SHOW 1
void getFiles(string path, vector<string>& files)
{
	//文件句柄
	intptr_t   hFile = 0;
	//文件信息
	struct _finddata_t fileinfo;
	string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
	{
		do
		{
			//如果是目录,迭代之
			//如果不是,加入列表
			if ((fileinfo.attrib & _A_SUBDIR))
			{
				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
					getFiles(p.assign(path).append("\\").append(fileinfo.name), files);
			}
			else
			{
				files.push_back(p.assign(path).append("\\").append(fileinfo.name));
			}
		} while (_findnext(hFile, &fileinfo) == 0);
		_findclose(hFile);
	}
}
std::vector<std::string> read_info(std::string path)
{
	std::vector<std::string> out;
	ifstream fin(path, ios::in);
	std::string str;
	if (!fin) {
		printf("The file is not exist!");
	}
	while (getline(fin, str))
	{
		out.push_back(str);
	}
	return out;
}
vector<string> SplitString_main(const string& s, const string& c)
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
std::vector<model_struct> trans_config(std::vector<std::string> input)
{
	std::vector<model_struct> output;
	for (auto s:input)
	{
		model_struct s_m;
		std::vector<std::string> sp;
		sp = SplitString_main(s, ",");
		if (sp.size() == 5)
		{
			strcpy(s_m.class_name, sp[0].c_str());
			s_m.x = std::stoi(sp[1]);
			s_m.y= std::stoi(sp[2]);
			s_m.w= std::stoi(sp[3]);
			s_m.h= std::stoi(sp[4]);
			output.push_back(s_m);
		}
		else
		{
			std::cout<<s<<std::endl;
		}
	}
	return output;
}
int main0()
{
	vector<string> files,infofiles,spstr;
	std::string folderpath = "C:\\Users\\wang\\Desktop\\Puzzle";
	std::string info_folderpath = "E:\\bc\\suanfa\\suanfa\\screw_location";
	getFiles(folderpath, files);
	getFiles(info_folderpath, infofiles);
	std::map<string, string> infomap;
	for (auto s:infofiles)
	{
		string map_k = s.substr(info_folderpath.size()+1, s.length()-info_folderpath.size() -5);
		infomap[map_k] = s;
	}
	main_class test;
	char log[100];
	test.init(log);
	box_info* res_f;
	for (auto img: files)
	{
		input_struct test_input_struct;
		std::string imgpath = img;
		cv::Mat inputimg = cv::imread(imgpath);
		spstr=SplitString_main(imgpath,"_");
		std::vector<std::string> info = read_info(infomap[spstr[6]]);
		vector <model_struct> mode_box = trans_config(info);
		for (auto model:mode_box)
		{
			cv::Rect rect_view;
			rect_view.x = model.x;
			rect_view.y = model.y;
			rect_view.width = model.w;
			rect_view.height = model.h;
			cv::rectangle(inputimg, rect_view, cv::Scalar(0,0, 255 ), 1);
			
		}


		test_input_struct.imgNO = 0;
		strcpy(test_input_struct.img_path, imgpath.c_str());
		strcpy(test_input_struct.location_str, "");
		strcpy(test_input_struct.only_str, "");
		strcpy(test_input_struct.part_location_str, "");
		strcpy(test_input_struct.part_str, "");
		vector<int> task_list = {0};
		int len = 0;
		int state_num = 0;
		vector<box_info> res;
		res_f = test.get_imgres(test_input_struct, mode_box, len, task_list, state_num, log);
		
		for (int i = 0; i < len; i++)
		{
			cv::Rect rect_view;
			rect_view.x = res_f[i].x;
			rect_view.y = res_f[i].y;
			rect_view.width = res_f[i].w;
			rect_view.height = res_f[i].h;
			if (res_f[i].state == 0)
			{
				cv::rectangle(inputimg, rect_view, cv::Scalar(0, 255, 0), 1);
			}
			else
			{
				cv::rectangle(inputimg, rect_view, cv::Scalar(255, 0, 0), 1);
			}
			cv::putText(inputimg, std::string(res_f[i].name) + ":" + std::to_string(res_f[i].state), cv::Point(res_f[i].x, res_f[i].y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0, 255, 0), 1);
		}
		//int write_state=cv::imwrite(img,inputimg);
		/*if (!write_state)
		{
			std::cout << "存储失败：" << imgpath << std::endl;
		}*/
	}
	return 0;
}
int main()
{

	main_class test;
	char log[100];
	int len = 0;


	test.init(log);
	box_info* res_f;
	input_struct test_input_struct;
	std::string imgpath = "../380AL_2572_common_1.jpg";
	cv::Mat iutimg = cv::imread(imgpath);
	test_input_struct.imgNO = 0;
	strcpy(test_input_struct.img_path, imgpath.c_str());
	strcpy(test_input_struct.location_str, "");
	strcpy(test_input_struct.only_str, "");
	strcpy(test_input_struct.part_location_str, "");
	strcpy(test_input_struct.part_str, "");
	vector<int> task_list = { 0 };	
	int state_num = 0;
	//std::vector<std::string> info = read_info("../screw_location/60001010030101.txt");	
	vector <model_struct> mode_box;// = trans_config(info);
	for (auto s: mode_box)
	{
		cv::Rect rect_view;
		rect_view.x = s.x;
		rect_view.y = s.y;
		rect_view.width = s.w;
		rect_view.height = s.h;
		cv::rectangle(iutimg, rect_view, cv::Scalar(0, 255, 0), 1);
		cv::putText(iutimg, std::string(s.class_name), cv::Point(s.x, s.y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0, 255, 0), 1);
	}
	res_f = test.get_imgres(test_input_struct, mode_box, len, task_list, state_num, log);
	//res_f = test.new_get_imgres(imgpath,test_input_struct, mode_box, len, task_list, state_num, log);
	cv::Mat inputimg = cv::imread(imgpath);
	for (int i = 0; i < len; i++)
	{
		cv::Rect rect_view;
		rect_view.x = res_f[i].x;
		rect_view.y = res_f[i].y;
		rect_view.width = res_f[i].w;
		rect_view.height = res_f[i].h;
		if (res_f[i].state==0)
		{
			cv::rectangle(inputimg, rect_view, cv::Scalar(0, 255, 0), 1);
		}
		else
		{
			cv::rectangle(inputimg, rect_view, cv::Scalar(0,  0,255), 1);
		}
		cv::putText(inputimg, std::string(res_f[i].name) + ":" + std::to_string(res_f[i].state), cv::Point(res_f[i].x, res_f[i].y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0, 255, 0), 1);
	}

	return 0;
}
