//#define testFunc

using AlgorithmLib;
using EMU.Util;
using GW.Function.ExcelFunction;
using GW.Function.FileFunction;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ResultControl : UserControl
    {
#if testFunc
        private TestForm test = null;
#endif
        private bool isMove = false;
        private bool isPicture = false;
        private bool showSign = true;
        private int lighOne = 0;
        private int lighPars = 0;
        private int selectedIndex = 0;
        private double oneZoom = 0.1d;
        private double parsZoom = 0.1d;
        private Point orcPoint;
        private Point orcPicPoint;
        private Image trainImage = null;
        private Image selectTrainImage = null;
        private PictureBox selectTrain = null;
        private ResultStatisticsCollection collection = null;
        public bool Is16Count
        {
            set
            {
                pb9.Visible = pb10.Visible = pb11.Visible = pb12.Visible = pb13.Visible = pb14.Visible = pb15.Visible = pb16.Visible = value;
            }
        }
        public ProjectModel ProjectModel { get; set; }
        public List<ResultData> ResultDatas { get; set; }

        public ResultControl()
        {
            InitializeComponent();
        }

        public void RefreshDatas()
        {
            RefreshData();
            ServerGlobal.DataBase.SaveTs<ResultData>(ResultDatas, ProjectModel);
        }

        private void ResultControl_Load(object sender, EventArgs e)
        {
            dataGridView3D.AutoGenerateColumns = false;
            string[] vs = Enum.GetNames(typeof(DataWhere));
            cb_where.Items.AddRange(vs);
            cb_pars.DisplayMember = "Name";
            cb_pars.ValueMember = "Data";

            pb_one.MouseWheel += Pb_one_MouseWheel;
            pb_pars.MouseWheel += Pb_pars_MouseWheel;
            
            oneZoom = double.Parse(FileSystem.ReadIniFile("ImageSize", "OneZoom", "0.1", ServerGlobal.OprationPath));
            parsZoom = double.Parse(FileSystem.ReadIniFile("ImageSize", "ParsZoom", "0.1", ServerGlobal.OprationPath));

            tb_one.Value = lighOne = int.Parse(FileSystem.ReadIniFile("ImageLigh", "ImgOne", "0", ServerGlobal.OprationPath));
            tb_pars.Value = lighPars = int.Parse(FileSystem.ReadIniFile("ImageLigh", "ImgPars", "0", ServerGlobal.OprationPath));

            pb_one.Location = new Point(
                int.Parse(FileSystem.ReadIniFile("ImageLocation", "OneX", "0", ServerGlobal.OprationPath)),
                int.Parse(FileSystem.ReadIniFile("ImageLocation", "OneY", "0", ServerGlobal.OprationPath))
            );
            pb_pars.Location = new Point(
                int.Parse(FileSystem.ReadIniFile("ImageLocation", "ParsX", "0", ServerGlobal.OprationPath)),
                int.Parse(FileSystem.ReadIniFile("ImageLocation", "ParsY", "0", ServerGlobal.OprationPath))
            );

            splitContainer1.SplitterDistance = int.Parse(FileSystem.ReadIniFile("SplitContainer", "Panel1", splitContainer1.Panel1.Width.ToString(), ServerGlobal.OprationPath));
            splitContainer1.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);

            splitContainer2.SplitterDistance = int.Parse(FileSystem.ReadIniFile("SplitContainer", "Panel2", splitContainer2.Panel1.Width.ToString(), ServerGlobal.OprationPath));
            splitContainer2.SplitterMoved += SplitContainer2_SplitterMoved;

            try
            {
                pb1_Click(null, null);
            }
            catch (Exception) { }
#if testFunc
            test = new TestForm();
            test.TestVariableDict.Add("orcPoint.X", orcPoint.X);
            test.TestVariableDict.Add("orcPoint.Y", orcPoint.Y);
            test.TestVariableDict.Add("orcPicPoint.X", orcPicPoint.X);
            test.TestVariableDict.Add("orcPicPoint.Y", orcPicPoint.Y);
            test.TestVariableDict.Add("pb_one.Location.X", pb_one.Location.X);
            test.TestVariableDict.Add("pb_one.Location.Y", pb_one.Location.Y);
            test.Show(); 
#endif
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            FileSystem.WriteIniFile("SplitContainer", "Panel1", splitContainer1.SplitterDistance.ToString(), ServerGlobal.OprationPath);
        }

        private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            FileSystem.WriteIniFile("SplitContainer", "Panel2", splitContainer2.SplitterDistance.ToString(), ServerGlobal.OprationPath);
        }

        private void Pb_one_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pb_one.Image != null)
            {
                ZoomPicture(pb_one, e, pb_one.Image.Size, 0.1, 1.5, ref oneZoom);
                FileSystem.WriteIniFile("ImageSize", "OneZoom", oneZoom.ToString(), ServerGlobal.OprationPath);
            }
        }

        private void Pb_pars_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pb_pars.Image != null)
            {
                ZoomPicture(pb_pars, e, pb_pars.Image.Size, 0.1, 2.0, ref parsZoom);
                FileSystem.WriteIniFile("ImageSize", "ParsZoom", parsZoom.ToString(), ServerGlobal.OprationPath);
            }
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isPicture = true;
            }
            orcPoint = e.Location;
            orcPicPoint = (sender as PictureBox).Location;
            导出ToolStripMenuItem.Visible = isMove = true;
