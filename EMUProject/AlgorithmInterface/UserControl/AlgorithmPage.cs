﻿using EMU.Parameter;
using GW.Function.ComputerFunction;
using System;
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
        private string resultDir = "";

        public IAlgorithmInterface Project { get; set; }

        public AlgorithmPage()
        {
            InitializeComponent();
        }

        public void RunAlgorithm(string type, string json, string url_now, string url_up, string id, EMU.Util.ThreadEventArgs eventArgs)
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
                json = json.Replace(" ", "").Replace("\"", "&&");
                eventArgs.SetVariableValue("传输用Json", json);
                AddLog("任务编号：" + id, LogType.OtherLog);
                AddLog("识别类型：" + type, LogType.OtherLog);
                AddLog("坐标Json：" + json, LogType.OtherLog);
                AddLog("本次图片：" + url_now, LogType.OtherLog);
                AddLog("上次图片：" + url_up, LogType.OtherLog);
                string resultFile = resultDir + "\\" + id + ".json";
                if (File.Exists(resultFile))
                {
                    AddLog("删除结果文件", LogType.OtherLog);
                    File.Delete(resultFile);
                }
                SetAlgorithmPars(url_now, url_up, json, type, id);
                AddLog("启动算法", LogType.OtherLog);
                bool monitor = true;
                Process da = new Process();
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    int i = 0;
                    string path = Application.StartupPath + "\\bak_img\\" + id + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    while (monitor)
                    {
                        if (i >= 60)
                        {
                            da.Kill();
                            break;
                        }
                        try
                        {
                            Bitmap bitmap = Computer.GetScreenImgByteArray();
                            bitmap.Save(path + i + ".jpg");
                            i++;
                        }
                        catch (Exception) { }
                        Thread.Sleep(500);
                    }
                }));
                da.StartInfo.FileName = Application.StartupPath + "\\AlgorithmControl.exe";
                da.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4}", id, type, url_now, url_up, json);
                lock (runLock)
                {
                    while (exeCount >= algorithmMax)
                    {
                        Thread.Sleep(50);
                    }
                    exeCount++;
                }
                da.Start();
                thread.Start();
                da.WaitForExit();
                monitor = false;
                exeCount--;
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
                AddLog("算法执行完毕", LogType.OtherLog);
                json = "";
                if (File.Exists(resultFile))
                {
                    using (StreamReader sr = new StreamReader(resultFile))
                    {
                        json = sr.ReadToEnd();
                    }
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
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            if (arg2 == LogType.OtherLog)
            {
                logCount++;
                BeginInvoke(new Action(() =>
                {
                    if (logCount >= 5000)
                    {
                        logCount = 0;
                        textBox1.Text = "";
                    }
                    textBox1.Text += arg1 + "\r\n";
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
