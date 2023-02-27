using AlgorithmLib;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
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
        private string bakPath = Application.StartupPath + "\\bak_img\\";
        private string resultDir = "";
        private int init = -1;
        private IntPtr ptr = IntPtr.Zero;

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
            while (init != 0)
            {
                Thread.Sleep(3000);
            }
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
                int width1 = bitmap1.Width;
                int height1 = bitmap1.Height;
                int width2 = bitmap2.Width;
                int height2 = bitmap2.Height;
                byte[] bytes1 = bitmap1.ToBytes2(true);
                byte[] bytes2 = bitmap2.ToBytes2(true);
                string[] tmp = type.Split(';');
                int[] taskIds = new int[tmp.Length];
                for (int i = 0; i < taskIds.Length; i++)
                {
                    taskIds[i] = int.Parse(tmp[i].Trim());
                }
                model_struct[] models = null;
                if (businesses != null && businesses.Length > 0)
                {
                    List<model_struct> ms = new List<model_struct>();
                    foreach (RedisBusiness business in businesses)
                    {
                        ms.AddRange(business.TaskList);
                    }
                    models = ms.ToArray();
                }

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
                    while (monitor)
                    {
                        if (i >= outTime)
                        {
                            AddLog("算法超时", LogType.OtherLog);
                            break; 
                        }
                        i++;
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

                int len = 0;
                IntPtr rPtr = IntPtr.Zero;
                ThreadManager.TaskRun((ThreadEventArgs tea) =>
                {
                    tea.ThreadName = "算法运行线程";
                    if (init == 0)
                    {
                        rPtr = Algorithm.NewCallgetres(ptr, bytes1, width1, height1, bytes2, width2, height2, taskIds, taskIds.Length, models, models == null ? 0 : models.Length, ref len);
                        GC.Collect();
                    }
                    monitor = false;
                });
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

                json = "";
                box_info[] boxes = null;
                if (rPtr == IntPtr.Zero)
                {
                    return;
                }
                boxes = new box_info[len];
                int box_length = System.Runtime.InteropServices.Marshal.SizeOf(typeof(box_info));
                long _len = rPtr.ToInt64();
                for (int j = 0; j < len; j++)
                {
                    boxes[j] = System.Runtime.InteropServices.Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * box_length)));
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
                boxes = null;
                bytes1 = null;
                bytes2 = null;
                models = null;
                tmp = null;
                taskIds = null;
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
            ThreadManager.TaskRun((ThreadEventArgs eventArgs) =>
            {
                eventArgs.ThreadName = "算法初始化线程";
            Run:
                ptr = Algorithm.ExportObjectFactory();
                AddLog("算法启动", LogType.OtherLog);
                init = Algorithm.CallOnInit(ptr, null);
                AddLog("算法初始化: " + init, LogType.OtherLog);
                if (init != 0)
                {
                    AddLog("3秒后重新初始化算法", LogType.OtherLog);
                    Thread.Sleep(3000);
                    goto Run;
                }
            });
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
