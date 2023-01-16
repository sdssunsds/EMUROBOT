using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class TestAGVForm : Form
    {
        public TestAGVForm()
        {
            InitializeComponent();
        }

        private void TestAGVForm_Shown(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    testControl1.RefreshMap();
                    Thread.Sleep(250);
                }
            });
        }
    }
}
