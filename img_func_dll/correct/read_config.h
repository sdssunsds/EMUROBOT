#ifndef READ_CONFIG_H_
#define READ_CONFIG_H_

#include "utils.h"
#include <iostream>


struct SuperPointConfig {
    int max_keypoints = { 500 };
  double keypoint_threshold{ 0.004 };
  int remove_borders=4;
  int dla_core{-1};
  std::vector<std::string> input_tensor_names{ "input" };
  std::vector<std::string> output_tensor_names{ "scores","descriptors" };
  std::string onnx_file="superpoint_v1_sim_int32.onnx";
  std::string engine_file= "superpoint_v1_sim_int32.engine";
};

struct SuperGlueConfig {
  int image_width{320};
  int image_height{240};
  int dla_core{-1};
  std::vector<std::string> input_tensor_names{ "keypoints_0"
      , "scores_0"
      , "descriptors_0"
      , "keypoints_1"
      , "scores_1"
      , "descriptors_1" };
  std::vector<std::string> output_tensor_names{ "scores" };
  std::string onnx_file{ "superglue_outdoor_sim_int32.onnx" };
  std::string engine_file{"superglue_outdoor_sim_int32.engine"};
};

#endif  // READ_CONFIG_H_
