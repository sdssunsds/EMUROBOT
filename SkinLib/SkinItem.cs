using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skin
{
    public partial class SkinItem : UserControl
    {
        public bool Checked
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public string SSK { get; set; }

        public string ImagePath
        {
            set
            {
                pictureBox1.Image = Image.FromFile(value);
            }
        }

        public event Action<bool, SkinItem> CheckChanged;

        public SkinItem()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckChanged?.Invoke(checkBox1.Checked, this);
        }
    }
}
