#include "axis_check.h"
#include "slide_window_infer.h"
void axis_check::analy_res(cv::Mat inputimg, std::vector<inf_res> target_box, basic_yolo* infer, std::vector<box_info_str>& res
	)
{
	std::vector<box_info_str> res_s;
	cv::Mat show;;
	inputimg.copyTo(show);
	std::vector <cv::Rect> effect_area;
	std::vector<std::string> work_name = {"axle"};
	for (auto s : target_box)
	{
		if (count(work_name.begin(), work_name.end(), s.box_name))
		{
			cv::Rect box = s.box;
			effect_area.push_back(box);
			cv::rectangle(show, box, cv::Scalar(152, 0, 255), 2);
		}
	}
	int w = infer->basic_config.INPUT_W;
	for (auto s : effect_area)
	{
		//cv::rectangle(show, s, cv::Scalar(152,0,255), 2);
		cv::Rect detect_area = s;
		if (s.width < w)
		{
			detect_area.x = detect_area.x - (w - s.width) / 2 < 0 ? 0 : detect_area.x - (w - s.width) / 2;
			detect_area.y = detect_area.y - (w - s.height) / 2 < 0 ? 0 : detect_area.y - (w - s.height) / 2;
			if (detect_area.x + detect_area.width > inputimg.cols)
			{
				detect_area.x = inputimg.cols - w;
			}
			if (detect_area.y + detect_area.height > inputimg.rows)
			{
				detect_area.y = inputimg.rows - w;
			}
		}
		cv::rectangle(show, detect_area, cv::Scalar(152, 152, 0), 2);
		std::vector<inf_res> res_detect = slide_window_infer::infer(inputimg(detect_area), infer);
		std::vector<inf_res>  scratch;
		for (auto ss : res_detect)
		{
			inf_res info_s = ss;
			info_s.box.x = ss.box.x + detect_area.x;
			info_s.box.y = ss.box.y + detect_area.y;
			if ((info_s.box & s).area() > 0)
			{
				if (info_s.box_name == "HJ")
				{
					scratch.push_back(info_s);
				}
			}
		}
		if (scratch.size() > 0)
		{
			for (auto s:scratch)
			{
				box_info_str box_s;
				box_s.box = s.box;
				box_s.name = "Scratch_marks";
				box_s.state = Scratch_marks;
				res_s.push_back(box_s);
			}
			

		}
	}
	res.insert(res.end(), res_s.begin(), res_s.end());
}