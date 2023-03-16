#pragma once
#include "basic_struct.h"
#include "basic_yolo.h"
#include <direct.h>
#include <io.h>
#include <iostream>
bool sort_score_4(inf_res& box1, inf_res& box2);
bool sort_area(std::vector<cv::Point>& box1, std::vector<cv::Point>& box2);
class common_func
{
public:
	static float iou(cv::Rect& A, cv::Rect& B);
	static bool prehandel_inputimg(cv::Mat& input, cv::Mat& output, int size);
	static void getFiles(std::string path, std::vector<std::string>& files);
	//static float IOU_4(box_info_str& A, box_info_str& B);
	//static float IOU_4(box_info& A, box_info& B);
	static std::vector < box_info> trans(std::vector < box_info_str> input);
	//static void delet_repeat(std::vector<box_info>& input, std::vector<box_info>& output);
	static std::vector<std::string> SplitString(const std::string& s, const std::string& c);
	static std::vector<pt> sliding_window(cv::Size inputimg_size, int stepSize, cv::Size windowSize);
	static std::vector<inf_res> per_img_nms(std::vector<inf_res>& infer_out, int model);
	static std::vector<inf_res> nms_fuc(std::vector<inf_res> vec, float NMS_THRESH);
	static double IoU_compute(cv::Rect A, cv::Rect B, bool GIoU = false, bool DIoU = false, bool CIoU = false, bool EIoU = false, double eps = 1e-9);
	static void whitening(cv::Mat image);
	static double trans(double min, double handel, double pixelVal);
	static float get_angel_2p(cv::Point  pt1, cv::Point pt2, bool rad);
	static double pt2line_distance(cv::Point P, cv::Point A, cv::Point B);
	static std::string rand_str(const int len);
	static std::string save_img(cv::Mat& img, std::string path);
	//»æÖÆÐý×ª¾ØÕó
	static void draw_rotate_rect(cv::Mat& image, cv::RotatedRect rotate_rect);
	static cv::Rect expend_box(cv::Rect basic_box, int img_cols, int img_rows, int expend_x, int expend_y);
	static cv::Point point_trans(cv::Mat trans_mat, cv::Point inputpoint);
	static cv::Rect rect_trans(cv::Mat trans_mat, cv::Rect inputpoint);
};