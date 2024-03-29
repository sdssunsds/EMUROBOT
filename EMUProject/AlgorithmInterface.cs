﻿//#define testRedis

using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
#region test
using GW.Function.FileFunction;
using Project.ServerClass;
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;
using static EMU.Util.LogManager;

namespace Project
{
    public class AlgorithmInterface : IAlgorithmInterface, IMainForm
    {
        public IAppServer appServer { get; set; }
        public ICameraControl[] cameras { get; set; }
        public IDataBase dataBase { get; set; }
        public ILaserControl laser { get; set; }
        public ILightControl light { get; set; }
        public IRgvControl rgv { get; set; }
        public IRobotControl robot { get; set; }
        public IService upload { get; set; }
        public IHomePage homePage { get; set; }
        public string ChineseTitle { get; set; } = "算法接口服务";
        public string EnglishTitle { get; set; } = "Algorithm Interface";
        public string PathParameter1 { get; set; } = Application.StartupPath + "\\opration.ini";
        public string PathParameter2 { get; set; }
        public string PathParameter3 { get; set; }
        public string PathParameter4 { get; set; }

        public string PathName1 { get { return "待定"; } }

        public string PathName2 { get { return "待定"; } }

        public string PathName3 { get { return "待定"; } }

        public string PathName4 { get { return "待定"; } }

        public Action SkinChanged { get; set; }
        public Action ColorChanged { get; set; }
        public Action<Action> FormInvoke { get; set; }
        public Action<Action> FormBeginInvoke { get; set; }

        public UserControl BaslerUserControl()
        {
            return null;
        }

        public UserControl CognexUserControl()
        {
            return null;
        }

        public object GetParameterObject()
        {
            return null;
        }

        public UserControl HikUserControl()
        {
            return null;
        }

        public void InitMenu(ToolStripMenuItem[] menus)
        {
            foreach (ToolStripMenuItem item in menus)
            {
                if (item.Name == "功能ToolStripMenuItem" ||
                    item.Name == "日志ToolStripMenuItem" ||
                    item.Name == "流程ToolStripMenuItem" ||
                    item.Name == "图片ToolStripMenuItem" ||
                    item.Name == "控制ToolStripMenuItem" ||
                    item.Name == "缓存ToolStripMenuItem" ||
                    item.Name == "参数ToolStripMenuItem")
                {
                    item.Visible = false;
                }
                else if (item.Name == "软件ToolStripMenuItem")
                {
                    item.DropDownItems.RemoveByKey("toolStripMenuItem3");
                    ToolStripMenuItem strip = new ToolStripMenuItem();
                    strip.Name = "export";
                    strip.Text = "数据导出";
                    strip.Click += (object sender, EventArgs e) =>
                    {
                        new ExportAlgorithmDataForm().Show();
                    };
                    item.DropDownItems.Insert(0, strip);

                    ToolStripMenuItem change = new ToolStripMenuItem();
                    change.Name = "artificial";
                    change.Text = "人工修正";
                    change.Click += (object sender, EventArgs e) =>
                    {
                        if (algorithmPage.ChangeForm == null)
                        {
                            ChangeForm.CanChanged = false;
                            algorithmPage.ChangeForm = new ChangeForm();
                            algorithmPage.ChangeForm.FormClosed += (object o, FormClosedEventArgs fce) =>
                            {
                                algorithmPage.ChangeForm = null;
                            };
                            algorithmPage.ChangeForm.Show(); 
                        }
                        else
                        {
                            algorithmPage.ChangeForm.BringToFront();
                        }
                    };
                    item.DropDownItems.Insert(1, change);

                    ToolStripMenuItem history = new ToolStripMenuItem();
                    history.Name = "history";
                    history.Text = "修正历史";
                    history.Click += (object sender, EventArgs e) =>
                    {
                        new ChangeHistoryForm().Show();
                    };
                    item.DropDownItems.Insert(2, history);
                }
                else if (item.Name == "设置ToolStripMenuItem")
                {
                    item.DropDownItems.RemoveByKey("toolStripMenuItem1");
                }
                else if (item.Name == "帮助ToolStripMenuItem")
                {
                    ToolStripItem strip = item.DropDownItems.Add("DEBUG");
                    strip.Click += (object sender, EventArgs e) =>
                    {
                        new ThreadManagerForm().Show();
                    };
                }
            }
        }

