#include "foreign_body.h"
#include "slide_window_infer.h"

void foreign_body::analy_res(cv::Mat inputimg,
	std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com,std::vector<box_info_str>& res_s)
{
	const int id = 1;
	if (color)
	{
		std::vector<inf_res> inf = infer["foreign_body1"]->do_infer(inputimg);
		if (inf.size() > 0)
		{
			
			for (auto s : inf)
			{
				box_info_str box_s;
				box_s.box = s.box;
				box_s.name = s.box_name.c_str();
				box_s.state = Foreign_body;
				res_s.push_back(box_s);
			}
		}
	}
	task_id_com.push_back(id);
}
void foreign_body::analy_res(cv::Mat inputimg, basic_yolo* infer, std::vector<box_info_str>& res)
{
	cv::Mat copy;
	inputimg.copyTo(copy);
	std::vector<box_info_str> res_s;
	cv::Mat resize;
	int stepSize_0 = 900; //infer["foreign_body"]->basic_config.INPUT_H - infer["foreign_body"]->basic_config.INPUT_H % 100 - 100;
	std::vector<inf_res> get_res;
	bool slide = common_func::prehandel_inputimg(inputimg, resize, stepSize_0);
	if (slide)
	{
		get_res = slide_window_infer::infer(inputimg, infer);
		for (auto s : get_res)
		{
			box_info_str box_s;
			box_s.box = s.box;
			box_s.name = s.box_name.c_str();
			box_s.state = Foreign_body;
			res_s.push_back(box_s);
			cv::rectangle(copy,s.box,cv::Scalar(0,0,255));
		}
	}
	res.insert(res.end(), res_s.begin(), res_s.end());
}