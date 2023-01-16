using EMU.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class PictureForm : Form
    {
        private bool isMove = false;
        private int ligh = 0;
        private double zoom = 1;
        private Point orcPoint;
        private Point orcPicPoint;
        public Image Image { get; set; }
        public Color RectColor { get; set; }
        public Rectangle Rect { get; set; }

        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {
            ShowImage();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;

            int centerX = (panel1.Width - Rect.Width) / 2;
            int centerY = (this.Height - Rect.Height + 85) / 2;
            pictureBox1.Location = new Point(centerX - Rect.X, centerY - Rect.Y);
        }

        private void tb_pars_Scroll(object sender, EventArgs e)
        {
            ligh = tb_pars.Value;
            ShowImage();
        }

        private void cb_sign_CheckedChanged(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            orcPoint = e.Location;
            orcPicPoint = (sender as PictureBox).Location;
            isMove = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (isMove)
            {
                int x = e.X - orcPoint.X;
                int y = e.Y - orcPoint.Y;
                pb.Location = new Point(orcPicPoint.X + x, orcPicPoint.Y + y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom += 0.01;
            }
            else
            {
                zoom -= 0.01;
            }
            if (zoom < 0.01)
            {
                zoom = 0.01;
            }
            ShowImage();
        }

        private void ShowImage()
        {
            if (Image != null)
            {
                Bitmap bitmap = ImageManager.KiLighten(Image.Clone() as Bitmap, ligh);
                if (cb_sign.Checked)
                {
                    Graphics g = Graphics.FromImage(bitmap);
                    g.DrawRectangle(new Pen(RectColor), Rect);
                }
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
                pictureBox1.Size = new Size((int)(bitmap.Width * zoom), (int)(bitmap.Height * zoom));
            }
        }
    }
}
