#pragma once
#include "basic_struct.h"
#include "common_func.h"
using namespace std;
//��������ʱͨ��ģ���Ŀ�궪ʧ���м��
class loss_check
{public:
    
    static std::vector<std::vector<box_info_str>> getloss_res(cv::Mat& inputmat, std::vector<box_info_str> model_input, std::vector<model_struct_box> compare_res);
private:
    static vector<vector<model_struct_box>> getloss(cv::Mat& inputmat, vector<model_struct_box> input1, vector<model_struct_box> input2,int state);
    //��һ��ƥ�䷽��
    static vector<vector<model_struct_box>> first_match(cv::Mat& inputmat,  vector<model_struct_box> luosimap1,
        vector<model_struct_box> luosimap2);
    static vector<vector<model_struct_box>> easy_match(cv::Mat& inputmat, vector<model_struct_box> model, vector<model_struct_box> comp);
    static vector<vector<model_struct_box>> second_match(cv::Mat& inputmat, vector<model_struct_box> model,
         vector<model_struct_box> comp);
};