#include "oil_leakage.h"
#include "slide_window_infer.h"
void oil_leakage::analy_res(cv::Mat inputimg,
	std::map<std::string, basic_yolo*> infer,bool color, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	const int id = 8;
	cv::Mat show;;
	inputimg.copyTo(show);
	if (!color)
	{
		cv::Mat input_resize;
		float f = float(infer["zxj"]->basic_config.INPUT_W) / float(inputimg.cols);
		cv::resize(inputimg, input_resize, cv::Size(), f, f);
		std::vector<inf_res> target_box = slide_window_infer::infer(input_resize, infer["zxj"]);
		std::vector <cv::Rect> effect_area;
		std::vector<std::string> work_name = { "oil_damper","clx"};
		for (auto s : target_box)
		{
			if (count(work_name.begin(), work_name.end(), s.box_name))
			{
				cv::Rect box = s.box;
				box.x = int(box.x / f);
				box.y = int(box.y / f);
				box.width = int(box.width / f);
				box.height = int(box.height / f);
				/*if (box.width> box.height)
				{*/
					effect_area.push_back(box);
				//}
				
				cv::rectangle(show, box, cv::Scalar(152, 0, 255), 2);
			}
		}
		int w = infer["oil_leakage"]->basic_config.INPUT_W;
		for (auto s : effect_area )
		{
			//cv::rectangle(show, s, cv::Scalar(152,0,255), 2);
			cv::Rect detect_area=s;
			detect_area.width = w;
			if (s.width< infer["oil_leakage"]->basic_config.INPUT_W)
			{
				detect_area.x = detect_area.x - (w - s.width)/2 < 0 ? 0 : detect_area.x - (w - s.width) / 2;
				if (detect_area.x+ detect_area.width> inputimg.cols)
				{
					detect_area.x = inputimg.cols - w;
				}
				if (detect_area.x<0)
				{
					std::cout << "ÓÍÎ»Ð¹Â©¼ì²âÍ¼ÏñÊäÈë³ß´ç´íÎó¡£" << std::endl;
				}
			}
			cv::rectangle(show, detect_area, cv::Scalar(152, 152, 0), 2);
			std::vector<inf_res> res_detect = slide_window_infer::infer(inputimg(detect_area), infer["oil_leakage"]);
			std::vector<inf_res> res_detect_trans,yj,yd;
			for (auto ss: res_detect)
			{
				inf_res info_s = ss;
				info_s.box.x = ss.box.x + detect_area.x;
				info_s.box.y = ss.box.y + detect_area.y;				
				if ((info_s.box&s).area()>0)
				{
					res_detect_trans.push_back(info_s);
					if (info_s.box_name=="yj")
					{
						yj.push_back(info_s);
					}
					else if (info_s.box_name == "yd")
					{
						yd.push_back(info_s);
					}
				}
			}
			if (res_detect_trans.size() >5&& yd.size()>=3)
			{

				cv::Point pt1, pt2;
				for (int i = 0; i < res_detect_trans.size(); i++)
				{

					cv::rectangle(show, res_detect_trans[i].box, cv::Scalar(0, 152, 255), 2);
					if (i == 0)
					{
						pt1.x = res_detect_trans[i].box.x;
						pt1.y = res_detect_trans[i].box.y;
						pt2.x = res_detect_trans[i].box.x+res_detect_trans[i].box.width;
						pt2.y = res_detect_trans[i].box.y+res_detect_trans[i].box.height;
					}
					else
					{
						
						pt1.x = pt1.x < res_detect_trans[i].box.x ? pt1.x : res_detect_trans[i].box.x;
						pt1.y = pt1.y < res_detect_trans[i].box.y ? pt1.y : res_detect_trans[i].box.y;
						pt2.x = pt2.x > res_detect_trans[i].box.x + res_detect_trans[i].box.width ? pt2.x : res_detect_trans[i].box.x + res_detect_trans[i].box.width;
						pt2.y = pt2.y > res_detect_trans[i].box.y + res_detect_trans[i].box.height ? pt2.y : res_detect_trans[i].box.y + res_detect_trans[i].box.height;
					}
				}
				cv::Rect boxexpend(pt1, pt2);
				//cv::rectangle(show, boxexpend, cv::Scalar(0, 255, 0), 2);

				box_info_str box_s;
				box_s.box = boxexpend;
				box_s.name = "oil_leakage";
				box_s.state = YW;
				res_s.push_back(box_s);
				
			}
		}	
	}
	else
	{
		std::cout << "Ä¿Ç°¼ì²âÏßÕóÍ¼Ïñ" << std::endl;
	}
	task_id_com.push_back(id);
}
void oil_leakage::analy_res_oil(cv::Mat inputimg, std::vector<inf_res> target_box, basic_yolo* infer, std::vector<box_info_str>& res)
{
	std::vector<box_info_str> res_s;
	cv::Mat show;;
	inputimg.copyTo(show);
	std::vector <cv::Rect> effect_area;
	std::vector<std::string> work_name = { "oil_damper","clx" };
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
		cv::rectangle(show, s, cv::Scalar(152,0,255), 2);
		cv::Rect detect_area = s;
		detect_area.width = w;
		if (s.width < infer->basic_config.INPUT_W)
		{
			detect_area.x = detect_area.x - (w - s.width) / 2 < 0 ? 0 : detect_area.x - (w - s.width) / 2;
			if (detect_area.x + detect_area.width > inputimg.cols)
			{
				detect_area.x = inputimg.cols - w;
			}
			if (detect_area.x < 0)
			{
				std::cout << "ÓÍÎ»Ð¹Â©¼ì²âÍ¼ÏñÊäÈë³ß´ç´íÎó¡£" << std::endl;
			}
		}
		cv::rectangle(show, detect_area, cv::Scalar(152, 152, 0), 2);
		std::vector<inf_res> res_detect = slide_window_infer::infer(inputimg(detect_area), infer);
		std::vector<inf_res> res_detect_trans, yj, yd;
		for (auto ss : res_detect)
		{
			inf_res info_s = ss;
			info_s.box.x = ss.box.x + detect_area.x;
			info_s.box.y = ss.box.y + detect_area.y;
			if ((info_s.box & s).area() > 0)
			{
				res_detect_trans.push_back(info_s);
				if (info_s.box_name == "yj")
				{
					yj.push_back(info_s);
				}
				else if (info_s.box_name == "yd")
				{
					yd.push_back(info_s);
				}
			}
		}
		if (res_detect_trans.size() >5||yd.size() >= 3)
		{

			cv::Point pt1, pt2;
			for (int i = 0; i < res_detect_trans.size(); i++)
			{

				cv::rectangle(show, res_detect_trans[i].box, cv::Scalar(0, 152, 255), 2);
				if (i == 0)
				{
					pt1.x = res_detect_trans[i].box.x;
					pt1.y = res_detect_trans[i].box.y;
					pt2.x = res_detect_trans[i].box.x + res_detect_trans[i].box.width;
					pt2.y = res_detect_trans[i].box.y + res_detect_trans[i].box.height;
				}
				else
				{

					pt1.x = pt1.x < res_detect_trans[i].box.x ? pt1.x : res_detect_trans[i].box.x;
					pt1.y = pt1.y < res_detect_trans[i].box.y ? pt1.y : res_detect_trans[i].box.y;
					pt2.x = pt2.x > res_detect_trans[i].box.x + res_detect_trans[i].box.width ? pt2.x : res_detect_trans[i].box.x + res_detect_trans[i].box.width;
					pt2.y = pt2.y > res_detect_trans[i].box.y + res_detect_trans[i].box.height ? pt2.y : res_detect_trans[i].box.y + res_detect_trans[i].box.height;
				}
			}
			cv::Rect boxexpend(pt1, pt2);
			box_info_str box_s;
			box_s.box = boxexpend;
			box_s.name = "oil_leakage";
			box_s.state = YW;
			res_s.push_back(box_s);

		}
		
	}
	res.insert(res.end(), res_s.begin(), res_s.end());
}