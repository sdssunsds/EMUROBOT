using EMU.Util;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Project
{
    public partial class LogPage : UserControl
    {
        private const int clearMaxDay = 3;
        private const int clearMaxDayForLog = 30;

        private int logCount = 0;
        private string logDir = "";
        private object writeLock = new object();
        private DateTime startTime;

        public LogPage()
        {
            InitializeComponent();
        }

        private void LogPage_Load(object sender, EventArgs e)
        {
            CheckLogDir();
            LogManager.AddLogEvent += LogManager_AddLogEvent;

            ThreadManager.BackTask((int i, ThreadEventArgs eventArgs) =>
            {
                eventArgs.ThreadName = "文件清理线程";

                if (i % 172800000 == 0)  // 等待一天
                {
                    string[] dirs = Directory.GetDirectories(Application.StartupPath + "\\log");
                    if (dirs != null)
                    {
                        foreach (string item in dirs)
                        {
                            DirectoryInfo directory = new DirectoryInfo(item);
                            if ((DateTime.Now - directory.CreationTime).TotalDays > clearMaxDayForLog)
                            {
                                eventArgs.SetVariableValue("正在清理的文件夹", item);
                                Directory.Delete(item, true);
                            }
                        }
                    }

                    DeleteDirFiles(Application.StartupPath + "\\bak_img", eventArgs);
                    DeleteDirFiles(Application.StartupPath + "\\bak_redis", eventArgs);
                    DeleteDirFiles(Application.StartupPath + "\\bak_result", eventArgs); 
                }
            });
        }

        private void LogManager_AddLogEvent(string arg1, EMU.Parameter.LogType arg2)
        {
            logCount++;
            string s = arg1 + "\r\n";
            WriteLogFile(s, arg2);
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

        private void CheckLogDir()
        {
            startTime = DateTime.Now;
            logDir = Application.StartupPath + "\\log\\" + startTime.ToString("yyyy-MM-dd") + "\\";
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        private void DeleteDirFiles(string dirPath, ThreadEventArgs eventArgs)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
            FileInfo[] files = directoryInfo.GetFiles();
            if (files != null)
            {
                foreach (FileInfo item in files)
                {
                    if ((DateTime.Now - item.CreationTime).TotalDays > clearMaxDay)
                    {
                        eventArgs.SetVariableValue("正在清理的文件", item.FullName);
                        File.Delete(item.FullName);
                    }
                }
            }
        }

        private void WriteLogFile(string log, EMU.Parameter.LogType type)
        {
            DateTime now = DateTime.Now;
            if (startTime.Day != now.Day || startTime.Month != now.Month || startTime.Year != now.Year)
            {
                CheckLogDir();
            }
            string time = now.ToString("HH:mm:ss");
            string logPath = logDir;
            switch (type)
            {
                case EMU.Parameter.LogType.GeneralLog:
                    logPath += "result.log";
                    break;
                case EMU.Parameter.LogType.ProcessLog:
                    logPath += "inport.log";
                    break;
                case EMU.Parameter.LogType.ErrorLog:
                    logPath += "error.log";
                    break;
                case EMU.Parameter.LogType.TestLog:
                    break;
                case EMU.Parameter.LogType.CameraLog:
                    break;
                case EMU.Parameter.LogType.RgvLog:
                    break;
                case EMU.Parameter.LogType.RobotLog:
                    break;
                case EMU.Parameter.LogType.OtherLog:
                    logPath += "algorithm.log";
                    break;
            }
            lock (writeLock)
            {
                using (StreamWriter sw = new StreamWriter(logPath, true))
                {
                    sw.Write(time + "\t" + log);
                } 
            }
        }
    }
}
