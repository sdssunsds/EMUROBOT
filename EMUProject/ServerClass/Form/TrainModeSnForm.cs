using AlgorithmLib;
using GW.Function.ExcelFunction;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class TrainModeSnForm : Form
    {
        public TrainModeSnForm()
        {
            InitializeComponent();
        }

        private void TrainModeSnForm_Load(object sender, EventArgs e)
        {
            dgv_mode.AutoGenerateColumns = false;
            dgv_sn.AutoGenerateColumns = false;
            SetupMode();
            dgv_mode.CellClick += Dgv_mode_CellClick;
            dgv_sn.CellClick += Dgv_sn_CellClick;
        }

        private void Dgv_mode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    if (MessageBox.Show("确定删除数据吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    string mode = dgv_mode.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DataList.Instance.snList.Remove(mode);
                    DataList.Instance.modeList.Remove(mode);
                    DataList.Instance.Save();
                    SetupMode();
                }
                else
                {
                    SetupSn(dgv_mode.Rows[e.RowIndex].Cells[0].Value.ToString());
                } 
            }
        }

        private void Dgv_sn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string mode = dgv_mode.Rows[dgv_mode.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string sn = dgv_sn.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (e.ColumnIndex == 5)
                {
                    if (MessageBox.Show("确定删除数据吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    if (dgv_mode.SelectedCells == null || dgv_mode.SelectedCells.Count == 0)
                    {
                        MessageBox.Show("未选择车型信息");
                        return;
                    }
                    DataList.Instance.snList[mode].Remove(sn);
                    DataList.Instance.Save();
                    SetupSn(mode);
                }
                else if (e.ColumnIndex == 1)
                {
                    string train = mode + "_" + sn;
                    List<AlgorithmParemeter> paremeters = null;
                    int[] height = null;
                    List<input_task> inputList = null;
                    Dictionary<string, string> dict = null;
                    ExcelModel excel = null;
                    height = train.GetHeight(0, ref excel);
                    inputList = new List<input_task>();
                    inputList.AddRange(excel.GetInputTask());
                    dict = excel.GetNameDict();
                    excel = null;
                    GC.Collect();

                    paremeters = new List<AlgorithmParemeter>();
                    paremeters.Add(new AlgorithmParemeter()
                    {
                        Heights = height,
                        InputTasks = inputList,
                        NameDict = dict
                    });
                    ServerGlobal.DataBase.SaveTs<AlgorithmParemeter>(paremeters, train);
                    MessageBox.Show("导入完成");
                }
                else if (e.ColumnIndex == 2)
                {
                    new SettingDataForm() { Text = "设置面阵数据", DataType = ResultType.Mz }.Show();
                }
                else if (e.ColumnIndex == 3)
                {
                    new SettingDataForm() { Text = "设置3D检测标准", DataType = ResultType.ThrD, Mode = mode, Sn = sn }.Show();
                }
                else if (e.ColumnIndex == 4)
                {
                    new SettingScrewLocationForm() { Mode = mode, Sn = sn }.Show();
                }
            }
        }

        private void btn_add_mode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_mode.Text))
            {
                MessageBox.Show("请输入车型后再添加信息");
                return;
            }
            if (DataList.Instance.modeList.IndexOf(tb_mode.Text) >= 0)
            {
                MessageBox.Show("车型添加重复");
                return;
            }
            DataList.Instance.modeList.Add(tb_mode.Text);
            DataList.Instance.snList.Add(tb_mode.Text, new List<string>());
            DataList.Instance.Save();

            string mode = "";
            if (dgv_mode.SelectedRows != null && dgv_mode.SelectedRows.Count > 0)
            {
                if (dgv_mode.SelectedRows[0].Index >= 0)
                {
                    mode = dgv_mode.SelectedRows[0].Cells[0].Value.ToString();
                }
            }
            SetupMode(mode);
        }

        private void btn_add_sn_Click(object sender, EventArgs e)
        {
            if (dgv_mode.SelectedCells == null ||dgv_mode.SelectedCells.Count == 0)
            {
                MessageBox.Show("未选择车型信息");
                return;
            }
            string mode = dgv_mode.Rows[dgv_mode.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            if (string.IsNullOrEmpty(tb_sn.Text))
            {
                MessageBox.Show("请输入车号后再添加信息");
                return;
            }
            if (!DataList.Instance.snList.ContainsKey(mode))
            {
                DataList.Instance.snList.Add(mode, new List<string>());
            }
            if (DataList.Instance.snList[mode].IndexOf(tb_sn.Text) >= 0)
            {
                MessageBox.Show("车号添加重复");
                return;
            }
            DataList.Instance.snList[mode].Add(tb_sn.Text);
            DataList.Instance.Save();
            SetupSn(mode);
        }

        private void SetupMode(string mode = "")
        {
            if (DataList.Instance.modeList.Count > 0)
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("Name"));
                table.Columns.Add(new DataColumn("Del"));
                foreach (string item in DataList.Instance.modeList)
                {
                    table.Rows.Add(item, "删除");
                }
                if (string.IsNullOrEmpty(mode))
                {
                    SetupSn(DataList.Instance.modeList[0]); 
                }
                else
                {
                    SetupSn(mode);
                }
                dgv_mode.DataSource = null;
                dgv_mode.DataSource = table; 
            }
        }

        private void SetupSn(string mode)
        {
            dgv_sn.DataSource = null;
            if (DataList.Instance.snList.ContainsKey(mode))
            {
                DataTable table = new DataTable();
                table.Columns.Add(new DataColumn("Name"));
                table.Columns.Add(new DataColumn("Del"));
                table.Columns.Add(new DataColumn("Xz"));
                table.Columns.Add(new DataColumn("Mz"));
                table.Columns.Add(new DataColumn("3D"));
                table.Columns.Add(new DataColumn("Screw"));
                foreach (string item in DataList.Instance.snList[mode])
                {
                    table.Rows.Add(item, "删除", "线阵数据", "面阵数据", "3D标准", "螺丝数据");
                }
                dgv_sn.DataSource = table; 
            }
        }
    }
}
