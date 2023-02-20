using AlgorithmLib;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static EMU.Util.LogManager;

namespace Project
{
    public partial class AlgorithmPage : UserControl
    {
        private const int algorithmMax = 1;
        private object runLock = new object();
        private int logCount = 0;
        private int exeCount = 0;
        private string algorithmParPath = Application.StartupPath + "\\algorithm.pars";
        private string bakPath = Application.StartupPath + "\\bak_img\\";
        private string resultDir = "";
        private string resultPath = Application.StartupPath + "\\algorithm.back";

        public IAlgorithmInterface Project { get; set; }

        public AlgorithmPage()
        {
            InitializeComponent();
            if (!Directory.Exists(bakPath))
            {
                Directory.CreateDirectory(bakPath);
            }
        }

        public void RunAlgorithm(string type, string json, string url_now, string url_up, string id, ThreadEventArgs eventArgs)
        {
            if (!string.IsNullOrEmpty(url_now) && !string.IsNullOrEmpty(json) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
            {
                eventArgs.SetVariableValue("任务编号", id);
                eventArgs.SetVariableValue("识别类型", type);
                eventArgs.SetVariableValue("本次图片", url_now);
                eventArgs.SetVariableValue("上次图片", url_up);
                eventArgs.SetVariableValue("原始版Json", json);
                json = Project.JsonErrorChange(json);
                eventArgs.SetVariableValue("修正后Json", json);
                AddLog("任务编号：" + id, LogType.OtherLog);
                AddLog("识别类型：" + type, LogType.OtherLog);
                AddLog("坐标Json：" + json, LogType.OtherLog);
                AddLog("本次图片：" + url_now, LogType.OtherLog);
                AddLog("上次图片：" + url_up, LogType.OtherLog);
                AddLog("下载图片：" + url_now, LogType.OtherLog);
                AddLog("转换Json", LogType.OtherLog);
                RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(json);
                AddLog("Json转换完成", LogType.OtherLog);
                Bitmap bitmap1 = GW.Function.ImageFunction.Manage.ImageManage.Download(url_now);
                bitmap1.Save(bakPath + id + ".jpg");
                Size size1 = bitmap1.Size;
                AddLog("缓存完成：" + url_now, LogType.OtherLog);
                AddLog("下载图片：" + url_up, LogType.OtherLog);
                Bitmap bitmap2 = GW.Function.ImageFunction.Manage.ImageManage.Download(url_up);
                bitmap2.Save(bakPath + id + "_up.jpg");
                Size size2 = bitmap2.Size;
                AddLog("缓存完成：" + url_up, LogType.OtherLog);
                bitmap1.Dispose();
                bitmap2.Dispose();
                bitmap1 = null;
                bitmap2 = null;
                string resultFile = resultDir + "\\" + id + ".json";
                if (File.Exists(resultFile))
                {
                    AddLog("删除结果文件", LogType.OtherLog);
                    File.Delete(resultFile);
                }
                SetAlgorithmPars(url_now, url_up, json, type, id);
                AddLog("启动算法", LogType.OtherLog);
                bool monitor = true;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    int i = 0;
                    int outTime = Properties.Settings.Default.AlgorithmOutTime * 2;
                    while (true)
                    {
                        if (i >= outTime)
                        {
                            AddLog("算法超时", LogType.OtherLog);
                            break;
                        }
                        else if (File.Exists(resultPath))
                        {
                            AddLog("读取的算法结果文件", LogType.OtherLog);
                            Thread.Sleep(100);
                            break;
                        }
                        Thread.Sleep(500);
                    }
                    monitor = false;
                }));
                lock (runLock)
                {
                    while (exeCount >= algorithmMax)
                    {
                        Thread.Sleep(50);
                    }
                    exeCount++;
                }
                using (StreamWriter sw = new StreamWriter(algorithmParPath))
                {
                    sw.WriteLine(id);
                    sw.WriteLine(type);
                    sw.WriteLine(bakPath + id + ".jpg");
                    sw.WriteLine(bakPath + id + "_up.jpg");
                    sw.WriteLine(size1.Width);
                    sw.WriteLine(size1.Height);
                    sw.WriteLine(size2.Width);
                    sw.WriteLine(size2.Height);
                    if (businesses != null && businesses.Length > 0)
                    {
                        List<model_struct> ms = new List<model_struct>();
                        foreach (RedisBusiness business in businesses)
                        {
                            ms.AddRange(business.TaskList);
                        }
                        sw.WriteLine(JsonManager.ObjectToJson(ms));
                    }
                    else
                    {
                        sw.WriteLine("null");
                    }
                }
                if (File.Exists(resultPath))
                {
                    File.Delete(resultPath);
                }
                thread.Start();
                while (monitor)
                {
                    Thread.Sleep(50);
                }
                exeCount--;
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
                if (!File.Exists(resultPath))
                {
                    return;
                }

                json = "";
                box_info[] boxes = null;
                using (StreamReader sr = new StreamReader(resultPath))
                {
                    boxes = JsonManager.JsonToObject<box_info[]>(sr.ReadToEnd());
                }
                List<RedisResult> list = new List<RedisResult>();
                string code = "";
                foreach (box_info box in boxes)
                {
                    code = box.state_enum.ChangeCode();
                    RedisResult result = list.Find(r => r.jclx == code);
                    if (result == null)
                    {
                        result = new RedisResult()
                        {
                            jclx = code,
                            result = new List<Rectangle>()
                        };
                        list.Add(result);
                    }
                    result.result.Add(new Rectangle(box.x, box.y, box.w, box.h));
                }
                json = JsonManager.ObjectToJson(list);

                AddLog("算法执行完毕", LogType.OtherLog);
                using (StreamWriter sw = new StreamWriter(resultFile))
                {
                    sw.WriteLine(json);
                }
                if (!string.IsNullOrEmpty(json))
                {
                    AddLog("算法结果：" + json, LogType.OtherLog);
                    Project.ResultBack(id, json);
                }
            }
        }

        private void AlgorithmPage_Load(object sender, EventArgs e)
        {
            resultDir = Application.StartupPath + "\\bak_result\\";
            if (!Directory.Exists(resultDir))
            {
                Directory.CreateDirectory(resultDir);
            }
            AddLogEvent += LogManager_AddLogEvent;
            Process.Start(Application.StartupPath + "\\AlgorithmControl.exe");
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            if (arg2 == LogType.OtherLog)
            {
                logCount++;
                string s = arg1 + "\r\n";
                BeginInvoke(new Action(() =>
                {
                    if (logCount >= 5000)
                    {
                        logCount = 0;
                        textBox1.Text = "";
                    }
                    textBox1.Text += s;
                    textBox1.SelectionStart = textBox1.Text.Length - 1;
                    textBox1.ScrollToCaret();
                }));
            }
        }

        private void SetAlgorithmPars(params string[] pars)
        {
            if (pars != null && !IsDisposed)
            {
                BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i < pars.Length; i++)
                    {
                        if (i < flowLayoutPanel1.Controls.Count)
                        {
                            flowLayoutPanel1.Controls[i].Text = pars[i];
                            flowLayoutPanel1.Controls[i].Size = TextRenderer.MeasureText(pars[i], flowLayoutPanel1.Controls[i].Font);
                        }
                        else
                        {
                            Label lb = new Label();
                            lb.Text = pars[i];
                            lb.AutoSize = false;
                            lb.Size = TextRenderer.MeasureText(lb.Text, lb.Font);
                            flowLayoutPanel1.Controls.Add(lb);
                        }
                    }
                }));
            }
        }
    }
}
