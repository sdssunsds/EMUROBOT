using EMU.ApplicationData;
using EMU.Parameter;
using System;
using System.Windows.Forms;

namespace Robot
{
    public partial class RobotTestControl : UserControl
    {
        public RobotTestControl()
        {
            InitializeComponent();
        }

        private void RobotTestControl_Load(object sender, EventArgs e)
        {
            RobotModCtrlHelper.Instance.GetRobotModInfo += Instance_GetRobotModInfo;

            frontRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Front]);
            frontRobotControl1.SetState(RobotGlobalInfo.Instance.FrontRobotRunStatMonitor);

            backRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Back]);
            backRobotControl1.SetState(RobotGlobalInfo.Instance.BackRobotRunStatMonitor);
        }

        private void Instance_GetRobotModInfo(RobotGlobalInfo robotinfo, RobotName robot)
        {
            switch (robot)
            {
                case RobotName.Front:
                    frontRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Front]);
                    frontRobotControl1.SetInfo(robotinfo.FrontRobotSiteData);
                    frontRobotControl1.SetState(RobotGlobalInfo.Instance.FrontRobotRunStatMonitor);
                    break;
                case RobotName.Back:
                    backRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Back]);
                    backRobotControl1.SetInfo(robotinfo.BackRobotSiteData);
                    backRobotControl1.SetState(RobotGlobalInfo.Instance.BackRobotRunStatMonitor);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RobotModCtrlHelper.Instance.RobotConnect(RobotName.Front);
            frontRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Front]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RobotModCtrlHelper.Instance.RobotConnect(RobotName.Back);
            backRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Back]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RobotModCtrlHelper.Instance.RobotDisConnect(RobotName.Front);
            frontRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Front]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RobotModCtrlHelper.Instance.RobotDisConnect(RobotName.Back);
            backRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Back]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (RobotModCtrlHelper.Instance.RobotMovePosition(RobotName.Front, new RobotDataPack()
                {
                    j1 = textBox1.Text,
                    j2 = textBox2.Text,
                    j3 = textBox3.Text,
                    j4 = textBox4.Text,
                    j5 = textBox5.Text,
                    j6 = textBox6.Text
                }))
            {
                frontRobotControl1.SetState(RobotGlobalInfo.Instance.FrontRobotRunStatMonitor);
            }
            frontRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Front]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (RobotModCtrlHelper.Instance.RobotMovePosition(RobotName.Back, new RobotDataPack()
            {
                j1 = textBox1.Text,
                j2 = textBox2.Text,
                j3 = textBox3.Text,
                j4 = textBox4.Text,
                j5 = textBox5.Text,
                j6 = textBox6.Text
            }))
            {
                backRobotControl1.SetState(RobotGlobalInfo.Instance.BackRobotRunStatMonitor);
            }
            backRobotControl1.SetConnect(RobotModCtrlHelper.Instance[RobotName.Back]);
        }
    }
}
