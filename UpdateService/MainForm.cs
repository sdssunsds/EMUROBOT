using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using UpdateServer;

namespace UpdateService
{
    public partial class MainForm : Form
    {
        private static string updatePath = Application.StartupPath + "\\Update\\";
        private static string servicePath = Properties.Settings.Default.ServicePath;
        private ServiceHost host = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(updatePath))
            {
                Directory.CreateDirectory(updatePath);
            }
            Service.UpdateDir = updatePath;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SkinFile))
            {
                skinEngine.SkinFile = Properties.Settings.Default.SkinFile;
            }
            lb_up.Text = Properties.Settings.Default.UpdateFiles;
            tb_version.Text = Properties.Settings.Default.Version;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (host == null)
            {
                host = new ServiceHost(typeof(Service));

                //绑定
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                //终结点
                host.AddServiceEndpoint(typeof(IService), binding, servicePath);
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    //行为
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;

                    //元数据地址
                    behavior.HttpGetUrl = new Uri(servicePath + "UpdateService");
                    host.Description.Behaviors.Add(behavior);

                    //启动
                    host.Open();
                    btn_close.Enabled = true;
                    btn_open.Enabled = false;
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (host != null)
            {
                host.Close();
                btn_open.Enabled = true;
                btn_close.Enabled = false;
            }
        }

        private void btn_skin_Click(object sender, EventArgs e)
        {
            Skin.SelectSkin skin = new Skin.SelectSkin();
            skin.ShowDialog(this);
            if (!string.IsNullOrEmpty(skin.SSK))
            {
                Properties.Settings.Default.SkinFile = skinEngine.SkinFile = skin.SSK;
                Properties.Settings.Default.Save();
            }
        }

        private void btn_add_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] files = openFileDialog.FileNames;
                foreach (string file in files)
                {
                    lb_now.Text += file + "\r\n";
                }
            }
        }

        private void btn_add_files_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string dir = folderBrowserDialog.SelectedPath;
                DirectoryInfo directory = new DirectoryInfo(dir);
                foreach (FileInfo file in directory.GetFiles())
                {
                    lb_now.Text += file.FullName + "\r\n";
                }
            }
        }

        private void btn_add_skin_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string dir = folderBrowserDialog.SelectedPath;
                Manager.CopyDirectory(dir, Application.StartupPath + "\\Skin\\");
                DirectoryInfo directory = new DirectoryInfo(dir);
                Service.AddSkins.Add(directory.Name);
            }
        }

        private void tb_version_TextChanged(object sender, EventArgs e)
        {
            btn_add_file.Enabled = btn_add_files.Enabled = tb_version.Text != Properties.Settings.Default.Version;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(updatePath);
                foreach (FileInfo fileInfo in directory.GetFiles())
                {
                    fileInfo.Delete();
                }

                string[] files = lb_now.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string file in files)
                {
                    string name = file.Substring(file.LastIndexOf("\\") + 1);
                    File.Copy(file, updatePath + name);
                }

                Properties.Settings.Default.Version = Service.Version = tb_version.Text;
                Properties.Settings.Default.UpdateFiles = lb_up.Text = lb_now.Text;
                Properties.Settings.Default.Save();

                lb_now.Text = "";
                MessageBox.Show("发布成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lb_now_TextChanged(object sender, EventArgs e)
        {
            btn_update.Enabled = !string.IsNullOrEmpty(lb_now.Text);
        }
    }
}
