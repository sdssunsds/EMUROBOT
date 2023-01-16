using EMU.ApplicationData;
using EMU.Parameter;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot
{
    public partial class RobotControl : UserControl
    {
        private const string text = "{0} : {1}";
        private const string j1 = "轴(J1)";
        private const string j2 = "轴(J2)";
        private const string j3 = "轴(J3)";
        private const string j4 = "轴(J4)";
        private const string j5 = "轴(J5)";
        private const string j6 = "轴(J6)";

        protected string GroupText
        {
            set { groupBox1.Text = value; }
        }

        public RobotControl()
        {
            InitializeComponent();
        }

        public void SetConnect(bool isConnect)
        {
            if (isConnect)
            {
                label7.Text = "静止";
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.Text = "离线";
                label7.ForeColor = Color.Red;
            }
        }

        public void SetInfo(RobotDataPack robot)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    label1.Text = string.Format(text, j1, robot.j1);
                    label2.Text = string.Format(text, j2, robot.j2);
                    label3.Text = string.Format(text, j3, robot.j3);
                    label4.Text = string.Format(text, j4, robot.j4);
                    label5.Text = string.Format(text, j5, robot.j5);
                    label6.Text = string.Format(text, j6, robot.j6);
                }));
            }
            catch (Exception) { }
        }

        public void SetState(EquipmentStatus status)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (status == EquipmentStatus.RUN)
                    {
                        progressBar1.Visible = true;
                        Task.Run(() =>
                        {
                            try
                            {
                                while (progressBar1.Visible)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        progressBar1.Value++;
                                        if (progressBar1.Value == progressBar1.Maximum)
                                        {
                                            progressBar1.Value = 0;
                                        }
                                    }));
                                    Thread.Sleep(100);
                                }
                            }
                            catch (Exception) { }
                        });
                    }
                    else
                    {
                        progressBar1.Visible = false;
                    }
                }));
            }
            catch (Exception) { }
        }

        private void RobotControl_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format(text, j1, "0.00");
            label2.Text = string.Format(text, j2, "0.00");
            label3.Text = string.Format(text, j3, "0.00");
            label4.Text = string.Format(text, j4, "0.00");
            label5.Text = string.Format(text, j5, "0.00");
            label6.Text = string.Format(text, j6, "0.00");
        }
    }
}
