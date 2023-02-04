using EMU.Util;
using GW.Function.FileFunction;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Project
{
    public partial class MainPage : UserControl
    {
        private int reportCount = 0;
        private int resultCount = 0;
        private StringBuilder sb = new StringBuilder();

        public AlgorithmInterface Project { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            tb_redis_url.Text = FileSystem.ReadIniFile("Redis", "Url", "", Project.PathParameter1);
            tb_redis_input.Text = FileSystem.ReadIniFile("Redis", "Input", "", Project.PathParameter1);
            tb_redis_output.Text = FileSystem.ReadIniFile("Redis", "OutPut", "", Project.PathParameter1);
            tb_redis_internal.Text = FileSystem.ReadIniFile("Redis", "Internal", "100", Project.PathParameter1);

            tb_redis_url.TextChanged += Tb_redis_url_TextChanged;
            tb_redis_input.TextChanged += Tb_redis_input_TextChanged;
            tb_redis_output.TextChanged += Tb_redis_output_TextChanged;
            tb_redis_internal.TextChanged += Tb_redis_internal_TextChanged;

            LogManager.AddLogEvent += LogManager_AddLogEvent;

            Project.RunInterface(tb_redis_url.Text, tb_redis_input.Text, tb_redis_output.Text, int.Parse(tb_redis_internal.Text));
            ThreadManager.BackTask((int startIndex, ThreadEventArgs threadEventArgs) =>
            {
                List<ThreadEventArgs> list = ThreadManager.GetThreadEventArgs();
                ThreadEventArgs eventArgs = list.Find(t => t.ThreadName == Project.RedisThreadName);
                if (eventArgs != null)
                {
                    sb.Clear();
                    
                    BeginInvoke(new Action(() =>
                    {
                        tb_redis.Text = sb.ToString();
                    })); 
                }
            });
        }

        private void LogManager_AddLogEvent(string arg1, EMU.Parameter.LogType arg2)
        {
            if (arg2 == EMU.Parameter.LogType.ProcessLog)
            {
                reportCount++;
                BeginInvoke(new Action(() =>
                {
                    if (reportCount >= 5000)
                    {
                        reportCount = 0;
                        tb_redis_report.Text = "";
                    }
                    tb_redis_report.Text += arg1 + "\r\n";
                    tb_redis_report.SelectionStart = tb_redis_report.Text.Length - 1;
                }));
            }
            else if (arg2 == EMU.Parameter.LogType.GeneralLog)
            {
                resultCount++;
                BeginInvoke(new Action(() =>
                {
                    if (resultCount >= 5000)
                    {
                        resultCount = 0;
                        tb_redis_result.Text = "";
                    }
                    tb_redis_result.Text += arg1 + "\r\n";
                    tb_redis_result.SelectionStart = tb_redis_result.Text.Length - 1;
                }));
            }
        }

        private void Tb_redis_url_TextChanged(object sender, EventArgs e)
        {
            SaveOperation("Url", tb_redis_url.Text);
        }

        private void Tb_redis_input_TextChanged(object sender, EventArgs e)
        {
            SaveOperation("Input", tb_redis_input.Text);
        }

        private void Tb_redis_output_TextChanged(object sender, EventArgs e)
        {
            SaveOperation("OutPut", tb_redis_output.Text);
        }

        private void Tb_redis_internal_TextChanged(object sender, EventArgs e)
        {
            SaveOperation("Internal", tb_redis_internal.Text);
        }

        private void SaveOperation(string key, string value)
        {
            try
            {
                Project.RunInterface(tb_redis_url.Text, tb_redis_input.Text, tb_redis_output.Text, int.Parse(tb_redis_internal.Text));
                FileSystem.WriteIniFile("Redis", key, value, Project.PathName1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
