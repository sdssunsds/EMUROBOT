#include "model_config.h"
const v6_basic_config screw_config1 = { 0.1,0.5 ,320,320,"../model/yolov5m_screw.engine",{"001","002","003","004"} };
const v6_basic_config screw_config = { 0.1,0.5 ,320,320,"../model/scraw_yolov5m_320_cls_1.engine",{"001"} };
const v6_basic_config screw_config_db = { 0.1,0.6 ,640,640,"../model/db_screw_640_l.engine",{"001","002"} };
const v6_basic_config foreign_body_config = { 0.45,0.5 ,640,640,"../model/forign_640_l.engine",{"foreign_body"} };
const v6_basic_config locking_wire_config = { 0.45,0.25 ,640,640,"../model/yolov5s_locking_wire.engine",{"locking_wire","locking_wire_break"} };
const v6_basic_config scratch_config = { 0.45,0.25 ,608,608,"../model/yolov5_scratch.engine",{"scratch"} };
const v6_basic_config oil_config = { 0.2,0.5 ,1024,1024,"../model/oil_1024_m_5.engine",{"oil_guage","scale","yellow","liquid_level"} };
const v6_basic_config screw_mz_config = { 0.5,0.5 ,1024,1024,"../model/screw_mz_1024.engine",{"LS-6J", "LM-6J","LS-N6","MD","TS-FS"} };
const v6_basic_config sashazui_config = { 0.5,0.5 ,800,800,"../model/nozzle_yolov5n_800.engine",{"screw","rect","up","down"} };
const v6_basic_config losscheck_config = { 0.5,0.2 ,1024,1024,"../model/losscheck_1024_m.engine",{"X-KK", "MFJ", "PQK", "YW", "ZYKD", "HK"} };//¿ª¿ÚÏú£¬ÃÜ·â½º£¬ÅÅÆø¿×£¬ÓÍÎÛ£¬×¢ÓÍ¿×¶Â£¬»¬¿é 
extern std::map<std::string, v6_basic_config> init_list_info = { { "screw",screw_config },{"screw1",screw_config1},{"screw_db",screw_config_db},{"screw_mz",screw_mz_config},{"screw_mz",screw_mz_config},{ "loss_check",losscheck_config } ,
	{ "locking_wire",locking_wire_config },{ "oil_level",oil_config } ,{ "foreign_body",foreign_body_config },{ "scratch",scratch_config } ,{"sashazui",sashazui_config} };//
extern std::map<int, std::string> sf_class_map =
	{
		{0,"screw"},{1,"foreign_body"},{2,"locking_wire"},{3,"oil_level"},{4,"scratch"},{5,"sashazui"},{6,"loss_check"}
	};
