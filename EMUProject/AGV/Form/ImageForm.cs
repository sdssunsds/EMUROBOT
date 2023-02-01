using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class ImageForm : Form
    {
        private double zoom = 1d;
        private Size imgSize;

        public Image Image { private get; set; }

        public ImageForm()
        {
            InitializeComponent();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Size = imgSize = new Size(Image.Width, Image.Height);
            pictureBox1.Image = Image;
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom += 0.1;
            }
            else
            {
                zoom -= 0.1;
            }
            pictureBox1.Size = new Size((int)(imgSize.Width * zoom), (int)(imgSize.Height * zoom));
        }
    }
}
