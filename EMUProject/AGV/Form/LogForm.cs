using System;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class LogForm : Form
    {
        private bool stop = false;

        public LogForm()
        {
            InitializeComponent();
        }

        public void AddLog(string log)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            if (!stop)
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        textBox1.Text += time + "  " + log + "\r\n";
                    }));
                }
                catch (Exception) { }
            }
        }

        private void LogForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (stop)
            {
                button1.Text = "暂停";
            }
            else
            {
                button1.Text = "继续";
            }
            stop = !stop;
        }
    }
}