        public PageControl[] InitPages()
        {
            PageControl[] pages = new PageControl[3];
            pages[0] = new PageControl()
            {
                Name = "服务控制",
                MainControl = mainPage = new MainPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[1] = new PageControl()
            {
                Name = "算法接口",
                MainControl = algorithmPage = new AlgorithmPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[2] = new PageControl()
            {
                Name = "异常日志",
                MainControl = new LogPage() { Dock = DockStyle.Fill }
            };
            return pages;
        }

        public UserControl LaserUserControl()
        {
            return null;
        }

        public UserControl LidarUserControl()
        {
            return null;
        }

        public UserControl LightUserControl()
        {
            return null;
        }

        public UserControl RgvUserControl(Color color)
        {
            return null;
        }

        public UserControl RobotUserControl()
        {
            return null;
        }

        public void SaveParameterObject()
        {
            
        }

        public void SavePathParameter()
        {
            
        }

        public UserControl ServerUserControl()
        {
            return null;
        }

        public void SetBaslerCameraError(Action<string, object, object> action)
        {
            
        }

        public void SetBaslerCameraStatusChanged(Action<string, object, object> action)
        {
            
        }

        public void SetBaslerImageReady(Action<string, object, object> action)
        {
            
        }

        public void SetPower(Address12Type type, bool open)
        {
            
        }

        public UserControl UploadImageUserControl()
        {
            return null;
        }

        public void Load(string[] args)
        {
            AddLog("软件参数：" + string.Join(" ", args), LogType.ProcessLog);
            if (args != null && args.Length > 0)
            {
                if (mainPage != null)
                {
                    mainPage.isOpenControl = false;
                    if (Array.IndexOf(args, "-c") > -1)
                    {
                        algorithmPage.InitAlgorithm();
                    }
                    if (Array.IndexOf(args, "start") > -1)
                    {
                        mainPage.btn_link_Click(null, null); 
                    }
                }
                int index = Array.IndexOf(args, "-f");
                if (index > -1 && index + 1 < args.Length)
                {
                    string sn = args[index + 1];
                    string type = args[index + 2];
                    string partId = args[index + 3];
                    string path = args[index + 4];
                    algorithmPage.InitAlgorithm();
                    ThreadManager.TaskRun((ThreadEventArgs eventArgs) =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (!algorithmPage.RunAlgorithm("380AL", sn, "00000001", partId, type, "", path, "", i.ToString("000000000000000"), eventArgs, true, true))
                            {
                                AddLog("算法执行失败", LogType.GeneralLog);
                            }
                            Thread.Sleep(1000);
                        }
                    });
                }
                if (Array.IndexOf(args, "-p") > -1)
                {
                    mainPage.Enabled = false;
                    bool runAlgorithm = Array.IndexOf(args, "-a") > -1;
                    int taskIndex = Array.IndexOf(args, "-t");
                    string taskType = "0";
                    if (taskIndex > -1)
                    {
                        taskType = args[taskIndex + 1];
                    }
                    if (runAlgorithm)
                    {
                        algorithmPage.InitAlgorithm();
                    }
                    ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
                    {
                        threadEventArgs.ThreadName = "压力测试线程";
                        string[] dirs = Directory.GetDirectories(Application.StartupPath + "\\muban");
                        Dictionary<string, List<string>> imgFiles = new Dictionary<string, List<string>>();
                        foreach (string dir in dirs)
                        {
                            string name = dir.Replace(Application.StartupPath + "\\muban\\", "");
                            imgFiles.Add(name, new List<string>());
                            imgFiles[name].AddRange(Directory.GetFiles(dir));
                        }
                        while (true)
                        {
                            ulong i = 0;
                            foreach (KeyValuePair<string, List<string>> item in imgFiles)
                            {
                                foreach (string file in item.Value)
                                {
                                    string partId = file.Substring(file.LastIndexOf("\\") + 1).Replace(".jpg", "");
                                    AddLog("读取图片：" + file, LogType.ProcessLog);
                                    if (!algorithmPage.RunAlgorithm("380AL", item.Key, "00000001", partId, taskType, "", file, file, i.ToString("000000000000000"), threadEventArgs, true, runAlgorithm))
                                    {
                                        AddLog("算法执行失败", LogType.GeneralLog);
                                    }
                                    Thread.Sleep(500);
                                    i++;
                                }
                            }
                        }
                    });
                }
            }
            else
            {
                //if (mainPage != null)
                //{
                //    mainPage.isOpenControl = true;
                //}
                algorithmPage?.InitAlgorithm();
            }
        }

        public void Shown()
        {
            dataBase.CustomEvent += DataBase_CustomEvent;
        }

        public void Closing()
        {
            Process[] ps = Process.GetProcessesByName("AlgorithmControl");
            if (ps != null)
            {
                foreach (Process item in ps)
                {
                    item.Kill();
                }
            }
        }

