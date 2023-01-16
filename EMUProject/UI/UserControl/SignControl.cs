using System;
using System.Drawing;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class SignControl : UserControl
    {
        private int upX = 0;
        private Pen pen = null;
        private Graphics topG = null;
        private Graphics bottomG = null;

        public SignControl()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
        }

        public void ClearData()
        {
            topG?.FillRectangle(new SolidBrush(paneltop.BackColor), 0, 0, paneltop.Width, paneltop.Height);
            bottomG?.FillRectangle(new SolidBrush(panelBottom.BackColor), 0, 0, panelBottom.Width, panelBottom.Height);
            BeginInvoke(new Action(() =>
            {
                paneltop.Refresh();
                panelBottom.Refresh();
                InitPanel();
            }));
        }

        /// <summary>
        /// 设置绘制的数据
        /// </summary>
        /// <param name="isStart">是否起始位</param>
        /// <param name="location">当前位置</param>
        /// <param name="length">总长度</param>
        /// <param name="txt">位置文本</param>
        /// <param name="isBack">是否返回过程</param>
        public void SetData(bool isStart, int location, int length, string txt, bool isBack = false)
        {
            double d = (double)location / length;
            int x = (int)(this.Width * d);
            if (isBack)
            {
                if (bottomG == null)
                {
                    Bitmap bitmap = new Bitmap(this.Width, 50);
                    bottomG = Graphics.FromImage(bitmap);
                    Invoke(new Action(() =>
                    {
                        panelBottom.BackgroundImage = bitmap;
                    }));
                }
                if (isStart)
                {
                    DrawTop(bottomG, x, txt, panelBottom.Font);
                }
                else
                {
                    DrawEnd(bottomG, x, txt, panelBottom.Font);
                }
                BeginInvoke(new Action(() =>
                {
                    panelBottom.Refresh();
                }));
            }
            else
            {
                if (topG == null)
                {
                    Bitmap bitmap = new Bitmap(this.Width, 50);
                    topG = Graphics.FromImage(bitmap);
                    Invoke(new Action(() =>
                    {
                        paneltop.BackgroundImage = bitmap;
                    }));
                }
                if (isStart)
                {
                    DrawTop(topG, x, txt, paneltop.Font);
                }
                else
                {
                    DrawEnd(topG, x, txt, paneltop.Font);
                }
                BeginInvoke(new Action(() =>
                {
                    paneltop.Refresh();
                }));
            }
            upX = x;
        }

        public void SetEnd()
        {
            BeginInvoke(new Action(() =>
            {
                panelBottom.Visible = true;
            }));
        }

        public void ShowAlarm(int location, int length)
        {
            double d = (double)location / length;
            int x = (int)(this.Width * d - 12.5);
            if (x < 0)
            {
                x = 0;
            }
            else if (x + 25 > this.Width)
            {
                x = this.Width - 25;
            }
            DrawAlarm(panelBottom.Visible ? bottomG : topG, x);
            Invoke(new Action(() =>
            {
                if (panelBottom.Visible)
                {
                    panelBottom.Refresh();
                }
                else
                {
                    paneltop.Refresh();
                }
            }));
        }

        private void SignControl1_Load(object sender, EventArgs e)
        {
            InitPanel();
        }

        private void DrawTop(Graphics g, int x, string txt, Font font)
        {
            g.DrawLine(pen, new Point(x, 20), new Point(x, 50));
            SizeF txtSize = g.MeasureString(txt, font);
            int _x = (int)(x - txtSize.Width / 2);
            _x = _x < 0 ? 0 : _x + txtSize.Width >= this.Width ? this.Width - 1 - (int)txtSize.Width : _x;
            g.DrawString(txt, font, new SolidBrush(pen.Color), new Point(_x, 5));
        }

        private void DrawEnd(Graphics g, int x, string txt, Font font)
        {
            g.DrawLine(pen, new Point(x, 20), new Point(x, 50));
            SizeF txtSize = g.MeasureString(txt, font);
            int _w = Math.Abs(upX - x);
            int _x = x < upX ? x + _w / 2 : upX + _w / 2;
            _x = _x - txtSize.Width / 2 < 0 ? 0 : _x + txtSize.Width / 2 >= this.Width ? this.Width - 1 - (int)txtSize.Width : _x - (int)(txtSize.Width / 2);
            int _y = txtSize.Height < 30 ? 35 - (int)(txtSize.Height / 2) : 48 - (int)txtSize.Height;
            g.DrawString(txt, font, new SolidBrush(pen.Color), new Point(_x, _y));
            if (_w > txtSize.Width)
            {
                _w = (int)((_w - txtSize.Width) / 2);
                _w = _w > 3 ? _w - 2 : _w;
                _x = x < upX ? 0 - _w : _w;
                if (txtSize.Height < 30)
                {
                    g.DrawLine(pen, new Point(upX, 35), new Point(upX + _x, 35));
                    g.DrawLine(pen, new Point(x, 35), new Point(x - _x, 35));
                }
                else
                {
                    g.DrawLine(pen, new Point(upX, 20), new Point(upX + _x, 20));
                    g.DrawLine(pen, new Point(x, 20), new Point(x - _x, 20));
                }
            }
        }

        private void DrawAlarm(Graphics g, int x)
        {
            g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(x - 3, 44, 6, 6));
            g.DrawLine(pen, new Point(x - 3, 49), new Point(x + 3, 49));
        }

        private void InitPanel()
        {
            panelBottom.Visible = false;
        }
    }
}
