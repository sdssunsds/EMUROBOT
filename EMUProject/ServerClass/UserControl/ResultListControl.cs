using EMU.Util;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ResultListControl : UserControl
    {
        private Image img = null;
        public ResultData Data { get; set; }
        public Action RefreshData { get; set; }

        public ResultListControl()
        {
            InitializeComponent();
        }

        private void ResultListControl_Load(object sender, EventArgs e)
        {
            lb_name.Text = Data.Name;
            lb_remark.Text = Data.Remarks;
            lb_data.Text = Data.Data;
            if (Data.ResultState == ResultState.Normal)
            {
                lb_data.ForeColor = Color.Green;
            }
            else
            {
                lb_data.ForeColor = Color.Red;
            }
            if (Data.IsIgnore)
            {
                lb_remark.Text = "已忽略 " + lb_remark.Text;
                btn_ignore.Enabled = false;
            }
            if (File.Exists(Data.ImagePath))
            {
                img = Image.FromFile(Data.ImagePath);
                int x = Data.X - 10, y = Data.Y - 10, w = Data.Width + 20, h = Data.Height + 20;
                if (x < 0)
                {
                    x = 0;
                }
                if (y < 0)
                {
                    y = 0;
                }
                if (w + x > img.Width)
                {
                    w = img.Width - x;
                }
                if (h + y > img.Height)
                {
                    h = img.Height - y;
                }
                img = ImageManager.CaptureImage(img, x, y, w, h);
                DrawImage(); 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DrawImage();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            ResultUpdForm form = new ResultUpdForm();
            form.Data = Data;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                DrawImage();
                RefreshData?.Invoke();
                lb_data.Text = Data.Data;
                if (Data.ResultState == ResultState.Normal)
                {
                    lb_data.ForeColor = Color.Green;
                }
                else
                {
                    lb_data.ForeColor = Color.Red;
                }
            }
        }

        private void btn_ignore_Click(object sender, EventArgs e)
        {
            Data.IsIgnore = true;
            RefreshData?.Invoke();
            btn_ignore.Enabled = false;
        }

        private void btn_remark_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm();
            form.Text = "添加备注";
            form.Value = Data.Remarks;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                lb_remark.Text = Data.Remarks = form.Value;
                RefreshData?.Invoke();
                if (Data.IsIgnore)
                {
                    lb_remark.Text = "已忽略 " + lb_remark.Text;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Data.ImagePath))
            {
                if (ResultListForm.picForm != null)
                {
                    ResultListForm.picForm.Close();
                }
                ResultListForm.picForm = new PictureForm()
                {
                    Image = Image.FromFile(Data.ImagePath),
                    RectColor = lb_data.ForeColor,
                    Rect = new Rectangle(Data.X, Data.Y, Data.Width, Data.Height)
                };
                ResultListForm.picForm.Show(this);
            }
        }

        private void DrawImage()
        {
            Image tmp = img.Clone() as Image;
            if (checkBox1.Checked)
            {
                Graphics g = Graphics.FromImage(tmp);
                Pen p = new Pen(lb_data.ForeColor);
                g.DrawRectangle(p, 10, 10, img.Width - 20, img.Height - 20);
            }
            pictureBox1.Image = tmp;
            pictureBox1.Refresh();
        }
    }
}