        #region 接口专用变量
        public const string redis_key_handle = "data_to_handle_";
        private const string redis_key_result = "data_handled_";

        private bool isRunInterface = false;
        private int reportSleep = 0;
        private string inName, outName;
        private AlgorithmPage algorithmPage = null;
        private RedisHelper RedisHelper = null;
        private MainPage mainPage = null;
        private List<string> redisKeys = new List<string>();
        private Dictionary<string, long> redisDB = new Dictionary<string, long>()
        {
            { "in", 0 },
            { "out", 0 }
        };

        public string RedisThreadName { get; set; } = "Redis请求线程";
        public string inParName1 { get; set; } = "识别类型";
        public string inParName2 { get; set; } = "本次图片";
        public string inParName3 { get; set; } = "上次图片";
        public string inParObject { get; set; } = "业务对象";
        public string outParName { get; set; } = "结果Json";
        #endregion

        #region 接口专用方法
        public AlgorithmInterface()
        {
            string bakredis = Application.StartupPath + "\\bak_redis\\";
            if (!Directory.Exists(bakredis))
            {
                Directory.CreateDirectory(bakredis);
            }
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                threadEventArgs.ThreadName = RedisThreadName;
                while (true)
                {
                    algorithmPage?.TestRunAlgorithm();
                    while (isRunInterface)
                    {
                        Thread.Sleep(reportSleep);
                        try
                        {
                            if (redisDB.ContainsKey(inName))
                            {
                                RedisHelper.ChangeDB(redisDB[inName]);
                            }
                            List<string> keys = RedisHelper.GetKeys(redis_key_handle);
                            if (keys != null)
                            {
                                foreach (string key in keys)
                                {
                                    if (!string.IsNullOrEmpty(key) && key.IndexOf(redis_key_handle) == 0 && redisKeys.IndexOf(key) < 0)
                                    {
                                        redisKeys.Add(key);
                                        AddLog("读取Redis key：" + key, LogType.ProcessLog);
                                        string task_id = key.Replace(redis_key_handle, "");
                                        string value = RedisHelper.Get(key);
                                        AddLog("读取Redis val：" + value, LogType.ProcessLog);
                                        using (StreamWriter sw = new StreamWriter(bakredis + task_id + ".txt"))
                                        {
                                            sw.WriteLine(value);
                                        }
                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            string[] args = value.Split('&');
                                            if (args.Length > 7)
                                            {
                                                ThreadManager.TaskRun((ThreadEventArgs tmp) =>
                                                {
                                                    tmp.ThreadName = "算法执行线程";
                                                    string robot = args[0];
                                                    string mode = args[1];
                                                    string sn = args[2];
                                                    string part = args[3];
                                                    string type = args[4];
                                                    string json = args[5];
                                                    string img = args[6];
                                                    string up_img = args[7];
                                                    tmp.SetVariableValue(nameof(mode), mode);
                                                    tmp.SetVariableValue(nameof(sn), sn);
                                                    tmp.SetVariableValue(nameof(robot), robot);
                                                    tmp.SetVariableValue(nameof(part), part);
                                                    tmp.SetVariableValue(nameof(type), type);
                                                    tmp.SetVariableValue(nameof(json), json);
                                                    tmp.SetVariableValue(nameof(img), img);
                                                    tmp.SetVariableValue(nameof(up_img), up_img);
                                                    tmp.SetVariableValue(nameof(key), key);
                                                    tmp.SetVariableValue(nameof(task_id), task_id);
                                                    if (!string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(robot) &&
                                                        !string.IsNullOrEmpty(part) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(img) &&
                                                        !string.IsNullOrEmpty(up_img) && !string.IsNullOrEmpty(task_id))
                                                    {
                                                        if (!algorithmPage.RunAlgorithm(mode, sn, robot, part, type, json, img, up_img, task_id, tmp))
                                                        {
                                                            AddLog("算法执行失败", LogType.GeneralLog);
                                                            DeleteRedis(key);
                                                        }
                                                    }
                                                });  
                                            }
                                        }
                                        else
                                        {
                                            DeleteRedis(key);
                                        }
                                    }
                                }
                            }
                            if (redisDB.ContainsKey(outName))
                            {
                                RedisHelper.ChangeDB(redisDB[outName]);
                            }
                        }
                        catch (Exception e)
                        {
                            AddLog(e.Message, LogType.ErrorLog);
                        }
                    } 
                }
            });
        }

        private object DataBase_CustomEvent(params object[] pars)
        {
            AddLog(pars[0].ToString(), (LogType)pars[1]);
            return null;
        }

        public string JsonErrorChange(string json)
        {
            json = json.Replace("coordinates:", "\"coordinates\":\"");
            json = json.Replace(",type:", "\",\"type\":\"");
            if (json.IndexOf("\"}]") < 0)
            {
                json = json.Replace("}]", "\"}]");
            }
            return json;
        }

        public void ResultBack(string id, string json)
        {
            string rKey = redis_key_result + id;
            string tKey = redis_key_handle + id;
            AddLog("回写Redis key：" + rKey, LogType.GeneralLog);
            AddLog("回写Redis val：" + json, LogType.GeneralLog);
            try
            {
                RedisHelper.Set(rKey, json);
            }
            catch (Exception e)
            {
                AddLog(e.Message, LogType.ErrorLog);
                redisKeys.Remove(tKey);
            }
            finally
            {
                DeleteRedis(tKey);
            }
        }

        public void RunInterface(string url, string inputName, string outputName, int sleep, string pwd)
        {
            isRunInterface = false;
            Thread.Sleep(50);
            if (!string.IsNullOrEmpty(url))
            {
                RedisHelper?.CloseRedis();
#if testRedis
                if (string.IsNullOrEmpty(pwd))
                {
                    InputForm pwdForm = new InputForm() { Text = "输入Redis密码" };
                    if (pwdForm.ShowDialog() == DialogResult.OK)
                    {
                        pwd = MainPage.Pwd = pwdForm.Value;
                        FileSystem.WriteIniFile("Redis", "Password", pwd, PathParameter1);
                    }
                } 
#endif
                inName = inputName;
                outName = outputName;
                reportSleep = sleep;
                RedisHelper = new RedisHelper(url, pwd);
                isRunInterface = true;
#if testRedis
                RedisHelper.Set("data_to_handle_1675747941889", "380AL&2589&60001020232604020050000&0&[{\"coordinates\":\"889, 1, 51, 43; 1105,1,50,44; 1409,111,45,43; 1152,111,44,42; 1663,113,44,43; 1152,203,45,43; 1409,205,46,43; 1663,206,45,43; 1126,415,43,42; 1603,415,47,46; 1125,1376,50,50; 1605,1379,51,50; 1374,1985,46,44; 1994,1995,45,34; 1116,1983,46,44; 1630,1986,45,43; 1211,2025,43,42; 67,2074,46,43; 416,2076,46,45; 775,2080,45,43; 1365,2099,44,45; 1614,2102,44,43; 1966,2104,46,46; 1212,2251,42,43; 1434,2287,44,45; 1645,2288,46,47; 1870,2290,41,34; 413,2348,44,43; 852,2438,44,45; 1364,2442,41,42; 1209,2477,41,41; 1613,2605,41,40; 1768,2607,41,41; 1211,2692,44,32; 1116,2717,44,42; 1378,2721,42,40; 1635,2723,40,39; 1997,2724,43,42; 71,2778,45,43; 420,2779,45,44; 778,2782,44,45; 1054,2859,47,46; 1439,2861,44,43; 1686,2863,44,43; 1942,2864,50,50; 1055,3666,45,44; 1308,3667,45,44; 1683,3669,46,46; 1937,3670,50,48; 266,3761,45,45; 553,3763,43,41; 1051,3839,44,43; 1416,3841,45,44; 1699,3843,45,46; 263,4706,47,46; 546,4708,46,44; 915,4711,44,42; 1412,4717,43,42; 1699,4718,40,43; 123,4865,43,41; 632,4869,43,42; 446,4869,43,42; 927,4871,42,42; 1316,4875,44,42; 1585,4878,41,41; 1918,4883,43,43; 1736,4958,42,43; 124,5150,39,38; 1737,5161,41,42; 1919,5165,40,40; 121,5432,39,39; 1734,5446,42,44; 1919,5451,40,41; 119,5718,40,40; 435,5720,44,42; 762,5725,37,37; 1410,5726,45,43; 1089,5726,41,41; 1733,5730,41,41; 1915,5733,42,42; 257,5878,46,44; 544,5880,43,41; 1042,5882,45,43; 1408,5883,44,43; 1693,5884,46,44; 250,6382,48,46; 909,6386,44,43; 540,6385,44,43; 1407,6389,46,44; 1694,6390,40,42; 1,6645,30,45; 1836,6653,46,47; 1890,6655,34,46; 1832,6760,37,37; 1931,6830,42,41; 248,8042,47,46; 107,8042,46,45; 1102,8053,46,45; 1643,8057,47,46; 1865,8073,44,48; 441,8145,42,41; 901,8153,40,39; 1084,8195,45,44; 267,8183,49,48; 439,8417,43,45; 902,8427,41,43; 265,8681,48,49; 1084,8691,43,46; 438,8698,43,43; 900,8705,43,44; 443,8972,42,43; 903,8981,40,39; 268,9173,47,47; 1081,9182,44,45; 1927,9216,39,38; 446,9247,42,41; 904,9254,40,40; 1821,9288,41,40; 1925,9289,38,38; 1,9391,24,41; 1741,9395,48,45; 1250,9393,44,41; 1500,9396,43,41; 2021,9400,27,45; 1906,9399,43,44; 538,9551,48,47; 1034,9553,47,46; 251,9553,45,43; 1399,9555,45,44; 1687,9557,40,40; 538,10110,48,45; 250,10110,49,47; 905,10111,47,46; 1690,10114,44,43; 1403,10114,46,44; 314,11215,46,44; 627,11217,45,43; 1324,11221,42,40; 1634,11223,42,41; 8,11295,41,41; 949,11297,42,40; 2001,11297,43,44; 1007,11299,42,40; 1937,11303,44,46; 1940,11594,42,43; 1010,11590,39,40; 10,11596,40,41; 952,11600,39,40; 2003,11637,41,41; 1011,11883,40,40; 1942,11888,42,44; 11,11900,40,40; 953,11903,42,40; 2007,11982,39,40; 1014,12171,40,40; 1946,12178,41,43; 12,12198,39,39; 956,12201,40,41; 319,12275,44,42; 633,12275,44,42; 632,12332,43,41; 320,12333,38,37; 11,12413,38,37; 953,12416,40,42; 1013,12463,37,38; 1947,12468,41,43; 13,12710,40,39; 1013,12754,41,42; 1949,12761,41,43; 1332,12832,44,42; 1643,12834,42,40; 1333,12889,42,38; 1643,12890,43,41; 1013,12966,40,40; 1951,12969,40,43; 12,12999,42,42; 955,13005,38,39; 1011,13275,38,40; 1947,13279,43,47; 8,13298,42,39; 951,13304,41,42; 315,13380,45,41; 629,13381,43,42; 317,13437,42,40; 631,13439,42,42; 11,13514,38,37; 953,13519,40,41; 1009,13582,40,41; 1945,13587,41,44; 2005,13835,42,44; 5,13838,42,43; 949,13845,41,43; 1007,13888,41,42; 1943,13893,42,44; 5,14172,42,43; 2004,14180,44,44; 950,14180,41,39; 1009,14201,39,41; 1943,14205,44,46; 8,14496,43,43; 952,14504,41,42; 1011,14506,39,40; 1947,14509,42,43; 2009,14519,39,42; 319,14583,40,38; 1643,14589,41,40; 631,14583,41,40; 1332,14587,41,40; 545,14752,45,44; 258,14754,45,44; 1045,14754,46,45; 1413,14756,43,42; 1695,14761,41,45; 259,15412,47,45; 545,15412,47,45; 906,15490,45,44; 993,15490,46,45; 1411,15546,43,42; 1694,15546,43,47; 1004,15624,42,42; 998,15955,39,41; 676,16230,43,45; 844,16233,44,45; 996,16280,39,41; 1185,16521,43,43; 1540,16523,43,43; 1884,16526,42,44; 675,16535,42,48; 996,16614,38,40; 670,16831,43,48; 840,16836,45,46; 1103,16866,42,42; 993,16937,39,42; 1538,16954,43,42; 1177,17216,45,46; 1532,17221,44,43; 1876,17222,45,45; 994,17272,37,38; 900,17385,46,43; 988,17385,45,43; 282,17496,46,44; 669,17500,45,42; 928,17501,46,42; 282,19195,46,44; 537,19197,48,44; 796,19199,46,44; \",\"type\":\"LS_6J\"}]&" +
                    "http://192.168.100.106/380AL_2637_common_1.jpg&http://192.168.100.106/380AL_2637_common_2.jpg&");
                RedisHelper.Set("data_to_handle_1675747941890", "380AL&2589&60001020232604020050000&0&[{coordinates:890, 1, 51, 43; 1105,1,50,44; 1409,111,45,43; 1152,111,44,42; 1663,113,44,43; 1152,203,45,43; 1409,205,46,43; 1663,206,45,43; 1126,415,43,42; 1603,415,47,46; 1125,1376,50,50; 1605,1379,51,50; 1374,1985,46,44; 1994,1995,45,34; 1116,1983,46,44; 1630,1986,45,43; 1211,2025,43,42; 67,2074,46,43; 416,2076,46,45; 775,2080,45,43; 1365,2099,44,45; 1614,2102,44,43; 1966,2104,46,46; 1212,2251,42,43; 1434,2287,44,45; 1645,2288,46,47; 1870,2290,41,34; 413,2348,44,43; 852,2438,44,45; 1364,2442,41,42; 1209,2477,41,41; 1613,2605,41,40; 1768,2607,41,41; 1211,2692,44,32; 1116,2717,44,42; 1378,2721,42,40; 1635,2723,40,39; 1997,2724,43,42; 71,2778,45,43; 420,2779,45,44; 778,2782,44,45; 1054,2859,47,46; 1439,2861,44,43; 1686,2863,44,43; 1942,2864,50,50; 1055,3666,45,44; 1308,3667,45,44; 1683,3669,46,46; 1937,3670,50,48; 266,3761,45,45; 553,3763,43,41; 1051,3839,44,43; 1416,3841,45,44; 1699,3843,45,46; 263,4706,47,46; 546,4708,46,44; 915,4711,44,42; 1412,4717,43,42; 1699,4718,40,43; 123,4865,43,41; 632,4869,43,42; 446,4869,43,42; 927,4871,42,42; 1316,4875,44,42; 1585,4878,41,41; 1918,4883,43,43; 1736,4958,42,43; 124,5150,39,38; 1737,5161,41,42; 1919,5165,40,40; 121,5432,39,39; 1734,5446,42,44; 1919,5451,40,41; 119,5718,40,40; 435,5720,44,42; 762,5725,37,37; 1410,5726,45,43; 1089,5726,41,41; 1733,5730,41,41; 1915,5733,42,42; 257,5878,46,44; 544,5880,43,41; 1042,5882,45,43; 1408,5883,44,43; 1693,5884,46,44; 250,6382,48,46; 909,6386,44,43; 540,6385,44,43; 1407,6389,46,44; 1694,6390,40,42; 1,6645,30,45; 1836,6653,46,47; 1890,6655,34,46; 1832,6760,37,37; 1931,6830,42,41; 248,8042,47,46; 107,8042,46,45; 1102,8053,46,45; 1643,8057,47,46; 1865,8073,44,48; 441,8145,42,41; 901,8153,40,39; 1084,8195,45,44; 267,8183,49,48; 439,8417,43,45; 902,8427,41,43; 265,8681,48,49; 1084,8691,43,46; 438,8698,43,43; 900,8705,43,44; 443,8972,42,43; 903,8981,40,39; 268,9173,47,47; 1081,9182,44,45; 1927,9216,39,38; 446,9247,42,41; 904,9254,40,40; 1821,9288,41,40; 1925,9289,38,38; 1,9391,24,41; 1741,9395,48,45; 1250,9393,44,41; 1500,9396,43,41; 2021,9400,27,45; 1906,9399,43,44; 538,9551,48,47; 1034,9553,47,46; 251,9553,45,43; 1399,9555,45,44; 1687,9557,40,40; 538,10110,48,45; 250,10110,49,47; 905,10111,47,46; 1690,10114,44,43; 1403,10114,46,44; 314,11215,46,44; 627,11217,45,43; 1324,11221,42,40; 1634,11223,42,41; 8,11295,41,41; 949,11297,42,40; 2001,11297,43,44; 1007,11299,42,40; 1937,11303,44,46; 1940,11594,42,43; 1010,11590,39,40; 10,11596,40,41; 952,11600,39,40; 2003,11637,41,41; 1011,11883,40,40; 1942,11888,42,44; 11,11900,40,40; 953,11903,42,40; 2007,11982,39,40; 1014,12171,40,40; 1946,12178,41,43; 12,12198,39,39; 956,12201,40,41; 319,12275,44,42; 633,12275,44,42; 632,12332,43,41; 320,12333,38,37; 11,12413,38,37; 953,12416,40,42; 1013,12463,37,38; 1947,12468,41,43; 13,12710,40,39; 1013,12754,41,42; 1949,12761,41,43; 1332,12832,44,42; 1643,12834,42,40; 1333,12889,42,38; 1643,12890,43,41; 1013,12966,40,40; 1951,12969,40,43; 12,12999,42,42; 955,13005,38,39; 1011,13275,38,40; 1947,13279,43,47; 8,13298,42,39; 951,13304,41,42; 315,13380,45,41; 629,13381,43,42; 317,13437,42,40; 631,13439,42,42; 11,13514,38,37; 953,13519,40,41; 1009,13582,40,41; 1945,13587,41,44; 2005,13835,42,44; 5,13838,42,43; 949,13845,41,43; 1007,13888,41,42; 1943,13893,42,44; 5,14172,42,43; 2004,14180,44,44; 950,14180,41,39; 1009,14201,39,41; 1943,14205,44,46; 8,14496,43,43; 952,14504,41,42; 1011,14506,39,40; 1947,14509,42,43; 2009,14519,39,42; 319,14583,40,38; 1643,14589,41,40; 631,14583,41,40; 1332,14587,41,40; 545,14752,45,44; 258,14754,45,44; 1045,14754,46,45; 1413,14756,43,42; 1695,14761,41,45; 259,15412,47,45; 545,15412,47,45; 906,15490,45,44; 993,15490,46,45; 1411,15546,43,42; 1694,15546,43,47; 1004,15624,42,42; 998,15955,39,41; 676,16230,43,45; 844,16233,44,45; 996,16280,39,41; 1185,16521,43,43; 1540,16523,43,43; 1884,16526,42,44; 675,16535,42,48; 996,16614,38,40; 670,16831,43,48; 840,16836,45,46; 1103,16866,42,42; 993,16937,39,42; 1538,16954,43,42; 1177,17216,45,46; 1532,17221,44,43; 1876,17222,45,45; 994,17272,37,38; 900,17385,46,43; 988,17385,45,43; 282,17496,46,44; 669,17500,45,42; 928,17501,46,42; 282,19195,46,44; 537,19197,48,44; 796,19199,46,44;,type:LS_6J}]&" +
                    "http://192.168.100.106/380AL_2637_common_3.jpg&http://192.168.100.106/380AL_2637_common_4.jpg&");
                RedisHelper.Set("data_to_handle_1675747941891", "380AL&2589&60001020232604020050000&0;1&[{coordinates:891, 1, 51, 43; 1105,1,50,44; 1409,111,45,43; 1152,111,44,42; 1663,113,44,43; 1152,203,45,43; 1409,205,46,43; 1663,206,45,43; 1126,415,43,42; 1603,415,47,46; 1125,1376,50,50; 1605,1379,51,50; 1374,1985,46,44; 1994,1995,45,34; 1116,1983,46,44; 1630,1986,45,43; 1211,2025,43,42; 67,2074,46,43; 416,2076,46,45; 775,2080,45,43; 1365,2099,44,45; 1614,2102,44,43; 1966,2104,46,46; 1212,2251,42,43; 1434,2287,44,45; 1645,2288,46,47; 1870,2290,41,34; 413,2348,44,43; 852,2438,44,45; 1364,2442,41,42; 1209,2477,41,41; 1613,2605,41,40; 1768,2607,41,41; 1211,2692,44,32; 1116,2717,44,42; 1378,2721,42,40; 1635,2723,40,39; 1997,2724,43,42; 71,2778,45,43; 420,2779,45,44; 778,2782,44,45; 1054,2859,47,46; 1439,2861,44,43; 1686,2863,44,43; 1942,2864,50,50; 1055,3666,45,44; 1308,3667,45,44; 1683,3669,46,46; 1937,3670,50,48; 266,3761,45,45; 553,3763,43,41; 1051,3839,44,43; 1416,3841,45,44; 1699,3843,45,46; 263,4706,47,46; 546,4708,46,44; 915,4711,44,42; 1412,4717,43,42; 1699,4718,40,43; 123,4865,43,41; 632,4869,43,42; 446,4869,43,42; 927,4871,42,42; 1316,4875,44,42; 1585,4878,41,41; 1918,4883,43,43; 1736,4958,42,43; 124,5150,39,38; 1737,5161,41,42; 1919,5165,40,40; 121,5432,39,39; 1734,5446,42,44; 1919,5451,40,41; 119,5718,40,40; 435,5720,44,42; 762,5725,37,37; 1410,5726,45,43; 1089,5726,41,41; 1733,5730,41,41; 1915,5733,42,42; 257,5878,46,44; 544,5880,43,41; 1042,5882,45,43; 1408,5883,44,43; 1693,5884,46,44; 250,6382,48,46; 909,6386,44,43; 540,6385,44,43; 1407,6389,46,44; 1694,6390,40,42; 1,6645,30,45; 1836,6653,46,47; 1890,6655,34,46; 1832,6760,37,37; 1931,6830,42,41; 248,8042,47,46; 107,8042,46,45; 1102,8053,46,45; 1643,8057,47,46; 1865,8073,44,48; 441,8145,42,41; 901,8153,40,39; 1084,8195,45,44; 267,8183,49,48; 439,8417,43,45; 902,8427,41,43; 265,8681,48,49; 1084,8691,43,46; 438,8698,43,43; 900,8705,43,44; 443,8972,42,43; 903,8981,40,39; 268,9173,47,47; 1081,9182,44,45; 1927,9216,39,38; 446,9247,42,41; 904,9254,40,40; 1821,9288,41,40; 1925,9289,38,38; 1,9391,24,41; 1741,9395,48,45; 1250,9393,44,41; 1500,9396,43,41; 2021,9400,27,45; 1906,9399,43,44; 538,9551,48,47; 1034,9553,47,46; 251,9553,45,43; 1399,9555,45,44; 1687,9557,40,40; 538,10110,48,45; 250,10110,49,47; 905,10111,47,46; 1690,10114,44,43; 1403,10114,46,44; 314,11215,46,44; 627,11217,45,43; 1324,11221,42,40; 1634,11223,42,41; 8,11295,41,41; 949,11297,42,40; 2001,11297,43,44; 1007,11299,42,40; 1937,11303,44,46; 1940,11594,42,43; 1010,11590,39,40; 10,11596,40,41; 952,11600,39,40; 2003,11637,41,41; 1011,11883,40,40; 1942,11888,42,44; 11,11900,40,40; 953,11903,42,40; 2007,11982,39,40; 1014,12171,40,40; 1946,12178,41,43; 12,12198,39,39; 956,12201,40,41; 319,12275,44,42; 633,12275,44,42; 632,12332,43,41; 320,12333,38,37; 11,12413,38,37; 953,12416,40,42; 1013,12463,37,38; 1947,12468,41,43; 13,12710,40,39; 1013,12754,41,42; 1949,12761,41,43; 1332,12832,44,42; 1643,12834,42,40; 1333,12889,42,38; 1643,12890,43,41; 1013,12966,40,40; 1951,12969,40,43; 12,12999,42,42; 955,13005,38,39; 1011,13275,38,40; 1947,13279,43,47; 8,13298,42,39; 951,13304,41,42; 315,13380,45,41; 629,13381,43,42; 317,13437,42,40; 631,13439,42,42; 11,13514,38,37; 953,13519,40,41; 1009,13582,40,41; 1945,13587,41,44; 2005,13835,42,44; 5,13838,42,43; 949,13845,41,43; 1007,13888,41,42; 1943,13893,42,44; 5,14172,42,43; 2004,14180,44,44; 950,14180,41,39; 1009,14201,39,41; 1943,14205,44,46; 8,14496,43,43; 952,14504,41,42; 1011,14506,39,40; 1947,14509,42,43; 2009,14519,39,42; 319,14583,40,38; 1643,14589,41,40; 631,14583,41,40; 1332,14587,41,40; 545,14752,45,44; 258,14754,45,44; 1045,14754,46,45; 1413,14756,43,42; 1695,14761,41,45; 259,15412,47,45; 545,15412,47,45; 906,15490,45,44; 993,15490,46,45; 1411,15546,43,42; 1694,15546,43,47; 1004,15624,42,42; 998,15955,39,41; 676,16230,43,45; 844,16233,44,45; 996,16280,39,41; 1185,16521,43,43; 1540,16523,43,43; 1884,16526,42,44; 675,16535,42,48; 996,16614,38,40; 670,16831,43,48; 840,16836,45,46; 1103,16866,42,42; 993,16937,39,42; 1538,16954,43,42; 1177,17216,45,46; 1532,17221,44,43; 1876,17222,45,45; 994,17272,37,38; 900,17385,46,43; 988,17385,45,43; 282,17496,46,44; 669,17500,45,42; 928,17501,46,42; 282,19195,46,44; 537,19197,48,44; 796,19199,46,44;,type:LS_6J}]&" +
                    "http://192.168.100.106/380AL_2637_common_5.jpg&http://192.168.100.106/380AL_2637_common_6.jpg&");
                RedisHelper.Set("data_to_handle_1675747941892", "380AL&2589&60001020232604020050000&0;1;2&[]&" +
                    "http://192.168.100.106/380AL_2637_common_7.jpg&http://192.168.100.106/380AL_2637_common_8.jpg&");
#endif
            }
        }

        public void DeleteRedis(string key)
        {
            RedisHelper.DeleteValue(key);
            redisKeys.Remove(key);
            AddLog("删除Redis key：" + key, LogType.GeneralLog);
        }
        #endregion
    }
}
