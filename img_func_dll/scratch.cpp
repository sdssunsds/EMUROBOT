#include "scratch.h"
void scratch::analy_res(cv::Mat inputimg, std::vector<box_info_str>& res,
	basic_yolo* infer, std::vector<int>& task_id_com)
{
	const int id = 4;
	std::vector<box_info_str> res_s;
	std::vector<inf_res> inf = infer->do_infer(inputimg);
	std::cout << "¼ì²â³ö»®ºÛÊý£º" << inf.size()<< std::endl;
	for (auto s : inf)
	{
		box_info_str box_s;
		box_s.box = s.box;
		box_s.name = s.box_name;
		if (s.box_name == "scratch")
		{
			box_s.state = Scratch_marks; 
		}
		else
		{
			box_s.state = Normal;
		}
		res_s.push_back(box_s);
	}
	task_id_com.push_back(id);
	res.insert(res.end(), res_s.begin(), res_s.end());
}