#include "common_func.h"

#define pi 3.1415926
bool sort_score_4(inf_res& box1, inf_res& box2)
{
	return (box1.conf > box2.conf);
}
bool sort_boxy(inf_res& box1, inf_res& box2)
{
	return (box1.box.y < box2.box.y);
}
bool sort_area(std::vector<cv::Point>& box1, std::vector<cv::Point>& box2)
{
	return (cv::contourArea(box1) > cv::contourArea(box2));
}
void common_func::getFiles(std::string path, std::vector<std::string>& files)
{
	//�ļ����
	intptr_t   hFile = 0;
	//�ļ���Ϣ
	struct _finddata_t fileinfo;
	std::string p;
	if ((hFile = _findfirst(p.assign(path).append("\\*").c_str(), &fileinfo)) != -1)
	{
		do
		{
			//�����Ŀ¼,����֮
			//�������,�����б�
			if ((fileinfo.attrib & _A_SUBDIR))
			{
				if (strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0)
					getFiles(p.assign("").append(fileinfo.name), files);
			}
			else
			{
				files.push_back(p.assign("").append(fileinfo.name));
			}
		} while (_findnext(hFile, &fileinfo) == 0);
		_findclose(hFile);
	}
}
bool common_func::prehandel_inputimg(cv::Mat& input, cv::Mat& output,int size)
{
	if (input.cols < size&& input.rows < size)
	{
		output=input;
		return false;
	}
	else if (input.cols < size)
	{
		float f = float(size) / float(input.cols);
		cv::resize(input, output,cv::Size(),f,f);
		return true;
	}
	else if (input.rows < size)
	{
		float f = float(size) / float(input.rows);
		cv::resize(input, output, cv::Size(), f, f);
		return true;
	}
	else
	{
		output = input;
		return true;
	}
}
std::vector<pt> common_func::sliding_window(cv::Size inputimg_size, int stepSize, cv::Size windowSize)
{
	std::vector<pt> out;
	if (inputimg_size.width < windowSize.width && inputimg_size.height < windowSize.height)
	{
		std::cout << "ͼƬ��С�������и" << std::endl;
		return out;
	}
	else
	{
		for (int y = 0; y < inputimg_size.height; y = y + stepSize)
		{
			int x = 0;
			if (inputimg_size.height - y <= windowSize.height)
			{
				y = std::max(inputimg_size.height - windowSize.height, 0);
			}
			for (x = 0; x < inputimg_size.width; x = x + stepSize)
			{
				if (inputimg_size.width - x <= windowSize.width)
				{
					x = std::max(inputimg_size.width - windowSize.width, 0);
					pt ptt;
					ptt.x = x;
					ptt.y = y;
					out.push_back(ptt);
					break;
				}
				else
				{
					pt ptt;
					ptt.x = x;
					ptt.y = y;
					out.push_back(ptt);
				}


			}
			if (y == std::max(inputimg_size.height - windowSize.height, 0))
			{
				break;
			}
		}
		return out;
	}

}
std::vector < box_info> common_func::trans(std::vector < box_info_str> input)
{
	std::vector < box_info> out;
	for (auto s : input)
	{
		box_info ss;
		ss.x = s.box.x;
		ss.y = s.box.y;
		ss.w = s.box.width;
		ss.h = s.box.height;
		memset(ss.name, '\0', 50 * sizeof(char));
		strcpy_s(ss.name, strlen(s.name.c_str()) + 1, s.name.c_str());
		ss.state = s.state;
		out.push_back(ss);
	}
	return out;
}
//void common_func::delet_repeat(std::vector<box_info>& input, std::vector<box_info>& output)
//{
//	std::map<std::string, std::vector<box_info>> inputmap;
//	for (auto s : input)
//	{
//		if (inputmap.find(s.name) == inputmap.end())
//		{
//
//			inputmap[s.name] = std::vector<box_info>{ s };
//		}
//		else
//		{
//			inputmap[s.name].push_back(s);
//		}
//	}
//
//	for (auto s : inputmap)
//	{
//		std::vector<int> throw_box;
//		for (int i = 0; i < s.second.size(); i++)
//		{
//			for (int j = 0; j < s.second.size(); j++)
//			{
//				if (j == i)
//				{
//					continue;
//				}
//				else {
//					if (IOU_4(s.second[i], s.second[j]) > 0.5)
//					{
//						throw_box.push_back(j);
//					}
//				}
//			}
//		}
//		std::vector<box_info> only_e;
//		for (int i = 0; i < s.second.size(); i++)
//		{
//			int t = 0;
//			for (auto s : throw_box)
//			{
//				if (s == i)
//				{
//					t = 1;
//					break;
//				}
//			}
//			if (t == 0)
//			{
//				output.push_back(s.second[i]);
//			}
//		}
//	}
//}
std::vector<std::string> common_func::SplitString(const std::string& s, const std::string& c)
{
	std::vector<std::string> v;
	std::string::size_type pos1, pos2;
	pos2 = s.find(c);
	pos1 = 0;
	while (std::string::npos != pos2)
	{
		v.push_back(s.substr(pos1, pos2 - pos1));

		pos1 = pos2 + c.size();
		pos2 = s.find(c, pos1);
	}
	if (pos1 != s.length())
		v.push_back(s.substr(pos1));
	return v;
}
std::vector<inf_res> common_func::per_img_nms(std::vector<inf_res>& infer_out,int model=1)
{
	//int model = 1;//modelΪ0��������nms���򲻷���
	std::map<std::string, std::vector<inf_res>> all_map;
	if (model==0)
	{
		for (auto ss : infer_out)
		{
			if (all_map.find(ss.box_name) == all_map.end())
			{
				std::vector<inf_res> valu = { ss };
				all_map[ss.box_name] = valu;
			}
			else
			{
				all_map[ss.box_name].push_back(ss);
			}
		}
		std::vector<inf_res> f_res;
		for (auto cls : all_map)
		{
			std::vector<inf_res> class_s = nms_fuc(cls.second, 0.5);
			f_res.insert(f_res.end(), class_s.begin(), class_s.end());
		}
		return f_res;
	}
	else
	{
		std::sort(infer_out.begin(), infer_out.end(), sort_boxy); 
		std::vector<inf_res> class_s = nms_fuc(infer_out, 0.3);
		return class_s;

	}

}
//�ú����Ǽ���IOU�Ķ�Ϊfalse�����Iou
double common_func::IoU_compute(cv::Rect A, cv::Rect B, bool GIoU , bool DIoU , bool CIoU , bool EIoU, double eps) {

	double iou = 0;
	//�����ԭ������㽻�����������BOX�ҽǵ���С�ĵ�-����BOX��ǵ����ĵ� ���ǽ����Ŀ�W��ͬ��HҲ��ô��
	double inter_width_line = std::min(A.x+ A.width, B.x + B.width) - std::max(A.x, B.x);
	inter_width_line = std::max(inter_width_line, 0.000);
	double inter_high_line = std::min(A.y + A.height, B.y + B.height) - std::max(A.y, B.y);
	inter_high_line = std::max(inter_high_line, 0.000);
	//����ԭʼ���ж�����BOX�Ƿ��н�������Ϊ��һ������Ľ������ο�͸����С��0��֤������BOX���κν�����eps�Ǹ���ĸ��һ����С����
	//������ʵû��Ҫ����Ϊ��������0�����ұ�GIOU�㷨��0Ҳ��Ҫ����LOSS
	//if(inter_width_line or inter_high_line){
		//�õ�BOX֮�佻���Ͳ���
	double inter_area = inter_width_line * inter_high_line;
	double union_area = (A.width) * (A.height) + (B.width) * (B.height) - inter_area + eps;
	iou = inter_area / union_area;
	//std::cout<<"iou:"<<iou<<std::endl;
	//}
	//������IOU�����������IOU�㷨
	if (GIoU || DIoU || CIoU || EIoU) 
	{
		//������С��Ӿ��εĿ�͸ߣ���������IOU����Ҫ����ģ���ʵ��������������ϵGIOU->DIOU->CIoU
		//GIoU����һ�����ԣ� 1.��IoUһ�������зǸ��ԡ��߶Ȳ����Ե����� 2.����B��G�����ڣ�GIoU<=IoU 3.-1< GIoU <=1, ��IoU����1ʱ��GIoUҲ����1 �ɴ˿ɼ���ֻ�е�B��G�غ�ʱ��GIoU Loss�Ż�Ϊ0�����IoU Loss��GIoU Loss����������¶����Խ���ѵ��
		double smallest_w = std::max(A.x + A.width, B.x + B.width) - std::min(A.x, B.x);
		double smallest_h = std::max(A.y + A.height, B.y + B.height) - std::min(A.y, B.y);
		if (CIoU || DIoU || EIoU) {
			//���ݹ�ʽ����������BOX��С��Ӿ��εĶԽ��߳���
			double c2 = pow(smallest_w, 2) + pow(smallest_h, 2) + eps;  //�Խ��߳���ƽ��ΪC2
			//�������ĵ����,���ĵ����꣺(x0+x1)/2 ����y0+y1)/2,��ŷ�Ͼ���,�õ�ƽ��ֵdis_center
			double dis_center = (pow(2*B.x + B.width - 2*A.x -A.width, 2) + pow(2*B.y + B.height-2*A.y - A.height, 2)) / 4;
			if (DIoU)
				//���GIoU��DIoU���ƵĲ�����С��Ӿ���B��G��������Ĳ�ֵ������ֱ����������С��Ӿص������B��G���ĵ��λ�ã����ʹ��������������ƶ�bounding box��λ��������Loss��ͬʱҲ������IoUԪ����ʹbounding box��ground truth�ĸ���������ӽӽ�
				return iou - dis_center / c2;
			else if (CIoU) {
				//DIoUֻ�����˸�����������ĵ���룬����CIOU����DIoU�Ļ����ϼ����˳���ȵ�����
				//1.���չ�ʽ����Ҫ��������BOX�ĳ����v
				double v = (4 / pow(pi, 2)) * pow(atan((B.width) / (B.height)) - atan((A.width) / (A.height)), 2);
				//2.����Ȩ��alpha loss�׶�
				double alpha = v / (1 + eps - iou + v);
				return iou - (dis_center / c2 + v * alpha);
			}
			else {//EIoU ,��DIOU������-��͸ߵ���ʧ ȥ���IOU������Աȣ�
				double w_dis = pow(A.width - B.width, 2);
				//std::cout<<"w dis:"<<w_dis<<std::endl;
				double h_dis = pow(A.height - B.height, 2);
				double cw2 = pow(smallest_w, 2) + eps;
				double ch2 = pow(smallest_h, 2) + eps;
				//std::cout<<"���;"<<w_dis/cw2<<std::endl;
				//std::cout<<"�߱�:"<<h_dis/ch2<<std::endl;
				//std::cout<<"���ĵ����ȣ�"<<dis_center/c2<<std::endl;
				return iou - (dis_center / c2 + w_dis / cw2 + h_dis / ch2);
			}
		}
		else {
			//GIOU:���IoU Loss�е�B��G���ཻʱ��LossΪ0�����⣬��֤��û���ཻʱҲ������ʧ����ֵ���ܹ����з��򴫲���
			//��������С�ڽӾ������convex area
			double convex_area = smallest_w * smallest_h + eps;
			return iou - (convex_area - union_area) / convex_area;
		}
	}
	else
		return iou;
}
float common_func::iou(cv::Rect& A, cv::Rect& B)
{
	cv::Rect C = A & B;
	float area_con = A.area() + B.area()-C.area();
	float res = C.area() / area_con;
	float area = A.area() > B.area() ? B.area(): A.area();
	if (area >0.5)
	{
		res = C.area() / area;
	}
	return res;
}
std::vector<inf_res> common_func::nms_fuc(std::vector<inf_res> vec,float NMS_THRESH)
{
	//cv::Mat in_put;
	//input.copyTo(in_put);
	std::vector<inf_res> result;
	std::sort(vec.begin(), vec.end(), sort_boxy);
	//��־��false�������£�true�����ӵ�
	std::vector<bool> del(vec.size(), false);
	if (vec.size()==0)
	{
		return result;
	}
	else if (vec.size() == 1)
	{
		result.push_back(vec[0]);
		return result;
	}
	for (int i = 0; i < vec.size() - 1; i++)
	{
		if (!del[i])
		{
			for (int j = i; j < vec.size(); j++)
			{
				if (i == j)
				{
					continue;
				}
				else if(vec[i].box.y> vec[j].box.y+50)
				{
					break;
				}
				//float test = IOU(vec[j], vec[i]);
				if (!del[j] && (iou(vec[j].box, vec[i].box) >= NMS_THRESH))
				{
					if (vec[i].conf < vec[j].conf)
					{
						if (vec[i].box.y < vec[j].box.y)
						{
							vec[j].box.y = vec[i].box.y;
						}
						if (vec[i].box.x < vec[j].box.x)
						{
							vec[j].box.x = vec[i].box.x;
						}
						if (vec[i].box.height > vec[j].box.height)
						{
							vec[j].box.height = vec[i].box.height;
						}
						if (vec[i].box.width > vec[j].box.width)
						{
							vec[j].box.width = vec[i].box.width;
						}
						del[i] = true; //IOU������ֵ�ӵ�
					}
					else
					{
						if (vec[i].box.height < vec[j].box.height)
						{
							vec[i].box.height = vec[j].box.height;
						}
						if (vec[i].box.width < vec[j].box.width)
						{
							vec[i].box.width = vec[j].box.width;
						}
						if (vec[i].box.y > vec[j].box.y)
						{
							vec[i].box.y = vec[j].box.y;
						}
						if (vec[i].box.x > vec[j].box.x)
						{
							vec[i].box.x = vec[j].box.x;
						}
						del[j] = true; //IOU������ֵ�ӵ�
					}

				}
			}
		}
	}
	
	for (int i = 0; i < vec.size(); i++)
	{
		if (!del[i])
		{
			result.push_back(vec[i]);
		}
	}
	return result;
}
double common_func::trans(double min, double handel, double pixelVal)
{
	return 255.0 * tanh((pixelVal - min) / handel * 3);
}
void common_func::whitening(cv::Mat image) {
	//�����ֵ����
	double mean, stddev;
	cv::Mat temp_m, temp_sd;
	meanStdDev(image, temp_m, temp_sd);
	mean = temp_m.at<double>(0, 0) / 255.0;
	stddev = temp_sd.at<double>(0, 0) / 255.0;

	cv::Mat temp_image(image.rows, image.cols, CV_64F);
	for (int i = 0; i < image.rows; i++)
	{
		for (int j = 0; j < image.cols; j++)
		{
			double pixelVal = image.at<uchar>(i, j) / 255.0;
			double temp = (pixelVal - mean) / stddev;
			temp_image.at<double>(i, j) = temp;
		}
	}
	double max, min;
	minMaxLoc(temp_image, &min, &max);
	for (int i = 0; i < image.rows; i++)
	{
		for (int j = 0; j < image.cols; j++)
		{
			double pixelVal = temp_image.at<double>(i, j);
			double handel = max - min;
			image.at<uchar>(i, j) = (uchar)round(trans(min, handel, pixelVal));
		}
	}

}
float common_func::get_angel_2p(cv::Point  pointO,cv::Point pointA,bool rad=false)
{
		cv::Point point;
		double temp;

		point = cv::Point((pointA.x - pointO.x), (pointA.y - pointO.y));

		if ((0 == point.x) && (0 == point.y))
		{
			return 0;
		}

		if (0 == point.x)
		{
			if (point.y > 0)
			{
				temp = 90;
			}
			if (point.y < 0)
			{
				temp = 270;
			}
			return temp;
		}

		if (0 == point.y)
		{
			if (point.x > 0)
			{
				temp = 0;
			}
			if (point.x < 0)
			{
				temp = 180;
			}
			return temp;
		}
		temp = atan2(float(point.y) , float(point.x));//���㻡��,�Ƕȷ�ΧΪ{-180,180}
		temp = temp * 180 / CV_PI;
		if (temp<0)
		{
			temp = 360 + temp;
		}

		return temp;

}
double common_func::pt2line_distance(cv::Point P, cv::Point A, cv::Point B)
{

	//����r |AB| |AP| |BP| |PC|

	double ab = sqrt(pow((B.x - A.x), 2) + pow((B.y - A.y), 2)); // |AB|
	double ap = sqrt(pow((P.x - A.x), 2) + pow((P.y - A.y), 2)); // |AP|
	double bp = sqrt(pow((P.x - B.x), 2) + pow((P.y - B.y), 2)); // |BP|
	double r = 0;
	if (ab > 0)
	{
		r = ((P.x - A.x) * (B.x - A.x) + (P.y - A.y) * (B.y - A.y)) / pow(ab, 2);
	} //r 
	else
	{
		std::cout << "no lines" << std::endl;
	}

	//double distance = 0;
	double distance = 0;
	if (ab > 0)
	{
		if (r >= 1)
			distance = bp;
		else if (r > 0 && r < 1)
			distance = sqrt(pow(ap, 2) - r * r * pow(ab, 2));
		else
			distance = ap;
	}
	return distance;


}
std::string common_func::rand_str(const int len = 20)
{
	std::string str;
	char c;
	int idx;
	for (idx = 0; idx < len; idx++)
	{
		c = 'a' + rand() % 26;
		str.push_back(c);
	}
	return str;
}
std::string common_func::save_img(cv::Mat& img,std::string path)
{
	if (0 != _access(path.c_str(), 0))
	{
		// if this folder not exist, create a new one.
		_mkdir(path.c_str());   // ���� 0 ��ʾ�����ɹ���-1 ��ʾʧ��
		//���� ::_mkdir  ::_access Ҳ�У���֪��ʲô��˼
	}
	std::string name = rand_str(15);
	cv::imwrite(path+"/"+ name +".jpg", img);
	return name;
}
void common_func::draw_rotate_rect(cv::Mat& image,cv::RotatedRect rotate_rect)
{
	cv::Point2f* vertices = new cv::Point2f[4];
	rotate_rect.points(vertices);

	//�����߻���
	for (int j = 0; j < 4; j++)
	{
		cv::line(image, vertices[j], vertices[(j + 1) % 4], cv::Scalar(255,0,  0));
	}
	delete vertices;
}
cv::Rect common_func::expend_box(cv::Rect basic_box, int img_cols, int img_rows, int expend_x, int expend_y)
{
	cv::Rect expend_box;
	if (basic_box.x> img_cols)
	{
		expend_box.x = img_cols - 1;
	}
	else
	{
		expend_box.x = basic_box.x;
	}
	if (basic_box.y > img_rows)
	{
		expend_box.y = img_rows - 1;
	}
	else
	{
		expend_box.y = basic_box.y;
	}
	if (basic_box.width + expend_x <= img_cols)
	{
		expend_box.width = basic_box.width + expend_x;
		if (basic_box.x + basic_box.width + expend_x > img_cols)
		{
			basic_box.x = img_cols - expend_box.width;
		}
		else
		{
			basic_box.x = std::max(0, basic_box.x- expend_x/2);
		}
	}
	else
	{
		expend_box.x = 0;
		expend_box.width = img_cols;
	}
	if (basic_box.height + expend_y <= img_rows)
	{
		expend_box.height = basic_box.height + expend_y;
		if (basic_box.y + basic_box.height + expend_x > img_rows)
		{
			basic_box.y = img_rows - expend_box.height;
		}
		else
		{
			basic_box.y = std::max(0, basic_box.y - expend_y / 2);
		}
	}
	else
	{
		expend_box.y = 0;
		expend_box.height = img_rows;
	}
	return expend_box;
}
cv::Point common_func::point_trans(cv::Mat trans_mat, cv::Point inputpoint)
{
	cv::Mat Pointmat = cv::Mat::ones(cv::Size(1,3), CV_64FC1);
	Pointmat.at<double>(0, 0) = inputpoint.x;
	Pointmat.at<double>(1, 0) = inputpoint.y;
	Pointmat.at<double>(2, 0) = 1;
	cv::Mat res = trans_mat * Pointmat;
	//double x = (trans_mat.at<double>(0, 0) * inputpoint.x + trans_mat.at<double>(0,1) * inputpoint.y + trans_mat.at<double>(0, 2)) / (trans_mat.at<double>(2, 0) * inputpoint.x + trans_mat.at<double>(2, 1) * inputpoint.y + trans_mat.at<double>(2, 2));
	//double y = (trans_mat.at<double>(1, 0) * inputpoint.x + trans_mat.at<double>(1, 1) * inputpoint.y + trans_mat.at<double>(1, 2)) / (trans_mat.at<double>(2, 0) * inputpoint.x + trans_mat.at<double>(2, 1) * inputpoint.y + trans_mat.at<double>(2, 2));
	return cv::Point(int(res.at<double>(0, 0)), int(res.at<double>(1, 0)));
}
cv::Rect common_func::rect_trans(cv::Mat trans_mat, cv::Rect inputrect)
{
	cv::Point p1 = inputrect.tl();
	cv::Point p2 = p1;
	p2.x += inputrect.width;
	cv::Point p3 = p1;
	p2.y+= inputrect.height;
	cv::Point p4 = inputrect.br();
	p1 = point_trans(trans_mat, p1);
	p2 = point_trans(trans_mat, p2);
	p3 = point_trans(trans_mat, p3);
	p4 = point_trans(trans_mat, p4);
	return cv::boundingRect(std::vector<cv::Point>{p1, p2, p3, p4});

}