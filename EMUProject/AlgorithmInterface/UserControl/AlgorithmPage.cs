#define readFile

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
        private const int modelFileMax = 10;

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

        public bool RunAlgorithm(string mode, string sn, string partId, string type, string json, string url_now, string url_up, string id, ThreadEventArgs eventArgs)
        {
            while (init != 0)
            {
                Thread.Sleep(3000);
            }
            #region 数据转换
            eventArgs.SetVariableValue("车型", mode);
            eventArgs.SetVariableValue("车号", sn);
            eventArgs.SetVariableValue("部件编号", partId);
            eventArgs.SetVariableValue("任务编号", id);
            eventArgs.SetVariableValue("识别类型", type);
            eventArgs.SetVariableValue("本次图片", url_now);
            eventArgs.SetVariableValue("上次图片", url_up);
            eventArgs.SetVariableValue("原始版Json", json);
#if !readFile
            json = Project.JsonErrorChange(json);
            eventArgs.SetVariableValue("修正后Json", json); 
#endif
            AddLog("车型：" + mode, LogType.OtherLog);
            AddLog("车号：" + sn, LogType.OtherLog);
            AddLog("部件编号：" + partId, LogType.OtherLog);
            AddLog("任务编号：" + id, LogType.OtherLog);
            AddLog("识别类型：" + type, LogType.OtherLog);
            AddLog("坐标Json：" + json, LogType.OtherLog);
            AddLog("本次图片：" + url_now, LogType.OtherLog);
            AddLog("上次图片：" + url_up, LogType.OtherLog);
            AddLog("下载图片：" + url_now, LogType.OtherLog);
            AddLog("转换Json", LogType.OtherLog);
#if !readFile
            RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(json);
            AddLog("Json转换完成", LogType.OtherLog); 
#endif
            #endregion

            #region 图片下载
            string imgPath1 = bakPath + id + ".jpg";
            Bitmap bitmap1 = GW.Function.ImageFunction.Manage.ImageManage.Download(url_now);
            bitmap1.Save(imgPath1);
            AddLog("缓存完成：" + url_now + " 本地路径：" + imgPath1, LogType.OtherLog);
#if readFile
            bitmap1.Dispose();
            bitmap1 = null;
#else
            string imgPath2 = bakPath + id + "_up.jpg";
            Bitmap bitmap2 = GW.Function.ImageFunction.Manage.ImageManage.Download(url_up);
            bitmap2.Save(imgPath2);
            AddLog("缓存完成：" + url_up + " 本地路径：" + imgPath2, LogType.OtherLog);
            int width1 = bitmap1.Width;
            int height1 = bitmap1.Height;
            int width2 = bitmap2.Width;
            int height2 = bitmap2.Height;
            byte[] bytes1 = bitmap1.ToBytes2(true);
            byte[] bytes2 = bitmap2.ToBytes2(true); 
#endif
            #endregion

            #region 参数解析
            string[] tmp = type.Split(';');
            int[] taskIds = new int[tmp.Length];
            for (int i = 0; i < taskIds.Length; i++)
            {
                taskIds[i] = int.Parse(tmp[i].Trim());
            }
#if readFile
            string mubanPath = Application.StartupPath + "\\muban\\";
            string upImgPath = mubanPath + sn + "\\" + partId + ".jpg";
            if (!File.Exists(upImgPath))
            {
                upImgPath = "";
            }

            string csvPath = mubanPath + mode + "_" + sn + ".csv";
            if (!File.Exists(csvPath))
            {
                AddLog("缺失的配置文件: " + csvPath, LogType.OtherLog);
                return false;
            }

            string uncorrectPath = mubanPath + "uncorrect.txt";
            if (File.Exists(uncorrectPath))
            {
                string uncorrect = "";
                using (StreamReader sr = new StreamReader(uncorrectPath))
                {
                    uncorrect = sr.ReadToEnd();
                }
                if (uncorrect.Contains(partId))
                {
                    List<int> tmpTask = new List<int>();
                    tmpTask.AddRange(taskIds);
                    tmpTask.Add(100);
                    taskIds = tmpTask.ToArray();
                    tmpTask = null;
                    AddLog("部件编号[" + partId + "]需要图像校正", LogType.OtherLog);
                }
            }
            else
            {
                AddLog("缺失的配置文件: " + uncorrectPath, LogType.OtherLog);
                return false;
            }

            List<model_struct> models = new List<model_struct>();
            using (StreamReader sr = new StreamReader(csvPath))
            {
                while (true)
                {
                    string row = sr.ReadLine();
                    if (string.IsNullOrEmpty(row))
                    {
                        break;
                    }
                    if (row.IndexOf(partId) == 0)
                    {
                        string pars = row.Substring(row.IndexOf(",") + 1);
                        string name = pars.Substring(0, pars.IndexOf(","));
                        string loc = pars.Substring(pars.IndexOf(",") + 1);
                        loc = loc.Replace("\"", "");
                        string[] locs = loc.Split(';');
                        AddLog("找到模板参数: " + row, LogType.OtherLog);
                        if (locs != null)
                        {
                            foreach (string item in locs)
                            {
                                string[] s = item.Split(',');
                                if (s != null && s.Length > 3)
                                {
                                    model_struct model = new model_struct();
                                    SetErrorLocation(
                                        int.TryParse(s[0], out model.x) &&
                                        int.TryParse(s[1], out model.y) &&
                                        int.TryParse(s[2], out model.w) &&
                                        int.TryParse(s[3], out model.h), partId, mode, sn, item,
                                        () =>
                                        {
                                            model.class_name = name.ToCharArray();
                                            models.Add(model);
                                        });
                                }
                            }
                        }
                        break;
                    }
                }
            }
#else
            model_struct[] models = null;
            if (businesses != null && businesses.Length > 0)
            {
                List<model_struct> ms = new List<model_struct>();
                foreach (RedisBusiness business in businesses)
                {
                    ms.AddRange(business.TaskList);
                }
                AddLog("解析的参数：" + JsonManager.ObjectToJson(ms), LogType.OtherLog);
                models = ms.ToArray();
            }
            if (models == null)
            {
                models = new model_struct[0];
            }
#endif
            #endregion

            #region 历史结果处理
            string resultFile = resultDir + "\\" + id + ".json";
            if (File.Exists(resultFile))
            {
                AddLog("删除结果文件", LogType.OtherLog);
                File.Delete(resultFile);
            }
            string modelPath = Application.StartupPath + "\\model\\" + mode + "\\" + sn + "\\" + partId + "\\";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
                AddLog("创建模板目录：" + modelPath, LogType.OtherLog);
            }
            SetAlgorithmPars("本次识别图片：" + imgPath1, "模板图片：" + upImgPath, "往次图片目录：" + modelPath, "车型：" + mode, "车号：" + sn, "识别任务：" + type, "业务编号：" + id, "部件编号：" + partId);
            #endregion

            #region 算法超时监控线程
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
            #endregion

            #region 算法多线程控制
            lock (runLock)
            {
                while (exeCount >= algorithmMax)
                {
                    Thread.Sleep(50);
                }
                exeCount++;
            }
            #endregion

            #region 算法调用线程
            int len = 0;
            IntPtr rPtr = IntPtr.Zero;
            ThreadManager.TaskRun((ThreadEventArgs tea) =>
            {
                tea.ThreadName = "算法运行线程";
                if (init == 0)
                {
#if readFile
                    AddLog("调用算法：[Callgetres]", LogType.OtherLog);
                    rPtr = Algorithm.Callgetres(ptr, imgPath1, upImgPath, modelPath, taskIds, taskIds.Length, models.ToArray(), models.Count, ref len);
#else
                    AddLog("调用算法：[NewCallgetres]", LogType.OtherLog);
                    rPtr = Algorithm.NewCallgetres(ptr, bytes1, width1, height1, bytes2, width2, height2, taskIds, taskIds.Length, models, models == null ? 0 : models.Length, ref len);
#endif
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
            #endregion

            #region 算法返回值解析
#if readFile
            bool isNormal = true;
#else
            json = "";
#endif
            box_info[] boxes = null;
            if (rPtr == IntPtr.Zero)
            {
                AddLog("未能获得算法结果", LogType.OtherLog);
                return false;
            }
            boxes = new box_info[len];
            int box_length = System.Runtime.InteropServices.Marshal.SizeOf(typeof(box_info));
            long _len = rPtr.ToInt64();
            for (int i = 0; i < len; i++)
            {
                boxes[i] = System.Runtime.InteropServices.Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + i * box_length)));
#if readFile
                if (isNormal && boxes[i].state_enum != 0)
                {
                    isNormal = false;
                } 
#endif
            }
            #endregion

            #region 结果数据转换，回写Redis用
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
            #endregion

            #region 清理内存
            boxes = null;
