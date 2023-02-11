using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static EMU.Util.LogManager;

namespace Project
{
    public partial class AlgorithmPage : UserControl
    {
        private int logCount = 0;

        public AlgorithmInterface Project { get; set; }

        public AlgorithmPage()
        {
            InitializeComponent();
        }

        private void AlgorithmPage_Load(object sender, EventArgs e)
        {
            AddLogEvent += LogManager_AddLogEvent;
            ThreadManager.BackTask((int startIndex, ThreadEventArgs threadEventArgs) =>
            {
                List<ThreadEventArgs> list = ThreadManager.GetThreadEventArgs();
                ThreadEventArgs eventArgs = list.Find(t => t.ThreadName == Project.RedisThreadName);
                if ((bool)eventArgs.GetVariableValue(Project.algorithmRunStart, false))
                {
                    eventArgs.SetVariableValue(Project.algorithmRunStart, false);
                }
                else
                {
                    return;
                }
                if (eventArgs != null)
                {
                    string json = eventArgs.GetVariableValue(Project.inParObject, "").ToString();
                    string url_now = eventArgs.GetVariableValue(Project.inParName2, "").ToString();
                    string url_up = eventArgs.GetVariableValue(Project.inParName3, "").ToString();
                    string type = eventArgs.GetVariableValue(Project.inParName1, "").ToString();
                    string id = eventArgs.GetVariableValue(Project.taskID, "").ToString();
                    AddLog("识别类型：" + type, LogType.OtherLog);
                    AddLog("坐标Json：" + json, LogType.OtherLog);
                    AddLog("本次图片url：" + url_now, LogType.OtherLog);
                    AddLog("上次图片url：" + url_up, LogType.OtherLog);

                    if (!string.IsNullOrEmpty(url_now) && !string.IsNullOrEmpty(json) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
                    {
                        string resultFile = Application.StartupPath + "\\AlgorithmResult.json";
                        if (File.Exists(resultFile))
                        {
                            AddLog("删除结果文件", LogType.OtherLog);
                            File.Delete(resultFile);
                        }
                        AddLog("启动算法", LogType.OtherLog);
                        Process da = new Process();
                        da.StartInfo.FileName = Application.StartupPath + "\\AlgorithmControl.exe";
                        da.StartInfo.Arguments = string.Format("{0} {1} {2} {3}", url_now, json.Replace("\"", "&&"), type, id);
                        da.Start();
                        da.WaitForExit();
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
                            eventArgs.SetVariableValue(Project.outParName, json);
                        }
                        eventArgs.SetVariableValue(Project.algorithmComplete, true);
                        eventArgs.SetVariableValue(Project.algorithmRunStart, false);
                        AddLog("完成状态回写", LogType.OtherLog);
                    }
                }
            });
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
    }
}
