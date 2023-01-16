#define newScrew

using AlgorithmLib;
using GW.Function.ExcelFunction;
using GW.XML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class SettingScrewLocationForm : Form
    {
        private bool isMove = false;
        private int selectedIndex = -1;
        private int selectedPartIndex = -1;
        private double oneZoom = 0.1d;
        private double parsZoom = 0.1d;
        private Point orcPoint;
        private Point orcPicPoint;
        private DataList dataList = DataList.Instance;
        private Image pbOneImage = null;
        private XmlModel xml = null;
        private List<BindScrew> screws = null;
#if newScrew
        private List<model_struct> saveList = null;
#endif
        private Dictionary<string, int> partDict = null;
        public string Mode { private get; set; }
        public string Sn { private get; set; }

        public SettingScrewLocationForm()
        {
            InitializeComponent();
        }

        private void SettingScrewLocationForm_Load(object sender, EventArgs e)
        {
            screws = new List<BindScrew>();
            partDict = new Dictionary<string, int>();
            dataGridView1.AutoGenerateColumns = false;
            pb_one.MouseWheel += Pb_MouseWheel;
            pb_pars.MouseWheel += Pb_MouseWheel;
            dataList.DataListComboBoxExtent(Mode, Sn, cb_mode, cb_sn);
            if (string.IsNullOrEmpty(Mode))
            {
                if (cb_mode.Items.Count > 0)
                {
                    cb_mode.SelectedIndex = 0;
                }
                if (cb_sn.Items.Count > 0)
                {
                    cb_sn.SelectedIndex = 0;
                }
            }
            cb_id.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Enabled = false;
            selectedPartIndex = -1;
            selectedIndex = e.RowIndex;
            cb_part.Items.Clear();
            for (int i = 0; i < screws[selectedIndex].RectList.Count; i++)
            {
                cb_part.Items.Add(screws[selectedIndex].Name + (i + 1));
            }
            cb_part.SelectedIndex = 0;
        }

        private void Pb_MouseWheel(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Image != null)
            {
                if (pb.Name == "pb_one")
                {
                    ZoomPicture(pb, e, pb.Image.Size, 0.1, 1.5, ref oneZoom); 
                }
                else
                {
                    ZoomPicture(pb, e, pb.Image.Size, 0.1, 2.0, ref parsZoom);
                }
            }
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            orcPoint = e.Location;
            orcPicPoint = (sender as PictureBox).Location;
            isMove = true;
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (isMove)
            {
                int x = e.X - orcPoint.X;
                int y = e.Y - orcPoint.Y;
                pb.Location = new Point(orcPicPoint.X + x, orcPicPoint.Y + y);
            }
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            isMove = false;
        }

        private void cb_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataList.snList.ContainsKey(cb_mode.Text))
            {
                cb_sn.Items.Clear();
                cb_sn.Items.AddRange(dataList.snList[cb_mode.Text]?.ToArray());
            }
        }

        private void cb_part_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPartIndex = cb_part.SelectedIndex;
            ShowScrew();
            this.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveScrew();
            WriteList();
            MessageBox.Show("保存完成");
        }

        private void 加载整车图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "整节车图片|*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbOneImage = Image.FromFile(openFileDialog.FileName);
                pb_one.Image?.Dispose();
                pb_one.Image = null;
                pb_one.Image = pbOneImage.Clone() as Image;
                ZoomPicture(pb_one, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0), pbOneImage.Size, 0.1, 1.5, ref oneZoom);
            }
        }

        private void 加载部件配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "部件配置文件|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    screws.Clear();
                    partDict.Clear();
                    ExcelModel excel = new ExcelModel(openFileDialog.FileName);
                    ExcelSheet sheet = excel[0];
                    int cbSelectId = cb_id.SelectedIndex + 1;
                    for (int i = 2; i < sheet.RowCount; i++)
                    {
                        string id = sheet[i][2].Value;
                        if (int.Parse(id) == cbSelectId)
                        {
                            string name = sheet[i][5].Value.Trim();
                            if (!partDict.ContainsKey(name))
                            {
                                BindScrew screw = new BindScrew();
                                screw.Name = name;
                                partDict.Add(name, screws.Count);
                                screws.Add(screw);
                            }
                            screws[partDict[name]].OnlyIDList.Add("6" + sheet[i][2].Value + sheet[i][3].Value + sheet[i][4].Value.Substring(1) + sheet[i][6].Value + sheet[i][25].Value + sheet[i][26].Value + sheet[i][27].Value);
                            screws[partDict[name]].RectList.Add(new Rectangle(
                                int.Parse(sheet[i][19].Value), int.Parse(sheet[i][20].Value),
                                int.Parse(sheet[i][21].Value), int.Parse(sheet[i][22].Value)));
                        }
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = screws;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 加载螺丝定位文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "螺丝定位文件|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<Rectangle, BindScrew> dict = new Dictionary<Rectangle, BindScrew>();
                    xml = new XmlModel(openFileDialog.FileName, true);
                    foreach (BindScrew screw in screws)
                    {
                        foreach (Rectangle rect in screw.RectList)
                        {
                            if (!dict.ContainsKey(rect))
                            {
                                dict.Add(rect, screw);
                            }
                        }
                    }
#if newScrew
                    if (saveList == null)
                    {
                        saveList = new List<model_struct>();
                    }
                    saveList.Clear();
#endif
                    for (int i = 0; i < xml["object"].BrotherCount; i++)
                    {
                        int x = xml["object"][i]["bndbox"]["xmin"];
                        int y = xml["object"][i]["bndbox"]["ymin"];
                        int w = xml["object"][i]["bndbox"]["xmax"].ToInt() - x;
                        int h = xml["object"][i]["bndbox"]["ymax"].ToInt() - y;
                        string name = xml["object"][i]["name"];
                        Point point = new Point(x, y);
#if newScrew
                        saveList.Add(new model_struct(name, x, y, w, h));
#endif
                        foreach (BindScrew screw in screws)
                        {
                            for (int j = 0; j < screw.RectList.Count; j++)
                            {
                                Rectangle rect = screw.RectList[j];
                                if (rect.Contains(point))
                                {
                                    if (!screw.ScrewNameDict.ContainsKey(j))
                                    {
                                        screw.ScrewNameDict.Add(j, new List<string>());
                                    }
                                    if (!screw.ScrewRectDict.ContainsKey(j))
                                    {
                                        screw.ScrewRectDict.Add(j, new List<Rectangle>());
                                    }
                                    screw.ScrewNameDict[j].Add(name);
                                    screw.ScrewRectDict[j].Add(new Rectangle(x - rect.X, y - rect.Y, w, h));
                                }
                            }
                        }
                    }
                    xml = null;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = screws;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveScrew()
        {
#if newScrew
            ServerGlobal.DataBase.SaveTs<model_struct>(saveList, cb_mode.Text + "_" + cb_sn.Text, cb_id.SelectedIndex);
#else
            foreach (BindScrew screw in screws)
            {
                foreach (KeyValuePair<int, List<string>> item in screw.ScrewNameDict)
                {
                    List<model_struct> list = new List<model_struct>();
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        string name = item.Value[i];
                        Rectangle rect = screw.ScrewRectDict[item.Key][i];
                        model_struct model_Struct = new model_struct(name,
                            rect.X, rect.Y, rect.Width, rect.Height);
                        list.Add(model_Struct);
                    }
                    ServerGlobal.DataBase.SaveTs<model_struct>(list, cb_mode.Text + "_" + cb_sn.Text, screw.OnlyIDList[item.Key]);
                }
            }
#endif
        }

        private void ShowPart()
        {
            if (selectedPartIndex > -1 && selectedIndex > -1 && pbOneImage != null)
            {
                Rectangle rect = screws[selectedIndex].RectList[selectedPartIndex];
                Bitmap orcMap = new Bitmap(pbOneImage);
                Bitmap cropMap = orcMap.Clone(rect, orcMap.PixelFormat);
                this.Invoke(new Action(() =>
                {
                    pb_pars.Image?.Dispose();
                    pb_pars.Image = null;
                    pb_pars.Image = cropMap;
                }));

                if (screws[selectedIndex].ScrewRectDict.Count > 0 && screws[selectedIndex].ScrewRectDict.ContainsKey(selectedPartIndex))
                {
                    Graphics g = Graphics.FromImage(pb_pars.Image);
                    Pen pen = new Pen(Color.OrangeRed, 5);
                    foreach (Rectangle item in screws[selectedIndex].ScrewRectDict[selectedPartIndex])
                    {
                        g.DrawRectangle(pen, new Rectangle(item.X, item.Y, item.Width - 5, item.Height - 5));
                    }
                }
            }
        }

        private void ShowScrew()
        {
            if (selectedIndex > -1 && screws.Count > 0 && pbOneImage != null)
            {
                this.Invoke(new Action(() =>
                {
                    pb_one.Image?.Dispose();
                    pb_one.Image = null;
                    pb_one.Image = pbOneImage.Clone() as Image;
                }));
                
                if (screws[selectedIndex].RectList.Count > 0)
                {
                    Graphics g = Graphics.FromImage(pb_one.Image);
                    Pen pen = new Pen(Color.OrangeRed, 5);
                    foreach (Rectangle item in screws[selectedIndex].RectList)
                    {
                        g.DrawRectangle(pen, new Rectangle(item.X, item.Y, item.Width - 5, item.Height - 5));
                    }
                    ShowPart(); 
                }
            }
        }

        private void WriteList()
        {
            if (dataList.modeList.IndexOf(cb_mode.Text) < 0)
            {
                dataList.modeList.Add(cb_mode.Text);
            }
            if (!dataList.snList.ContainsKey(cb_mode.Text))
            {
                dataList.snList.Add(cb_mode.Text, new List<string>());
            }
            if (dataList.snList[cb_mode.Text].IndexOf(cb_sn.Text) < 0)
            {
                dataList.snList[cb_mode.Text].Add(cb_sn.Text);
            }
            dataList.Save();
        }

        private void ZoomPicture(PictureBox pictureBox, MouseEventArgs e, Size orcSize, double min, double max, ref double zoom)
        {
            if (e.Delta > 0)
            {
                zoom += 0.01;
            }
            else
            {
                zoom -= 0.01;
            }
            if (zoom < min)
            {
                zoom = min;
            }
            if (zoom > max)
            {
                zoom = max;
            }
            int con = 50;
            int newWidth = (int)(orcSize.Width * zoom);
            int newHeight = (int)(orcSize.Height * zoom);
            pictureBox.Size = new Size(newWidth, newHeight);

            if (pictureBox.Location.X + pictureBox.Width < 0)
            {
                if (pictureBox.Location.Y + pictureBox.Height < 0)
                {
                    pictureBox.Location = new Point(con - pictureBox.Width, con - pictureBox.Height);
                }
                else if (pictureBox.Location.Y > panel1.Height)
                {
                    pictureBox.Location = new Point(con - pictureBox.Width, panel1.Height - con);
                }
                else
                {
                    pictureBox.Location = new Point(con - pictureBox.Width, pictureBox.Location.Y);
                }
            }
            else if (pictureBox.Location.X > panel1.Width)
            {
                if (pictureBox.Location.Y + pictureBox.Height < 0)
                {
                    pictureBox.Location = new Point(panel1.Width - con, con - pictureBox.Height);
                }
                else if (pictureBox.Location.Y > panel1.Height)
                {
                    pictureBox.Location = new Point(panel1.Width - con, panel1.Height - con);
                }
                else
                {
                    pictureBox.Location = new Point(panel1.Width - con, pictureBox.Location.Y);
                }
            }
            else if (pictureBox.Location.Y + pictureBox.Height < 0)
            {
                if (pictureBox.Location.X + pictureBox.Width < 0)
                {
                    pictureBox.Location = new Point(con - pictureBox.Width, con - pictureBox.Height);
                }
                else if (pictureBox.Location.X > panel1.Width)
                {
                    pictureBox.Location = new Point(panel1.Width - con, con - pictureBox.Height);
                }
                else
                {
                    pictureBox.Location = new Point(pictureBox.Location.X, con - pictureBox.Height);
                }
            }
            else if (pictureBox.Location.Y > panel1.Height)
            {
                if (pictureBox.Location.X + pictureBox.Width < 0)
                {
                    pictureBox.Location = new Point(con - pictureBox.Width, panel1.Height - con);
                }
                else if (pictureBox.Location.X > panel1.Width)
                {
                    pictureBox.Location = new Point(panel1.Width - con, panel1.Height - con);
                }
                else
                {
                    pictureBox.Location = new Point(pictureBox.Location.X, panel1.Height - con);
                }
            }
        }

        private class BindScrew
        {
            public string Name { get; set; }
            public int Count
            {
                get
                {
                    int count = 0;
                    foreach (KeyValuePair<int, List<Rectangle>> item in ScrewRectDict)
                    {
                        if (item.Value != null)
                        {
                            count += item.Value.Count; 
                        }
                    }
                    return count;
                }
            }
            public List<string> OnlyIDList { get; set; }
            public List<Rectangle> RectList { get; set; }
            public Dictionary<int, List<string>> ScrewNameDict { get; set; }
            public Dictionary<int, List<Rectangle>> ScrewRectDict { get; set; }
            public BindScrew()
            {
                OnlyIDList = new List<string>();
                RectList = new List<Rectangle>();
                ScrewNameDict = new Dictionary<int, List<string>>();
                ScrewRectDict = new Dictionary<int, List<Rectangle>>();
            }
        }
    }
}
