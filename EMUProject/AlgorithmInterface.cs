using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using GW.Function.FileFunction;
using Project.ServerClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;
using static EMU.Util.LogManager;

namespace Project
{
    public class AlgorithmInterface : IProject
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
                }
                else if (item.Name == "设置ToolStripMenuItem")
                {
                    item.DropDownItems.RemoveByKey("toolStripMenuItem1");
                }
            }
        }

        public PageControl[] InitPages()
        {
            PageControl[] pages = new PageControl[3];
            pages[0] = new PageControl()
            {
                Name = "服务控制",
                MainControl = new MainPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[1] = new PageControl()
            {
                Name = "算法接口",
                MainControl = algorithmPage = new AlgorithmPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[2] = new PageControl()
            {
                Name = "日志",
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

        #region 接口专用变量
        private const string redis_key_handle = "data_to_handle_";
        private const string redis_key_result = "data_handled_";

        private bool isRunInterface = false;
        private int reportSleep = 0;
        private string inName, outName;
        private RedisHelper RedisHelper = null;
        private AlgorithmPage algorithmPage = null;
        private Dictionary<string, long> redisDB = new Dictionary<string, long>()
        {
            { "in", 0 },
            { "out", 0 }
        };

        public string RedisThreadName = "Redis请求线程";
        public string inParName1 = "识别类型";
        public string inParName2 = "本次图片";
        public string inParName3 = "上次图片";
        public string inParObject = "业务对象";
        public string outParName = "结果Json";
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
                    while (isRunInterface)
                    {
                        Thread.Sleep(reportSleep);
                        try
                        {
                            if (redisDB.ContainsKey(inName))
                            {
                                RedisHelper.ChangeDB(redisDB[inName]);
                            }
                            List<string> keys = RedisHelper.GetKeys();
                            if (keys != null)
                            {
                                foreach (string key in keys)
                                {
                                    if (!string.IsNullOrEmpty(key) && key.IndexOf(redis_key_handle) == 0)
                                    {
                                        AddLog("读取Redis key：" + key, LogType.ProcessLog);
                                        string task_id = key.Replace(redis_key_handle, "");
                                        string value = RedisHelper.GetValue<string>(key);
                                        AddLog("读取Redis val：" + value, LogType.ProcessLog);
                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            string[] args = value.Split('&');
                                            if (args != null)
                                            {
                                                AddLog("参数数量：" + args.Length, LogType.ProcessLog);
                                                if (args.Length > 0)
                                                {
                                                    threadEventArgs.SetVariableValue(inParName1, args[0]);
                                                    AddLog("识别类型：" + args[0], LogType.ProcessLog);
                                                }
                                                if (args.Length > 1)
                                                {
                                                    AddLog("坐标Json：" + args[1], LogType.ProcessLog);
                                                    threadEventArgs.SetVariableValue(inParObject, args[1]);
                                                }
                                                if (args.Length > 2)
                                                {
                                                    AddLog("本次图片url：" + args[2], LogType.ProcessLog);
                                                    threadEventArgs.SetVariableValue(inParName2, args[2]);
                                                }
                                                if (args.Length > 3)
                                                {
                                                    AddLog("上次图片url：" + args[3], LogType.ProcessLog);
                                                    threadEventArgs.SetVariableValue(inParName3, args[3]);
                                                }
                                                ThreadManager.TaskRun((ThreadEventArgs tmp) =>
                                                {
                                                    algorithmPage.RunAlgorithm(args[0], args[1], args[2], args[3], task_id);
                                                });
                                            }
                                        }
                                        using (StreamWriter sw = new StreamWriter(bakredis + task_id + ".txt"))
                                        {
                                            sw.WriteLine(value);
                                        }
                                        RedisHelper.DeleteValue(key);
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

        public void ResultBack(string id, string json)
        {
            string rKey = redis_key_result + id;
            AddLog("回写Redis key：" + rKey, LogType.GeneralLog);
            AddLog("回写Redis val：" + json, LogType.GeneralLog);
            try
            {
                RedisHelper.SetValue<string>(rKey, json);
            }
            catch (Exception e)
            {
                AddLog(e.Message, LogType.ErrorLog);
            }
        }

        public void RunInterface(string url, string inputName, string outputName, int sleep, string pwd)
        {
            isRunInterface = false;
            Thread.Sleep(50);
            if (!string.IsNullOrEmpty(url))
            {
                RedisHelper?.CloseRedis();
                if (string.IsNullOrEmpty(pwd))
                {
                    InputForm pwdForm = new InputForm() { Text = "输入Redis密码" };
                    if (pwdForm.ShowDialog() == DialogResult.OK)
                    {
                        pwd = MainPage.Pwd = pwdForm.Value;
                        FileSystem.WriteIniFile("Redis", "Password", pwd, PathParameter1);
                    } 
                }
                inName = inputName;
                outName = outputName;
                reportSleep = sleep;
                RedisHelper = new RedisHelper(url, pwd);
                isRunInterface = true;
            }
        }
        #endregion
    }
}
