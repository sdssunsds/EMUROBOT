using EMU.Parameter;
using GW.Function.FileFunction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using UploadImageServer;
using static EMU.Util.LogManager;

namespace Project
{
    public partial class MainPage : UserControl
    {
        private int reportCount = 0;
        private int resultCount = 0;
        private string urlKey = "Url", inputKey = "Input", outputKey = "OutPut", internalKey = "Internal";
        private ServiceHost host = null;
        private Dictionary<string, string> pairs;

        public static string Pwd = "";

        public bool isOpenControl
        {
            set
            {
                if (value)
                {
                    Process[] ps = Process.GetProcessesByName("AlgorithmControl");
                    if (ps == null || ps.Length == 0)
                    {
                        Process process = new Process();
                        process.StartInfo = new ProcessStartInfo();
                        process.StartInfo.Arguments = "EMUROBOT";
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.FileName = Application.StartupPath + "\\AlgorithmControl.exe";
                        process.StartInfo.UseShellExecute = false;
                        process.Start();
                    }
                }
            }
        }

        public IAlgorithmInterface Project { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        public void btn_link_Click(object sender, EventArgs e)
        {
            Project.RunInterface(tb_redis_url.Text, tb_redis_input.Text, tb_redis_output.Text, int.Parse(tb_redis_internal.Text), Pwd);
            AddLog("连接Redis成功", LogType.ProcessLog);
            btn_link.Enabled = false;
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            tb_redis_url.Text = FileSystem.ReadIniFile("Redis", urlKey, "", Project.PathParameter1);
            tb_redis_input.Text = FileSystem.ReadIniFile("Redis", inputKey, "", Project.PathParameter1);
            tb_redis_output.Text = FileSystem.ReadIniFile("Redis", outputKey, "", Project.PathParameter1);
            tb_redis_internal.Text = FileSystem.ReadIniFile("Redis", internalKey, "100", Project.PathParameter1);
            Pwd = FileSystem.ReadIniFile("Redis", "Password", "", Project.PathParameter1);

            pairs = new Dictionary<string, string>()
            {
                { urlKey, tb_redis_url.Text },{ inputKey, tb_redis_input.Text },{ outputKey, tb_redis_output.Text },{ internalKey, tb_redis_internal.Text }
            };

            tb_redis_url.KeyDown += Tb_redis_url_KeyDown;
            tb_redis_input.KeyDown += Tb_redis_input_KeyDown;
            tb_redis_output.KeyDown += Tb_redis_output_KeyDown;
            tb_redis_internal.KeyDown += Tb_redis_internal_KeyDown;
            AddLogEvent += LogManager_AddLogEvent;

            if (host == null)
            {
                string serverPath = "http://localhost:8101/";
                host = new ServiceHost(typeof(UploadService));
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                host.AddServiceEndpoint(typeof(IService), binding, serverPath);
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(serverPath + "PicService");
                    host.Description.Behaviors.Add(behavior);
                    host.Open();
                }
            }
            AddLog("服务初始化完成", LogType.ProcessLog);
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            string s = arg1 + "\r\n";
            if (arg2 == LogType.ProcessLog)
            {
                reportCount++;
                BeginInvoke(new Action(() =>
                {
                    if (reportCount >= 100)
                    {
                        reportCount = 0;
                        tb_redis_report.Text = "";
                    }
                    tb_redis_report.Text += s;
                    tb_redis_report.SelectionStart = tb_redis_report.Text.Length - 1;
                    tb_redis_report.ScrollToCaret();
                }));
            }
            else if (arg2 == LogType.GeneralLog)
            {
                resultCount++;
                BeginInvoke(new Action(() =>
                {
                    if (resultCount >= 100)
                    {
                        resultCount = 0;
                        tb_redis_result.Text = "";
                    }
                    tb_redis_result.Text += s;
                    tb_redis_result.SelectionStart = tb_redis_result.Text.Length - 1;
                    tb_redis_result.ScrollToCaret();
                }));
            }
        }

        private void tb_redis_url_Leave(object sender, EventArgs e)
        {
            SaveOperation(urlKey, tb_redis_url.Text);
        }

        private void Tb_redis_url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation(urlKey, tb_redis_url.Text);
            }
        }

        private void tb_redis_input_Leave(object sender, EventArgs e)
        {
            SaveOperation(inputKey, tb_redis_input.Text);
        }

        private void Tb_redis_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation(inputKey, tb_redis_input.Text);
            }
        }

        private void tb_redis_output_Leave(object sender, EventArgs e)
        {
            SaveOperation(outputKey, tb_redis_output.Text);
        }

        private void Tb_redis_output_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation(outputKey, tb_redis_output.Text);
            }
        }

        private void tb_redis_internal_Leave(object sender, EventArgs e)
        {
            SaveOperation(internalKey, tb_redis_internal.Text);
        }

        private void Tb_redis_internal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation(internalKey, tb_redis_internal.Text);
            }
        }

        private void SaveOperation(string key, string value)
        {
            try
            {
                if (pairs.ContainsKey(key))
                {
                    if (pairs[key] == value)
                    {
                        return;
                    }
                    else
                    {
                        pairs[key] = value;
                    }
                }
                else
                {
                    pairs.Add(key, value);
                }
                Pwd = "";
                FileSystem.WriteIniFile("Redis", key, value, Project.PathParameter1);
            }
            catch (Exception e)
            {
                AddLog(e.Message, LogType.ErrorLog);
            }
        }
    }
}
