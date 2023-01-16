using AlgorithmLib;
using EMU.ApplicationData;
using EMU.Parameter;
using EMU.Util;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ResultListForm : Form
    {
        private int progressIndex = 0;
        private int progressMax = 100;
        private Action ResultData = null;
        private List<AlgorithmParemeter> paremeters = null;
        private List<BackDataModel> backList = null;
        private List<ResultData> errorData = null;
        private Dictionary<string, GroupBox> boxDict = null;
        public static PictureForm picForm = null;

        public ProjectModel Project { get; set; }
        public List<ResultData> Datas { get; set; }

        public ResultListForm()
        {
            InitializeComponent();
        }

        public void AddResfresh(Action action)
        {
            ResultData = new Action(() =>
            {
                action?.Invoke();
                errorData = Datas.FindAll(d => d.ResultState != ResultState.Normal);
                pagesControl1.MaxDataNumber = errorData.Count;
            });
        }

        private void ResultListForm_Load(object sender, EventArgs e)
        {
            string train = Project.Mode + "_" + Project.Sn;
            boxDict = new Dictionary<string, GroupBox>();
            paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, train);
            backList = ServerGlobal.DataBase.GetTs<BackDataModel>(null, train);
            errorData = Datas.FindAll(d => d.ResultState != ResultState.Normal);
            pagesControl1.MaxDataNumber = errorData.Count;
            pagesControl1.PageChanged += new Action<int, int>(pagesControl1_PageChanged);
            pagesControl1.PageChanging();
        }

        private void btn_exp1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export2.docx";
            FileManager.ExportWord(path);
        }

        private void btn_exp2_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export3.docx";
            FileManager.ExportWord(path);
        }

        private void btn_exp3_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\doc\export1.docx";
            FileManager.ExportWord(path);
        }

        private void pagesControl1_PageChanged(int index, int count)
        {
            if (index + count > errorData.Count)
            {
                count = errorData.Count - index;
            }
            List<ResultData> datas = errorData.GetRange(index, count);
            progressIndex = 0;
            progressMax = count;
            ClearControl();
            foreach (ResultData item in datas)
            {
                AddDataControl(item);
                progressIndex++;
            }
            InitControl();
        }

        private PageProgress pagesControl1_PageProgressBar()
        {
            return new PageProgress() { Index = progressIndex, Max = progressMax };
        }

        private void AddDataControl(ResultData data)
        {
            string onlyID = data.LengthID;
            if (data.ResultType == ResultType.Xz)
            {
                int index = paremeters[0].InputTasks.FindIndex(p => p.only_str.ToString('\0') == onlyID);
                if (index > -1)
                {
                    AddDataControl(data, paremeters[0].InputTasks[index].imgNO + "车" + GetLocation(paremeters[0].InputTasks[index].location_str.ToString('\0')) + GetPartLocation(paremeters[0].InputTasks[index].part_location_str.ToString('\0')) + PartsDict.GetType(paremeters[0].InputTasks[index].part_str.ToString('\0')).ToString());
                }
                else
                {
                    AddDataControl(data, "未知");
                }
            }
            else if (data.ResultType == ResultType.Mz)
            {
                int index = backList.FindIndex(b => b.OnlyID == onlyID);
                if (index > -1)
                {
                    BackDataModel item = backList[index];
                    AddDataControl(data, item.GroupName);
                }
                else
                {
                    AddDataControl(data, "未知");
                }
            }
            else
            {
                return;
            }
        }

        private void AddDataControl(ResultData data, string key)
        {
            if (!boxDict.ContainsKey(key))
            {
                GroupBox group = new GroupBox();
                group.Text = key;
                group.Size = new Size(290, 200);
                try
                {
                    boxDict.Add(key, group);
                    group.Controls.Add(new FlowLayoutPanel() { Dock = DockStyle.Fill });
                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() => { flowLayoutPanel1.Controls.Add(group); }));
                    }
                }
                catch (Exception) { }
            }
            ResultListControl control = new ResultListControl();
            control.Data = data;
            control.RefreshData = ResultData;
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() => { boxDict[key].Controls[0].Controls.Add(control); }));
            }
        }

        private void ClearControl()
        {
            if (!this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    boxDict.Clear();
                    flowLayoutPanel1.Controls.Clear();
                }));
            }
        }

        private void InitControl()
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    foreach (KeyValuePair<string, GroupBox> item in boxDict)
                    {
                        int height = 0;
                        foreach (Control control in item.Value.Controls[0].Controls)
                        {
                            height += control.Height;
                        }
                        item.Value.Size = new Size(330, height + 30 + item.Value.Controls[0].Controls.Count * 4);
                    }
                }));
            }
        }

        private string GetLocation(string loc)
        {
            switch (loc)
            {
                default:
                    return "";
            }
        }

        private string GetPartLocation(string loc)
        {
            switch (loc)
            {
                default:
                    return "";
            }
        }
    }
}
