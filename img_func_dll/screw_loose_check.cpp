#include "screw_loose_check.h"

float getdistance(std::vector<cv::Point> contour,cv::Point center)
{
	cv::RotatedRect screw_mark = cv::minAreaRect(contour);
	return abs(screw_mark.center.x - center.x) + abs(screw_mark.center.y - center.y); 
}
cv::Mat getkmark(cv::Mat input)
{
	cv::Mat hsv, hr, hr1, hr2;
	cv::cvtColor(input, hsv, cv::COLOR_BGR2HSV);
	//cv::cvtColor(total, gray, cv::COLOR_BGR2GRAY);
	std::vector<cv::Mat> sp, rgb;
	cv::split(hsv, sp);
	cv::Scalar hsvlow(30, 43, 20);
	cv::Scalar hsvup(124, 255, 180);
	cv::inRange(hsv, hsvlow, hsvup, hr);
	cv::Scalar hsvlow1(15, 40, 25);
	cv::Scalar hsvup1(30, 80, 60);
	cv::inRange(hsv, hsvlow1, hsvup1, hr1);
	cv::Scalar hsvlow2(60, 60, 190);
	cv::Scalar hsvup2(100, 160, 255);
	cv::inRange(hsv, hsvlow2, hsvup2, hr2);
	cv::Mat result, result1;
	result = hr + hr1 + hr2;
	return result;
}

