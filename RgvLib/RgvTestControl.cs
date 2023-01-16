using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rgv
{
    public partial class RgvTestControl : UserControl
    {
        public Color RgvColor
        {
            get { return rgvControl1.RgvColor; }
            set { rgvControl1.RgvColor = value; }
        }

        public RgvTestControl()
        {
            InitializeComponent();
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvConnect();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvDisConnect();
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvForwardMove();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvBackMove();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvNormalStop();
        }

        private void btn_estop_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvEmergeStop();
        }

        private void btn_power_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvIntelligentCharging();
        }

        private void btn_stopPower_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvTerminateCharging();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvClearAlarm();
        }

        private void btn_speed_Click(object sender, EventArgs e)
        {
            try
            {
                RgvModCtrlHelper.Instance.RgvSetSpeed(int.Parse(textBox1.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_distance_Click(object sender, EventArgs e)
        {
            try
            {
                RgvModCtrlHelper.Instance.RgvSetDistance(int.Parse(textBox2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_length_Click(object sender, EventArgs e)
        {
            try
            {
                RgvModCtrlHelper.Instance.RgvSetLength(int.Parse(textBox3.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_move_Click(object sender, EventArgs e)
        {
            RgvModCtrlHelper.Instance.RgvRunDistance();
        }
    }
}
