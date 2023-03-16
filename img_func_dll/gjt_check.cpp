#include "gjt_check.h"
#include "slide_window_infer.h"
void gjt_check::analy_res(cv::Mat inputimg, std::vector<box_info_str>& res,
	std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com)
{
	std::vector<std::string> model_name{"zxj","loss_check"};
	std::vector<box_info_str> res_s;
	std::vector<std::string> work_name = { "GJT","pipe" };
	std::vector<inf_res> gjt, guan;
	std::vector<inf_res> target_box;
	if (color)
	{
		target_box = infer[model_name[1]]->do_infer(inputimg);
	}
	for (auto s : target_box)
	{
		if (s.box_name == "GJT")
		{
			gjt.push_back(s);
		}
		else if (s.box_name == "pipe")
		{
			guan.push_back(s);
		}
	}

	for (auto s : gjt)
	{
		bool have = false;
		cv::Rect box = common_func::expend_box(s.box, inputimg.cols, inputimg.rows, 30, 30);
		for (auto ss : guan)
		{
			cv::Rect box_and = box & ss.box;
			cv::Rect box_or = box | ss.box;
			if (box_and.area() > 0)
			{
				have = true;
				box = box_or;
				break;
			}
		}
		box_info_str box_s;
		box_s.box = box;
		box_s.name = "GJT";
		if (have)
		{

			box_s.state = Normal;

		}
		else
		{
			box_s.state = Gjt_loose;
		}
		res_s.push_back(box_s);
	}

	res.insert(res.end(), res_s.begin(), res_s.end());
}
void gjt_check::analy_res(cv::Mat& inputimg, std::vector<inf_res> target_box, std::vector<box_info_str>& res)
{
	std::vector<box_info_str> res_s;
	std::vector<inf_res> gjt, guan;
	for (auto s : target_box)
	{
		if (s.box_name == "GJT")
		{
			gjt.push_back(s);
		}
		else if (s.box_name == "pipe")
		{
			guan.push_back(s);
		}
	}

	for (auto s : gjt)
	{
		bool have = false;
		cv::Rect box = common_func::expend_box(s.box, inputimg.cols, inputimg.rows, 30, 30);
		for (auto ss : guan)
		{
			cv::Rect box_and = box & ss.box;
			cv::Rect box_or = box | ss.box;
			if (box_and.area() > 0)
			{
				have = true;
				box = box_or;
				break;
			}
		}
		box_info_str box_s;
		box_s.box = box;
		box_s.name = "GJT";
		if (have)
		{

			box_s.state = Normal;

		}
		else
		{
			box_s.state = Gjt_loose;
		}
		res_s.push_back(box_s);
	}

	res.insert(res.end(), res_s.begin(), res_s.end());
}