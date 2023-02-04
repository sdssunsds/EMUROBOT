using EMU.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AlgorithmPage : UserControl
    {
        private int logCount = 0;

        public AlgorithmInterface Project { get; set; }

        public AlgorithmPage()
        {
            InitializeComponent();
        }

        private void AlgorithmPage_Load(object sender, EventArgs e)
        {
            LogManager.AddLogEvent += LogManager_AddLogEvent;
            ThreadManager.BackTask((int startIndex, ThreadEventArgs threadEventArgs) =>
            {
                List<ThreadEventArgs> list = ThreadManager.GetThreadEventArgs();
                ThreadEventArgs eventArgs = list.Find(t => t.ThreadName == Project.RedisThreadName);
                if (eventArgs != null)
                {
                    
                }
            });
        }

        private void LogManager_AddLogEvent(string arg1, EMU.Parameter.LogType arg2)
        {
            if (arg2 == EMU.Parameter.LogType.OtherLog)
            {
                logCount++;
                BeginInvoke(new Action(() =>
                {
                    if (logCount >= 5000)
                    {
                        logCount = 0;
                        textBox1.Text = "";
                    }
                    textBox1.Text += arg1 + "\r\n";
                    textBox1.SelectionStart = textBox1.Text.Length - 1;
                }));
            }
        }
    }
}
