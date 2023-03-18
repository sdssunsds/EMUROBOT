#include "screw.h"
#include <fstream>
struct cut_2_label
{
	cv::Rect box;
	vector<string> labels;
};
struct cut_box
{
	cv::Rect box;
	cv::Rect info;
};
bool in_or_out(cv::Rect box, model_struct_box s)
{
	if (s.box.x + s.box.width/2 < box.x || s.box.x + s.box.width /2 > box.x+ box.width || s.box.y + s.box.height/2 < box.y || s.box.y + s.box.height / 2 > box.y+box.height)
	{
		return false;
	}
	else
	{
		return true;
	}
}
cv::Rect get_lossrect(box_info_str info, int w, int h, int cut_len)
{
	int begin_x, begin_y, end_x, end_y;
	int cut_len_half = cut_len / 2;
	if (info.box.x+info.box.width /2+ cut_len_half<=w&& info.box.x + info.box.width / 2 - cut_len_half >= 0)
	{
		begin_x = info.box.x + info.box.width / 2 - cut_len_half;
		end_x = info.box.x + info.box.width / 2 + cut_len_half;
	}
	else
	{
		if (w < cut_len)
		{
			begin_x = 0;
			end_x = w;
		}
		else
		{
			begin_x = info.box.x + info.box.width / 2 - cut_len_half <= 0 ? 0 : info.box.x + info.box.width / 2 - cut_len_half;
			if (begin_x==0)
			{
				end_x= info.box.x + info.box.width / 2 - cut_len_half >= w ?w : info.box.x + info.box.width / 2 +cut_len_half;
			}
			else
			{
				end_x = info.box.x + info.box.width / 2 + cut_len_half >= w ? w : info.box.x + info.box.width / 2 + cut_len_half;
				if (end_x==w)
				{
					begin_x = w - cut_len > 0 ? w - cut_len : 0;
				}
			}
		}
	}
	if (info.box.y + info.box.height / 2 + cut_len_half <= h && info.box.y + info.box.height / 2 - cut_len_half >= 0)
	{
		begin_y = info.box.y + info.box.height / 2 - cut_len_half;
		end_y = info.box.y + info.box.height / 2 + cut_len_half;
	}
	else
	{
		if (h < cut_len)
		{
			begin_y = 0;
			end_y = h;
		}
		else
		{
			begin_y = info.box.y + info.box.height / 2 - cut_len_half <= 0 ? 0 : info.box.y + info.box.height / 2 - cut_len_half;
			if (begin_y == 0)
			{
				end_y = info.box.y + info.box.height / 2 - cut_len_half >= h ? h : info.box.y + info.box.height / 2 + cut_len_half;
			}
			else
			{
				end_y = info.box.y + info.box.height / 2 +cut_len_half >= h ? h : info.box.y + info.box.height / 2 + cut_len_half;
				if (end_y == h)
				{
					begin_y = h - cut_len > 0 ? h - cut_len : 0;
				}
			}
		}
	}
	cv::Rect box;
	box.x = begin_x;
	box.y = begin_y;
	box.width = end_x-begin_x;
	box.height = end_y - begin_y;
	return box;
}

