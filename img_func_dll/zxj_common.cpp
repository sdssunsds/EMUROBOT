#include"zxj_common.h"
#include"basic_struct.h"
#include "common_func.h"
#include "slide_window_infer.h"

void zxj_common::analy_res(cv::Mat inputimg,
	std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	if (!color)
	{
		cv::Mat show;;
		inputimg.copyTo(show);
		cv::Mat input_resize;
		//std::vector<box_info_str> res_s;
		float f = float(infer["zxj"]->basic_config.INPUT_W) / float(inputimg.cols);
		cv::resize(inputimg, input_resize, cv::Size(), f, f);
		std::vector<inf_res> target_box = slide_window_infer::infer(input_resize, infer["zxj"]);
		std::vector <inf_res> effect_area;
		for (auto s : target_box)
		{
			inf_res change;
			change.box_name = s.box_name;
			cv::Rect box = s.box;
			box.x = int(box.x / f);
			box.y = int(box.y / f);
			box.width = int(box.width / f);
			box.height = int(box.height / f);
			change.box = box;
			change.conf = s.conf;
			effect_area.push_back(change);
			cv::rectangle(show, box, cv::Scalar(152, 0, 255), 2);
		}

		//“ÏŒÔºÏ≤‚
		foreign_body::analy_res(inputimg, infer["foreign_body"], res_s);
		//÷·ªÆ∫€ºÏ≤‚
		axis_check::analy_res(inputimg, effect_area, infer["oil_leakage"], res_s);
		//”ÕŒ€ºÏ≤‚
		oil_leakage::analy_res_oil(inputimg, effect_area, infer["oil_leakage"], res_s);
		//π‹Ω”Õ∑À…Õ—ºÏ≤‚
		//gjt_check::analy_res(inputimg, effect_area, res_s);
	}
	else
	{
		std::cout << "◊™œÚº‹ºÏ≤‚–Ë“™ ‰»Îœﬂ’ÛÕº∆¨£°£°" << std::endl;
	}
}