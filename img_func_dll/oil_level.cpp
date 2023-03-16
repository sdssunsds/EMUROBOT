#include"oil_level.h"
bool oil_level::in_or_out(cv::Point point, cv::Rect area)
{
	int point_x = point.x, point_y = point.y;
	if (point_y <area.y-10)
	{
		return false;
	}

	else
	{
		return true;
	}

}
bool sort_oil_kd(inf_res& box1, inf_res& box2)
{
	return (box1.conf > box2.conf);
}
void get_kd(std::vector<inf_res> kd,int& hmax,int& hmin)
{
	if (kd.size() > 2)
	{
		std::sort(kd.begin(), kd.end(), sort_oil_kd);
		//确定刻度线位置
		if (abs(kd[0].box.y - kd[1].box.y) < 15)
		{
			hmax = (kd[0].box.y + kd[1].box.y) / 2;
		}
		else
		{
			hmax = kd[0].box.y < kd[1].box.y ? kd[0].box.y : kd[1].box.y;
		}
		if (abs((kd[0].box.y+ kd[0].box.height) - (kd[1].box.y+ kd[1].box.height)) < 15)
		{
			hmin = (kd[0].box.y + kd[0].box.height + kd[1].box.y + kd[1].box.height) / 2;
		}
		else
		{
			hmin = kd[0].box.y + kd[0].box.height > kd[1].box.y + kd[1].box.height ? kd[0].box.y + kd[0].box.height : kd[1].box.y + kd[1].box.height;
		}

	}
	else
	{
		hmin = kd[0].box.y + kd[0].box.height;
		hmax = kd[0].box.y ;
	}

}
void Rotate(const cv::Mat& srcImage, cv::Mat& destImage,cv::Point ct, double angle)
{
	//cv::Point2f center(srcImage.cols/2, srcImage.rows/2);//中心
	cv::Mat M = getRotationMatrix2D(ct, angle, 1);//计算旋转的仿射变换矩阵 
	cv::warpAffine(srcImage, destImage, M, cv::Size(srcImage.cols, srcImage.rows));//仿射变换  
	//circle(destImage, center, 10, Scalar(255, 0, 0),-1);
}
cv::Rect tran_box(cv::Rect input_box,int w,int h)
{
	cv::Rect new_box=input_box; 
	new_box.x = w - input_box.x - input_box.width;
	new_box.y = h - input_box.y - input_box.height;
	return new_box;
}
bool sort_dis(cv::Point& A, cv::Point& B)
{
	return abs(A.x) + abs(A.y) < abs(B.x) + abs(B.y);
}
void oil_level::analy_res(cv::Mat srcimg,
	std::map<std::string, basic_yolo*> infermap,bool color ,std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	const int id = 3;
	std::cout << "开始进行油位表检测任务》》》" << std::endl;
	if (!color)
	{
		std::cout << "油位表检测，不是面阵相机图片，无法进行任务，请检查" << std::endl;
		task_id_com.push_back(id);
	}
	else
	{
		cv::Mat inputimg;
		srcimg.copyTo(inputimg);
		cv::Point center = { inputimg.cols / 2,inputimg.rows / 2 };
		std::vector<inf_res> inf = infermap["oil_level"]->do_infer(inputimg);

		cv::Rect oil_guage_rect{ 0,0,0,0 };
		cv::Rect yellow_rect{ 0,0,0,0 };
		cv::Rect liquid_level_rect{ 0,0,0,0 };
		cv::Point ct;
		cv::Mat copy_input;
		inputimg.copyTo(copy_input);
		bool state = false;
		for (auto s : inf)
		{
			if (s.box_name == "oil_guage")
			{
				oil_guage_rect = s.box;
				ct.x = inputimg.cols / 2;
				ct.y = inputimg.rows / 2;

			}
			else if (s.box_name == "yellow")
			{
				yellow_rect = s.box;
			}
			else if (s.box_name == "liquid_level")
			{
				liquid_level_rect = s.box;
			}
			cv::rectangle(copy_input, s.box, cv::Scalar(255, 0, 0));
			cv::putText(copy_input, s.box_name, cv::Point(s.box.x, s.box.y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0xFF, 0xFF, 0xFF), 1);
		}
		if (oil_guage_rect.area() !=0 && yellow_rect.area() !=0 && liquid_level_rect.area()!= 0)
		{
			state = true;
		}
		if (state)
		{

			box_info_str box_s;
			box_s.box = oil_guage_rect;
			box_s.name = "oil_guage";
			box_info_str llv;
			llv.box = liquid_level_rect;
			llv.name = "liquid_level";

			cv::Mat oil_guage;
			//inputimg(oil_guage_rect).copyTo(oil_guage);
			inputimg.copyTo(oil_guage);
			if (yellow_rect.y + yellow_rect.height / 2 < liquid_level_rect.y + liquid_level_rect.height / 2)
			{
				Rotate(inputimg, inputimg, ct, 180);
				oil_guage_rect = tran_box(oil_guage_rect, inputimg.cols, inputimg.rows);
				yellow_rect = tran_box(yellow_rect, inputimg.cols, inputimg.rows);
				liquid_level_rect = tran_box(liquid_level_rect, inputimg.cols, inputimg.rows);
				cv::rectangle(inputimg, oil_guage_rect, cv::Scalar(255, 0, 0));
			}
			inputimg.copyTo(oil_guage);
			std::vector<inf_res> screw = infermap["screw_mz"]->do_infer(oil_guage);
			oil_guage = oil_guage(oil_guage_rect);
			std::vector<cv::Point> getrect;
			for (auto s : screw)
			{
				if (s.box_name == "LS_6J" && (oil_guage_rect & s.box).area()> s.box.area()*0.5)
				{
					cv::Point p = cv::Point{ s.box.x - oil_guage_rect.x + s.box.width / 2,s.box.y - oil_guage_rect.y + s.box.height / 2 };
					getrect.push_back(p);
					cv::circle(oil_guage,p,2,cv::Scalar(0,255,255),-1);
				}

			}
			
			cv::RotatedRect box;
			std::cout << "getrect.size()=" + std::to_string(getrect.size()) << std::endl;
			if (getrect.size() >6)
			{
				cv::Point ct_r(oil_guage_rect.width/2, oil_guage_rect.height / 2);
				std::vector<cv::Point> getrect_c;
				for (auto s:getrect)
				{
					getrect_c.push_back(s - ct_r);
				}
				std::sort(getrect_c.begin(), getrect_c.end(),sort_dis);
				getrect.clear();
				for (int i=0;i<6;i++)
				{
					getrect.push_back(getrect_c[i] + ct_r);
				}
			}
			if (getrect.size() > 4)
			{
				
				box = cv::minAreaRect(getrect);
				cv::Point2f vertex[4];
				box.points(vertex);
				//绘制点
				//绘制旋转矩形
				for (int i = 0; i < 4; i++)
				{
					cv::line(oil_guage, vertex[i], vertex[(i + 1) % 4], cv::Scalar(255, 100, 200), 2);

				}

				float ang = 0;
				if (box.size.width > box.size.height)
				{
					ang = 90 + box.angle - 50;// -20;
				}
				else
				{
					ang = box.angle - 50;// -20;
				}

				cv::Mat img_rot;
				Rotate(inputimg, img_rot, ct, ang);
				//cv::imwrite("../" + strname, img_rot);
				std::vector<inf_res> inf_rot = infermap["oil_level"]->do_infer(img_rot);
				cv::Mat show_res;
				img_rot.copyTo(show_res);
				for (auto s : inf_rot)
				{
					cv::rectangle(show_res, s.box, cv::Scalar(255, 0, 0));
					cv::putText(show_res, s.box_name, cv::Point(s.box.x, s.box.y - 1), cv::FONT_HERSHEY_PLAIN, 1.2, cv::Scalar(0xFF, 0xFF, 0xFF), 1);
				}

				std::map<std::string, inf_res> dict_map;
				std::vector<inf_res> kd;
				for (auto s : inf_rot)
				{
					if (s.box_name == "scale")
					{
						kd.push_back(s);
					}
					else
					{
						if (dict_map.find(s.box_name) == dict_map.end())
						{
							dict_map[s.box_name] = s;
						}
						else
						{
							if (dict_map[s.box_name].conf < s.conf)
							{
								dict_map[s.box_name] = s;
							}
						}
					}


				}
				if (kd.size() > 0)
				{

					//if (dict_map.find("yellow") == dict_map.end())
					//{

					//	box_s.state = Oil_turbidity;

					//	res_s.push_back(box_s);
					//	llv.state = Normal;
					//	res_s.push_back(llv);
					//}
					//else
					//{

					//	if (dict_map["yellow"].conf < 0.4)
					//	{
					//		box_s.state = Oil_turbidity;
					//		res_s.push_back(box_s);
					//	}
					//	else
					//	{
							int hmax, hmin;
							get_kd(kd, hmax, hmin);
							if (abs(hmax - hmin) / 20 >= 2)
							{
								hmin = hmax + abs(hmax - hmin) / 2;
							}

							inf_res coordinate_liquid = dict_map["liquid_level"];
							//inf_res scale_area = dict_map["scale"];
							cv::Point liquid_center;
							liquid_center.x = int(coordinate_liquid.box.x + 0.5 * coordinate_liquid.box.width);
							liquid_center.y = int(coordinate_liquid.box.y + coordinate_liquid.box.height / 2);
							bool flag = liquid_center.y <= hmin+5 && liquid_center.y >= hmax-5;
							if (flag)
							{
								box_s.state = Normal;
								res_s.push_back(box_s);
								llv.state = Normal;
								res_s.push_back(llv);
							}
							else
							{
								box_s.state = Abnormal_oil_level;
								res_s.push_back(box_s);
								llv.state = Normal;
								res_s.push_back(llv);
							}
					//	}
					//}
				}
				else
				{
					std::cout << "未找刻度。" << std::endl;
					box_s.name = "oil_guage";
					box_s.state = UnSafecheck;
					res_s.push_back(box_s);
				}
			}
			else
			{
				std::cout << "未找到定位螺丝。" << std::endl;
				box_s.name = "oil_guage";
				box_s.state = UnSafecheck;
				res_s.push_back(box_s);
			}
		}
		task_id_com.push_back(id);
	}
	
	
}

	