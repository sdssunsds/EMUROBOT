#define readFile
//#define outTime
//#define testData

using AlgorithmLib;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
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
        private int box_length = 0;
        private int exeCount = 0;
        private int logCount = 0;
        private string bakPath = Application.StartupPath + "\\bak_img\\";
#if readFile
        private string errorPath = Application.StartupPath + "\\bak_error\\";
#endif
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
            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }
            box_length = Marshal.SizeOf(typeof(box_info));
        }

        public void InitAlgorithm()
        {
            ThreadManager.TaskRun((ThreadEventArgs eventArgs) =>
            {
                lock (runLock)
                {
                    eventArgs.ThreadName = "算法初始化线程";
                    if (init < 0)
                    {
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
                    }
                }
            });
        }

        public bool RunAlgorithm(string mode, string sn, string partId, string type, string json, string url_now, string url_up, string id, ThreadEventArgs eventArgs, bool isTest = false, bool isRun = true)
        {
            while (init != 0 && isRun)
            {
                Thread.Sleep(3000);
            }

            #region 算法多线程控制
            lock (runLock)
            {
                bool showLog = false;
                while (exeCount >= algorithmMax)
                {
                    if (!showLog)
                    {
                        showLog = true;
                        AddLog("等待前一个算法运行完成", LogType.OtherLog);
                    }
                    Thread.Sleep(50);
                }
                exeCount++;
            }
            #endregion

            #region 数据转换
            eventArgs?.SetVariableValue("车型", mode);
            eventArgs?.SetVariableValue("车号", sn);
            eventArgs?.SetVariableValue("部件编号", partId);
            eventArgs?.SetVariableValue("任务编号", id);
            eventArgs?.SetVariableValue("识别类型", type);
            eventArgs?.SetVariableValue("本次图片", url_now);
            eventArgs?.SetVariableValue("上次图片", url_up);
            eventArgs?.SetVariableValue("原始版Json", json);
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
#if testData
            string imgPath1 = url_now;
#else
            string imgPath1 = bakPath + id + ".jpg";
            Bitmap bitmap1 = null;
            if (isTest)
            {
                bitmap1 = (Bitmap)Image.FromFile(url_now);
                bitmap1.Save(imgPath1);
            }
            else
            {
                bitmap1 = GW.Function.ImageFunction.Manage.ImageManage.Download(url_now);
                bitmap1.Save(imgPath1); 
            }
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
                if (!uncorrect.Contains(partId))
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
                                            model.class_name = new char[50];
                                            for (int i = 0; i < name.Length; i++)
                                            {
                                                model.class_name[i] = name[i];
                                            }
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
#if outTime
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
#endif
            #endregion

            #region 算法调用线程
            int len = 0;
            IntPtr rPtr = IntPtr.Zero;
            ThreadManager.TaskRun((ThreadEventArgs tea) =>
            {
                tea.ThreadName = "算法运行线程";
                if (init == 0)
                {
                    GC.Collect();
#if readFile
                    string mjson = JsonManager.ObjectToJson(models);
                    AddLog("调用算法：[Callgetres]", LogType.OtherLog);
                    AddLog("\t>> " + mode, LogType.OtherLog);
                    AddLog("\t>> " + sn, LogType.OtherLog);
                    AddLog("\t>> " + partId, LogType.OtherLog);
                    AddLog("\t>> " + imgPath1, LogType.OtherLog);
                    AddLog("\t>> " + upImgPath, LogType.OtherLog);
                    AddLog("\t>> " + modelPath, LogType.OtherLog);
                    AddLog("\t>> " + string.Join(",", taskIds), LogType.OtherLog);
                    AddLog("\t>> " + mjson, LogType.OtherLog);
                    try
                    {
                        if (isRun)
                        {
                            rPtr = Algorithm.Callgetres(ptr, imgPath1, upImgPath, modelPath, taskIds, taskIds.Length, models.ToArray(), models.Count, ref len); 
                        }
                    }
                    catch (SEHException e)
                    {
                        AddLog(e.Message + "\r\n" + e.StackTrace, LogType.ErrorLog);
                        try
                        {
                            string error = errorPath + id + "\\";
                            if (!Directory.Exists(error))
                            {
                                Directory.CreateDirectory(error);
                            }
                            File.Copy(imgPath1, error + "被检测图.jpg");

                            if (File.Exists(upImgPath))
                            {
                                File.Copy(upImgPath, error + "模板图片.jpg");
                            }

                            string file = error + "参数文件.txt";
                            using (StreamWriter sw = new StreamWriter(file))
                            {
                                sw.WriteLine(partId);
                                sw.WriteLine(modelPath);
                                sw.WriteLine(type);
                                sw.WriteLine(mjson);
                            }
                        }
                        catch (Exception ex)
                        {
                            AddLog(ex.Message, LogType.ErrorLog);
                        }
                    }
#else
                    AddLog("调用算法：[NewCallgetres]", LogType.OtherLog);
                    rPtr = Algorithm.NewCallgetres(ptr, bytes1, width1, height1, bytes2, width2, height2, taskIds, taskIds.Length, models, models == null ? 0 : models.Length, ref len);
#endif
                    AddLog("收到算法返回的结果句柄：" + rPtr, LogType.OtherLog);
                    GC.Collect();
                }
                monitor = false;
            });
#if outTime
            thread.Start(); 
#endif
            while (monitor)
            {
                Thread.Sleep(50);
            }
            exeCount--;
#if outTime
            if (thread.IsAlive)
            {
                thread.Abort();
            } 
#endif
            #endregion

            #region 算法返回值解析
#if readFile
            bool isNormal = true;
#else
            json = "";
#endif
            box_info[] boxes = null;
            if (isRun && rPtr == IntPtr.Zero)
            {
                AddLog("未能获得算法结果", LogType.OtherLog);
                return false;
            }
            if (isRun)
            {
                boxes = new box_info[len];
                AddLog("创建结果对象数组", LogType.OtherLog);
                long _len = rPtr.ToInt64();
                for (int i = 0; i < len; i++)
                {
                    boxes[i] = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + i * box_length)));
