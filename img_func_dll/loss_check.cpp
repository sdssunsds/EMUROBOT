#include "loss_check.h"

//高度匹配
struct h_pair
{
    int model;
    int comp;
};
struct mc_pair
{
    model_struct_box model;
    model_struct_box comp;
    int x;//x偏移量
    int y;//y偏移量
    int hitnum = 0;//击中数
    double dis=0;//距离
};
struct pair_info
{
    int fx;
    int dis;
};
cv::Rect getrect(model_struct_box input)
{
    return input.box;

}
//获取一定区域的rect
vector<model_struct_box> get_range_vec(std::vector<vector<model_struct_box>>& input, int h, int range)
{
    vector<model_struct_box> res;
    for (int hh=max(h-range,0);hh<min(int(input.size()),h+range+1);hh++)
    {
        if (input[hh].size()!=0)
        {
            res.insert(res.end(), input[hh].begin(), input[hh].end());
        }
    }
    return res;
}
//判断周围是否有目标
bool judg_have(cv::Point a,map<int,std::vector<model_struct_box>>& b)
{
    if (b.find(a.y / 100-1)!=b.end())
    {
        for (auto s : b[a.y / 100-1])
        {
            if (a.x - 50 <= s.box.x && a.x + 50 >= s.box.x && a.y - 100 <= s.box.y && a.y + 100 >= s.box.y)
            {
                return true;
            }
        }
    }
    if (b.find(a.y / 100) != b.end())
    {
        for (auto s : b[a.y / 100])
        {
            if (a.x - 50 <= s.box.x && a.x + 50 >= s.box.x && a.y - 100 <= s.box.y && a.y + 100 >= s.box.y)
            {
                return true;
            }
        }
    }
    if (b.find(a.y / 100+1) != b.end())
    {
        for (auto s : b[a.y / 100+1])
        {
            if (a.x - 50 <= s.box.x && a.x + 50 >= s.box.x && a.y - 100 <= s.box.y && a.y + 100 >= s.box.y)
            {
                return true;
            }
        }
    }
    return false;
}
bool judg_have(cv::Point a, std::vector<std::vector<model_struct_box>>& b)
{
    std::vector<model_struct_box> st=get_range_vec(b, floor(a.y/100.0), 1);
    for (auto s : st)
    {
        if (a.x - 50 <= s.box.x && a.x + 50 >= s.box.x && a.y - 50 <= s.box.y && a.y + 50 >= s.box.y)
        {
            return true;
        }
    }
   
    return false;
}
//计算在偏移击中数
int GetHitNum(int biasx, int biasy, map<int,std::vector<model_struct_box>> model_map, map<int, std::vector<model_struct_box>> comp_map,int h)
{
    int i = 0;
    cv::Point p;
    int total = 0;
    for (auto s: model_map)
    {
        if (s.first>=h-5&& s.first  <= h+ 5)
        {
            total += s.second.size();
            for (auto ss:s.second)
            {
                p.x = ss.box.x + biasx;
                p.y = ss.box.y + biasy;
                if (judg_have(p, comp_map))
                {
                    i = i + 1;
                }
            }
            
        }
        
    }
    if (total>0)
    {
        return (i*100)/total;
    }
    else
    {
        return 0;
    }
   
}
int GetHitNum_vec(int biasx, int biasy, std::vector<std::vector<model_struct_box>>& model_vec, std::vector<std::vector<model_struct_box>>& comp_vec, int h)
{
    cv::Point p{ 0,0 };
    int range = 5;
    std::vector<model_struct_box> model = get_range_vec(model_vec, h, range);
    std::vector<model_struct_box> comp = get_range_vec(model_vec, h + floor(biasy / 100.0), range + 2);
    int i = 0;
    int total = model.size();

    for(auto s:model)
    {
        p.x = s.box.x + biasx;
        p.y = s.box.y + biasy;
        if (judg_have(p, comp_vec))
        {
            i = i + 1;
        }

    }
    return i;
}
int get_dis(model_struct_box pt1, model_struct_box pt2, float bia)
{
    return abs(pt1.box.x - pt2.box.x) + abs(pt1.box.y + bia - pt2.box.y);
}
int get_dis(cv::Point pt1, model_struct_box pt2, float bia)
{
    return abs(pt1.x - pt2.box.x) + abs(pt1.y + bia - pt2.box.y);
}
bool mc_pair_p (mc_pair a, mc_pair b)
{
    return a.hitnum >b.hitnum;
}
bool mc_pair_d(mc_pair a, mc_pair b)
{
    return a.dis < b.dis;
}
bool mc_pair_d_un(mc_pair a, mc_pair b)
{
    return a.dis > b.dis;
}
bool cmp(model_struct_box a, model_struct_box b)
{
    return a.box.y < b.box.y;
}
//比较距离
bool cpd(model_struct_box a, model_struct_box b)
{
    if (a.box.y < b.box.y)
    {
        return true;
    }
    else if (a.box.y == b.box.y && a.box.x < b.box.x)
    {
        return true;
    }
    else
    {
        return false;
    }

}
bool cmp_h(h_pair a, h_pair b)
{
    std::cout << a.model << "," << a.comp << "," << b.model << "," << b.comp << std::endl;
    return a.model < b.model;
}
int minid(vector<int> input)
{
    int id = -1;
    int min = 99999;
    for (size_t i = 0; i < input.size(); i++)
    {
        if (input[i] < min)
        {
            id = i;
            min = input[i];
        }
    }
    return id;
}
vector<int> get_comp(vector < model_struct_box> vec_pt1, vector<model_struct_box> vec_pt2,int bia=0)
{
    vector<vector<int>> id_map;
    vector<int> comp_res;
    for (int i=0;i<vec_pt1.size();i++)
    {
        vector<int> dis_vec;
        for (int j = 0; j < vec_pt2.size(); j++)
        {
            //计算该点对应每个点的距离
            dis_vec.push_back(get_dis(vec_pt1[i], vec_pt2[j],bia));
        }
        //选出最小值并获取其id位置
        int min_id = minid(dis_vec);
        //得出合适的map
        id_map.push_back({ min_id,dis_vec[min_id] }) ;
    }
     
    for (int i=0;i<id_map.size();i++)
    {
        int val = 0;
        //遍历所有点来匹配，若匹配不上则定义为-1，若匹配上则返回对应id
        for (int j = 0; j < id_map.size(); j++)
        {
            if (i==j)
            {
                continue;
            }
            else
            {
                if (id_map[i][0]== id_map[j][0])
                {
                    if (id_map[i][1] >id_map[j][1])
                    {
                        val=-1;
                    }
                }
            }
        }
        if (val == -1)
        {
            comp_res.push_back(-1);
        }
        else
        {
            comp_res.push_back(id_map[i][0]);
        }
    }
    return comp_res;
}

