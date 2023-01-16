using EMU.Parameter;
using EMU.Util;
using System;
using System.Windows.Forms;

namespace Laser
{
    public partial class LaserControl : UserControl
    {
        private int rangingLogCount = 0;

        public LaserControl()
        {
            InitializeComponent();
        }

        private void LaserControl_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            LogManager.AddLogEvent += LogManager_AddLogEvent;
            LaserManager.Instance.GetLaserData += Instance_GetLaserData;
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            BeginInvoke(new Action(() =>
            {
                textBox3.Text += arg1 + "\r\n";
                textBox3.SelectionStart = textBox3.Text.Length;
                textBox3.ScrollToCaret();
            }));
        }

        private void Instance_GetLaserData(string data, LaserName laser)
        {
            BeginInvoke(new Action(() =>
            {
                switch (laser)
                {
                    case LaserName.LocationLaser:
                        break;
                    case LaserName.RangingLaser:
                        textBox2.Text += data + "\r\n";
                        rangingLogCount++;
                        if (rangingLogCount > 1500)
                        {
                            btn_clear2_Click(null, null);
                        }
                        textBox2.SelectionStart = textBox2.Text.Length;
                        textBox2.ScrollToCaret();
                        break;
                }
            }));
        }

        private void btn_open1_Click(object sender, EventArgs e)
        {
            AddText(LaserManager.Instance.LaserConnect(LaserName.LocationLaser), textBox1, "连接成功", "连接失败");
        }

        private void btn_close1_Click(object sender, EventArgs e)
        {
            AddText(LaserManager.Instance.LaserDisConnect(LaserName.LocationLaser), textBox1, "已完成断开", "断开时出错");
        }

        private void btn_open2_Click(object sender, EventArgs e)
        {
            AddText(LaserManager.Instance.LaserConnect(LaserName.RangingLaser), textBox2, "连接成功", "连接失败");
        }

        private void btn_close2_Click(object sender, EventArgs e)
        {
            AddText(LaserManager.Instance.LaserDisConnect(LaserName.RangingLaser), textBox2, "已完成断开", "断开时出错");
        }

        private void AddText(bool b, TextBox tb, params string[] vs)
        {
            if (b)
            {
                tb.Text += vs[0] + "\r\n";
            }
            else
            {
                tb.Text += vs[1] + "\r\n";
            }
            tb.SelectionStart = tb.Text.Length;
            tb.ScrollToCaret();
        }

        private void btn_clear1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void btn_clear2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            rangingLogCount = 0;
        }
    }
}
