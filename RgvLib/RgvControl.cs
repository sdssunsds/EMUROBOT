using EMU.ApplicationData;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rgv
{
    public partial class RgvControl : UserControl
    {
        private bool isShowAlarm = false;
        private RgvModCtrlHelper helper = null;
        public Color RgvColor { get; set; }

        public RgvControl()
        {
            InitializeComponent();
            helper = RgvModCtrlHelper.Instance;
        }

        private void RgvControl_Load(object sender, EventArgs e)
        {
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            helper.GetRgvModInfo += Helper_GetRgvModInfo;

            ShowRgv(RgvColor);
            Bitmap bitmap = new Bitmap(pictureBox2.Width, 15);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.Maroon), 0, 0, pictureBox2.Width, 15);
            pictureBox2.Image = bitmap;
            graphics.Dispose();
        }

        private void Helper_GetRgvModInfo(RgvGlobalInfo rgvinfo)
        {
            toolTip.SetToolTip(pictureBox1, string.Format("位置：{0}\r\n速度：{1}\r\n电量：{2}\r\n温度：{3}",
                rgvinfo.RgvCurrentRunDistacnce,
                rgvinfo.RgvCurrentRunSpeed,
                rgvinfo.RgvCurrentPowerElectricity,
                rgvinfo.RgvCurrentPowerTempture));
            SetDistacnce(rgvinfo.RgvCurrentRunDistacnce);

            if (rgvinfo.RgvIsAlarm > 0)
            {
                ShowAlarm(true);
            }
            else
            {
                ShowAlarm(false);
            }
        }

        private void SetDistacnce(int distacnce)
        {
            int length = pictureBox2.Width;
            double inter = (double)length / RgvGlobalInfo.Instance.RgvTrackLength;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    int x = (int)(distacnce * inter);
                    pictureBox1.Location = new Point(x, pictureBox1.Location.Y);
                }));
            }
            catch (Exception) { }
        }

        private void ShowAlarm(bool isShow)
        {
            if (isShowAlarm != isShow)
            {
                isShowAlarm = isShow;
                Task.Run(() =>
                {
                    while (isShowAlarm)
                    {
                        ShowRgv(Color.Red);
                        Thread.Sleep(500);
                        ShowRgv(RgvColor);
                        Thread.Sleep(500);
                    }
                }); 
            }
        }

        private void ShowRgv(Color color)
        {
            try
            {
                Bitmap bitmap = new Bitmap(120, 65);
                Graphics g = Graphics.FromImage(bitmap);
                SolidBrush brush = new SolidBrush(color);
                g.FillRectangle(brush, 0, 10, 20, 40);
                g.FillRectangle(brush, 20, 20, 30, 30);
                g.FillRectangle(brush, 50, 10, 20, 40);
                g.FillRectangle(brush, 70, 20, 30, 30);
                g.FillRectangle(brush, 100, 10, 20, 40);
                g.FillEllipse(brush, 25, 5, 20, 20);
                g.FillEllipse(brush, 75, 5, 20, 20);
                g.FillEllipse(brush, 10, 35, 40, 30);
                g.FillEllipse(brush, 70, 35, 40, 30);
                try
                {
                    Invoke(new Action(() =>
                    {
                        pictureBox1.Image = bitmap;
                    }));
                }
                catch (Exception) { }
                g.Dispose();
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
            }
        }
    }
}
