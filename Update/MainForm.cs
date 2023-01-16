using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Update.ServiceReference;

namespace Update
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(UpdateFIle)).Start();
        }

        private void UpdateFIle()
        {
            try
            {
                ShowText("初始化...");
                IService service = new ServiceClient();
                ShowText("获取文件列表");
                string[] files = service.GetUpdateFiles();
                SetProgress(files.Length);
                foreach (string file in files)
                {
                    string path = Application.StartupPath + "\\" + file;
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    ShowText("正在下载: " + file);
                    byte[] bytes = service.GetUpdateFile(file);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    SetProgress();
                }
                string version = service.GetVersion();
                Process.Start(Application.StartupPath + "\\EMUROBOT.exe", version);
                Application.Exit();
            }
            catch (Exception e)
            {
                this.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show(e.Message);
                    Application.Exit();
                }));
            }
        }

        private void ShowText(string txt)
        {
            this.BeginInvoke(new Action(() =>
            {
                label1.Text = txt;
            }));
        }

        private void SetProgress(int max = 0)
        {
            this.Invoke(new Action(() =>
            {
                if (max > 0)
                {
                    progressBar1.Maximum = max;
                }
                progressBar1.Value++;
            }));
        }
    }
}
