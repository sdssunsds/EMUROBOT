using System;
using System.Threading;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class ProgressBarForm : Form
    {
        public Action Run { get; set; }

        public ProgressBarForm()
        {
            InitializeComponent();
        }

        private void ProgressBarForm_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            new Thread(new ThreadStart(() =>
            {
                Run?.Invoke();
                Invoke(new Action(() =>
                {
                    this.Close();
                }));
            })).Start();
        }

        public void SetMax(int max)
        {
            BeginInvoke(new Action(() =>
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = max;
            }));
        }

        public void SetValue()
        {
            BeginInvoke(new Action(() =>
            {
                if (progressBar1.Value < progressBar1.Maximum)
                {
                    progressBar1.Value++;
                    label1.Text = Math.Round((double)progressBar1.Value / progressBar1.Maximum * 100, 2) + "%";
                }
            }));
        }
    }
}