int get_loss_y(vector<h_pair> h_map,model_struct_box model)
{
    //int y1, y2;
    if (h_map.size()<=0)
    {
        return model.box.y;
    }
    else
    {
        for (int i = 0; i < h_map.size()-1; i++)
        {
            if (model.box.y > h_map[i].model && model.box.y < h_map[i + 1].model)
            {
                //y1 = h_map[i].model;
                //y2 = h_map[i + 1].model;

                return (model.box.y - h_map[i].model) * (h_map[i + 1].comp - h_map[i].comp) / (h_map[i + 1].model - h_map[i].model) + h_map[i].comp;
            }
            else if (i == 0 && model.box.y > 0 && model.box.y < h_map[0].model)
            {
                return model.box.y + h_map[i].comp - h_map[i].model;
            }
            else if (model.box.y == h_map[i].model)
            {
                return h_map[i].comp;
            }
            else if (model.box.y == h_map[i + 1].model)
            {
                return h_map[i + 1].comp;
            }
            else if (i + 1 == h_map.size()&& model.box.y> h_map[i + 1].model)
            {
                return model.box.y + h_map[i + 1].comp - h_map[i + 1].model;
            }
        }
        return model.box.y;
    }
}
bool sort_boxx(model_struct_box& box1, model_struct_box& box2)
{
    return (box1.box.x < box2.box.x);
}
struct hit_struct
{
    int id1 = 0;
    int id2 = 0;
    model_struct_box hit_res;
};
//map1是模板，map2是监测
vector<vector<model_struct_box>> loss_check::easy_match(cv::Mat& inputmat, vector<model_struct_box> model, vector<model_struct_box> comp)
{
    int expand =16;
    cv::Mat copy_img;
    cv::Mat copy_img_basic;
    inputmat.copyTo(copy_img_basic);
    std::vector<vector<model_struct_box>> model_vec, comp_vec;
    std::vector<vector<bool>> ease_map;
    vector<model_struct_box> marrymap, lossmap;
    cv::Point bias_point{ 0,0 };
    int vec_len = floor(inputmat.rows / 100.0);
    for (int i = 0; i < vec_len; i++)
    {
        model_vec.push_back(vector<model_struct_box>{});
        comp_vec.push_back(vector<model_struct_box>{});
        ease_map.push_back(vector<bool>{});
    }
    //划分纵向格子
    for (auto s : model)
    {
        int i = floor(s.box.y / 100.0);
        if (i < vec_len)
        {
            model_vec[i].push_back(s);
        }
    }
    for (int i=0;i<model_vec.size();i++)
    {
        vector<model_struct_box> s = model_vec[i];
        std::sort(s.begin(), s.end(), sort_boxx);
        model_vec[i] = s;
    }
    for (auto s : comp)
    {
        int i = floor(s.box.y / 100.0);

        if (i < vec_len)
        {
            comp_vec[i].push_back(s);
            ease_map[i].push_back(false);

        }
    }
    for (int i = 0; i < model_vec.size(); i++)
    {
        vector<model_struct_box> s = comp_vec[i];
        std::sort(s.begin(), s.end(), sort_boxx);
        comp_vec[i] = s;
    }
    std::vector<int> comped{ 0 ,0};//model,comp
    int bias_y=0;//,y
    int bias_x = 0;
    //按格子计算位置。
    for (int i=0;i< model_vec.size();i++)
    {
        if (model_vec[i].size()==0)
        {
            continue;
        }
        else
        {
            vector<model_struct_box> s_model = model_vec[i];
            std::vector<int> bais_y_layer;
            std::vector<int> bais_x_layer;
            int comped_y = 0;
            int e = i + 2 > int(comp_vec.size()) ? int(comp_vec.size()) : i + 2;
            int bg = i - 1;
            for (int idm = 0; idm < s_model.size(); idm++)
            {
                cv::Rect box2 = cv::Rect(s_model[idm].box.x - bias_x - expand, s_model[idm].box.y - bias_y - expand, s_model[idm].box.width + 2 * expand, s_model[idm].box.height + 2 * expand);
                cv::rectangle(copy_img_basic, box2, cv::Scalar(255, 0, 0), 1);
                std::vector<hit_struct> hit_res;
                for (int k = bg; k < e; k++)
                {
                    vector<model_struct_box> s_comp = comp_vec[k];
                    if (s_comp.size() == 0)
                    {
                        continue;
                    }
                    for (int id = 0; id < s_comp.size(); id++)
                    {
                        if (ease_map[k][id])
                        {
                            continue;
                        }
                        cv::Rect box1 = cv::Rect(s_comp[id].box.x, s_comp[id].box.y, s_comp[id].box.width, s_comp[id].box.height);
                        if ((box1 & box2).area() > 0)
                        {
                            hit_res.emplace_back(hit_struct{k,id,s_comp[id]});                          
                        }
                    }
                }
                if (hit_res.empty())
                {
                    model_struct_box loss;
                    loss = s_model[idm];
                    loss.box = box2;
                    lossmap.push_back(loss);
                    cv::rectangle(copy_img_basic, box2, cv::Scalar(0, 128, 128), 1);
                }
                else if(hit_res.size()==1)
                {
                    comped_y++;
                    marrymap.emplace_back(hit_res[0].hit_res);
                    bais_y_layer.emplace_back(s_model[idm].box.y+ s_model[idm].box.height /2 - hit_res[0].hit_res.box.y-hit_res[0].hit_res.box.height/2);
                    bais_x_layer.emplace_back(s_model[idm].box.x + s_model[idm].box.width / 2 - hit_res[0].hit_res.box.x - hit_res[0].hit_res.box.width / 2);
                    ease_map[hit_res[0].id1][hit_res[0].id2] = true;
                    cv::rectangle(copy_img_basic, getrect(hit_res[0].hit_res), cv::Scalar(0, 0, 255), 1);
                }
                else
                {
                    if (hit_res.size()>1)
                    {
                        int hit_id = 0;
                        int dis = 0;
                        for (int gethit=0; gethit< hit_res.size(); gethit++)
                        {
                            cv::Rect box = hit_res[gethit].hit_res.box;
                            
                            int s_area =abs(box.x+box.width/2-box2.x- box2.width / 2)+abs(box.y + box.height / 2 - box2.y - box2.height / 2);
                            if (s_area> dis)
                            {
                                hit_id = gethit;
                                dis = s_area;
                            }
                        }
                        cv::rectangle(copy_img_basic, getrect(hit_res[hit_id].hit_res), cv::Scalar(0, 0, 255), 1);
                        comped_y++;
                        marrymap.emplace_back(hit_res[hit_id].hit_res);
                        bais_y_layer.emplace_back(s_model[idm].box.y - hit_res[hit_id].hit_res.box.y);
                        ease_map[hit_res[hit_id].id1][hit_res[hit_id].id2] = true;

                    }
                }
            }
            if (comped_y > 3)
            {
                int getbias_y = 0;
                int getbias_x = 0;
                for (auto s : bais_y_layer)
                {
                    getbias_y += s;

                }
                bias_y = getbias_y / int(bais_y_layer.size());
                for (auto s : bais_x_layer)
                {
                    getbias_x += s;

                }
                bias_x = getbias_x / int(bais_x_layer.size());
            } 
        }
    }
    for (int s = 0; s < ease_map.size(); s++)
    {
        for (int ss = 0; ss < ease_map[s].size(); ss++)
        {
            if (!ease_map[s][ss] && comp_vec[s][ss].conf > 0.7)
            {
                marrymap.push_back(comp_vec[s][ss]);
                cv::rectangle(copy_img_basic, getrect(comp_vec[s][ss]), cv::Scalar(0, 0, 255), 1);
            }
        }

    }
    return { marrymap ,lossmap };
}
vector<vector<model_struct_box>> loss_check::first_match(cv::Mat& inputmat, vector<model_struct_box> model,vector<model_struct_box> comp)
{
    std::cout << "model size:" << model.size() << std::endl;
    std::cout << "comp size:" << comp.size() << std::endl;
    cv::Mat copy_img;
    cv::Mat copy_img_basic;
    inputmat.copyTo(copy_img);
    inputmat.copyTo(copy_img_basic);
    vector<h_pair> hpair;
    vector<model_struct_box> marrymap;
    vector<model_struct_box> lossmap, lossres;
    std::vector< std::vector< mc_pair>> marrymap_v;
    std::vector< mc_pair> marrymap_enum0, marrymap_enum1;
    std::sort(model.begin(), model.end(), cpd);
    std::vector<vector<model_struct_box>> model_vec, comp_vec;
    std::vector<vector<int>> comp_re;
    int vec_len = floor(inputmat.rows / 100.0);
    for (int i = 0; i < vec_len; i++)
    {
        model_vec.push_back(vector<model_struct_box>{});
        comp_vec.push_back(vector<model_struct_box>{});
    }
    //划分纵向格子
    for (auto s : model)
    {
        int i = floor(s.box.y / 100.0);
        if (i < vec_len)
        {
            model_vec[i].push_back(s);
            cv::rectangle(copy_img, getrect(s), cv::Scalar(255, 255, 0), 1);
        }
    }
    for (auto s : comp)
    {
        int i = floor(s.box.y / 100.0);

        if (i < vec_len)
        {
            comp_vec[i].push_back(s);
            //comp_re[i].push_back(0);
            cv::rectangle(copy_img_basic, getrect(s), cv::Scalar(255, 0, 0), 1);
        }
    }

    int bias_h = 0;
    int first = 0;
    for (int i = 0; i < model_vec.size(); i++)
    {
        vector<model_struct_box> s1 = model_vec[i];
        if (s1.empty())
        {
            continue;
        }
        else
        {
            std::vector< mc_pair> comp_map;
            for (auto s : s1)
            {
                if (first==0)
                {
                    first += 1;
                }
                std::vector< mc_pair> biasmap;
                int begin = max(0, int(floor(s.box.y / 100.0)));
                vector<model_struct_box> st = get_range_vec(comp_vec, begin, 3);//寻找该高度的所有检测框
                for (auto com_s : st)
                {
                    if (s.box.x + 100 > com_s.box.x || s.box.x - 100 < com_s.box.x)
                    {
                        mc_pair mp;
                        mp.x = com_s.box.x - s.box.x;
                        mp.y = com_s.box.y - s.box.y;
                        mp.comp = com_s;
                        mp.model = s;
                        mp.hitnum =0;
                        mp.dis = common_func::IoU_compute(s.box, com_s.box,true,false, false, false, 1e-9);//get_dis(s, com_s, 0);
                        biasmap.push_back(mp);
                    }
                }

                std::sort(biasmap.begin(), biasmap.end(), mc_pair_d_un);
                comp_map.push_back(biasmap[0]);
            }
            //判断是否丢失，计算偏移
            std::sort(comp_map.begin(), comp_map.end(), mc_pair_d_un);
            for (int jj=0;jj<comp_map.size();jj++)
            {
                if (comp_map[jj].dis > 0)
                {
                    model_struct_box comp_map_res = comp_map[jj].comp;
                    comp_map_res.class_name=comp_map[jj].model.class_name;
                    marrymap.push_back(comp_map_res);
                }
                else
                {
                    lossmap.push_back(comp_map[jj].model);
                }
                
               
            }

        }
    }
    return { marrymap ,lossmap };
}
vector<vector<model_struct_box>> loss_check::second_match(cv::Mat& inputmat, vector<model_struct_box> model,
    vector<model_struct_box> comp)
{
    std::cout << "model size:" << model.size() << std::endl;
    std::cout << "comp size:" << comp.size() << std::endl;
    cv::Mat copy_img;
    cv::Mat copy_img_basic;
    cv::Mat copy_img_fix;
    inputmat.copyTo(copy_img);
    inputmat.copyTo(copy_img_basic);
    inputmat.copyTo(copy_img_fix);
    vector<h_pair> hpair;
    vector<model_struct_box> marrymap;
    vector<model_struct_box> lossmap, lossres;
    std::vector< std::vector< mc_pair>> marrymap_v;
    std::vector< mc_pair> marrymap_enum0, marrymap_enum1;
    std::sort(model.begin(), model.end(), cpd);
    std::vector<vector<model_struct_box>> model_vec, comp_vec;
    std::vector<vector<int>> comp_re;
    int vec_len = floor(inputmat.rows /100.0);
    for (int i=0;i< vec_len;i++)
    {
        model_vec.push_back(vector<model_struct_box>{});
        comp_vec.push_back(vector<model_struct_box>{});
        //comp_re.push_back(vector<int>{});
    }
    //int h_num = 0,h_id=0;
    for (auto s : model)
    {
        int i = floor(s.box.y / 100.0);
        if (i < vec_len)
        {
            model_vec[i].push_back(s);
            cv::rectangle(copy_img, getrect(s), cv::Scalar(255, 255, 0), 1);
        }     
    }
    for (auto s : comp)
    {
        int i = floor(s.box.y / 100.0);

        if (i < vec_len)
        {
            comp_vec[i].push_back(s);
            //comp_re[i].push_back(0);
            cv::rectangle(copy_img_basic, getrect(s), cv::Scalar(255, 0, 0), 1);
        }
    }
    //确认偏移量
    int find_num = min(int(model.size()/10),20);
    //进行初步的筛选，确定偏移位置
    for (int i=0;i< model_vec.size();i++)
    {
        vector<model_struct_box> s1 = model_vec[i];
        if (s1.empty())
        {
            continue;
        }
        else
        {
            for (auto s : s1)
            {  
                std::vector< mc_pair> biasmap;
                int begin = max(0, int(floor(s.box.y / 100.0)));
                vector<model_struct_box> st = get_range_vec(comp_vec, begin,6);
                for (auto com_s:st)
                {
                    if (s.box.x + 100 > com_s.box.x || s.box.x - 100 < com_s.box.x)
                    {
                        mc_pair mp;
                        mp.x = com_s.box.x - s.box.x;
                        mp.y = com_s.box.y - s.box.y;
                        mp.comp = com_s;
                        mp.model = s;
                        mp.hitnum = GetHitNum_vec(mp.x, mp.y, model_vec, comp_vec, floor(s.box.y / 100.0));
                        mp.dis = get_dis(s, com_s, 0);
                        biasmap.push_back(mp);
                    }
                }
                std::sort(biasmap.begin(), biasmap.end(), mc_pair_p);
                std::vector< mc_pair> ss;
                if (biasmap.size() >= 1)
                {
                    ss.push_back(biasmap[0]);
                    if (biasmap.size() >= 2)
                    {
                        for (int i = 1; i < biasmap.size(); i++)
                        {
                            if (biasmap[i].hitnum >= biasmap[0].hitnum)
                            {
                                ss.push_back(biasmap[i]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }


                }
                marrymap_v.push_back(ss);
            }
            if (marrymap_v.size()>= find_num)
            {
                break;
            }
        }
    }
    for (auto s : marrymap_v)
    {
        for (auto ss : s)
        {
            marrymap_enum0.push_back(ss);
        }
    }
    std::sort(marrymap_enum0.begin(), marrymap_enum0.end(), mc_pair_p);
    std::vector< mc_pair> res_bias_v;
    if (marrymap_enum0.size() >= 1)
    {
        res_bias_v.push_back(marrymap_enum0[0]);
        if (marrymap_enum0.size() >= 2)
        {
            for (int i = 1; i < marrymap_enum0.size(); i++)
            {
                if (marrymap_enum0[i].hitnum >= marrymap_enum0[0].hitnum)
                {
                    res_bias_v.push_back(marrymap_enum0[i]);
                }
                else
                {
                    break;
                }
            }
        }

    }
    cv::Point center = { 0,0 };
    //取中心值
    //for (int i = 0; i < res_bias_v.size(); i++)
    //{
    //    center.x += res_bias_v[i].x;
    //    center.y += res_bias_v[i].y;
    //}
    //int size = res_bias_v.size();
    //center.x = int(center.x / size);
    //center.y = int(center.y / size);
    center.x = res_bias_v[0].x;
    center.y = res_bias_v[0].y;
    if (center.x>50|| center.y>150)
    {
        center.x = 0;
        center.y =0;
    }
    
    //开始判断是否丢失
    bool update_h = false;
    for (int i=0;i<model_vec.size();i++)
    {
        vector<model_struct_box> model_s = model_vec[i];
        if (i%10==0)
        {
            update_h = true;
        }
        if (model_s.empty())
        {

            continue;
        }
        for (auto model_ss: model_s)
        {
            model_struct_box fix_model= model_ss;
            fix_model.box.x = model_ss.box.x + center.x;
            fix_model.box.y = model_ss.box.y + center.y;

            std::vector<mc_pair> marrys;
            for (int hh = max(int(floor(fix_model.box.y / 100.0)) - 1, 0); hh < min(int(comp_vec.size()), int(floor(fix_model.box.y / 100.0)) + 2); hh++)
            {
                if (comp_vec[hh].size() != 0)
                {
                    for (int i=0;i< comp_vec[hh].size();i++)
                    {
                        if (abs(fix_model.box.x - comp_vec[hh][i].box.x) <= 50 && abs(fix_model.box.y - comp_vec[hh][i].box.y) < 50)
                        {
                            mc_pair marry;
                            marry.comp = comp_vec[hh][i];
                            marry.model = fix_model;
                            marry.dis = get_dis(fix_model, marry.comp, 0);
                            marry.hitnum = 0;
                            marry.x = fix_model.box.x - marry.comp.box.x;
                            marry.y = fix_model.box.y - marry.comp.box.y;
                            marrys.push_back(marry);
                        }
                    }
                }
            }
            if (marrys.size() >= 1)
            {
                sort(marrys.begin(), marrys.end(), mc_pair_d);
                marrymap_enum1.push_back(marrys[0]);
                if (update_h)
                {
                    update_h = false;
                    center.y-= marrys[0].y;
                }
                cv::Rect model;
                model.x = marrys[0].model.box.x - marrys[0].x;
                model.y = marrys[0].model.box.y - marrys[0].y;
                model.width = marrys[0].model.box.width ;
                model.height = marrys[0].model.box.height;

                cv::rectangle(copy_img_fix, getrect(marrys[0].comp), cv::Scalar(255, 0, 0), 1);
                cv::rectangle(copy_img_fix, model, cv::Scalar(0,255,  0), 1);
            }
            else
            {
                lossmap.push_back(fix_model);
                cv::rectangle(copy_img_fix, getrect(fix_model), cv::Scalar(0, 0,255 ), 1);
            }
        }
    }
    //去掉重复
    map<int, bool> remap;
    for (int i = 0; i < marrymap_enum1.size(); i++)
    {
        remap[i] = true;
    }
    for (int i = 0; i < marrymap_enum1.size(); i++)
    {
        bool re = true;
        for (int j = i; j < marrymap_enum1.size(); j++)
        {
            if (i == j)
            {
                continue;
            }
            else {
                if (marrymap_enum1[i].comp.box.x == marrymap_enum1[j].comp.box.x && marrymap_enum1[i].comp.box.y == marrymap_enum1[j].comp.box.y)
                {
                    re = false;
                    if (marrymap_enum1[i].x > marrymap_enum1[j].x)
                    {
                        remap[i] = false;
                    }
                    else
                    {
                        remap[j] = false;
                    }
                }
            }
        }

    }
    for (int i = 0; i < marrymap_enum1.size(); i++)
    {
        if (remap[i])
        {
            if (string(marrymap_enum1[i].comp.class_name) != string(marrymap_enum1[i].model.class_name))
            {
              marrymap_enum1[i].comp.class_name= marrymap_enum1[i].model.class_name;
            }
            marrymap.push_back(marrymap_enum1[i].comp);
            //cv::rectangle(copy_img, getrect(marrymap_enum1[i].comp), cv::Scalar(0, 0, 255), 1);
            h_pair hpair_s;
            hpair_s.model = marrymap_enum1[i].model.box.y;
            hpair_s.comp = marrymap_enum1[i].comp.box.y;
            hpair.push_back(hpair_s);
        }
        else
        {
            lossmap.push_back(marrymap_enum1[i].model);
        }
    }
    for (auto s : lossmap)
    {

        s.box.x = s.box.x;
        s.box.y =s.box.y;
        lossres.push_back(s);
        cv::rectangle(copy_img, getrect(s), cv::Scalar(255, 0, 0), 1);
    }
    return { marrymap ,lossres };
}

//input1为模板
vector<vector<model_struct_box>> loss_check::getloss(cv::Mat& inputmat,vector<model_struct_box> input1, vector<model_struct_box> input2,int state=0)
{

       //return first_match(inputmat, input1, input2);
    //second_match(inputmat, input1, input2);
    return easy_match(inputmat, input1, input2);

}
std::vector<std::vector<box_info_str>> loss_check::getloss_res(cv::Mat& inputmat,std::vector<box_info_str> compare_res, std::vector<model_struct_box> model)
{
    std::vector<box_info_str> res,res_loss;
    std::map<std::string, std::vector<model_struct_box>> model_map;
    std::map<std::string, std::vector<model_struct_box>> compare_res_map;
    //进行模板分类
    std::vector<model_struct_box> single;
    model_map["single"] = single;
    for (auto s : model)
    {    
        model_map["single"].push_back(s);
    }
    std::vector<model_struct_box> input1;
    compare_res_map["single"] = input1;
    //std::cout << "     检测结果大小为："<< compare_res.size() << std::endl;
    for (auto s : compare_res)
    {
        model_struct_box s_store;
        s_store.box = s.box;
        s_store.class_name=s.name.c_str();
        s_store.conf = s.conf;
        compare_res_map["single"].push_back(s_store);
    }
    vector<vector<model_struct_box>> fianl_res = getloss(inputmat,model_map["single"], compare_res_map["single"]);
    if ( fianl_res[0].size()>0)
    {
        for (auto s : fianl_res[0])
        {
            box_info_str box_s;
            box_s.box = s.box;
            box_s.name = s.class_name;
            box_s.state = Normal;
            res.push_back(box_s);
        }
    }
    if (fianl_res[1].size() != 0)
    {
        for (auto s : fianl_res[1])
        {
            box_info_str box_s;
            box_s.box = s.box;
            box_s.name = s.class_name;
            box_s.state = Screw_missing;
            res_loss.push_back(box_s);
        }
    }
    return {res,res_loss };
}