#include "slide_window_infer.h"
std::vector<inf_res> slide_window_infer::infer(cv::Mat input, basic_yolo* model)
{
	std::map<pt, std::vector<inf_res>> infer_out;
	std::vector<inf_res> inf_vec;
	int stepSize = model->basic_config.INPUT_H - model->basic_config.INPUT_H % 100 - 100;	
	cv::Size windowSize(model->basic_config.INPUT_H, model->basic_config.INPUT_W);
	std::vector<pt> out = common_func::sliding_window(input.size(), stepSize, windowSize);
	for (auto img_map : out)
	{
		cv::Mat cut_img;
		int h = img_map.y + windowSize.height < input.rows ? img_map.y + windowSize.height : input.rows;
		int w = img_map.x + windowSize.width < input.cols ? img_map.x + windowSize.width : input.cols;
		input(cv::Range(img_map.y, h), cv::Range(img_map.x, w)).copyTo(cut_img);
		std::vector<inf_res> inf = model->do_infer(cut_img);
		//for (auto s: inf)
		//{
		//	cv::rectangle(cut_img, s.box, cv::Scalar(255, 0, 0), 1);
		//}
		infer_out[img_map] = inf;
	}
	for (auto s : infer_out)
	{
		for (auto ss : s.second)
		{
			inf_res s_inf;
			s_inf.box.x = ss.box.x + s.first.x;
			s_inf.box.y = ss.box.y + s.first.y;
			s_inf.box.width = ss.box.width;
			s_inf.box.height = ss.box.height;
			s_inf.box_name = ss.box_name;
			s_inf.conf = ss.conf;
			//cv::rectangle(input, s_inf.box, cv::Scalar(255, 0, 0), 1);
			inf_vec.push_back(s_inf);
		}
	}
	return common_func::per_img_nms(inf_vec, 0);
}