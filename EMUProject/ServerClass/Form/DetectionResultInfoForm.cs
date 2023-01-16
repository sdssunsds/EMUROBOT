using EMU.Util;
using System;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class DetectionResultInfoForm : Form
    {
        public DetectionResultInfoForm()
        {
            InitializeComponent();
        }

        private void DetectionResultInfoForm_Load(object sender, EventArgs e)
        {
            
        }

        private void DetectionResultInfoForm_Shown(object sender, EventArgs e)
        {
            ResultListForm form = new ResultListForm()
            {
                Project = detectionResultControl.ProjectModel,
                Datas = detectionResultControl.resultControl.ResultDatas
            };
            form.AddResfresh(detectionResultControl.resultControl.RefreshDatas);
            form.Show(this);
        }
    }
}
