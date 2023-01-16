using EMU.ApplicationData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class RgvInfoControl : UserControl
    {
        private bool isShown = false;
        private Dictionary<string, int> distacnceDict = new Dictionary<string, int>();
        private Dictionary<string, PointF[]> signDict = new Dictionary<string, PointF[]>();
        private RgvGlobalInfo rgvGlobalInfo = new RgvGlobalInfo();
        public RgvGlobalInfo RgvGlobalInfo
        {
            set
            {
                if (rgvGlobalInfo != null)
                {
                    rgvGlobalInfo.PropertyChangedEvent -= RgvGlobalInfo_PropertyChangedEvent;
                    SaveSign();
                }
                rgvGlobalInfo = value;
                rgvGlobalInfo.PropertyChangedEvent += RgvGlobalInfo_PropertyChangedEvent;
                InitSign();
            }
        }

        public RgvInfoControl()
        {
            InitializeComponent();
        }

        public void InitSign()
        {
            if (signDict.ContainsKey(rgvGlobalInfo.ID))
            {
                for (int i = 0; i < 16; i++)
                {
                    aGauge_distacnce.GaugeRanges[i + 1].StartValue = signDict[rgvGlobalInfo.ID][i].X;
                    aGauge_distacnce.GaugeRanges[i + 1].EndValue = signDict[rgvGlobalInfo.ID][i].Y;
                }
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    aGauge_distacnce.GaugeRanges[i + 1].StartValue = 0;
                    aGauge_distacnce.GaugeRanges[i + 1].EndValue = 0;
                }
            }
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    tb_speed.Text = rgvGlobalInfo.RgvCurrentRunSpeed.ToString();
                    tb_power.Text = rgvGlobalInfo.RgvCurrentPowerElectricity.ToString();
                    tb_current.Text = rgvGlobalInfo.RgvCurrentPowerCurrent.ToString();
                    tb_tempture.Text = rgvGlobalInfo.RgvCurrentPowerTempture.ToString();
                    tb_distacnce.Text = rgvGlobalInfo.RgvCurrentRunDistacnce.ToString();
                }));
            }
        }

        public void SaveSign()
        {
            try
            {
                if (!signDict.ContainsKey(rgvGlobalInfo.ID))
                {
                    signDict.Add(rgvGlobalInfo.ID, new PointF[16]);
                }
                for (int i = 0; i < 16; i++)
                {
                    signDict[rgvGlobalInfo.ID][i] = new PointF(aGauge_distacnce.GaugeRanges[i + 1].StartValue, aGauge_distacnce.GaugeRanges[i + 1].EndValue);
                }
            }
            catch (Exception) { }
        }

        public void SetSign(int i, int run_distacnce, string id, bool isStart = true)
        {
            if (isStart)
            {
                if (!distacnceDict.ContainsKey(id))
                {
                    distacnceDict.Add(id, 0);
                }
                distacnceDict[id] = run_distacnce;
            }
            else
            {
                int start = 0;
                if (distacnceDict.ContainsKey(id))
                {
                    start = distacnceDict[id];
                }
                if (start > 0)
                {
                    if (id == rgvGlobalInfo.ID)
                    {
                        if (!this.IsDisposed)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                aGauge_distacnce.GaugeRanges[i].StartValue = start / 1000f;
                                aGauge_distacnce.GaugeRanges[i].EndValue = run_distacnce / 1000f;
                            }));
                        }
                    }
                    else
                    {
                        if (!signDict.ContainsKey(rgvGlobalInfo.ID))
                        {
                            signDict.Add(rgvGlobalInfo.ID, new PointF[16]);
                        }
                        signDict[rgvGlobalInfo.ID][i - 1] = new PointF(start / 1000f, run_distacnce / 1000f);
                    } 
                }
            }
        }

        private void RgvInfoControl_Load(object sender, EventArgs e)
        {
            #region 速度初始化
            aGauge_speed.GaugeRanges[0].StartValue = Properties.Settings.Default.RGV正常速度刻度;
            aGauge_speed.GaugeRanges[0].EndValue = Properties.Settings.Default.RGV超速速度刻度;
            aGauge_speed.GaugeRanges[1].StartValue = Properties.Settings.Default.RGV超速速度刻度;
            aGauge_speed.GaugeRanges[1].EndValue = aGauge_speed.MaxValue;
            tb_speed.Text = rgvGlobalInfo.RgvCurrentRunSpeed.ToString();
            aGauge_speed.Value = rgvGlobalInfo.RgvCurrentRunSpeed;
            #endregion
            #region 电量初始化
            aGauge_power.GaugeRanges[0].StartValue = Properties.Settings.Default.RGV低电报警刻度;
            aGauge_power.GaugeRanges[0].EndValue = Properties.Settings.Default.RGV电量预警刻度;
            aGauge_power.GaugeRanges[1].StartValue = 0;
            aGauge_power.GaugeRanges[1].EndValue = Properties.Settings.Default.RGV低电报警刻度;
            tb_power.Text = rgvGlobalInfo.RgvCurrentPowerElectricity.ToString();
            aGauge_power.Value = rgvGlobalInfo.RgvCurrentPowerElectricity;
            #endregion
            #region 电流初始化
            aGauge_current.MaxValue = Properties.Settings.Default.RGV最大电流刻度;
            aGauge_current.ScaleLinesMajorStepValue = (aGauge_current.MaxValue - aGauge_current.MinValue) / 10;
            aGauge_current.GaugeRanges[0].StartValue = Properties.Settings.Default.RGV警告电流刻度;
            aGauge_current.GaugeRanges[0].EndValue = Properties.Settings.Default.RGV危险电流刻度;
            aGauge_current.GaugeRanges[1].StartValue = Properties.Settings.Default.RGV危险电流刻度;
            aGauge_current.GaugeRanges[1].EndValue = aGauge_current.MaxValue;
            tb_current.Text = rgvGlobalInfo.RgvCurrentPowerCurrent.ToString();
            aGauge_current.Value = rgvGlobalInfo.RgvCurrentPowerCurrent;
            #endregion
            #region 温度初始化
            aGauge_tempture.MinValue = Properties.Settings.Default.RGV最低温刻度;
            aGauge_tempture.MaxValue = Properties.Settings.Default.RGV最高温刻度;
            aGauge_tempture.ScaleLinesMajorStepValue = (aGauge_tempture.MaxValue - aGauge_tempture.MinValue) / 10;
            aGauge_tempture.GaugeRanges[0].StartValue = aGauge_tempture.MinValue;
            aGauge_tempture.GaugeRanges[0].EndValue = Properties.Settings.Default.RGV低温提醒刻度;
            aGauge_tempture.GaugeRanges[1].StartValue = Properties.Settings.Default.RGV高温提醒刻度;
            aGauge_tempture.GaugeRanges[1].EndValue = aGauge_tempture.MaxValue;
            tb_tempture.Text = rgvGlobalInfo.RgvCurrentPowerTempture.ToString();
            aGauge_tempture.Value = rgvGlobalInfo.RgvCurrentPowerTempture;
            #endregion
            #region 位置初始化
            aGauge_distacnce.MaxValue = Properties.Settings.Default.RGV轨道长度;
            aGauge_distacnce.ScaleLinesMajorStepValue = (aGauge_distacnce.MaxValue - aGauge_distacnce.MinValue) / 20;
            aGauge_distacnce.GaugeRanges[0].EndValue = Properties.Settings.Default.RGV充电位位置;
            aGauge_distacnce.GaugeRanges[1].Color = Properties.Settings.Default.第一节车标记色;
            aGauge_distacnce.GaugeRanges[2].Color = Properties.Settings.Default.第二节车标记色;
            aGauge_distacnce.GaugeRanges[3].Color = Properties.Settings.Default.第三节车标记色;
            aGauge_distacnce.GaugeRanges[4].Color = Properties.Settings.Default.第四节车标记色;
            aGauge_distacnce.GaugeRanges[5].Color = Properties.Settings.Default.第五节车标记色;
            aGauge_distacnce.GaugeRanges[6].Color = Properties.Settings.Default.第六节车标记色;
            aGauge_distacnce.GaugeRanges[7].Color = Properties.Settings.Default.第七节车标记色;
            aGauge_distacnce.GaugeRanges[8].Color = Properties.Settings.Default.第八节车标记色;
            aGauge_distacnce.GaugeRanges[9].Color = Properties.Settings.Default.第九节车标记色;
            aGauge_distacnce.GaugeRanges[10].Color = Properties.Settings.Default.第十节车标记色;
            aGauge_distacnce.GaugeRanges[11].Color = Properties.Settings.Default.第11节车标记色;
            aGauge_distacnce.GaugeRanges[12].Color = Properties.Settings.Default.第12节车标记色;
            aGauge_distacnce.GaugeRanges[13].Color = Properties.Settings.Default.第13节车标记色;
            aGauge_distacnce.GaugeRanges[14].Color = Properties.Settings.Default.第14节车标记色;
            aGauge_distacnce.GaugeRanges[15].Color = Properties.Settings.Default.第15节车标记色;
            aGauge_distacnce.GaugeRanges[16].Color = Properties.Settings.Default.第16节车标记色;
            tb_distacnce.Text = rgvGlobalInfo.RgvCurrentRunDistacnce.ToString();
            aGauge_distacnce.Value = rgvGlobalInfo.RgvCurrentRunDistacnce / 1000f;
            #endregion
            isShown = true;
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if (isShown)
            {
                if (sender == tb_speed)
                {
                    aGauge_speed.Value = rgvGlobalInfo.RgvCurrentRunSpeed;
                }
                else if (sender == tb_power)
                {
                    aGauge_power.Value = rgvGlobalInfo.RgvCurrentPowerElectricity;
                }
                else if (sender == tb_current)
                {
                    aGauge_current.Value = rgvGlobalInfo.RgvCurrentPowerCurrent;
                }
                else if (sender == tb_tempture)
                {
                    aGauge_tempture.Value = rgvGlobalInfo.RgvCurrentPowerTempture;
                }
                else if (sender == tb_distacnce)
                {
                    aGauge_distacnce.Value = rgvGlobalInfo.RgvCurrentRunDistacnce / 1000f;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tb_current.Text = tb_distacnce.Text = tb_power.Text = tb_speed.Text = tb_tempture.Text = rgvGlobalInfo.RgvCurrentStat;
            if (groupBox1.ForeColor == Color.Red)
            {
                groupBox1.ForeColor = groupBox2.ForeColor = groupBox3.ForeColor = groupBox4.ForeColor = groupBox5.ForeColor = Color.Orange;
            }
            else
            {
                groupBox1.ForeColor = groupBox2.ForeColor = groupBox3.ForeColor = groupBox4.ForeColor = groupBox5.ForeColor = Color.Red;
            }
        }

        private void RgvGlobalInfo_PropertyChangedEvent()
        {
            if (isShown)
            {
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (rgvGlobalInfo.RgvIsAlarm > 0)
                        {
                            timer1.Enabled = true;
                            return;
                        }
                        else
                        {
                            timer1.Enabled = false;
                            groupBox1.ForeColor = groupBox2.ForeColor = groupBox3.ForeColor = groupBox4.ForeColor = groupBox5.ForeColor = SystemColors.ControlText;
                        }
                        tb_speed.Text = GetAlarmStr(rgvGlobalInfo.RgvCurrentRunSpeed,
                            aGauge_speed.GaugeRanges[0], aGauge_speed.GaugeRanges[1],
                            groupBox1, tb_speed) +
                            rgvGlobalInfo.RgvCurrentRunSpeed.ToString();
                        tb_power.Text = GetAlarmStr(rgvGlobalInfo.RgvCurrentPowerElectricity,
                            aGauge_power.GaugeRanges[0], aGauge_power.GaugeRanges[1],
                            groupBox2, tb_power) +
                            rgvGlobalInfo.RgvCurrentPowerElectricity.ToString();
                        tb_current.Text = GetAlarmStr(rgvGlobalInfo.RgvCurrentPowerCurrent,
                            aGauge_current.GaugeRanges[0], aGauge_current.GaugeRanges[1],
                            groupBox3, tb_current) +
                            rgvGlobalInfo.RgvCurrentPowerCurrent.ToString();
                        tb_tempture.Text = GetAlarmStr(rgvGlobalInfo.RgvCurrentPowerTempture,
                            aGauge_tempture.GaugeRanges[0], aGauge_tempture.GaugeRanges[1],
                            groupBox4, tb_tempture) +
                            rgvGlobalInfo.RgvCurrentPowerTempture.ToString();
                        tb_distacnce.Text = rgvGlobalInfo.RgvCurrentRunDistacnce.ToString();
                    }));
                }
            }
        }

        private string GetAlarmStr(int value, AGaugeRange alarm1, AGaugeRange alarm2, GroupBox group, TextBox tb)
        {
            if (value >= alarm1.StartValue && value <= alarm1.EndValue)
            {
                group.ForeColor = tb.ForeColor = Color.Orange;
                return "注意：";
            }
            else if (value >= alarm2.StartValue && value <= alarm2.EndValue)
            {
                group.ForeColor = tb.ForeColor = Color.Red;
                return "危险：";
            }
            else
            {
                group.ForeColor = tb.ForeColor = SystemColors.ControlText;
            }
            return "";
        }
    }
}
