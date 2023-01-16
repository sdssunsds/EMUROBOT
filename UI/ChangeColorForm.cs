using System;
using System.Drawing;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class ChangeColorForm : Form
    {
        private MainColor mainColor = null;
        public Color SelectColor
        {
            get { return mainColor.主题色; }
            set { mainColor.主题色 = value; }
        }
        public Color FontColor
        {
            get { return mainColor.文字色; }
            set { mainColor.文字色 = value; }
        }

        public ChangeColorForm()
        {
            InitializeComponent();
            mainColor = new MainColor();
        }

        private void ChangeColorForm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = mainColor;
        }

        private class MainColor
        {
            public Color 主题色 { get; set; }
            public Color 文字色 { get; set; }
        }
    }
}
