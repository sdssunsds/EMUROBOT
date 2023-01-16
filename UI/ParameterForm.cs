using EMU.Interface;
using System;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class ParameterForm : Form
    {
        public IProject Project { get; set; }

        public ParameterForm()
        {
            InitializeComponent();
        }

        private void ParameterForm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Project.GetParameterObject();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Project.SaveParameterObject();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