cv::Rect combine_rect(cv::Rect A, cv::Rect B)
{
	cv::Rect C;
	C.x = A.x < B.x ? A.x : B.x;
	C.y = A.y < B.y ? A.y : B.y;
	C.width = A.width + A.x > B.width + B.x ? A.width + A.x - C.x : B.width + B.x - C.x;
	C.height = A.height + A.y > B.height + B.y ? A.height + A.y - C.y : B.height + B.y - C.y;
	return C;
}
float screw_loose_check::getangel(cv::Mat& input,std::vector<cv::Point> contour, cv::Point center)
{
	cv::RotatedRect screw_mark = cv::minAreaRect(contour);
	common_func::draw_rotate_rect(input, screw_mark);
	cv::Point2f vertices[4];
	screw_mark.points(vertices);
	std::vector<cv::Point> mid_point;
	for (int i = 0; i < 4; i++)
	{
		int j = (i + 1) % 4;
		cv::Point mid((vertices[i].x + vertices[j].x) / 2, (vertices[i].y + vertices[j].y) / 2);
		mid_point.push_back(mid);
	}
	int pt1 = abs(center.x - mid_point[0].x) + abs(center.y - mid_point[0].y) > abs(center.x - mid_point[2].x) + abs(center.y - mid_point[2].y) ? 0 : 2;
	int pt2 = abs(center.x - mid_point[1].x) + abs(center.y - mid_point[1].y) > abs(center.x - mid_point[3].x) + abs(center.y - mid_point[3].y) ? 1 : 3;
	int pt3 = pt1 == 0 ? 2 : 0;
	int pt4 = pt2 == 1 ? 3 : 1;
	float w = screw_mark.size.width > screw_mark.size.height ? screw_mark.size.height : screw_mark.size.width;
	float h = screw_mark.size.width <= screw_mark.size.height ? screw_mark.size.height : screw_mark.size.width;
	float wh =  h/w ;
	cv::circle(input, mid_point[pt1], 1, cv::Scalar(255, 0, 0), -1);
	cv::circle(input, mid_point[pt2], 1, cv::Scalar(0, 255, 0), -1);
	cv::circle(input, mid_point[pt3], 1, cv::Scalar(0, 0, 255), -1);
	cv::circle(input, mid_point[pt4], 1, cv::Scalar(128, 128, 0), -1);
	int pt11 = abs(mid_point[pt1].x - mid_point[pt3].x) + abs(mid_point[pt1].y - mid_point[pt3].y) >
		abs(mid_point[pt2].x - mid_point[pt4].x) + abs(mid_point[pt2].y - mid_point[pt4].y) ? pt1 : pt2;
	int pt22 = pt11 == pt1 ? pt3 : pt4;
	if (wh > 1.5&&w>8)
	{
		
		cv::line(input, mid_point[pt22], mid_point[pt11], cv::Scalar(0,255,0));
		return common_func::get_angel_2p(mid_point[pt22], mid_point[pt11], false);
	}
	else
	{
		/*float center_ang = get_angel_2p(center, screw_mark.center, false);		
		double ang1 = get_angel_2p(mid_point[pt1], mid_point[pt3], false);
		double ang2 = get_angel_2p(mid_point[pt2], mid_point[pt4], false);
		return abs(ang1 - center_ang) > abs(ang2 - center_ang) ? ang2 : ang1;*/
		float center_ang = common_func::get_angel_2p(center, mid_point[pt11], false);
		cv::circle(input, mid_point[pt11], 1, cv::Scalar(0,255,0), -1);
		return center_ang;
	}
	
	//return center_ang;
}
float screw_loose_check::getangel_basic(cv::Mat& input, std::vector<cv::Point> contour, cv::Point center)
{
	cv::RotatedRect screw_mark = cv::minAreaRect(contour);
	cv::Point2f vertices[4];
	screw_mark.points(vertices);
	std::vector<cv::Point> mid_point;
	for (int i = 0; i < 4; i++)
	{
		int j = (i + 1) % 4;
		cv::Point mid((vertices[i].x + vertices[j].x) / 2, (vertices[i].y + vertices[j].y) / 2);
		mid_point.push_back(mid);
	}
	int pt1 = abs(center.x - mid_point[0].x) + abs(center.y - mid_point[0].y) > abs(center.x - mid_point[2].x) + abs(center.y - mid_point[2].y) ? 0 : 2;
	int pt2 = abs(center.x - mid_point[1].x) + abs(center.y - mid_point[1].y) > abs(center.x - mid_point[3].x) + abs(center.y - mid_point[3].y) ? 1 : 3;
	int pt3 = pt1 == 0 ? 2 : 0;
	int pt4 = pt2 == 1 ? 3 : 1;
	float w = screw_mark.size.width > screw_mark.size.height ? screw_mark.size.height : screw_mark.size.width;
	float h= screw_mark.size.width <= screw_mark.size.height ? screw_mark.size.height : screw_mark.size.width;
	float wh =h /w;
	cv::circle(input, mid_point[pt1], 1, cv::Scalar(255,0,0), -1);
	cv::circle(input, mid_point[pt2], 1, cv::Scalar(0,255,0), -1);
	cv::circle(input, mid_point[pt3], 1, cv::Scalar(0,0,255), -1);
	cv::circle(input, mid_point[pt4], 1, cv::Scalar(128,128,0), -1);
	common_func::draw_rotate_rect(input, screw_mark);
	if (wh > 1.5&&w>15)
	{
		int pt11 = abs(mid_point[pt1].x - mid_point[pt3].x) + abs(mid_point[pt1].y - mid_point[pt3].y) >
			abs(mid_point[pt2].x - mid_point[pt4].x) + abs(mid_point[pt2].y - mid_point[pt4].y) ? pt1 : pt2;
		int pt22 = pt11 == pt1 ? pt3 : pt4;
		cv::line(input, mid_point[pt22], mid_point[pt11], cv::Scalar(0, 255, 0));
		return common_func::get_angel_2p(mid_point[pt22], mid_point[pt11], false);
	}
	else
	{
		float center_ang = common_func::get_angel_2p(center, screw_mark.center, false);
		cv::circle(input, screw_mark.center, 1, cv::Scalar(255), -1);
		return center_ang;
	}

	//return center_ang;
}
bool screw_loose_check::loose_judge(cv::Mat& inputimg, cv::Rect box, cv::Rect box1)
{
	cv::Mat total = inputimg(box);
	cv::medianBlur(total, total, 5);
	cv::Rect screw_box;
	screw_box.x = box.x < box1.x ? box1.x : box.x;
	screw_box.y = box.y < box1.y ? box1.y: box.y;
	screw_box.width = box.x + box.width > box1.x + box1.width ? box1.x + box1.width - screw_box.x : box.x + box.width - screw_box.x;
	screw_box.height = box.y + box.height > box1.y + box1.height ? box1.y + box1.height - screw_box.y : box.y + box.height - screw_box.y;
	screw_box.x = screw_box.x - box.x;
	screw_box.y = screw_box.y - box.y;
	cv::Mat screw,result;// = total(screw_box);
	
	result = getkmark(total);
	cv::Mat kernel = cv::getStructuringElement(cv::MORPH_RECT, cv::Size(3, 3));
	cv::morphologyEx(result, result, cv::MORPH_CLOSE, kernel, cv::Point(-1, -1), 1);
	cv::morphologyEx(result, result, cv::MORPH_OPEN, kernel, cv::Point(-1, -1), 1);
	cv::morphologyEx(result, result, cv::MORPH_CLOSE, kernel, cv::Point(-1, -1), 2);
	cv::morphologyEx(result, result, cv::MORPH_OPEN, kernel, cv::Point(-1, -1), 2);
	
	
	//cv::bitwise_and(brany, hr, brany);
 	std::vector<std::vector<cv::Point>> contours;                                                                              
	std::vector<cv::Vec4i> hierarchy;
	findContours(result, contours, hierarchy, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE, cv::Point());
	//只有一根线，默认为未松动
	if (contours.size()==1)
	{
		
		return false;
	}
	else
	{
		//有多根线
		std::vector<cv::Point> screw_contour;
		std::vector < std::vector<cv::Point>> basic_conntour;
		std::sort(contours.begin(), contours.end(), sort_area);
		int num = contours.size() >= 3 ? 3 : contours.size();
		for (int i=0;i< num;i++)
		{
			float area = cv::contourArea(contours[i]);
			if (area <=50)
			{
				continue;
			}
			cv::Rect rect = cv::boundingRect(contours[i]);
			float iou_box = common_func::IoU_compute(rect, screw_box, true, false, false, false, 1e-9);

			if (iou_box > 0)
			{
				if (screw_contour.size() > 0)
				{
					if (common_func::IoU_compute(cv::boundingRect(screw_contour), screw_box, true, false, false, false, 1e-9) > iou_box)
					{
						basic_conntour.push_back(contours[i]);
					}
					else
					{
						basic_conntour.push_back(screw_contour);
						//screw_contour = contours[i];
					}
				}
				else
				{
					screw_contour = contours[i];
				}

			}
			else
			{
				basic_conntour.push_back(contours[i]);
			}

			

		}
		//计算角度，首先计算螺丝旋转角度
		cv::Point center(box1.x+ box1.width/2- box.x, box1.y+ box1.height/2-box.y);
		cv::circle(total, center, 1, cv::Scalar(255), -1);
		if (screw_contour.size()!=0)
		{
			if (screw_contour.size()==0|| basic_conntour.size()==0)
			{
				return false;

			}
			std::vector<float> angels;
			for (auto s : basic_conntour)
			{
				float basic_angel = getangel_basic(total, s, center);
				angels.push_back(basic_angel);
			}
			int choose = 0;
			float screw_angel = getangel(total, screw_contour, center);
			float screw_angel2 = screw_angel > 180 ? screw_angel - 180 : screw_angel + 180;
			if (angels.size()==2)
			{
				choose = getdistance(basic_conntour[0],center) > getdistance(basic_conntour[1],center) ? 0 : 1;
				int dp = choose == 0 ? 1 : 0;
				float ang_dp = abs(screw_angel - angels[dp]);
				float ang2_dp = abs(screw_angel2 - angels[dp]);
				if (ang_dp > 180)
				{
					ang_dp = 360 - ang_dp;
				}
				if (ang2_dp > 180)
				{
					ang2_dp = 360 - ang2_dp;
				}
				if (ang_dp < 20)// || ang2_dp <30)
				{
				}
				else
				{
					std::cout << "发现垫片松动" << std::endl;
					return true;
				}
			}

			
			
			float ang = abs(screw_angel - angels[choose]);
			float ang2 = abs(screw_angel2 - angels[choose]);
			if (ang > 180)
			{
				ang = 360 - ang;
			}
			if (ang2 > 180)
			{
				ang2 = 360 - ang2;
			}
			if (ang < 20)// || ang2 < 30)
			{

				return false;
			}
			else
			{
				std::cout << "发现松动" << std::endl;
				return true;
			}
			
			
		}
		else
		{
			//没检测出来.螺丝上的标记
			return false;
		}
	}

	 
	
}
void screw_loose_check::analy_res(cv::Mat& inputimg, std::vector<model_struct_box> mode_box,  std::map<std::string, basic_yolo*> infer, bool color, Classifier* Ts, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	const int id = 7;
	cv::Mat copyimg,rectimg;
	inputimg.copyTo(copyimg);
	inputimg.copyTo(rectimg);
	std::vector<inf_res> inf = infer["screw_db"]->do_infer(inputimg);
	std::vector<inf_res> box_green,screw_box,lossbox;

	//区分类别
	for (auto s:inf)
	{
		if (s.box_name=="GREEN")
		{
			box_green.push_back(s);
			cv::rectangle(rectimg, s.box, cv::Scalar(0, 0, 255), 2);
		}
		else if (s.box_name == "LS")
		{
			screw_box.push_back(s);
			cv::rectangle(rectimg, s.box, cv::Scalar(0,  255,0), 2);
		}
		else if (s.box_name == "KLS")
		{
			lossbox.push_back(s);
			cv::rectangle(rectimg, s.box, cv::Scalar( 255,0, 0), 2);
		}
	}
	
	//匹配位置
	for (int i=0;i<box_green.size();i++)
	{
		auto s= box_green[i];
		int id_v = -1;
		float maxiou = 0;
		cv::Rect expend_box;
		expend_box.x = s.box.x - 100 > 0 ? s.box.x - 100 : 0;
		expend_box.y = s.box.y - 100 > 0 ? s.box.y - 100 : 0;
		expend_box.width = s.box.x + s.box.width + 100 > inputimg.cols - 1 ? inputimg.cols - 1 - expend_box.x : (s.box.x + s.box.width + 100) - expend_box.x;
		expend_box.height = s.box.y + s.box.height + 100 > inputimg.rows - 1 ? inputimg.rows - 1 - expend_box.y : (s.box.y + s.box.height + 100) - expend_box.y;
		for (int i = 0; i < screw_box.size(); i++)
		{
			//计算IOU
			double iou = common_func::IoU_compute(screw_box[i].box, expend_box, false, false, false, false, 1e-9);
			if (iou > maxiou)
			{
				maxiou = iou;
				id_v = i;

			}
		}		
		if (id_v != -1)
		{
			cv::Mat cut = inputimg(s.box);
			infer_class_info class_res = Ts->infer(cut);
			;
			inf_res ss = screw_box[id_v];
			//test
			cv::rectangle(copyimg, ss.box, cv::Scalar(0, 255, 0), 2);
			cv::Rect total_box = s.box|ss.box;
			cv::rectangle(copyimg, total_box, cv::Scalar(255, 0, 0), 2);
			box_info_str box_s;
			box_s.box = ss.box;
			if(class_res.class_name == "loose_ls")
			{
				std::string name = "screw_loose";
				box_s.name = name.c_str();
				box_s.state = Screw_loose;
				res_s.push_back(box_s);
			}
			else if (class_res.class_name == "green_ls")
			{

				bool loose = loose_judge(inputimg, total_box, ss.box);			
				if (loose)
				{
					std::string name = "screw_loose";
					box_s.name = name.c_str();
					box_s.state = Screw_loose;
				}
				else
				{
					std::string name = "screw";
					box_s.name = name.c_str();
					box_s.state = Normal;
				}

				res_s.push_back(box_s);
			}
		}
		else
		{
			bool check_loose = false;
			for (int i = 0; i < lossbox.size(); i++)
			{
				inf_res ss = lossbox[i];
				double iou = common_func::IoU_compute(ss.box, s.box, true, false, false, false, 1e-9);
				if (iou > 0)
				{
					check_loose = true;
					box_info_str box_s;
					box_s.box = ss.box;
					std::string name = "screwloss";
					box_s.name = name.c_str();
					std::cout << "发现丢失" << std::endl;
					box_s.state = Screw_missing;
					res_s.push_back(box_s);
					lossbox.erase(lossbox.begin() + i);
				}

			}
			if (!check_loose)
			{
				box_info_str box_s;
				box_s.box = s.box;
				std::string name = "screwloss";
				box_s.name = name.c_str();
				std::cout << "发现丢失" << std::endl;
				box_s.state = Screw_missing;
				res_s.push_back(box_s);
			}
		}
	}

	for (auto ss : lossbox)
	{
		if (ss.conf >= 0.7)
		{
			box_info_str box_s;
			box_s.box = ss.box;
			std::string name = "screwloss";
			box_s.name = name.c_str();
			std::cout << "发现丢失" << std::endl;
			box_s.state = Screw_missing;
			res_s.push_back(box_s);
		}

	}
	task_id_com.push_back(id);
}