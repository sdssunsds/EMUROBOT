#include "get_loss.h"

//{"gjt", "HK", "LOCK_6L", "LOCK_6L_S", "MFJ", "PQK", "SH", "X_KK", "X_P", "ZPT", "ZYKD", "guan"}
//面阵相机丢失检测
std::map<std::string, std::string> transmap{ {"LS_SZ","LS_SZ"}, {"Zp","Zp"},{"Djmp","Djmp"}, {"LS_6J","LS_6J"}, 
	{"LS_LS","LS_LS"}, {"LS_N6","LS_N6"},{"LS_4J","LS_4J"},{"Hk","Hk"},{"Lock_6l","Lock_6l"}, {"Lock_6l_s","Lock_6l_s"},
	{"Mfj","Mfj"}, {"Pqk","Pqk"}, {"Sh","Sh"}, {"X_kk","Kkx"}, {"Zykd","Zykd"},
	{"X_p","X_p"}, {"Kkx","Kkx"},{"Gjt","Gjt"}, {"Pipe","Gjt"} ,{"Zpt","Zpt"} ,{"Zp","Zp"} };

bool sort_model_pos(model_struct_box& box1, model_struct_box& box2)
{
	return (box1.box.x+ box1.box.y*2 < box2.box.x + box2.box.y * 2);
}
bool sort_inf_pos(inf_res& box1, inf_res& box2)
{
	return (box1.box.x + box1.box.y * 2 < box2.box.x + box2.box.y * 2);
}
std::vector<model_struct_box> modelArea_flitter(std::vector<model_struct_box> model,cv::Size inputsize)
{
	cv::Rect effect_area(0, 0, inputsize.width, inputsize.height);
	std::vector<model_struct_box> res;
	for (auto s: model)
	{
		if (effect_area.contains(s.box.br())&& effect_area.contains(s.box.tl()))
		{
			res.emplace_back(s);
		}
	}
	return res;
}
void get_loss::analy_res(cv::Mat inputimg, std::vector<model_struct_box> mode_box_input,
	std::map<std::string, basic_yolo*> infer, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	std::vector<model_struct_box> mode_box;
	std::vector<model_struct_box> mode_box_flitter = modelArea_flitter(mode_box_input, inputimg.size());
	//过滤掉无效标签
	for (auto s : mode_box_flitter)
	{
		if (transmap.find(s.class_name) != transmap.end())
		{
			mode_box.push_back(s);
		}
	}
	if (mode_box.size()!=0)
	{

		//检查项模板
		std::map < std::string, std::vector<model_struct_box>> model_check_map, loss_check;
		//对模板进行分类
		for (auto s : mode_box)
		{
			if (model_check_map.find(transmap[s.class_name]) == model_check_map.end())
			{
				model_check_map[transmap[s.class_name]] = std::vector<model_struct_box>{ s };
			}
			else
			{
				model_check_map[transmap[s.class_name]].push_back(s);
			}

		}
		std::vector<inf_res> inf = infer["loss_check"]->do_infer(inputimg);
		std::vector<inf_res> inf1 = infer["screw_mz"]->do_infer(inputimg);
		inf.insert(inf.end(), inf1.begin(), inf1.end());
		cv::Mat show;
		//if (show_test)
		//{
		//	inputimg.copyTo(show);
		//	for (auto s : inf)
		//	{
		//		cv::rectangle(show, s.box, cv::Scalar(0, 0, 255), 2);
		//	}
		//	for (auto s : mode_box_input)
		//	{
		//		cv::rectangle(show, s.box, cv::Scalar(0, 255, 0), 2);
		//	}
		//}
		std::map < std::string, std::vector <inf_res>> check_map;
		for (auto s : inf)
		{
			if (check_map.find(transmap[s.box_name]) == check_map.end())
			{
				check_map[transmap[s.box_name]] = std::vector<inf_res>{ s };
			}
			else
			{
				check_map[transmap[s.box_name]].push_back(s);
			}

		}
		//std::sort(mode_box.begin(), mode_box.begin(), sort_box_area);
		std::vector<cv::Point> bias;
		for (auto ss : model_check_map)
		{
			std::vector <inf_res> class_v = check_map[ss.first];
			std::vector<model_struct_box> model = ss.second;
			if (ss.second.size() == class_v.size())
			{

				for (auto i : class_v)
				{
					box_info_str obj;
					obj.box = i.box;
					cv::rectangle(inputimg, obj.box, cv::Scalar(0, 0, 250), 1);
					obj.name = transmap[ss.first];
					obj.state = Normal;
					res_s.push_back(obj);
				}
				//计算bias
				std::sort(class_v.begin(), class_v.begin(), sort_inf_pos);
				std::sort(model.begin(), model.begin(), sort_model_pos);
				for (int i = 0; i < class_v.size(); i++)
				{
					int x = (ss.second[i].box.x + ss.second[i].box.width / 2) - (class_v[i].box.x + class_v[i].box.width / 2);
					int y = (ss.second[i].box.y + ss.second[i].box.height / 2) - (class_v[i].box.y + class_v[i].box.height / 2);
					bias.push_back(cv::Point(x, y));

				}


			}
			//说明有检测多的
			else if( class_v.size()>ss.second.size())
			{
				for (auto i : class_v)
				{
					box_info_str obj;
					obj.box = i.box;
					obj.name = transmap[ss.first];
					obj.state = Normal;
					res_s.push_back(obj);
				}
			}
			else
			{
				loss_check[ss.first] = ss.second;
			}
		}
		cv::Rect box = cv::boundingRect(bias);
		cv::Point center(box.x + box.width / 2, box.y + box.height / 2);
		for (auto s : loss_check)
		{
			std::vector <inf_res> class_v = check_map[s.first];
			std::vector<model_struct_box> model = s.second;
			for (auto i : model)
			{
				bool have = false;
				box_info_str obj;
				obj.box = i.box;
				obj.name = s.first;
				cv::Rect correct(i.box.tl() - center, i.box.br() - center);
				cv::rectangle(inputimg, correct, cv::Scalar(0,128, 128), 1);
				for (auto sss : class_v)
				{					
					float iou = common_func::IoU_compute(sss.box, correct);
					if (iou > 0)
					{
						have = true;
						obj.box = sss.box;
						obj.state = Normal;
					}
				}
				if (!have)
				{
					obj.state = BJDS;
				}
				res_s.push_back(obj);
			}
		}
	}
	else
	{
		std::vector<inf_res> inf = infer["loss_check"]->do_infer(inputimg);
		std::vector<inf_res> inf1 = infer["screw_mz"]->do_infer(inputimg);
		inf.insert(inf.end(), inf1.begin(), inf1.end());
		for (auto s : inf)
		{
			box_info_str obj;
			obj.box = s.box;
			obj.name = s.box_name;
			obj.state = Normal;
			res_s.push_back(obj);
		}
	}
}
void get_loss::analy_res(cv::Mat inputimg, cv::Mat modelimg, std::vector<model_struct_box> mode_box,
	std::map<std::string, basic_yolo*> infer, spsg& correct_img, std::vector<int>& task_id_com, std::vector<box_info_str>& res_s)
{
	cv::Mat input_copy;
	inputimg.copyTo(input_copy);
	cv::Mat gray_model, gray_input, trans_res, tran_input;
	cv::cvtColor(inputimg, gray_input, cv::COLOR_BGR2GRAY);
	cv::cvtColor(modelimg, gray_model, cv::COLOR_BGR2GRAY);
	cv::Scalar mean_input = cv::mean(gray_input);
	cv::Scalar mean_model = cv::mean(gray_model);
	gray_input = gray_input - (mean_model - mean_input);
	cv::Mat invert_trans;
	std::vector<box_info_str>res;
	cv::Mat trans_Mat = correct_img.change(gray_input, gray_model, invert_trans);
	if (!trans_Mat.empty())
	{
		cv::warpPerspective(input_copy, input_copy, trans_Mat, inputimg.size());
		analy_res(input_copy, mode_box, infer, task_id_com, res);
		for (int i = 0; i < res.size(); i++)
		{
			cv::Rect box=res[i].box;
			box = common_func::rect_trans(invert_trans, box);
			if (res[i].state != 0)
			{
				if (box.tl().x<0 || box.tl().y<0 || box.br().x > inputimg.cols || box.br().y > inputimg.rows)
				{
					continue;
				}
				else
				{
					//cv::rectangle(inputimg, box, cv::Scalar(0, 0, 255));
				}
			}
			else
			{
				//cv::rectangle(inputimg, box, cv::Scalar(0, 255, 0));
			}

			
			res[i].box = box&cv::Rect(0,0, inputimg.cols, inputimg.rows);
			res_s.emplace_back(res[i]);
		}
	}
	else
	{
		analy_res(input_copy, mode_box, infer, task_id_com, res);
		res_s.insert(res_s.end(),res.begin(), res.end());
	}
}