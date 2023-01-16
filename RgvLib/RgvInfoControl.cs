using EMU.Interface;
using System;
using System.Windows.Forms;

namespace Rgv
{
    public partial class RgvInfoControl : UserControl
    {
        public RgvInfoControl()
        {
            InitializeComponent();
        }

        public void SetRgvInfo(EMU.ApplicationData.RgvGlobalInfo rgvinfo)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    label2.Text = rgvinfo.RgvCurrentRunDistacnce.ToString();
                    label4.Text = rgvinfo.RgvCurrentRunSpeed.ToString();
                    label6.Text = rgvinfo.RgvTargetRunSpeed.ToString();
                    label8.Text = rgvinfo.RgvCurrentPowerElectricity.ToString();
                    label10.Text = rgvinfo.RgvCurrentPowerCurrent.ToString();
                    label12.Text = rgvinfo.RgvCurrentPowerTempture.ToString();
                    label14.Text = rgvinfo.RgvTargetRunDistance.ToString();
                    label16.Text = rgvinfo.RgvTrackLength.ToString();
                    label18.Text = rgvinfo.RgvIsAlarm.ToString();
                    label22.Text = rgvinfo.RgvCurrentStat;
                }));
            }
            catch (Exception) { }
        }

        public void SetRgvCmd(EMU.Parameter.RgvCmd rgvCmd)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    switch (rgvCmd)
                    {
                        case EMU.Parameter.RgvCmd.INVALID:
                            label20.Text = "初始化RGV";
                            break;
                        case EMU.Parameter.RgvCmd.CONNECT:
                            label20.Text = "连接RGV";
                            break;
                        case EMU.Parameter.RgvCmd.DISCONNECT:
                            label20.Text = "断开RGV";
                            break;
                        case EMU.Parameter.RgvCmd.NORMALSTOP:
                            label20.Text = "正常停止";
                            break;
                        case EMU.Parameter.RgvCmd.FORWARDMOTOR:
                            label20.Text = "正向运动";
                            break;
                        case EMU.Parameter.RgvCmd.BACKWARDMOTOR:
                            label20.Text = "反向运动";
                            break;
                        case EMU.Parameter.RgvCmd.STOPPOWERCHARGE:
                            label20.Text = "停止充电";
                            break;
                        case EMU.Parameter.RgvCmd.CLEARALARM:
                            label20.Text = "清除报警";
                            break;
                        case EMU.Parameter.RgvCmd.STARTPOWERCHARGE:
                            label20.Text = "开始充电";
                            break;
                        case EMU.Parameter.RgvCmd.RUNAPPOINTDISTANCE:
                            label20.Text = "运动到指定位置";
                            break;
                        case EMU.Parameter.RgvCmd.SETTARGETSPEED:
                            label20.Text = "设置目标速度";
                            break;
                        case EMU.Parameter.RgvCmd.SETTARGETDISATNCE:
                            label20.Text = "设置目标距离";
                            break;
                        case EMU.Parameter.RgvCmd.SETTRACKLENGTH:
                            label20.Text = "设置轨道长度";
                            break;
                    }
                }));
            }
            catch (Exception) { }
        }

        private void RgvInfoControl_Load(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.SetRgvModCmd += SetRgvCmd;
            RgvModCtrlHelper.Instance.GetRgvModInfo += SetRgvInfo;
        }
    }
}
