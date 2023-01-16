using EMU.ApplicationData;
using EMU.Parameter;
using EMU.Util;
using GW.Function.ExcelFunction;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class SettingDataForm : Form
    {
        private object bindData = null;
        private DataList dataList = DataList.Instance;
        private ResultType dataType;
        public string Mode { private get; set; }
        public string Sn { private get; set; }
        public ResultType DataType
        {
            set
            {
                dataType = value;
            }
        }

        public SettingDataForm()
        {
            InitializeComponent();
        }

        private void SettingDataForm_Load(object sender, EventArgs e)
        {
            bindData = CreateData();
            InitControl();
            dataList.DataListComboBoxExtent(Mode, Sn, cb_mode, cb_sn);
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "面阵数据 | *.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelModel excelModel = new ExcelModel(openFileDialog.FileName);
                    List<BackDataModel> list = new List<BackDataModel>();
                    List<int> distanceList = new List<int>();
                    for (int i = 1; i < excelModel[0].RowCount; i++)
                    {
                        try
                        {
                            BackDataModel model = new BackDataModel();
                            MzLocation lp = GetLocation(excelModel[0][i]);
                            if (!string.IsNullOrEmpty(excelModel[0][i][19].Value))
                            {
                                model.Camera3d_Id = int.Parse(excelModel[0][i][19].Value);
                            }
                            switch (excelModel[0][i][8].Value)
                            {
                                case "3d":
                                    model.CameraType = CameraType.Cognext3DCamera;
                                    model.CanSort = true;
                                    break;
                                case "Mz":
                                    model.CameraType = CameraType.BaslerCamera;
                                    model.CanSort = true;
                                    break;
                                case "Ywc":
                                    model.CameraType = CameraType.MovedCamera;
                                    model.CanSort = true;
                                    break;
                                default:
                                    model.CameraType = CameraType.BaslerLineCamera;
                                    model.CanSort = false;
                                    break;
                            }
                            model.GroupID = int.Parse(excelModel[0][i][7].Value);
                            model.GroupName = excelModel[0][i][4].Value;
                            model.ID = int.Parse(excelModel[0][i][0].Value);
                            model.Location = lp.location;
                            model.Mode = excelModel[0][i][1].Value;
                            model.OnlyID = excelModel[0][i][20].Value;
                            model.PartsType = PartsDict.GetType(excelModel[0][i][5].Value);
                            model.Point = lp.point;
                            model.RgvRunDistacnce = int.Parse(excelModel[0][i][9].Value);
                            model.RobotLocation = new RobotDataPack()
                            {
                                j1 = excelModel[0][i][12].Value,
                                j2 = excelModel[0][i][13].Value,
                                j3 = excelModel[0][i][14].Value,
                                j4 = excelModel[0][i][15].Value,
                                j5 = excelModel[0][i][16].Value,
                                j6 = excelModel[0][i][17].Value
                            };
                            model.RobotName = (RobotName)Enum.Parse(typeof(RobotName), excelModel[0][i][10].Value);
                            model.Sn = excelModel[0][i][2].Value;
                            list.Add(model);

                            if (!PartsDict.PartDict.ContainsKey(model.PartsType))
                            {
                                PartsDict.PartDict.Add(model.PartsType, new List<string>());
                            }
                            if (!PartsDict.PartDict[model.PartsType].Contains(model.OnlyID))
                            {
                                PartsDict.PartDict[model.PartsType].Add(model.OnlyID); 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                    excelModel = null;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = bindData = list;

                    using (StreamWriter sw = new StreamWriter(ServerGlobal.PartsPath))
                    {
                        sw.Write(JsonManager.ObjectToJson(PartsDict.PartDict));
                    }

                    int j = dataList.modeList.IndexOf(list[0].Mode);
                    if (j > -1)
                    {
                        cb_mode.SelectedIndex = j;
                        j = dataList.snList[list[0].Mode].IndexOf(list[0].Sn);
                        if (j > -1)
                        {
                            cb_sn.SelectedIndex = j;
                        }
                        else
                        {
                            cb_sn.Enabled = true;
                        }
                    }
                    else
                    {
                        cb_mode.Enabled = cb_sn.Enabled = true; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cb_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataList.snList.ContainsKey(cb_mode.Text))
            {
                cb_sn.Items.Clear();
                cb_sn.Items.AddRange(dataList.snList[cb_mode.Text]?.ToArray());
            }
        }

        private void cb_sn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string train = cb_mode.Text + "_" + cb_sn.Text;
            switch (dataType)
            {
                case ResultType.Xz:
                    List<FowardDataModel> list = ServerGlobal.DataBase.GetTs<FowardDataModel>(null, train);
                    if (list == null)
                    {
                        list = bindData as List<FowardDataModel>;
                        foreach (FowardDataModel item in list)
                        {
                            item.Mode = cb_mode.Text;
                            item.Sn = cb_sn.Text;
                        }
                    }
                    else
                    {
                        if (list.Count < 16)
                        {
                            for (int i = list.Count; i < 16; i++)
                            {
                                list.Add(new FowardDataModel() { ID = i, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 });
                            }
                        }
                        bindData = list;
                    }
                    dataGridView1.DataSource = null;
                    if (cb_num.SelectedIndex == 0)
                    {
                        dataGridView1.DataSource = (bindData as List<FowardDataModel>).GetRange(0, 8);
                    }
                    else
                    {
                        dataGridView1.DataSource = bindData;
                    }
                    break;
                case ResultType.ThrD:
                    List<TestStandard3D> _3dList = ServerGlobal.DataBase.GetTs<TestStandard3D>(null, train);
                    if (_3dList == null)
                    {
                        List<BackDataModel> backList = ServerGlobal.DataBase.GetTs<BackDataModel>(null, train);
                        if (backList == null || backList.Count == 0)
                        {
                            return;
                        }
                        foreach (BackDataModel item in backList)
                        {
                            if (item.Camera3d_Id > -1)
                            {
                                _3dList.Add(new TestStandard3D()
                                {
                                    ID = item.OnlyID,
                                    MinValue = 10,
                                    MaxValue = 50
                                });
                            }
                        }
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = bindData = _3dList;
                    break;
            }
        }

        private void cb_num_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindData != null)
            {
                dataGridView1.DataSource = null;
                if (cb_num.SelectedIndex == 0)
                {
                    dataGridView1.DataSource = (bindData as List<FowardDataModel>).GetRange(0, 8);
                }
                else
                {
                    dataGridView1.DataSource = bindData;
                } 
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string train = "";
            if (dataType != ResultType.Mz)
            {
                if (cb_mode.SelectedIndex >= 0 && cb_sn.SelectedIndex >= 0)
                {
                    train = cb_mode.Text + "_" + cb_sn.Text;
                }
                else
                {
                    MessageBox.Show("请选择车型车号");
                    return;
                }
            }
            else if (bindData as List<BackDataModel> == null || (bindData as List<BackDataModel>).Count == 0)
            {
                MessageBox.Show("请加载面阵数据");
                return;
            }
            switch (dataType)
            {
                case ResultType.Xz:
                    List<FowardDataModel> data = bindData as List<FowardDataModel>;
                    ServerGlobal.DataBase.SaveTs<FowardDataModel>(cb_num.SelectedIndex == 0 ? data.GetRange(0, 8) : data, train);
                    break;
                case ResultType.Mz:
                    List<BackDataModel> list = bindData as List<BackDataModel>;
                    train = list[0].Mode + "_" + list[0].Sn;
                    ServerGlobal.DataBase.SaveTs<BackDataModel>(bindData as List<BackDataModel>, train);
                    break;
                case ResultType.ThrD:
                    ServerGlobal.DataBase.SaveTs<TestStandard3D>(bindData as List<TestStandard3D>, train);
                    break;
            }
            MessageBox.Show("保存完成");
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        private object CreateData()
        {
            switch (dataType)
            {
                case ResultType.Xz:
                    return new List<FowardDataModel>()
                    {
                        new FowardDataModel() { ID = 0, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 1, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 2, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 3, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 4, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 5, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 6, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 7, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 8, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 9, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 10, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 11, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 12, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 13, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 14, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 },
                        new FowardDataModel() { ID = 15, Mode = cb_mode.Text, Sn = cb_sn.Text, CarriageLength = 30, ShotCount = 40 }
                    };
                case ResultType.Mz:
                    return new List<BackDataModel>();
                case ResultType.ThrD:
                    List<TestStandard3D> list = new List<TestStandard3D>();
                    Array names = Enum.GetNames(typeof(PartsType));
                    foreach (var item in names)
                    {
                        PartsType pt = (PartsType)Enum.Parse(typeof(PartsType), item.ToString());
                        list.Add(new TestStandard3D() { ID = PartsDict.GetID(pt), Name = pt.ToString() });
                    }
                    return list;
            }
            return null;
        }

        private void InitControl()
        {
            switch (dataType)
            {
                case ResultType.Xz:
                    btn_select.Visible = false;
                    cb_num.SelectedIndex = 0;
                    break;
                case ResultType.Mz:
                    cb_mode.Enabled = cb_sn.Enabled = cb_num.Visible = false;
                    break;
                case ResultType.ThrD:
                    btn_select.Visible = cb_num.Visible = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = bindData;
                    break;
            }
        }

        private MzLocation GetLocation(ExcelRow excelRow)
        {
            string location = "{0}_{1}";
            int index = 0;
            if (int.TryParse(excelRow[3].Value, out _))
            {
                index = int.Parse(excelRow[3].Value) - 1;
            }
            int index2 = 0;
            string poi = "";
            switch (excelRow[6].Value)
            {
                case "011":
                    index2 = 0;
                    poi = "z1";
                    break;
                case "012":
                    index2 = 0;
                    poi = "z2";
                    break;
                case "021":
                    index2 = 1;
                    poi = "z1";
                    break;
                case "022":
                    index2 = 1;
                    poi = "z2";
                    break;
                default:
                    index2 = 0;
                    poi = "";
                    break;
            }
            return new MzLocation() { location = string.Format(location, index, index2), point = poi };
        }

        private class MzLocation
        {
            public string location { get; set; }
            public string point { get; set; }
        }
    }
}