void screw::analy_res(cv::Mat& inputimg, vector<model_struct_box> mode_box, std::map<std::string, basic_yolo*> infer,bool color, std::vector<int>& task_id_com, std::vector<box_info_str>& res_ss)
{
	const int id = 0;
	std::vector<box_info_str>  res_s;
	cv::Mat show;
	inputimg.copyTo(show);
	cv::Mat loss_img;
	inputimg.copyTo(loss_img);
	for (auto s: mode_box)
	{
		cv::rectangle(show,s.box, cv::Scalar(255, 255, 0), 1);
	}
	if (!color)
	{
		cv::Mat resize;
		int stepSize_0 = infer["screw"]->basic_config.INPUT_H - infer["screw"]->basic_config.INPUT_H % 100 - 100;
		std::vector<inf_res> get_res0;
		bool slide = common_func::prehandel_inputimg(inputimg, resize, stepSize_0);
		if (slide)
		{
			std::map<pt, std::vector<inf_res>> infer_out_0, infer_out_1;
			std::vector<inf_res> inf_vec, inf_vec1;

			cv::Size windowSize_0(infer["screw"]->basic_config.INPUT_H, infer["screw"]->basic_config.INPUT_W);
			std::vector<pt> out_0 = common_func::sliding_window(inputimg.size(), stepSize_0, windowSize_0);
			for (auto img_map : out_0)
			{
				cv::Mat cut_img;
				inputimg(cv::Range(img_map.y, img_map.y + windowSize_0.height), cv::Range(img_map.x, img_map.x + windowSize_0.width)).copyTo(cut_img);
				std::vector<inf_res> inf = infer["screw"]->do_infer(cut_img);
				std::vector<inf_res> nms_res = common_func::nms_fuc(inf, 0.1);

				infer_out_0[img_map] = nms_res;
			}
			for (auto s : infer_out_0)
			{
				for (auto ss : s.second)
				{
					if (ss.box_name=="MD")
					{
						continue;
					}
					inf_res s_inf;
					s_inf.box.x = ss.box.x + s.first.x;
					s_inf.box.y = ss.box.y + s.first.y;
					s_inf.box.width = ss.box.width;
					s_inf.box.height = ss.box.height;
					s_inf.box_name = ss.box_name;
					s_inf.conf = ss.conf;
					//cv::rectangle(inputimg, s_inf.box, cv::Scalar(255, 0, 0), 1);
					inf_vec.push_back(s_inf);
				}
			}
			//test
			cv::Mat test_img;
			inputimg.copyTo(test_img);
			for (int i = 0; i < inf_vec.size(); i++)
			{
				cv::Rect rect_view;
				rect_view.x = inf_vec[i].box.x;
				rect_view.y = inf_vec[i].box.y;
				rect_view.width = inf_vec[i].box.width;
				rect_view.height = inf_vec[i].box.height;
			}
			get_res0 = common_func::per_img_nms(inf_vec, 1);
			for (auto s: get_res0)
			{
				cv::rectangle(test_img, s.box, cv::Scalar(0, 255, 0), 2);
			}
		}
		else
		{
			std::vector<inf_res> inf = infer["screw"]->do_infer(inputimg);
			std::vector<inf_res> nms_res = common_func::nms_fuc(inf, 0.1);

			get_res0 = nms_res;
		}
		
		for (auto s : get_res0)
		{
			box_info_str box_s;
			box_s.box = s.box;
			box_s.conf = s.conf;
			cv::rectangle(show, box_s.box, cv::Scalar(0, 255, 0), 2);
			box_s.name = s.box_name.c_str();
			box_s.state = Normal;
			res_s.push_back(box_s);
		}
		if (mode_box.size() > 0)
		{
			std::vector<std::vector<box_info_str>> loss_res = getloss_res(inputimg, res_s, mode_box);

			vector<bool> second_list(loss_res[1].size(),false);
			
			//测试丢失		
			for (int i=0;i<loss_res.size();i++)
			{
				for (int j = 0; j < loss_res[i].size(); j++)
				{
					cv::Rect rect_view;
					rect_view = loss_res[i][j].box;
					if (i == 0)
					{
						cv::rectangle(loss_img, rect_view, cv::Scalar(0, 255,0 ), 2);
						continue;
					}
					else
					{
						cv::rectangle(loss_img, rect_view, cv::Scalar(0, 0, 255), 2);
					}
				}
				
			}
			
			bool second_check = false;
			if (second_check)
			{
				std::cout << "对丢失的螺丝进行二次检查......" << std::endl;
				int num = 0;
				for (int i = 0; i < loss_res[1].size(); i++)
				{
					//std::cout << i << std::endl;
					cv::Rect cut_label = get_lossrect(loss_res[1][i], inputimg.cols, inputimg.rows, 320);
					cv::Mat cut_img=inputimg(cut_label);
					std::vector<inf_res> inf = infer["screw_loss"]->do_infer(cut_img);
					cv::Rect label, lose_box;
					lose_box.x = loss_res[1][i].box.x-25;
					lose_box.y = loss_res[1][i].box.y-25;
					lose_box.width = loss_res[1][i].box.width+50;
					lose_box.height = loss_res[1][i].box.height+50;
					for (auto s : inf)
					{
						label.x = cut_label.x + s.box.x;
						label.y = cut_label.y + s.box.y;
						label.width = s.box.width;
						label.height = s.box.height;

						if (common_func::IoU_compute(label, lose_box, true, false, false, false, 1e-9) > 0)
						{
							second_list[i] = true;
							num += 1;
						}
					}
				}
				std::cout << "螺丝总数共" << mode_box.size() << endl; 
				std::cout << "丢失总数共"<< loss_res[1].size() - num << std::endl;
				for (int i = 0; i < loss_res.size(); i++)
				{
					if (i == 0)
					{
						for (auto s : loss_res[i])
						{
							box_info_str box_s;
							box_s.box = s.box;
							box_s.name = s.name;
							box_s.state = Normal;
							res_ss.push_back(box_s);
						}
					}
					else if (i == 1)
					{
						for (int j = 0; j < loss_res[i].size(); j++)
						{
							box_info_str box_s;
							box_s.box = loss_res[i][j].box;
							box_s.name = loss_res[i][j].name;
							if (second_list[j])
							{
								box_s.state = Normal;
							}
							else
							{
								box_s.state = Screw_missing;
							
							}
							res_ss.push_back(box_s);
						}
					}

				}
			}
			else
			{
				for (int i = 0; i < loss_res.size(); i++)
				{
					if (i == 0)
					{
						for (auto s : loss_res[i])
						{
							box_info_str box_s;
							box_s.box = s.box;
							box_s.name = s.name;
							box_s.state = Normal;
							res_ss.push_back(box_s);
						}
					}
					else if (i == 1)
					{
						for (int j = 0; j < loss_res[i].size(); j++)
						{

								box_info_str box_s;
								box_s.box = loss_res[i][j].box;
								box_s.name = loss_res[i][j].name;
								box_s.state = Screw_missing;
								res_ss.push_back(box_s);
						}
					}

				}
			}



			bool write_loss = false;//用来设置是否存储图片的变量
			if (write_loss)
			{
				int vec_len = floor(inputimg.rows / 100.0);
				std::vector<vector<model_struct_box>> model_vec;
				for (int i = 0; i < vec_len; i++)
				{
					model_vec.push_back(vector<model_struct_box>{});
					//comp_re.push_back(vector<int>{});
				}
				//int h_num = 0,h_id=0;
				for (auto s : mode_box)
				{
					int i = floor(s.box.y / 100.0);
					if (i < vec_len)
					{
						model_vec[i].push_back(s);
						//cv::rectangle(copy_img, getrect(s), cv::Scalar(255, 255, 0), 1);
					}
				}
				cv::Mat cut_recheck;
				std::vector <int> have_stuf;
				for (int i = 0; i < loss_res[1].size(); i++)
				{
					std::cout << i << std::endl;
					int x_pad = (infer["screw"]->basic_config.INPUT_H - loss_res[1][i].box.width) / 2;
					int y_pad = (infer["screw"]->basic_config.INPUT_H - loss_res[1][i].box.height) / 2;
					if (max(0, loss_res[1][i].box.y - y_pad) > min(inputimg.rows, loss_res[1][i].box.y - y_pad + infer["screw"]->basic_config.INPUT_H) ||
						max(0, loss_res[1][i].box.x - x_pad) > min(inputimg.cols, loss_res[1][i].box.x - x_pad + infer["screw"]->basic_config.INPUT_H))
					{
						std::cout << "二次检测切图参数非法。" << std::endl;
						continue;
					}
				}
				res_ss.insert(res_ss.end(), loss_res[0].begin(), loss_res[0].end());
				for (int i = 0; i < loss_res[1].size(); i++)
				{
					bool in_out = false;
					for (auto ss : have_stuf)
					{
						if (ss == i)
						{
							in_out = true;
						}
					}
					if (in_out)
					{
						continue;
					}
					else
					{
						res_ss.push_back(loss_res[1][i]);
					}
				}
			}
		}
		else
		{
		res_ss.insert(res_ss.end(), res_s.begin(), res_s.end());
		}
	}

	 //面阵检测使用
	else
	{
		std::vector<inf_res> inf = infer["screw_mz"]->do_infer(inputimg);
		for (auto s:inf)
		{
			cv::rectangle(show, s.box, cv::Scalar(0, 0, 255), 2);
		}
		for (auto s:inf)
		{
			box_info_str box_s;
			box_s.box = s.box;
			box_s.name = s.box_name.c_str();
			box_s.state = Normal;
			res_s.push_back(box_s);
		}
		if (mode_box.size()>0)
		{
			std::vector<std::vector<box_info_str>> loss_res = getloss_res(inputimg, res_s, mode_box);
			for (auto s: loss_res)
			{
				for (auto ss:s)
				{
					res_ss.push_back(ss);
				}
			}
		}
		else
		{
			res_ss.insert(res_ss.end(), res_s.begin(), res_s.end());
		}
	}
	task_id_com.push_back(id);
}