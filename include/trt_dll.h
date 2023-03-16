#pragma once

#ifndef V7_TRT_H
#define V7_TRT_H
#include <string>
extern "C" __declspec(dllexport) int __stdcall onnx_trans(std::string onnxpath, std::string savepath, std::string other);
#endif