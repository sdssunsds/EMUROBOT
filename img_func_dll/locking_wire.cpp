#include "locking_wire.h"
#include "common_func.h"
bool in_or_out(cv::Rect& A, cv::Rect& B)
{
	cv::Rect C = A & B;
	return C == A || C == B;
}
void locking_wire::analy_res(cv::Mat inputimg,
	basic_yolo* infer, Classifier* Ts, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	std::cout << "开始进行防松铁丝检测任务》》》" << std::endl;
	const int id = 2;
	cv::Rect effect_area;
	cv::Mat copy;
	inputimg.copyTo(copy);
	int edge = 50;
	effect_area = cv::Rect(edge, edge, inputimg.cols-2* edge, inputimg.rows - 2 * edge);
	cv::rectangle(copy, effect_area,cv::Scalar(0,255,0),2);
	std::vector<inf_res> inf = infer->do_infer(inputimg);
	std::vector<inf_res> LS,FSTS,ls_s,ts_s;
	for (auto s:inf)
	{
		bool inorout = in_or_out(effect_area, s.box);
		if (!inorout)
		{
			continue;
		}
		if (s.box_name == "TS_FS")
		{
			FSTS.push_back(s);
			cv::rectangle(copy, s.box, cv::Scalar(0, 255, 0), 2);
		}
		else if (s.box_name == "TS_4J" || s.box_name == "TS_ZYK")
		{
			ts_s.push_back(s);
			cv::rectangle(copy, s.box, cv::Scalar(0, 154, 152), 2);
		}
		else if (s.box_name == "LS_6J" )
		{
			LS.push_back(s);
			cv::rectangle(copy, s.box, cv::Scalar(0, 0, 255), 2);
		}
		else if (s.box_name == "LS_D6J" || s.box_name == "LS_4J")
		{
			ls_s.push_back(s);
			cv::rectangle(copy, s.box, cv::Scalar(154, 152, 0), 2);
		}
		
	}
	for (auto s : FSTS)
	{
		cv::rectangle(copy, s.box, cv::Scalar(255, 0, 0));
		if (s.box_name=="TS_FS")
		{
			box_info_str box_s;
			box_s.box = s.box;
			box_s.name = s.box_name;
			int num = 0;
			for (auto ss:LS)
			{
				float iou = common_func::IoU_compute(ss.box, s.box, false, false, false, false, 1e-9);
				if (iou > 0)
				{
					num++;
				}
			}

			cv::Mat cut;
			//cv::Mat cut = inputimg(expbox);
			inputimg(s.box).copyTo(cut);
			//if (num <= 2)
			//{
			//	box_s.state = Broken_wire;
			//	cv::rectangle(copy, s.box, cv::Scalar(0, 0, 255), 2);
			//}
			//else
			//{
				infer_class_info class_res = Ts->infer(cut);
				if (class_res.class_name == "TS_FS"||(class_res.class_name== "TS_DL" && class_res.conf<0.9))
				{
					box_s.state = Normal;
					cv::rectangle(copy, s.box, cv::Scalar(0, 255, 0), 2);
				}
				else
				{
					box_s.state = Broken_wire;
					cv::rectangle(copy, s.box, cv::Scalar(0, 0, 255), 2);
				}
					
			//}
			res_s.push_back(box_s);
			
		}
	}
	//反向检测注油口螺丝，四角螺丝
	for (auto s : ls_s)
	{
		int num = 0;
		inf_res compete;
		for (auto ss : ts_s)
		{
			float iou = common_func::IoU_compute(ss.box, s.box, false, false, false, false, 1e-9);
			if (iou > 0)
			{
				num++;
				compete = ss;
			}
		
		}
		box_info_str box_s;
		
		if (num != 1)
		{
			box_s.box = s.box;
			box_s.name = s.box_name;
			box_s.state = Broken_wire;
			
		}
		else
		{
			box_s.box = compete.box;
			box_s.name = compete.box_name;
			//cv::Rect expbox = expendrect(compete.box, inputimg.cols, inputimg.rows, 15, 15);
			cv::Mat cut;
			inputimg(compete.box).copyTo(cut);
			//if (expbox.x > 0 && expbox.y > 0 && expbox.x + expbox.width < inputimg.cols && expbox.y + expbox.height < inputimg.rows)
			//{
			infer_class_info class_res = Ts->infer(cut);
			if (class_res.class_name == "TS_FS")
			{
				box_s.state = Normal;
				cv::rectangle(copy, compete.box, cv::Scalar(0, 255, 0), 2);
			}
			else
			{
				box_s.state = Broken_wire;
				cv::rectangle(copy, compete.box, cv::Scalar(0, 0, 255), 2);
			}
			//}
		}
		res_s.push_back(box_s);
	}
}