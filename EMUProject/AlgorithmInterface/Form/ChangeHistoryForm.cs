using AlgorithmLib;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project
{
    public partial class ChangeHistoryForm : Form
    {
        private int barIndex = 0;
        private int barMax = 100;
        private string path = "";
        private Dictionary<string, string[]> selDict = new Dictionary<string, string[]>();

        public ChangeHistoryForm()
        {
            InitializeComponent();
        }

        private void ChangeHistoryForm_Load(object sender, EventArgs e)
        {
            path = Application.StartupPath + "\\Bak\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            cb_state.Items.Add("全部");
            cb_state.Items.AddRange(Enum.GetNames(typeof(AlgorithmStateEnum)));
            cb_state.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Select((DateTime dt) =>
            {
                return Math.Abs((dt - dateTimePicker1.Value).TotalDays) < 1;
            });
        }

        private void btn_sel_Click(object sender, EventArgs e)
        {
            lb_list.Items.Clear();
            pb.Image = null;
            foreach (KeyValuePair<string, string[]> item in selDict)
            {
                if (!string.IsNullOrEmpty(tb_partId.Text) && item.Key.Contains(tb_partId.Text))
                {
                    lb_list.Items.Add(item.Key);
                }
                else if (!string.IsNullOrEmpty(tb_mode.Text) && item.Value[1].Contains(tb_mode.Text))
                {
                    lb_list.Items.Add(item.Key);
                }
                else if (!string.IsNullOrEmpty(tb_sn.Text) && item.Value[1].Contains(tb_sn.Text))
                {
                    lb_list.Items.Add(item.Key);
                }
                else if (cb_state.SelectedIndex > 0)
                {
                    AlgorithmStateEnum algorithmState = (AlgorithmStateEnum)Enum.Parse(typeof(AlgorithmStateEnum), cb_state.Text);
                    int i = (int)algorithmState;
                    if (item.Value[7].Contains("state_enum\":" + i) || item.Value[8].Contains("jclx\":\"" + i.ChangeCode()))
                    {
                        lb_list.Items.Add(item.Key);
                    }
                }
                else if (cb_state.SelectedIndex == 0)
                {
                    lb_list.Items.Add(item.Key);
                }
            }
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            Select(funcStr: (string[] vs) =>
            {
                int index = 0;
                Invoke(new Action(() => { index = cb_state.SelectedIndex; }));
                if (!string.IsNullOrEmpty(tb_partId.Text) && vs[2].Contains(tb_partId.Text))
                {
                    return true;
                }
                else if (!string.IsNullOrEmpty(tb_mode.Text) && vs[1].Contains(tb_mode.Text))
                {
                    return true;
                }
                else if (!string.IsNullOrEmpty(tb_sn.Text) && vs[1].Contains(tb_sn.Text))
                {
                    return true;
                }
                else if (index > 0)
                {
                    AlgorithmStateEnum algorithmState = (AlgorithmStateEnum)Enum.Parse(typeof(AlgorithmStateEnum), cb_state.Text);
                    int i = (int)algorithmState;
                    if (vs[7].Contains("state_enum\":" + i) || vs[8].Contains("jclx\":\"" + i.ChangeCode()))
                    {
                        return true;
                    }
                }
                else if (index == 0)
                {
                    return true;
                }
                return false;
            });
        }

        private void lb_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bar.Maximum != barMax)
            {
                bar.Maximum = barMax; 
            }
            if (barIndex < barMax)
            {
                bar.Value = barIndex;
            }
        }

        private void Select(Func<DateTime, bool> funcTime = null, Func<string[], bool> funcStr = null)
        {
            panel1.Enabled = lb_list.Enabled = false;
            barIndex = 0;
            lb_list.Items.Clear();
            selDict.Clear();
            pb.Image = null;
            timer1.Enabled = true;
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                FileInfo[] files = new DirectoryInfo(path).GetFiles("*.txt");
                barMax = files.Length;
                foreach (FileInfo file in files)
                {
                    if (funcTime != null ? funcTime(file.LastWriteTime) : true)
                    {
                        string txt = "";
                        using (StreamReader sr = new StreamReader(file.FullName))
                        {
                            txt = sr.ReadToEnd();
                        }
                        string[] vs = txt.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if (funcStr != null ? funcStr(vs) : true)
                        {
                            string name = vs[0].Split('：')[1] + "-" + vs[2].Split('：')[1];
                            if (!selDict.ContainsKey(name))
                            {
                                selDict.Add(name, vs);
                            }
                            Invoke(new Action(() => { lb_list.Items.Add(name); })); 
                        }
                    }
                    barIndex++;
                }
                Invoke(new Action(() =>
                {
                    timer1.Enabled = false;
                    panel1.Enabled = lb_list.Enabled = true;
                    bar.Value = 0;
                }));
            });
        }

        private void ShowImage()
        {
            if (lb_list.SelectedIndex >= 0)
            {
                string key = lb_list.SelectedItem.ToString();
                if (selDict.ContainsKey(key))
                {
                    string imgPath = selDict[key][3].Split('：')[1];
                    Image image = Image.FromFile(imgPath);
                    Graphics g = Graphics.FromImage(image);
                    Pen green = new Pen(Color.Green, 2);
                    Pen red = new Pen(Color.Red, 2);

                    if (radioButton1.Checked)
                    {
                        box_info[] boxes = JsonManager.JsonToObject<box_info[]>(selDict[key][7].Split('：')[1]);
                        foreach (box_info item in boxes)
                        {
                            if (item.state_enum == 0)
                            {
                                g.DrawRectangle(green, item.x, item.y, item.w, item.h);
                            }
                            else
                            {
                                g.DrawRectangle(red, item.x, item.y, item.w, item.h);
                                g.DrawString(((AlgorithmStateEnum)item.state_enum).ToString(), Font, new SolidBrush(Color.Red), item.x + 2, item.y + 2);
                            }
                        }
                    }
                    else
                    {
                        List<RedisResult> list = JsonManager.JsonToObject<List<RedisResult>>(selDict[key][8].Split('：')[1]);
                        foreach (RedisResult item in list)
                        {
                            Pen pen;
                            if (item.jclx == "0200")
                            {
                                pen = green;
                            }
                            else
                            {
                                pen = red;
                            }
                            foreach (Rectangle rectangle in item.result)
                            {
                                g.DrawRectangle(pen, rectangle);
                                if (pen == red)
                                {
                                    g.DrawString(item.jclx.ToState().ToString(), Font, new SolidBrush(Color.Red), rectangle.X + 2, rectangle.Y + 2);
                                }
                            }
                        }
                    }

                    pb.Image = image;
                }
            }
        }
    }
}
