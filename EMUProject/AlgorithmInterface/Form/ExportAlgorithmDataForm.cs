using AlgorithmLib;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class ExportAlgorithmDataForm : Form
    {
        private int value = 0;
        private CheckedListBox clb = null;

        public ExportAlgorithmDataForm()
        {
            InitializeComponent();
        }

        private void ExportAlgorithmDataForm_Load(object sender, EventArgs e)
        {
            string[] names = Enum.GetNames(typeof(AlgorithmStateEnum));
            cb_type.Items.AddRange(names);
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb_export_path.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\log\\" + dtp.Value.ToString("yyyy-MM-dd") + "\\algorithm.log";
            if (!File.Exists(path))
            {
                MessageBox.Show("没有数据");
                return;
            }
            if (string.IsNullOrEmpty(tb_export_path.Text))
            {
                MessageBox.Show("未选择导出位置");
                return;
            }
            timer1.Enabled = pb.Visible = true;
            Task.Run(() =>
            {
                Export(path);
                if (!IsDisposed)
                {
                    BeginInvoke(new Action(() =>
                    {
                        timer1.Enabled = pb.Visible = false;
                    }));
                }
            });
        }

        private void tb_export_path_TextChanged(object sender, EventArgs e)
        {
            if (tb_export_path.Text[tb_export_path.Text.Length - 1] != '\\')
            {
                tb_export_path.Text += "\\";
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, true);
            }
        }

        private void 全不选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, false);
            }
        }

        private void cb_type_MouseEnter(object sender, EventArgs e)
        {
            clb = cb_type;
        }

        private void cb_pars_MouseEnter(object sender, EventArgs e)
        {
            clb = cb_pars;
        }

        private void cb_image_MouseEnter(object sender, EventArgs e)
        {
            clb = cb_image;
        }

        private void cb_result_MouseEnter(object sender, EventArgs e)
        {
            clb = cb_result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (value < pb.Maximum)
            {
                pb.Value = value;
            }
        }

        private void Export(string path)
        {
            Dictionary<string, string> resultDict = new Dictionary<string, string>();
            Dictionary<string, string[]> imgDict = new Dictionary<string, string[]>();
            Dictionary<string, string[]> parsDict = new Dictionary<string, string[]>();
            Dictionary<AlgorithmStateEnum, List<string>> typeDict = new Dictionary<AlgorithmStateEnum, List<string>>();
            Array array = Enum.GetValues(typeof(AlgorithmStateEnum));
            foreach (int item in array)
            {
                typeDict.Add((AlgorithmStateEnum)item, new List<string>());
            }
            using (StreamReader sr = new StreamReader(path))
            {
                string id = "", mode = "", sn = "";
                string s;
                List<string> imgs = new List<string>();
                List<string> pars = new List<string>();
                while (true)
                {
                    s = sr.ReadLine();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }
                    if (s.Contains("车型"))
                    {
                        pars.Clear();
                        pars.AddRange(new string[4]);
                        pars[0] = mode = s.Substring(s.IndexOf("：") + 1);
                        imgs.Clear();
                        imgs.AddRange(new string[3]);
                        imgs[2] = Application.StartupPath + "\\model\\" + mode + "\\";
                    }
                    else if (s.Contains("车号"))
                    {
                        sn = s.Substring(s.IndexOf("：") + 1);
                        pars[0] += "_" + sn;
                        imgs[1] = Application.StartupPath + "\\muban\\" + sn + "\\";
                        imgs[2] += sn + "\\";
                    }
                    else if (s.Contains("部件编号："))
                    {
                        pars[1] = s.Substring(s.IndexOf("：") + 1);
                        imgs[1] += pars[1] + ".jpg";
                        imgs[2] += pars[1] + "\\";
                    }
                    else if (s.Contains("任务编号"))
                    {
                        id = s.Substring(s.IndexOf("：") + 1);
                        imgs[0] = Application.StartupPath + "\\bak_img\\" + id + ".jpg";
                        if (!resultDict.ContainsKey(id))
                        {
                            resultDict.Add(id, "");
                        }
                    }
                    else if (s.Contains("识别类型"))
                    {
                        pars[2] = s.Substring(s.IndexOf("：") + 1) + "+100";
                    }
                    else if (s.Contains("需要图像校正"))
                    {
                        pars[2] = pars[2].Replace("+100", "");
                    }
                    else if (s.Contains(">> [{\"class_name\""))
                    {
                        pars[3] = s.Substring(s.IndexOf(">> ") + 3);
                    }
                    else if (s.Contains("映射结果码"))
                    {
                        s = s.Substring(s.IndexOf("映射结果码 ") + 6);
                        s = s.Substring(0, s.IndexOf(" "));
                        AlgorithmStateEnum algorithm = (AlgorithmStateEnum)int.Parse(s);
                        if (typeDict[algorithm].IndexOf(id) < 0)
                        {
                            typeDict[algorithm].Add(id);
                        }
                    }
                    else if (s.Contains("算法结果"))
                    {
                        resultDict[id] = s.Substring(s.IndexOf("：") + 1);
                        if (!imgDict.ContainsKey(id))
                        {
                            imgDict.Add(id, imgs.ToArray());
                        }
                        if (!parsDict.ContainsKey(id))
                        {
                            parsDict.Add(id, pars.ToArray());
                        }
                    }
                }
                s = null;
                imgs = null;
                pars = null;
            }
            value++;
            decimal v = 1m;
            decimal d = 99m / (cb_type.CheckedItems.Count);
            path = tb_export_path.Text + "export\\";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            string tmpPath = "";
            foreach (string item in cb_type.CheckedItems)
            {
                v += d;
                value = (int)v;
                AlgorithmStateEnum algorithm = (AlgorithmStateEnum)Enum.Parse(typeof(AlgorithmStateEnum), item);
                foreach (string id in typeDict[algorithm])
                {
                    tmpPath = path + id;
                    if (!Directory.Exists(tmpPath))
                    {
                        Directory.CreateDirectory(tmpPath);
                        if (imgDict.ContainsKey(id) && File.Exists(imgDict[id][0]))
                        {
                            File.Copy(imgDict[id][0], tmpPath + "\\检测图.jpg");
                        }
                        if (parsDict.ContainsKey(id))
                        {
                            using (StreamWriter sw = new StreamWriter(tmpPath + "\\pars.txt"))
                            {
                                foreach (string cb in cb_pars.CheckedItems)
                                {
                                    switch (cb)
                                    {
                                        case "车型车号":
                                            sw.WriteLine(parsDict[id][0]);
                                            break;
                                        case "部件编号":
                                            sw.WriteLine(parsDict[id][1]);
                                            break;
                                        case "识别任务":
                                            sw.WriteLine(parsDict[id][2]);
                                            break;
                                        case "模板坐标":
                                            sw.WriteLine(parsDict[id][3]);
                                            break;
                                    }
                                }
                                foreach (string cb in cb_result.CheckedItems)
                                {
                                    switch (cb)
                                    {
                                        case "算法结果":
                                            sw.WriteLine(resultDict[id]);
                                            break;
                                    }
                                }
                            } 
                        }
                        foreach (string cb in cb_image.CheckedItems)
                        {
                            switch (cb)
                            {
                                case "模板图片":
                                    if (imgDict.ContainsKey(id) && File.Exists(imgDict[id][1]))
                                    {
                                        File.Copy(imgDict[id][1], tmpPath + "\\模板图.jpg");
                                    }
                                    break;
                                case "往次图片":
                                    Directory.CreateDirectory(tmpPath + "\\往次图");
                                    if (imgDict.ContainsKey(id) && Directory.Exists(imgDict[id][2]))
                                    {
                                        string[] files = Directory.GetFiles(imgDict[id][2]);
                                        for (int i = 0; i < files.Length; i++)
                                        {
                                            File.Copy(files[i], tmpPath + "\\往次图\\" + i.ToString("00") + ".jpg");
                                        }
                                    }
                                    break;
                                case "结果图片":
                                    if (imgDict.ContainsKey(id) && resultDict.ContainsKey(id) && !string.IsNullOrEmpty(resultDict[id]))
                                    {
                                        List<RedisResult> list = JsonManager.JsonToObject<List<RedisResult>>(resultDict[id]);
                                        if (list != null && list.Count > 0)
                                        {
                                            Image image = Image.FromFile(imgDict[id][0]);
                                            Graphics graphics = Graphics.FromImage(image);
                                            foreach (RedisResult rr in list)
                                            {
                                                Color c = Color.Red;
                                                if (rr.jclx == "0200")
                                                {
                                                    c = Color.Green;
                                                }
                                                foreach (Rectangle rect in rr.result)
                                                {
                                                    graphics.DrawRectangle(new Pen(c, 3), rect);
                                                }
                                            }
                                            image.Save(tmpPath + "\\结果图.jpg");
                                            graphics.Dispose();
                                            image.Dispose();
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            MessageBox.Show("导出完成");
        }
    }
}
