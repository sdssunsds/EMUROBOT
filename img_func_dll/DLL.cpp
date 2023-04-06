#include "DLL.h"
#include "mainclass_dll.h" // 实现了接口类的具体子类

void IExport::get_modelconfig(std::string func_name, float NMS_THRESH, float CONF_THRESH, int INPUT_H_v6, int INPUT_W_v6, std::string engine_name,std::string classname)
{
}
std::vector<box_info> IExport::get_imgres_img(cv::Mat inputimg, std::vector<int> task_list, std::vector <model_struct> mode_box)
{
	std::vector<box_info> res;
	return res;
}//返回分析结果
std::vector<box_info> IExport::get_imgres_img(cv::Mat inputimg, cv::Mat modelimg, std::vector<int> task_list, std::vector<model_struct> mode_box, std::string historypath)
{
	std::vector<box_info> res;
	return res;
}
extern "C" CPPLIBDLL_API IExport * __stdcall ExportObjectFactory()
{
	return new main_class();
}

extern "C" CPPLIBDLL_API void __stdcall DestroyExportObject(IExport * obj)
{
	if (obj)
	{
		delete obj;
		obj = nullptr;
	}
}
extern "C" CPPLIBDLL_API void __stdcall GetModelConfig(IExport* obj, const char* func_name, const float NMS_THRESH, const float CONF_THRESH,
	const int INPUT_H_v6, const int INPUT_W_v6, const char* engine_name_c, const char* classname_c)
{
	if (obj) {
		return obj->get_modelconfig(func_name, NMS_THRESH, CONF_THRESH, INPUT_H_v6, INPUT_W_v6, engine_name_c,classname_c);
	}
}
extern "C" CPPLIBDLL_API int __stdcall CallOnInit(IExport * obj, char* log)
{
	if (obj) {
		return obj->init(log);
	}
	else
	{
		return -1;
	}
}
extern "C" CPPLIBDLL_API box_info * __stdcall NewCallgetres(IExport * obj, uint8_t * img1_info,int w1, int h1, uint8_t * img2_info, int w2, int h2,
	const int* input_task,const int input_info_len,const model_struct* mode_box,const int model_struct_len,int& res_len)
{
	
	std::vector<int> input_info_vec;
	std::vector<model_struct> mode_box_vec;

	cv::Mat input1(h1, w1, CV_8UC3, img1_info);
	cv::Mat input1_copy, input2_copy;
	std::cout << "开始复制图片" <<std::endl;
	//input1.copyTo(input1_copy);
	input1_copy = input1;
	cv::Mat input2(h2, w2, CV_8UC3, img2_info);
	input2.copyTo(input2_copy);
	if (obj) {
		
		if (input_info_len != 0)
		{
			input_info_vec.insert(input_info_vec.begin(), input_task, input_task + input_info_len);
		}
		std::cout << "任务个数为：" << input_info_len << std::endl;
		if (model_struct_len != 0)
		{
			mode_box_vec.insert(mode_box_vec.begin(), mode_box, mode_box + model_struct_len);
			std::cout <<"模板总数为：" << mode_box_vec.size() << std::endl;
		}
		std::vector<box_info> res_f = obj->get_imgres_img(input1_copy, input2_copy, input_info_vec, mode_box_vec,"");
		res_len = res_f.size();
		std::cout << "结果获取成功" << std::endl;
		box_info* buffer = new box_info[sizeof(box_info)];
		if (!res_f.empty())
		{
			memcpy(buffer, &res_f[0], res_f.size() * sizeof(box_info));
		}
		std::cout << "返回结果" << std::endl;
		return buffer;
	}
	else {
		return nullptr;
	}
}
extern "C" CPPLIBDLL_API box_info * __stdcall Callgetres(IExport * obj, const char* checkimg_path,const char* modelimg_path, const char* historypath ,
	const int* input_task, const int input_info_len, const model_struct * mode_box, const int model_struct_len, int& res_len)
{

	std::vector<int> input_info_vec;
	std::vector<model_struct> mode_box_vec;
	cv::Mat checkimg = cv::imread(std::string(checkimg_path));
	cv::Mat modelimg = cv::imread(std::string(modelimg_path));
	if (obj) {

		if (input_info_len != 0)
		{
			input_info_vec.insert(input_info_vec.begin(), input_task, input_task + input_info_len);
		}
		std::cout << "任务个数为：" << input_info_len << std::endl;
		if (model_struct_len != 0)
		{
			mode_box_vec.insert(mode_box_vec.begin(), mode_box, mode_box + model_struct_len);
			std::cout << "模板总数为：" << mode_box_vec.size() << std::endl;
		}
		std::vector<box_info> res_f = obj->get_imgres_img(checkimg, modelimg, input_info_vec, mode_box_vec,historypath);
		res_len = res_f.size();
		std::cout << "结果获取成功" << std::endl;
		box_info* buffer = new box_info[res_len];
		if (!res_f.empty())
		{
			memcpy(buffer, &res_f[0], res_f.size() * sizeof(box_info));
		}
		std::cout << "返回结果" << std::endl;
		return buffer;
	}
	else {
		return nullptr;
	}
}