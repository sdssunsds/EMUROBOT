using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project
{
    public partial class TestForm : Form
    {
        private List<string> variableSortList = new List<string>();
        private Dictionary<string, object> testVariable = new Dictionary<string, object>();

        public TestForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置调试变量
        /// </summary>
        public void SetTestVariable(string name, object value)
        {
            if (!this.IsDisposed && this.Created)
            {
                if (testVariable.ContainsKey(name))
                {
                    testVariable[name] = value;
                    variableSortList.Remove(name);
                }
                else
                {
                    testVariable.Add(name, value);
                }
                variableSortList.Insert(0, name);
                Invoke(new Action(BindVariable)); 
            }
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void BindVariable()
        {
            listBox1.Items.Clear();
            foreach (string name in variableSortList)
            {
                listBox1.Items.Add(name + ": " + testVariable[name]?.ToString());
            }
        }
    }
}
