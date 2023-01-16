using EMU.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Laser
{
    public partial class LidarControl : UserControl
    {
        private int radius = 2000;  // 图像半径，最大值10000，数值越大，资源占用越大
        private float num = 0;
        private Graphics g = null;
        private Point center = new Point(0, 0);
        private Pen pen = null;
        private SolidBrush brush = null;

        public LidarControl()
        {
            InitializeComponent();
        }

        private void LidarControl_Load(object sender, EventArgs e)
        {
            LaserManager.Instance.LaserConnect(LaserName.Lidar);
            LaserManager.Instance.GetLaserData += Instance_GetLaserData;
            LaserManager.Instance.GetLaserObjEx += Instance_GetLaserObjEx;

            center = new Point(radius, radius);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Red);
            InitImage();

            var check = new Action<ToolStripMenuItem>((ToolStripMenuItem item) =>
            {
                item.Checked = int.Parse(item.Text) == radius;
            });
            check(toolStripMenuItem2);
            check(toolStripMenuItem3);
            check(toolStripMenuItem4);
            check(toolStripMenuItem5);
            check(toolStripMenuItem6);
            check(toolStripMenuItem7);
            check(toolStripMenuItem8);
        }

        private void Instance_GetLaserData(string data, LaserName laser)
        {
            Invoke(new Action(() =>
            {
                if (tb_received.Text.Length > 10000)
                {
                    tb_received.Text = "";
                }

                tb_received.Text += data.ToUpper() + "\r\n";
                tb_received.SelectionStart = tb_received.Text.Length;
                tb_received.ScrollToCaret();
            }));
        }

        private void Instance_GetLaserObjEx(LaserName laser, params object[] exPars)
        {
            BeginInvoke(new Action(() =>
            {
                List<float> listR = exPars[2] as List<float>;
                List<string> list = exPars[0] as List<string>;
                List<string> list1 = exPars[0] as List<string>;
                List<string> tmpList = exPars[3] as List<string>;

                listBox1.DataSource = null;
                listBox2.DataSource = null;
                listBox1.DataSource = list;
                listBox2.DataSource = list1;
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox2.SelectedIndex = listBox2.Items.Count - 1;

                for (int i = 0; i < listR.Count; i++)
                {
                    string s = tmpList[i].Split('′')[1].Split(' ')[0];
                    DrawImage(listR[i], int.Parse(s));
                }
                pictureBox1.Refresh();
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitImage();
            tb_send.Text += LaserManager.Instance.Lidar.Start() + "\r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tb_send.Text += LaserManager.Instance.Lidar.Stop() + "\r\n";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            InitImage();
        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == listBox1)
            {
                listBox2.SelectedIndex = listBox1.SelectedIndex;
            }
            else
            {
                listBox1.SelectedIndex = listBox2.SelectedIndex;
            }

            if (listBox2.SelectedItem != null)
            {
                string s = listBox2.SelectedItem.ToString();
                s = s.Split(' ')[1];
                string[] vs = s.Split('°');
                float r = float.Parse(vs[0]);
                vs = vs[1].Split('′');
                r += float.Parse(vs[0]) / 60;
                g.DrawLine(new Pen(Color.Green), center, GetPointF(r, int.Parse(vs[1].Split(' ')[0])));
                pictureBox1.Refresh();
            }
        }

        private void 复制数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Focused)
            {
                if (listBox1.SelectedItem != null)
                {
                    Clipboard.SetDataObject(listBox1.SelectedItem.ToString());
                }
            }
            else
            {
                if (listBox2.SelectedItem != null)
                {
                    Clipboard.SetDataObject(listBox2.SelectedItem.ToString());
                }
            }
        }

        private void 编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = listBox1.DataSource as List<string>;
            List<string> list1 = listBox2.DataSource as List<string>;
            list.Sort();
            list1.Sort();
            listBox1.DataSource = null;
            listBox2.DataSource = null;
            listBox1.DataSource = list;
            listBox2.DataSource = list1;
        }

        private void 角度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = listBox1.DataSource as List<string>;
            List<string> list1 = listBox2.DataSource as List<string>;
            list1.Sort(new Comparison<string>(new Func<string, string, int>((string s1, string s2) =>
            {
                string[] vs = s1.Split(' ');
                vs = vs[1].Split('°');
                float r1 = float.Parse(vs[0]) + float.Parse(vs[1].Split('′')[0]) / 60;
                vs = s2.Split(' ');
                vs = vs[1].Split('°');
                float r2 = float.Parse(vs[0]) + float.Parse(vs[1].Split('′')[0]) / 60;
                return (int)(r1 - r2);
            })));
            string[] array = list.ToArray();
            list.Clear();
            foreach (string item in list1)
            {
                string id = item.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                list.Add(array[Array.FindIndex(array, new Predicate<string>(new Func<string, bool>((string s) => { return s.IndexOf(id) == 0; })))]);
            }
            listBox1.DataSource = null;
            listBox2.DataSource = null;
            listBox1.DataSource = list;
            listBox2.DataSource = list1;
        }

        private void 距离ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = listBox1.DataSource as List<string>;
            List<string> list1 = listBox2.DataSource as List<string>;
            list1.Sort(new Comparison<string>(new Func<string, string, int>((string s1, string s2) =>
            {
                string[] vs = s1.Split(' ');
                string s = vs[1].Split('′')[1];
                int len1 = int.Parse(s);
                vs = s2.Split(' ');
                s = vs[1].Split('′')[1];
                int len2 = int.Parse(s);
                return len1 - len2;
            })));
            string[] array = list.ToArray();
            list.Clear();
            foreach (string item in list1)
            {
                string id = item.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                list.Add(array[Array.FindIndex(array, new Predicate<string>(new Func<string, bool>((string s) => { return s.IndexOf(id) == 0; })))]);
            }
            listBox1.DataSource = null;
            listBox2.DataSource = null;
            listBox1.DataSource = list;
            listBox2.DataSource = list1;
        }

        private void 数据导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var export = new Action<List<string>>((List<string> list) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "文本文件|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        File.Delete(saveFileDialog.FileName);
                    }
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (string item in list)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
            });
            if (listBox1.Focused)
            {
                export(listBox1.DataSource as List<string>);
            }
            else
            {
                export(listBox2.DataSource as List<string>);
            }
        }

        private void 图片导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPG文件|*.jpg|GIF文件|*.gif|PNG图像|*.png|位图|*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image?.Save(saveFileDialog.FileName);
            }
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void toolStripMeAnuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            radius = int.Parse(toolStripMenuItem.Text);
            num = 16000f / radius;
            center = new Point(radius, radius);
            InitImage();

            toolStripMenuItem2.Checked = toolStripMenuItem3.Checked = toolStripMenuItem4.Checked = toolStripMenuItem5.Checked = toolStripMenuItem6.Checked = toolStripMenuItem7.Checked = toolStripMenuItem8.Checked = false;
            toolStripMenuItem.Checked = true;
        }

        private void DrawImage(float r, int l)
        {
            PointF endPoint = GetPointF(r, l);
            Rectangle rectangle = new Rectangle((int)endPoint.X - 5, (int)endPoint.Y - 5, 10, 10);
            g.FillRectangle(brush, rectangle);
        }

        private void InitImage()
        {
            g?.Dispose();
            g = null;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = null;

            Bitmap bitmap = new Bitmap(radius * 2, radius * 2);
            g = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            if (checkBox1.Checked)
            {
                for (int i = 0; i < 24; i++)
                {
                    g.DrawLine(pen, center, GetPointF(15 * i, 200000));
                }
            }
        }

        private PointF GetPointF(float r, int l)
        {
            PointF pointF = new PointF();
            pointF.X = (float)(l / num * Math.Cos((r + 270) * Math.PI / 180) + radius);
            pointF.Y = (float)(l / num * Math.Sin((r + 270) * Math.PI / 180) + radius);
            return pointF;
        }
    }
}
