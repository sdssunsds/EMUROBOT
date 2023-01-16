using EMU.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class TestForm : Form
    {
        public Dictionary<string, object> TestVariableDict { get; private set; }

        public TestForm()
        {
            InitializeComponent();
            TestVariableDict = new Dictionary<string, object>();
        }

        public void TestVariableChange(string name, object value)
        {
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                if (TestVariableDict.ContainsKey(name))
                {
                    TestVariableDict[name] = value;
                    UpdTestVariable(name, value);
                }
                else
                {
                    TestVariableDict.Add(name, value);
                    AddTestVariable(name, value);
                }
            });
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, object> item in TestVariableDict)
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = item.Key;
                dataGridView1.Rows[i].Cells[1].Value = item.Value;
            }
        }

        private void AddTestVariable(string name, object value)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    int i = dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = name;
                    dataGridView1.Rows[i].Cells[1].Value = value;
                })); 
            }
        }

        private void UpdTestVariable(string name, object value)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value?.ToString() == name)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = value; 
                        }
                    }
                }));
            }
        }
    }
}
