#define httpInterface

using AlgorithmLib;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project
{
    public partial class ChangeForm : Form
    {
        private object dataLock = new object();
        private string savePath = "";
        private RectModel selectRect = null;
        private FrameSelectControl frameSelectControl;
        private string[] stateArray = null;
        private List<Data> datas = new List<Data>();

        public static bool CanChanged = false;

        public Action<bool, Data, List<RedisResult>, bool> ResultAct { get; set; }

        public ChangeForm()
        {
            InitializeComponent();
            savePath = Application.StartupPath + "\\Bak\\";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        public void AddData(bool isTest, string id, string mode, string sn, string robotId, string part, string imgPath, string imgUrl, string modelPath, string resultFile, box_info[] boxes)
        {
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                threadEventArgs.ThreadName = "添加人工数据线程 " + id;
                lock (dataLock)
                {
                    if (datas.FindIndex(d => d.id == id) < 0)
                    {
                        datas.Add(new Data()
                        {
                            isTest = isTest,
                            id = id,
                            mode = mode,
                            sn = sn,
                            robotId = robotId,
                            part = part,
                            imgPath = imgPath,
                            imgUrl = imgUrl,
                            modelPath = modelPath,
                            resultFile = resultFile,
                            boxes = boxes
                        });
                        ShowDataNum();
                        if (datas.Count == 1)
                        {
                            Invoke(new Action(() => { ShowData(); }));
                        }
                    } 
                }
            });
        }

        private void ChangeForm_Load(object sender, EventArgs e)
        {
            frameSelectControl = new FrameSelectControl();
            frameSelectControl.Dock = DockStyle.Fill;
            frameSelectControl.Name = "frameSelectControl";
            //frameSelectControl.Image = Image.FromFile(@"E:\RGV\线阵和面阵图片\380AL_2591_2023_04_11_13_15_00000001\Back\60001010010102021020000.jpg");
            frameSelectControl.AltAndKeyUpAction = (Keys key) =>
            {
                switch (key)
                {
                    case Keys.D1:
                        comboBox1.SelectedIndex = 0;
                        break;
                    case Keys.D2:
                        comboBox1.SelectedIndex = 1;
                        break;
                    case Keys.D3:
                        comboBox1.SelectedIndex = 2;
                        break;
                    case Keys.D4:
                        comboBox1.SelectedIndex = 3;
                        break;
                    case Keys.D5:
                        comboBox1.SelectedIndex = 4;
                        break;
                    case Keys.D6:
                        comboBox1.SelectedIndex = 5;
                        break;
                    case Keys.D7:
                        comboBox1.SelectedIndex = 6;
                        break;
                    case Keys.D8:
                        comboBox1.SelectedIndex = 7;
                        break;
                    case Keys.D9:
                        comboBox1.SelectedIndex = 8;
                        break;
                    case Keys.D0:
                        comboBox1.SelectedIndex = 9;
                        break;
                    case Keys.Q:
                        comboBox1.SelectedIndex = 10;
                        break;
                    case Keys.W:
                        comboBox1.SelectedIndex = 11;
                        break;
                    case Keys.E:
                        comboBox1.SelectedIndex = 12;
                        break;
                    case Keys.R:
                        comboBox1.SelectedIndex = 13;
                        break;
                    default:
                        return;
                }
                btn_ok_Click(null, null);
            };
            frameSelectControl.CtrlAndKeyUpAction = (Keys key) =>
            {
                int i = -1;
                switch (key)
                {
                    case Keys.D1:
                        i = 0;
                        break;
                    case Keys.D2:
                        i = 1;
                        break;
                    case Keys.D3:
                        i = 2;
                        break;
                    case Keys.D4:
                        i = 3;
                        break;
                    case Keys.D5:
                        i = 4;
                        break;
                    case Keys.D6:
                        i = 5;
                        break;
                    case Keys.D7:
                        i = 6;
                        break;
                    case Keys.D8:
                        i = 7;
                        break;
                    case Keys.D9:
                        i = 8;
                        break;
                    case Keys.D0:
                        i = 9;
                        break;
                    case Keys.Q:
                        i = 10;
                        break;
                    case Keys.W:
                        i = 11;
                        break;
                    case Keys.E:
                        i = 12;
                        break;
                    case Keys.R:
                        i = 13;
                        break;
                    default:
                        return;
                }
                if (i > -1)
                {
                    checkedListBox1.SetItemChecked(i, true); 
                }
            };
            frameSelectControl.DataChanged = (RectModel rect) =>
            {
                if (rect?.ID == selectRect?.ID)
                {
                    ShowData(rect);
                }
            };
            frameSelectControl.SelectRectangle = (RectModel rect) =>
            {
                ShowData(rect);
                selectRect = rect;
            };
            this.Controls.Add(frameSelectControl);
            frameSelectControl.BringToFront();

            stateArray = Enum.GetNames(typeof(AlgorithmStateEnum));
            comboBox1.Items.AddRange(stateArray);
            checkedListBox1.Items.AddRange(stateArray);
            comboBox1.SelectedIndex = 0;
            checkedListBox1.SetItemChecked(0, true);

            ShowDataNum();
            CanChanged = true;
        }

        private void ChangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CanChanged = false;

            while (datas.Count > 0)
            {
                btn_save_Click(null, null);
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            frameSelectControl.Delete();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (selectRect != null)
            {
                selectRect = new RectModel(selectRect);
                selectRect.AlgorithmState = (AlgorithmStateEnum)Enum.Parse(typeof(AlgorithmStateEnum), comboBox1.Text);
                selectRect.Rectangle = new Rectangle(int.Parse(tb_x.Text), int.Parse(tb_y.Text), int.Parse(tb_w.Text), int.Parse(tb_h.Text));
                frameSelectControl.SetRectangleData(selectRect);
            }
        }

        private void btn_revoke_Click(object sender, EventArgs e)
        {
            frameSelectControl.Revoke();
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            frameSelectControl.Redo();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (datas.Count > 0)
            {
                string id = datas[0].id;
                Dictionary<AlgorithmStateEnum, List<Rectangle>> dict = frameSelectControl.GetRectangles();
                List<RedisResult> list = new List<RedisResult>();
                foreach (KeyValuePair<AlgorithmStateEnum, List<Rectangle>> item in dict)
                {
                    RedisResult result = new RedisResult()
                    {
                        jclx = ((int)item.Key).ChangeCode(),
                        result = item.Value
                    };
                    list.Add(result);
                }
                SaveData(datas[0], list);
#if httpInterface
                ResultAct?.Invoke(dict.ContainsKey(AlgorithmStateEnum.正常) && dict.Count == 1, datas[0], list, false);
#else
                ResultAct?.Invoke(dict.ContainsKey(AlgorithmStateEnum.正常) && dict.Count == 1, datas[0], list, true);
#endif
                while (datas.Count > 0 && datas[0].id == id)
                {
                    datas.RemoveAt(0);
                }
                ShowData();
                ShowDataNum(); 
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && e.Index >= 0)
            {
                for (int i = 0; i < stateArray.Length; i++)
                {
                    if (checkedListBox1.GetItemChecked(i) && i != checkedListBox1.SelectedIndex)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
                frameSelectControl.AlgorithmState = (AlgorithmStateEnum)Enum.Parse(typeof(AlgorithmStateEnum), checkedListBox1.Items[e.Index].ToString());
            }
        }

        private void SaveData(Data data, List<RedisResult> list)
        {
            try
            {
                string txt = $"任务编号：{data.id}\r\n车型车号：{data.mode}-{data.sn}\r\n部件编号：{data.part}\r\n任务图片：{data.imgPath}\r\n任务模板：{data.modelPath}\r\n返回数据：{data.resultFile}\r\n测试数据：{(data.isTest ? "是" : "否")}\r\n算法结果：";
                txt += JsonManager.ObjectToJson(data.boxes);
                txt += "\r\n人工修正：" + JsonManager.ObjectToJson(list);

                string path = savePath + data.id + ".txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(txt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ShowData()
        {
            if (datas.Count > 0)
            {
                box_info[] boxes = datas[0].boxes;
                frameSelectControl.Image = Image.FromFile(datas[0].imgPath);
                foreach (box_info item in boxes)
                {
                    AlgorithmStateEnum algorithm = (AlgorithmStateEnum)item.state_enum;
                    Rectangle rectangle = new Rectangle(item.x, item.y, item.w, item.h);
                    frameSelectControl.AddRectangle(algorithm, rectangle);
                }
                frameSelectControl.DrawRectangles();
            }
            else
            {
                frameSelectControl.Image = null;
                tb_h.Text = tb_w.Text = tb_x.Text = tb_y.Text = "";
            }
        }

        private void ShowData(RectModel rect)
        {
            if (rect != null)
            {
                tb_h.Text = rect.Rectangle.Height.ToString();
                tb_w.Text = rect.Rectangle.Width.ToString();
                tb_x.Text = rect.Rectangle.X.ToString();
                tb_y.Text = rect.Rectangle.Y.ToString();
                comboBox1.SelectedItem = rect.AlgorithmState.ToString();
            }
            else
            {
                tb_h.Text = tb_w.Text = tb_x.Text = tb_y.Text = "";
            }
        }

        private void ShowDataNum()
        {
            BeginInvoke(new Action(() => { lb_num.Text = "待修正数量：" + datas.Count; }));
        }
    }

    public class Data
    {
        public bool isTest { get; set; }
        public string id { get; set; }
        public string mode { get; set; }
        public string sn { get; set; }
        public string robotId { get; set; }
        public string part { get; set; }
        public string imgPath { get; set; }
        public string imgUrl { get; set; }
        public string modelPath { get; set; }
        public string resultFile { get; set; }
        public box_info[] boxes { get; set; }
    }
}