#if testFunc
            test.TestVariableChange("orcPoint.X", orcPoint.X);
            test.TestVariableChange("orcPoint.Y", orcPoint.Y);
            test.TestVariableChange("orcPicPoint.X", orcPicPoint.X);
            test.TestVariableChange("orcPicPoint.Y", orcPicPoint.Y);
            test.TestVariableChange("e.Location.X", e.Location.X);
            test.TestVariableChange("e.Location.Y", e.Location.Y);
#endif
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
            PictureBox pb = sender as PictureBox;
            if (pb == pb_one)
            {
                FileSystem.WriteIniFile("ImageLocation", "OneX", pb.Location.X.ToString(), ServerGlobal.OprationPath);
                FileSystem.WriteIniFile("ImageLocation", "OneY", pb.Location.Y.ToString(), ServerGlobal.OprationPath);
            }
            else
            {
                FileSystem.WriteIniFile("ImageLocation", "ParsX", pb.Location.X.ToString(), ServerGlobal.OprationPath);
                FileSystem.WriteIniFile("ImageLocation", "ParsY", pb.Location.Y.ToString(), ServerGlobal.OprationPath);
            }
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (isMove)
            {
                int x = e.X - orcPoint.X;
                int y = e.Y - orcPoint.Y;
                pb.Location = new Point(orcPicPoint.X + x, orcPicPoint.Y + y);
#if testFunc
                test.TestVariableChange("pb_one.Location.X", pb_one.Location.X);
                test.TestVariableChange("pb_one.Location.Y", pb_one.Location.Y);
                test.TestVariableChange("e.X", e.X);
                test.TestVariableChange("e.Y", e.Y);
#endif
            }
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            isMove = false;
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (ResultDatas != null && ResultDatas.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel文件(xlsx)|*.xlsx|Excel文件(xls)|*.xls";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelModel excelModel = new ExcelModel();
                    excelModel["sheet1"] = new ExcelSheet();
                    for (int i = 0; i < ResultDatas.Count; i++)
                    {
                        excelModel["sheet1"][i] = new ExcelRow();
                        excelModel["sheet1"][i][0] = new ExcelCell();

                    }
                    excelModel.SaveExcel(saveFileDialog.FileName);
                } 
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv == dataGridView)
                {
                    LocationData(collection[e.RowIndex].ResultDatas.ToArray());
                }
                else
                {
                    导出ToolStripMenuItem.Visible = isPicture = false;
                    LocationData((dgv.DataSource as List<ResultData>)[e.RowIndex]); 
                }
            }
        }

        private void dataGridView3D_Click(object sender, EventArgs e)
        {
            isPicture = false;
        }

        private void dataGridView3D_Enter(object sender, EventArgs e)
        {
            isPicture = false;
        }

        private void cb_pars_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ResultData> datas = cb_pars.SelectedValue as List<ResultData>;
            if (datas != null && datas.Count > 0)
            {
                pb_mz.Image?.Dispose();
                pb_mz.Image = null;
                if (datas[0].ResultType == ResultType.Mz)
                {
                    pb_mz.Image = RefreshImage(datas[0].ImagePath);
                }

                Image img = RefreshImage(datas[0].ImagePath);
                if (img != null)
                {
                    pb_pars.Image?.Dispose();
                    pb_pars.Image = null;
                    if (ImageManager.IsPixelFormatIndexed(img.PixelFormat))
                    {
                        Bitmap map = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                        map.Save("tmp.jpg", ImageFormat.Jpeg);
                        img = RefreshImage("tmp.jpg");
                    }
                    img = ImageManager.KiLighten(img as Bitmap, lighPars);
                    if (showSign)
                    {
                        Graphics g = Graphics.FromImage(img);
                        foreach (ResultData data in datas)
                        {
                            if (data.ResultType != ResultType.ThrD)
                            {
                                Pen pen = new Pen(data.ResultState == ResultState.Normal ? Color.Green : Color.Red);
                                if (data.Y > 15)
                                {
                                    g.DrawString(data.Name, this.Font, new SolidBrush(pen.Color), data.X, data.Y - 12);
                                }
                                else
                                {
                                    g.DrawString(data.Name, this.Font, new SolidBrush(pen.Color), data.X, data.Y + data.Height + 3);
                                }
                                g.DrawRectangle(new Pen(pen.Color, 5), data.X, data.Y, data.Width - 5, data.Height - 5);
                            }
                        } 
                    }
                    pb_pars.Image = img;
                    pb_pars.Refresh();
                    ZoomPicture(pb_pars, pb_pars.Image.Size, 0.1, 2.0, parsZoom);
                } 
            }
        }

        private void tb_one_Scroll(object sender, EventArgs e)
        {
            int ligh = tb_one.Value - lighOne;
            lighOne = tb_one.Value;
            FileSystem.WriteIniFile("ImageLigh", "ImgOne", lighOne.ToString(), ServerGlobal.OprationPath);
            if (pb_one.Image != null)
            {
                pb_one.Image = ImageManager.KiLighten(pb_one.Image as Bitmap, ligh);
            }
        }

        private void tb_pars_Scroll(object sender, EventArgs e)
        {
            int ligh = tb_pars.Value - lighPars;
            lighPars = tb_pars.Value;
            FileSystem.WriteIniFile("ImageLigh", "ImgPars", lighPars.ToString(), ServerGlobal.OprationPath);
            if (pb_pars.Image != null)
            {
                pb_pars.Image = ImageManager.KiLighten(pb_pars.Image as Bitmap, ligh);
            }
        }

        private void 结果修正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultUpdForm form = new ResultUpdForm();
            form.Data = SelectedData();
            if (form.Data == null)
            {
                MessageBox.Show("未选中数据！");
                return;
            }
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                RefreshDatas();
            }
        }

        private void 忽略异常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultData data = SelectedData();
            if (data == null)
            {
                MessageBox.Show("未选中数据！");
                return;
            }
            data.IsIgnore = true;
            RefreshDatas();
        }

        private void 添加备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultData data = SelectedData();
            if (data == null)
            {
                MessageBox.Show("未选中数据！");
                return;
            }
            InputForm form = new InputForm();
            form.Text = "添加备注";
            form.Value = data.Remarks;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                data.Remarks = form.Value;
                RefreshData();
                ServerGlobal.DataBase.SaveTs<ResultData>(ResultDatas, ProjectModel);
            }
        }

        private void 部件图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图片|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<ResultData> datas = cb_pars.SelectedValue as List<ResultData>;
                if (datas != null && datas.Count > 0)
                {
                    File.Copy(datas[0].ImagePath, saveFileDialog.FileName);
                }
            }
        }

        private void 结果标注图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图片|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pb_pars.Image?.Save(saveFileDialog.FileName);
            }
        }

        private void Export1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export1.docx";
            FileManager.ExportWord(path);
        }

        private void Export2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export2.docx";
            FileManager.ExportWord(path);
        }

        private void Export3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export3.docx";
            FileManager.ExportWord(path);
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSign = 显示ToolStripMenuItem.Checked = !显示ToolStripMenuItem.Checked;
            隐藏ToolStripMenuItem.Checked = !显示ToolStripMenuItem.Checked;
            cb_pars_SelectedIndexChanged(cb_pars, null);
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            隐藏ToolStripMenuItem.Checked = !隐藏ToolStripMenuItem.Checked;
            showSign = 显示ToolStripMenuItem.Checked = !隐藏ToolStripMenuItem.Checked;
            cb_pars_SelectedIndexChanged(cb_pars, null);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ResultData result = SelectedData();
            resultMenu.Visible = result != null;
            if (result != null)
            {
                resultMenu.Text = result.Data;
                resultMenu.ForeColor = result.ResultState == ResultState.Normal ? Color.Green : Color.Red;
            }
        }

        private void pb1_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb1)
            {
                selectedIndex = 0;
                HideSelectRect();
                ShowSelectRect(pb1); 
            }
        }

        private void pb2_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb2)
            {
                selectedIndex = 1;
                HideSelectRect();
                ShowSelectRect(pb2); 
            }
        }

        private void pb3_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb3)
            {
                selectedIndex = 2;
                HideSelectRect();
                ShowSelectRect(pb3); 
            }
        }

        private void pb4_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb4)
            {
                selectedIndex = 3;
                HideSelectRect();
                ShowSelectRect(pb4); 
            }
        }

        private void pb5_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb5)
            {
                selectedIndex = 4;
                HideSelectRect();
                ShowSelectRect(pb5); 
            }
        }

        private void pb6_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb6)
            {
                selectedIndex = 5;
                HideSelectRect();
                ShowSelectRect(pb6); 
            }
        }

        private void pb7_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb7)
            {
                selectedIndex = 6;
                HideSelectRect();
                ShowSelectRect(pb7); 
            }
        }

        private void pb8_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb8)
            {
                selectedIndex = 7;
                HideSelectRect();
                ShowSelectRect(pb8);
            }
        }

        private void pb9_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb9)
            {
                selectedIndex = 8;
                HideSelectRect();
                ShowSelectRect(pb9);
            }
        }

        private void pb10_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb10)
            {
                selectedIndex = 9;
                HideSelectRect();
                ShowSelectRect(pb10);
            }
        }

        private void pb11_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb11)
            {
                selectedIndex = 10;
                HideSelectRect();
                ShowSelectRect(pb11);
            }
        }

        private void pb12_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb12)
            {
                selectedIndex = 11;
                HideSelectRect();
                ShowSelectRect(pb12);
            }
        }

        private void pb13_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb13)
            {
                selectedIndex = 12;
                HideSelectRect();
                ShowSelectRect(pb13);
            }
        }

        private void pb14_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb14)
            {
                selectedIndex = 13;
                HideSelectRect();
                ShowSelectRect(pb14);
            }
        }

        private void pb15_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb15)
            {
                selectedIndex = 14;
                HideSelectRect();
                ShowSelectRect(pb15);
            }
        }

        private void pb16_Click(object sender, EventArgs e)
        {
            if (selectTrain != pb16)
            {
                selectedIndex = 15;
                HideSelectRect();
                ShowSelectRect(pb16);
            }
        }

        private void ShowSelectRect(PictureBox pictureBox)
        {
            Image image = pictureBox.Image;
            selectTrainImage = image.Clone() as Image;
            selectTrain = pictureBox;
            RefreshData();
            if (image != null)
            {
                Graphics g = Graphics.FromImage(image);
                g.DrawRectangle(new Pen(Color.Red, 5), new Rectangle(0, 0, image.Width - 10, image.Height - 10));
                pictureBox.Refresh();
            }
        }

        private void HideSelectRect()
        {
            if (selectTrain != null)
            {
                selectTrain.Image.Dispose();
                selectTrain.Image = null;
                selectTrain.Image = selectTrainImage; 
            }
        }

        private void LocationData(params ResultData[] datas)
        {
            Image trainImg = trainImage.Clone() as Image;
            if (datas != null && datas.Length > 0 && datas[0] != null)
            {
                List<AlgorithmParemeter> paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, ProjectModel.Mode + "_" + ProjectModel.Sn);
                AlgorithmParemeter paremeter = (paremeters ?? new List<AlgorithmParemeter>() { new AlgorithmParemeter() { InputTasks = new List<AlgorithmLib.input_task>() } })[0];
                Graphics trainG = null;
                if (ImageManager.IsPixelFormatIndexed(trainImg.PixelFormat))
                {
                    Bitmap map = new Bitmap(trainImg.Width, trainImg.Height, PixelFormat.Format32bppArgb);
                    map.Save("tmp.jpg", ImageFormat.Jpeg);
                    trainImg = RefreshImage("tmp.jpg");
                }
                trainG = Graphics.FromImage(trainImg);
                List<ParsData> list = new List<ParsData>();
                Dictionary<string, ParsData> dict = new Dictionary<string, ParsData>();
                foreach (ResultData item in datas)
                {
                    ParsData data = null;
                    if (dict.ContainsKey(item.LengthID))
                    {
                        dict[item.LengthID].Data.Add(item);
                    }
                    else
                    {
                        data = new ParsData();
                        data.Name = EMU.Parameter.PartsDict.GetType(item.ID).ToString() + (list.Count + 1);
                        data.Data.Add(item);
                        list.Add(data);
                        dict.Add(item.LengthID, data);
                    }

                    input_task input = paremeter.InputTasks.Find(p => p.imgNO - 1 == item.Index && p.only_str.ToString('\0') == item.LengthID);
                    trainG.DrawRectangle(new Pen(Color.GreenYellow, 5), new Rectangle(input.x, input.y, input.w - 5, input.h - 5));
                }
                cb_pars.DataSource = list;
                cb_pars.SelectedIndex = 0;
            }
            pb_one.Image = trainImg;
        }

        private void RefreshData()
        {
            DataWhere dataWhere = (DataWhere)cb_where.SelectedIndex;
            string whereStr = textBox1.Text;

            pb_one.Image?.Dispose();
            pb_one.Image = null;
            string imgPath = ServerGlobal.ImageDir + "\\" + ProjectModel.ID + "\\" + selectedIndex + FileManager.GetImageExtend();
            if (File.Exists(imgPath))
            {
                trainImage = ImageManager.KiLighten(Image.FromFile(imgPath) as Bitmap, lighOne);
            }

            List<ResultData> bindList = ResultDatas?.FindAll(d =>
            d.Index == selectedIndex &&
            d.ResultType != ResultType.ThrD && (
            string.IsNullOrEmpty(whereStr) ? true :
            dataWhere == DataWhere.总编号 ? d.LengthID.Contains(whereStr) :
            dataWhere == DataWhere.部件名称 ? d.Name.Contains(whereStr) :
            dataWhere == DataWhere.部件编号 ? d.ID.Contains(whereStr) : false));
            collection = new ResultStatisticsCollection(bindList);

            List<ResultData> _3dList = ResultDatas?.FindAll(d =>
            d.Index == selectedIndex &&
            d.ResultType == ResultType.ThrD && (
            string.IsNullOrEmpty(whereStr) ? true :
            dataWhere == DataWhere.总编号 ? d.LengthID.Contains(whereStr) :
            dataWhere == DataWhere.部件名称 ? d.Name.Contains(whereStr) :
            dataWhere == DataWhere.部件编号 ? d.ID.Contains(whereStr) : false));

            dataGridView.DataSource = null;
            dataGridView.DataSource = collection.BindResultDatas;
            ServerGlobal.SetDataGridViewHead(dataGridView);
            LocationData(collection[0].ResultDatas.ToArray());
            if (pb_pars.Image != null)
            {
                ZoomPicture(pb_pars, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0), pb_pars.Image.Size, 0.1, 2.0, ref parsZoom); 
            }

            dataGridView3D.DataSource = null;
            dataGridView3D.DataSource = _3dList;
            if (pb_one.Image != null)
            {
                ZoomPicture(pb_one, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0), pb_one.Image.Size, 0.1, 1.5, ref oneZoom); 
            }
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
            ZoomPicture(pictureBox, orcSize, min, max, zoom);
        }

        private void ZoomPicture(PictureBox pictureBox, Size orcSize, double min, double max, double zoom)
        {
            int _x = 10, _y = 85, maxX = panel1.Width - 10, maxY = panel1.Height - 100;
            int newWidth = (int)(orcSize.Width * zoom);
            int newHeight = (int)(orcSize.Height * zoom);
            pictureBox.Size = new Size(newWidth, newHeight);

            if (pictureBox.Location.X + pictureBox.Width < _x)
            {
                if (pictureBox.Location.Y + pictureBox.Height < _y)
                {
                    pictureBox.Location = new Point(_x, _y);
                }
                else if (pictureBox.Location.Y > maxY)
                {
                    pictureBox.Location = new Point(_x, maxY);
                }
                else
                {
                    pictureBox.Location = new Point(_x, pictureBox.Location.Y);
                }
            }
            else if (pictureBox.Location.X > maxX)
            {
                if (pictureBox.Location.Y + pictureBox.Height < _y)
                {
                    pictureBox.Location = new Point(maxX, _y);
                }
                else if (pictureBox.Location.Y > maxY)
                {
                    pictureBox.Location = new Point(maxX, maxY);
                }
                else
                {
                    pictureBox.Location = new Point(maxX, pictureBox.Location.Y);
                }
            }
            else if (pictureBox.Location.Y + pictureBox.Height < _y)
            {
                if (pictureBox.Location.X + pictureBox.Width < _x)
                {
                    pictureBox.Location = new Point(_x, _y);
                }
                else if (pictureBox.Location.X > maxX)
                {
                    pictureBox.Location = new Point(maxX, _y);
                }
                else
                {
                    pictureBox.Location = new Point(pictureBox.Location.X, _y);
                }
            }
            else if (pictureBox.Location.Y > maxY)
            {
                if (pictureBox.Location.X + pictureBox.Width < _x)
                {
                    pictureBox.Location = new Point(_x, maxY);
                }
                else if (pictureBox.Location.X > maxX)
                {
                    pictureBox.Location = new Point(maxX, maxY);
                }
                else
                {
                    pictureBox.Location = new Point(pictureBox.Location.X, maxY);
                }
            }
        }

        private Image RefreshImage(string path)
        {
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
        }

        private ResultData SelectedData()
        {
            if (isPicture && dataGridView.SelectedRows != null && dataGridView.SelectedRows.Count > 0)
            {
                return collection[dataGridView.SelectedRows[0].Index].ResultDatas.Find(d => new Rectangle((int)(d.X * parsZoom), (int)(d.Y * parsZoom), (int)(d.Width * parsZoom), (int)(d.Height * parsZoom)).Contains(orcPoint));
            }
            else if (dataGridView3D.SelectedRows != null && dataGridView3D.SelectedRows.Count > 0)
            {
                int i = dataGridView3D.SelectedRows[0].Index;
                List<ResultData> list = dataGridView3D.DataSource as List<ResultData>;
                if (i < list.Count)
                {
                    return list[i]; 
                }
            }
            return null;
        }

        private enum DataWhere
        {
            部件编号, 总编号, 部件名称
        }

        private class ParsData
        {
            public string Name { get; set; }
            public List<ResultData> Data { get; set; }
            public ParsData()
            {
                Data = new List<ResultData>();
            }
        }
    }

    public class ResultData
    {
        private string data = "";

        public bool IsUpdData
        {
            get
            {
                if (string.IsNullOrEmpty(OldData))
                {
                    return false;
                }
                return Data != OldData;
            }
        }
        public bool IsIgnore { get; set; }
        public int Index { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ID { get; set; }
        public string LengthID { get; set; }
        public string Name { get; set; }
        public string StateName
        {
            get
            {
                string name = "";
                switch (ResultState)
                {
                    case ResultState.Normal:
                        name = "正常";
                        break;
                    case ResultState.Abnormal:
                        name = "异常";
                        break;
                    case ResultState.None:
                        name = "未知";
                        break;
                }
                return name;
            }
        }
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                if (string.IsNullOrEmpty(OldData))
                {
                    OldData = value;
                }
            }
        }
        public string OldData { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public ResultState ResultState { get; set; }
        public ResultType ResultType { get; set; }
    }

    public enum ResultState
    {
        Normal, Abnormal, None
    }

    public enum ResultType
    {
        Xz, Mz, ThrD
    }
}