#if readFile
                    if (isNormal && boxes[i].state_enum != 0)
                    {
                        isNormal = false;
                    }
#endif
                }
            }
            else
            {
                boxes = new box_info[100];
                AddLog("生成虚拟结果", LogType.GeneralLog);
                for (int i = 0; i < 100; i++)
                {
                    boxes[i] = new box_info();
                    boxes[i].class_name = new char[50];
                    boxes[i].state_enum = boxes[i].x = boxes[i].y = boxes[i].w = boxes[i].h = 0;
                }
            }
            #endregion

            #region 结果数据转换，回写Redis用
            List<RedisResult> list = new List<RedisResult>();
            AddLog("创建回写Redis的数据集合", LogType.OtherLog);
            string code = "";
            Dictionary<int, string> codeId = new Dictionary<int, string>();
            foreach (box_info box in boxes)
            {
                code = box.state_enum.ChangeCode();
                if (!codeId.ContainsKey(box.state_enum))
                {
                    codeId.Add(box.state_enum, code);
                    AddLog("映射结果码 " + box.state_enum + " >> " + code, LogType.OtherLog); 
                }
                RedisResult result = list.Find(r => r.jclx == code);
                if (result == null)
                {
                    result = new RedisResult()
                    {
                        jclx = code,
                        result = new List<Rectangle>()
                    };
                    AddLog("未找到数据对象，并开始生成新的数据对象", LogType.OtherLog);
                    list.Add(result);
                    AddLog("添加数据对象", LogType.OtherLog);
                }
                result.result.Add(new Rectangle(box.x, box.y, box.w, box.h));
            }
            #endregion

            #region 清理内存
            AddLog("开始清理内存", LogType.OtherLog);
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
                AddLog("开始记录本次正常结果图像到历史中", LogType.OtherLog);
                string[] files = Directory.GetFiles(modelPath);
                if (files != null && files.Length >= modelFileMax)
                {
                    int d = files.Length - modelFileMax + 1;
                    string name = "";
                    for (int i = 0; i < d; i++)
                    {
                        name = modelPath + i.ToString("00") + ".jpg";
                        if (File.Exists(name))
                        {
                            File.Delete(name);
                        }
                    }
                    files = Directory.GetFiles(modelPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Move(files[i], modelPath + i.ToString("00") + ".jpg");
                        File.Delete(files[i]);
                    }
                }
                int index = files != null ? files.Length : 0;
                File.Copy(imgPath1, modelPath + index.ToString("00") + ".jpg");
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
#if testData
                AddLog("得到算法编号：" + id, LogType.GeneralLog);
                AddLog("得到算法结果：" + _json, LogType.GeneralLog);
#else
                if (!isTest)
                {
                    Project.ResultBack(id, _json);  
                }
#endif
            }
            #endregion

            return true;
        }

        public void TestRunAlgorithm()
        {
#if testData
            Thread.Sleep(5000);
            string[] redisFiles = Directory.GetFiles(Application.StartupPath + "\\bak_redis");
            string txt = "";
            foreach (string item in redisFiles)
            {
                string taskId = new FileInfo(item).Name;
                using (StreamReader sr = new StreamReader(item))
                {
                    txt = sr.ReadToEnd();
                }
                taskId = taskId.Replace(".txt", "");
                string resultPath = Application.StartupPath + "\\bak_result\\" + taskId + ".json";
                if (File.Exists(resultPath))
                {
                    string json = "";
                    using (StreamReader sr = new StreamReader(resultPath))
                    {
                        json = sr.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(json))
                    {
                        bool pass = true;
                        List<RedisResult> rr = JsonManager.JsonToObject<List<RedisResult>>(json);
                        foreach (RedisResult r in rr)
                        {
                            if (r.jclx == "0107")
                            {
                                pass = false;
                                break;
                            }
                        }
                        if (pass)
                        {
                            Thread.Sleep(1);
                            continue;
                        }
                    }
                }
                AddLog("读取的文件名：" + taskId + ".txt", LogType.ProcessLog);
                AddLog("读取文件内容：" + txt, LogType.ProcessLog);
                if (!string.IsNullOrEmpty(txt))
                {
                    string[] args = txt.Split('&');
                    if (args.Length > 6)
                    {
                        if (!RunAlgorithm(args[0], args[1], args[2], args[3], args[4], Application.StartupPath + "\\bak_img\\" + taskId + ".jpg", args[6], taskId, null))
                        {
                            AddLog("算法执行失败", LogType.GeneralLog);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
            AddLog("执行完毕", LogType.GeneralLog);
            AddLog("==============================", LogType.ProcessLog);
            AddLog("30秒后重复...", LogType.ProcessLog);
            Thread.Sleep(30000);
#endif
        }

        private void AlgorithmPage_Load(object sender, EventArgs e)
        {
            resultDir = Application.StartupPath + "\\bak_result\\";
            if (!Directory.Exists(resultDir))
            {
                Directory.CreateDirectory(resultDir);
            }
            AddLogEvent += LogManager_AddLogEvent;
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            if (arg2 == LogType.OtherLog)
            {
                logCount++;
                string s = arg1 + "\r\n";
                BeginInvoke(new Action(() =>
                {
                    if (logCount >= 100)
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
