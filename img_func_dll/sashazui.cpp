#include "sashazui.h"
void sashazui::analy_res(cv::Mat inputimg,
	std::map<std::string, basic_yolo*> infermap, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	const int id = 5;
	std::vector<inf_res> inf = infermap["sashazui"]->do_infer(inputimg);
	int screw_num = 0;
	cv::Rect box{0,0,0,0}, up{ 0,0,0,0 }, down{ 0,0,0,0 };
	for (auto s : inf)
	{
		//¼ì²â×´Ì¬
		if (s.box_name== "screw")
		{
			screw_num += 1;
		}
		else if (s.box_name == "rect")
		{
			box = s.box;
		}
		else if (s.box_name == "up")
		{
			up= s.box;
		}
		else if (s.box_name == "down")
		{
			down = s.box;
		}
		cv::rectangle(inputimg, s.box, cv::Scalar(255, 0, 0));
		cv::putText(inputimg, s.box_name, cv::Point(s.box.x, s.box.y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0xFF, 0xFF, 0xFF), 1);
	}
	if (box!= cv::Rect{0, 0, 0, 0})
	{
		if (up != cv::Rect{ 0, 0, 0, 0 }&& down != cv::Rect{ 0, 0, 0, 0 })
		{
			if (up.height/2+up.y< down.height / 2 + down.y)
			{
				box_info_str box_s;
				box_s.box = box;
				box_s.name = "sashazui";
				box_s.state = Normal;
				res_s.push_back(box_s);
			}
			else
			{
				box_info_str box_s;
				box_s.box = box;
				box_s.name = "sashazui";
				box_s.state = Abnormity;
				res_s.push_back(box_s);
			}
		 }
		else
		{
			box_info_str box_s;
			box_s.box = box;
			box_s.name = "sashazui";
			box_s.state = Abnormity;
			res_s.push_back(box_s);
		}
	}
	if (screw_num!=2)
	{
		box_info_str box_s;
		box_s.box = box;
		box_s.name = "sashazui_tiegu";
		box_s.state = Abnormity;
		res_s.push_back(box_s);
	}
	task_id_com.push_back(id);
}