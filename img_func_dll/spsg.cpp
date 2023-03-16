#include "spsg.h"
void spsg::init(std::string basic_path,std::string model_path)
{
    SuperPointConfig sp_config;
    sp_config.onnx_file = basic_path + "/" + sp_config.onnx_file;
    sp_config.engine_file = model_path + "/" + sp_config.engine_file;
    SuperGlueConfig sg_config;
    sg_config.onnx_file = basic_path + "/" + sg_config.onnx_file;
    sg_config.engine_file = model_path + "/" + sg_config.engine_file;
    width = sg_config.image_width;
    height = sg_config.image_height;
    radio_w = input_width / width;
    radio_h = input_height / height;
    superpoint = std::make_shared<SuperPoint>(sp_config);
    if (!superpoint->build()) 
    {
        std::cerr << "Error in SuperPoint building engine. Please check your onnx model path." << std::endl;
        return;
    }
    superglue = std::make_shared<SuperGlue>(sg_config);
    if (!superglue->build())
    {
        std::cerr << "Error in SuperGlue building engine. Please check your onnx model path." << std::endl;
        return;
    }
}

cv::Mat spsg::change(cv::Mat image0, cv::Mat image1,cv::Mat& invert)
{
    cv::resize(image0, image0, cv::Size(width, height));
    cv::resize(image1, image1, cv::Size(width, height));
    Eigen::Matrix<double, 259, Eigen::Dynamic> feature_points0, feature_points1;
    std::vector<cv::DMatch> superglue_matches,maches;
    if(!superpoint->infer(image0, feature_points0)){
        std::cerr << "Failed when extracting features from first image." << std::endl;
        return cv::Mat();
    }
    if(!superpoint->infer(image1, feature_points1)){
        std::cerr << "Failed when extracting features from second image." << std::endl;
        return cv::Mat();
    }
    superglue->matching_points(feature_points0, feature_points1, superglue_matches);
    int num0=feature_points0.cols();
    int num1 = feature_points1.cols();
  cv::Mat match_image;
  std::vector<cv::KeyPoint> keypoints0, keypoints1;
  std::vector<cv::Point> points0, points1;
  for (int i=0;i<superglue_matches.size();i++)
  {
    cv::DMatch match= superglue_matches[i];

    double score = feature_points0(0, match.queryIdx);
    double x0 = feature_points0(1, match.queryIdx)* radio_w;
    double y0 = feature_points0(2, match.queryIdx) * radio_h;
    points0.emplace_back(x0, y0);
    keypoints0.emplace_back(x0, y0, 8, -1, score);
    double score1 = feature_points1(0, match.trainIdx);
    double x1 = feature_points1(1, match.trainIdx) * radio_w;
    double y1 = feature_points1(2, match.trainIdx) * radio_h;
    points1.emplace_back(x1, y1);
    keypoints1.emplace_back(x1, y1, 8, -1, score1);
    //cv::line(show, cv::Point(int(x0), int(y0)), cv::Point(int(x1) + image0.cols, int(y1)), cv::Scalar(0, 255, 0));
  }
  cv::Mat image0_to_image1;
  if (points0.size()>=50)
  {
      image0_to_image1 = cv::findHomography(points0, points1, CV_FM_RANSAC);
      invert= cv::findHomography( points1,points0, CV_FM_RANSAC);
  }
  
  return image0_to_image1;
}
