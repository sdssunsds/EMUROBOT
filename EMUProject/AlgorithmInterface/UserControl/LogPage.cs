using EMU.Util;
using System;
using System.IO;
using System.Windows.Forms;

namespace Project
{
    public partial class LogPage : UserControl
    {
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
        }

        private void LogManager_AddLogEvent(string arg1, EMU.Parameter.LogType arg2)
        {
            logCount++;
            arg1 += "\r\n";
            WriteLogFile(arg1, arg2);
            BeginInvoke(new Action(() =>
            {
                if (logCount >= 5000)
                {
                    logCount = 0;
                    textBox1.Text = "";
                }
                textBox1.Text += arg1;
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
                    logPath += "report.log";
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
