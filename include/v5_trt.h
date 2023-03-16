#pragma once
#ifndef V5_TRT_H
#define V5_TRT_H
#include <string>
extern "C" __declspec(dllexport) int __stdcall v5_trans(std::string Model_path, std::string Save_path, std::string Model_type, int inputs, int classnum);
#endif