#if !readFile
            bytes1 = null;
            bytes2 = null; 
#endif
            models = null;
            tmp = null;
            taskIds = null;
            GC.Collect();
            #endregion

            #region 记录结果并返回
#if readFile
            if (isNormal)
            {
                string[] files = Directory.GetFiles(modelPath);
                if (files != null && files.Length >= modelFileMax)
                {
                    int d = files.Length - modelFileMax - 1;
                    string name = "";
                    for (int i = 0; i < d; i++)
                    {
                        name = modelPath + i + ".jpg";
                        if (File.Exists(name))
                        {
                            File.Delete(name);
                        }
                    }
                    files = Directory.GetFiles(modelPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Move(files[i], modelPath + i + ".jpg");
                    }
                }
                int index = files != null ? files.Length : 0;
                File.Copy(imgPath1, modelPath + index + ".jpg");
            } 
#endif
            string _json = JsonManager.ObjectToJson(list);
            AddLog("算法执行完毕", LogType.OtherLog);
            using (StreamWriter sw = new StreamWriter(resultFile))
            {
                sw.WriteLine(_json);
            }
            if (!string.IsNullOrEmpty(_json))
            {
                AddLog("算法结果：" + _json, LogType.OtherLog);
                Project.ResultBack(id, _json);
            }
            #endregion

            return true;
        }

        private void AlgorithmPage_Load(object sender, EventArgs e)
        {
            resultDir = Application.StartupPath + "\\bak_result\\";
            if (!Directory.Exists(resultDir))
            {
                Directory.CreateDirectory(resultDir);
            }
            AddLogEvent += LogManager_AddLogEvent;
#if true
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
#endif
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

        private void SetErrorLocation(bool b, string partId, string mode, string sn, string loc, Action act)
        {
            if (b)
            {
                act();
            }
            else
            {
                AddLog($"模板参数解析失败: {mode}.{sn}.{partId} >> {loc}", LogType.OtherLog); 
            }
        }
    }
}
