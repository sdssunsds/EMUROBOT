#include "ct_check.h"
#include "slide_window_infer.h"
void ct_check::analy_res(cv::Mat inputimg, std::vector<box_info_str>& res,
	std::map<std::string, basic_yolo*> infer, bool color, std::vector<int>& task_id_com)
{
	if (!color)
	{
		//宽度阈值
		float width_thr = 15;
		cv::Rect effect_rect{ 850,300,400,400 };
		cv::Mat chink, chink_brainy;
		inputimg(effect_rect).copyTo(chink);
		cv::cvtColor(chink, chink, cv::COLOR_BGR2GRAY);
		std::vector<box_info_str> res_s;
		//计算中缝间距是否过大
		cv::GaussianBlur(chink, chink, cv::Size(5, 5), 0);
		cv::adaptiveThreshold(chink, chink_brainy, 255, cv::ADAPTIVE_THRESH_GAUSSIAN_C, cv::THRESH_BINARY_INV, 25, 5);
		std::vector<std::vector<cv::Point>> contours;
		std::vector<cv::Vec4i> hierarchy;
		cv::findContours(chink_brainy, contours, hierarchy, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE, cv::Point());
		std::sort(contours.begin(), contours.end(), sort_area);
		if (contours.size() >= 1)
		{
			cv::Mat zero_basic = cv::Mat::zeros(chink_brainy.size(), CV_8UC1);
			cv::drawContours(zero_basic, contours, 0, 255, -1);
			cv::Point2f vertices[4];
			cv::RotatedRect screw_mark = cv::minAreaRect(contours[0]);
			cv::Rect box = screw_mark.boundingRect();
			int h = 0;
			int num = 0;
			for (int y = 0; y < box.height; y++)
			{
				bool effect = false;
				for (int x = 0; x < box.width; x++)
				{

					int k = zero_basic.at<uchar>(box.y + y, box.x + x);
					if (k != 0)
					{
						num++;
						effect = true;
					}
				}
				if (effect)
				{
					h++;
				}
			}
			float mean = float(num) / h;
			if (mean >= width_thr)
			{
				box_info_str box_s;
				box_s.box = box;
				box_s.name = "Chink_superthreshold";
				box_s.state = Chink_superthreshold;
				res_s.push_back(box_s);
			}
		}
		std::vector<inf_res> target_box = infer["ct"]->do_infer(inputimg);
		std::vector<std::string> work_name = { "AK","CLW" };
		for (auto s : target_box)
		{
			box_info_str box_s;
			box_s.box = s.box;
			if (s.conf > 0.7)
			{
				if (s.box_name == "CLW")
				{
					box_s.name = "Foreign_body";
					box_s.state = Foreign_body;
					res_s.push_back(box_s);
				}
				else if (s.box_name == "AK")
				{
					box_s.name = "AK";
					box_s.state = Scratch_marks;
					res_s.push_back(box_s);
				}

			}
		}

		res.insert(res.end(), res_s.begin(), res_s.end());
	}
	
}