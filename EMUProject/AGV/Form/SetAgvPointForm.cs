using EMU.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class SetAgvPointForm : Form
    {
        public bool Flip { get; set; } = false;
        public string NameValue { get; set; }
        public float TurnValue { get; set; }

        public SetAgvPointForm()
        {
            InitializeComponent();
        }

        private void SetAgvPointForm_Load(object sender, EventArgs e)
        {
            DrawTurn();
        }

        private void SetAgvPointForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NameValue = textBox1.Text;
            TurnValue = (float)Extend.GetRadian(trackBar1.Value);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            DrawTurn();
        }

        private void DrawTurn()
        {
            int a = trackBar1.Value;
            Bitmap bitmap = new Bitmap(45, 45);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Green);
            g.DrawEllipse(pen, 0, 0, 44, 44);
            Point origin = new Point(23, 23);
            g.DrawLine(pen, origin, Extend.GetRadianLineEnd(Extend.GetRadian(trackBar1.Value), 23, origin));
            if (Flip)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); 
            }
            pictureBox1.Image = bitmap;
        }
    }
}
