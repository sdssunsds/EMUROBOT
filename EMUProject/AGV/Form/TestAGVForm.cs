using EMU.Interface;
using EMU.Util;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class TestAGVForm : Form
    {
        private LogForm logForm;

        public IProject Project
        {
            get { return testControl1.Project; }
            set { testControl1.Project = value; }
        }

        public TestAGVForm()
        {
            InitializeComponent();
            logForm = new LogForm();
        }

        private void TestAGVForm_Shown(object sender, EventArgs e)
        {
            testControl1.AddLog = logForm.AddLog;
            ThreadManager.TaskRun((ThreadEventArgs args) =>
            {
                while (true)
                {
                    testControl1.RefreshMap();
                    Thread.Sleep(250);
                }
            });
        }

        private void btn_open_log_Click(object sender, EventArgs e)
        {
            logForm.Show(this);
        }
    }
}
