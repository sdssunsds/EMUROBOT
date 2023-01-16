using System;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class InputForm : Form
    {
        public string Value { get; set; }

        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Value = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
