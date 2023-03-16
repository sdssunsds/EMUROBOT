#pragma once
#define CPPLIBDLL_EXPORTS
#ifdef CPPLIBDLL_EXPORTS
#define CPPLIBDLL_API __declspec(dllexport)
#else
#define CPPLIBDLL_API __declspec(dllimport)
#endif
#include"basic_struct.h"
class IExport
{
public:
	// 初始化
	virtual int init(char* log) = 0;

	// 返回值:失败或成功指示
	virtual std::vector<box_info> get_imgres_img(cv::Mat inputimg, std::vector<int> task_list, std::vector <model_struct> mode_box);//返回分析结果
	virtual std::vector<box_info> get_imgres_img(cv::Mat inputimg, cv::Mat modelimg, std::vector<int> task_list, std::vector<model_struct> mode_box, std::string historypath);
	virtual void get_modelconfig(std::string func_name, float NMS_THRESH,float CONF_THRESH,int INPUT_H_v6,int INPUT_W_v6,std::string engine_name,std::string classname);
	//virtual ~IExport() {};
};

extern "C" CPPLIBDLL_API IExport * __stdcall ExportObjectFactory();
extern "C" CPPLIBDLL_API void __stdcall DestroyExportObject(IExport * obj);
extern "C" CPPLIBDLL_API void __stdcall GetModelConfig(IExport * obj, const char* func_name,const float NMS_THRESH, const float CONF_THRESH,
	const int INPUT_H_v6, const int INPUT_W_v6, const char* engine_name_c, const char* classname_c);//engine文件路径
extern "C" CPPLIBDLL_API int __stdcall CallOnInit(IExport * obj,char* log);//engine文件路径
extern "C" CPPLIBDLL_API box_info * __stdcall NewCallgetres(IExport * obj, uint8_t* img1_info, int w1, int h1, uint8_t* img2_info, int w2, int h2,
	const int* input_info, const int input_info_len, const model_struct * mode_box, const int model_struct_len, int& res_len);
extern "C" CPPLIBDLL_API box_info * __stdcall Callgetres(IExport * obj, const char* check_img, const char* modelimg, const char* historypath,
	const int* input_task, const int input_info_len, const model_struct * mode_box, const int model_struct_len, int& res_len);