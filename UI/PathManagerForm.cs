using EMU.Interface;
using System;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class PathManagerForm : Form
    {
        public IProject Project { get; set; }

        public PathManagerForm()
        {
            InitializeComponent();
        }

        private void PathManagerForm_Load(object sender, EventArgs e)
        {
            label1.Text = Project.PathName1;
            label2.Text = Project.PathName2;
            label3.Text = Project.PathName3;
            label4.Text = Project.PathName4;

            tb_img.Text = Project.PathParameter1;
            tb_data.Text = Project.PathParameter2;
            tb_task.Text = Project.PathParameter3;
            tb_3d.Text = Project.PathParameter4;
        }

        private void btn_img_Click(object sender, EventArgs e)
        {
            SetDir(tb_img);
        }

        private void btn_data_Click(object sender, EventArgs e)
        {
            SetDir(tb_data);
        }

        private void btn_task_Click(object sender, EventArgs e)
        {
            SetDir(tb_task);
        }

        private void btn_3d_Click(object sender, EventArgs e)
        {
            SetDir(tb_3d);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Project.PathParameter1 = tb_img.Text;
            Project.PathParameter2 = tb_data.Text;
            Project.PathParameter3 = tb_task.Text;
            Project.PathParameter4 = tb_3d.Text;
            Project.SavePathParameter();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetDir(TextBox tb)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
