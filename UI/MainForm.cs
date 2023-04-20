using EMU.Interface;
using Project;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class MainForm : Form
    {
        public static string Version = "1.0.0";
        public IMainForm MF { private get; set; }
        public IProject Project
        {
            get { return mainControl1.Project; }
            set
            {
                mainControl1.Project = value;
                Iinit iinit = null;
                if (value is RobotClient)
                {
                    value.homePage = new HomePage()
                    {
                        Project = value,
                        InitObject = new Lib.RobotInit()
                    };
                }
                else if (value is Server)
                {
                    iinit = new Lib.ServerInit();
                }
                else if (value is AGVServer)
                {
                    iinit = new Lib.AGVInit();
                }
                else if (value is AlgorithmInterface)
                {
                    iinit = new Lib.AlgorithmInterfaceInit();
                }
                iinit?.Setup(value, null);
            }
        }
        public string[] Args { private get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Project.SkinChanged = SkinChange;
            Project.ColorChanged = ColorChange;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SkinFile))
            {
                skinEngine.SkinDialogs = false;
                skinEngine.SkinFile = Properties.Settings.Default.SkinFile;
            }
            Global.MainColor = label1.BackColor = label2.BackColor = pictureBox1.BackColor = Properties.Settings.Default.Color;
            Global.ForeColor = ForeColor = label1.ForeColor = label2.ForeColor = Properties.Settings.Default.FontColor;
            if (!string.IsNullOrEmpty(Project.ChineseTitle))
            {
                label1.Text = Project.ChineseTitle; 
            }
            if (!string.IsNullOrEmpty(Project.EnglishTitle))
            {
                label2.Text = Project.EnglishTitle; 
            }
            MF?.Load(Args);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            MF?.Shown();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MF?.Closing();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Properties.Settings.Default.Color), 0, 0, panel1.Width, panel1.Height);
        }

        private void SkinChange()
        {
            skinEngine.SkinFile = Properties.Settings.Default.SkinFile;
        }

        private void ColorChange()
        {
            Global.MainColor = label1.BackColor = label2.BackColor = pictureBox1.BackColor = Properties.Settings.Default.Color;
            Global.ForeColor = label1.ForeColor = label2.ForeColor = Properties.Settings.Default.FontColor;
            panel1.Refresh();
        }
    }
}
