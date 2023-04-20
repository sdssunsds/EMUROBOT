#pragma once
#include"basic_struct.h"
//线阵相机检测螺丝模型1
const trt_basic_config screw_config = { "v7","w6",0.3, 0.5 ,320,320,"yolov7-w6-4-320_ep1",{"LS_SZ", "LS_6J" , "LM_6J" , "LS_N6"} };
// const trt_basic_config screw_config = {"v7","",0.3, 0.5 ,320,320,"screw_yolov7_w6_4_320",{"LS_6J", "LS_SZ" , "LM_6J" , "LS_N6"}};
//螺丝丢失二次检测模型
const trt_basic_config screw_loss = { "v5","x",0.1,0.5 ,320,320,"loss_screw_yolov5x_320_2_dongjie",{"001","002"} };
//面阵相机螺丝检测
const trt_basic_config screw_mz_config = { "v5","m",0.3,0.5 ,1024,1024,"LS_yolov560_m_1024_3_1204",{ "LS_6J","LS_N6", "LS_4J" } };

//面阵相机检测底板螺丝模型（用于绿色标记检测螺丝松动）
const trt_basic_config screw_config_db = { "v5","m",0.5 ,0.1,1280,1280,"screw_yolov5m_3_1280",{"KLS","GREEN","LS"}};

//线阵相机检测异物模型,线阵1为面阵
const trt_basic_config foreign_body_config = { "v7","w6",0.3 ,0.45,1024,1024,"x_CLW_yolov7_w6_1_1024",{"foreign_body"} };
const trt_basic_config foreign_body_config1 = { "v7","w6", 0.3 ,0.45,1280,1280,"MZ_CLW_yolov7_w6_1_1280",{"foreign_body"} };
//铁丝断裂检测
const trt_basic_config locking_wire_config = { "v7","w6",0.3 ,0.42,1280,1280,"TS_detect_6_1280",{"LS_4J", "LS_D6J", "LS_6J", "TS_4J", "TS_ZYK",  "TS_FS"} }; //
//油位表检测
const trt_basic_config oil_config = { "v5","m",0.2,0.5 ,1024,1024,"oil_1024_m_5",{"oil_guage","scale","yellow","liquid_level"} };
//撒沙咀检测
const trt_basic_config sashazui_config = { "v5","n",0.5,0.5 ,800,800,"nozzle_yolov5n_800",{"screw","rect","up","down"} };
//易丢失部件检测
const trt_basic_config losscheck_config = { "v7","",0.2 ,0.5,1024,1024,"yolov7-w6_17_1024",{ "Zpt","Sh" ,"Lock_6l_s","Lock_6l","X_p","Kkx","Zp","Djmp","Hk","X_kk","Fhf","Zykd","Pqk","Pipe","Gjt","Mfj","Pzq"} };
//漏油以及划痕检测
const trt_basic_config oil_leakage_config = { "v5","m", 0.3 ,0.6,640,640,"Drop_SJ_HJ_yolov560_m_640_3_1209",{"yd","yj","HJ"} };//油滴、划痕、油迹
//车头检测
const trt_basic_config ct_config = { "v5","m", 0.25 ,0.45,1024,1024,"AK_CLW_yolov560_m_1024_2_1208",{"AK","CLW"} };
//转向架部件检测
const trt_basic_config zxj_part_config = { "v5","m", 0.2,0.4 ,1024,1024,"ZXJ_yolov560_m_1024_11_1206",
	{"clx", "GJT", "LS_6J", "LZJ","LZJ-LS", "MFJ", "Overall", "TS_FS","axle", "oil_damper", "pipe"} };
	//油压减震，齿轮箱，包含六角螺丝防松铁丝的整体,六角螺丝,防松铁丝，轴，密封胶，管接头，管
std::map<std::string, trt_basic_config> init_list_info = { { "screw",screw_config },{"screw_loss",screw_loss},{"screw_db",screw_config_db},{"screw_mz",screw_mz_config},{ "loss_check",losscheck_config } ,{"ct",ct_config},
	{ "locking_wire", locking_wire_config },{ "oil_level",oil_config } ,{ "foreign_body",foreign_body_config },{ "foreign_body1",foreign_body_config1 },{"sashazui",sashazui_config},{"oil_leakage",oil_leakage_config},{"zxj",zxj_part_config}};

//分类模型
const infer_class_config locking_wire_class = { 2,1,3,256,256,{ 0.485, 0.456,0.406 },{  0.229, 0.224,0.225 },"TS_class_xception_2_256",{"TS_DL","TS_FS"}};
const infer_class_config dbbolts_class      = { 3,1,3,256,256,{ 0.485, 0.456,0.406 },{  0.229, 0.224,0.225 },"screw_xception_3_256",{ "green_ls", "kls", "loose_ls"}};
std::map<std::string, infer_class_config> init_class_info = { {"locking_wire_class",locking_wire_class},{"bdscrew",dbbolts_class}};
map<std::string, int> label_map6 = { {"LS_SZ",6}, {"LS_6J",6}, {"LS_LS",6},{"LS_N6",6}, {"LS_4J",6},
	{"Gjt",6}, {"Hk",6}, {"Lock_6l",6}, {"Lock_6l_s",6}, { "Mfj",6}, {"Pqk",6}, {"Pzq",6}, {"Sh",6}, {"X_kk",6}, { "X_p",6}, {"Zpt",6}, {"Zykd",6}, {"Pipe",6}};
map<std::string, int> label_map0 = { {"LS_SZ",0}, {"LS_6J",0}, {"LS_LS",0}, {"MD",0}, {"LS_N6",0}, {"LS_4J",0}};