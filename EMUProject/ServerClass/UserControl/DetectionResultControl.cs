using AlgorithmLib;
using EMU.Util;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class DetectionResultControl : UserControl
    {
        public ProjectModel ProjectModel
        {
            get { return resultControl.ProjectModel; }
            set { resultControl.ProjectModel = value; }
        }

        public DetectionResultControl()
        {
            InitializeComponent();
        }

        private void DetectionResultControl_Load(object sender, EventArgs e)
        {
            if (ProjectModel != null)
            {
                lb_address.Text = ProjectModel.TestAddress;
                lb_count.Text = ProjectModel.Count.ToString();
                lb_date.Text = ProjectModel.ProjectDate.ToString("yyyy-MM-dd HH:mm:ss");
                lb_end.Text = ProjectModel.TestEnd?.ToString("yyyy-MM-dd HH:mm:ss");
                lb_mode.Text = ProjectModel.Mode;
                lb_point.Text = ProjectModel.TestPoint;
                lb_robot.Text = ProjectModel.Robot;
                lb_rode.Text = ProjectModel.TestRode;
                lb_sn.Text = ProjectModel.Sn;
                lb_start.Text = ProjectModel.TestStart?.ToString("yyyy-MM-dd HH:mm:ss");
                lb_state.Text = ProjectModel.ProjectState.ToString();
                lb_text.Text = ProjectModel.ProjectText;
                resultControl.Is16Count = ProjectModel.Count == 16; 
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            timer1.Enabled = progressBar1.Visible = true;
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                ProjectResultDict projectResultDict = new ProjectResultDict();
                projectResultDict.Mode = ProjectModel.Mode;
                projectResultDict.SnList = new List<ModeSnDict>();
                projectResultDict.SnList.Add(new ModeSnDict()
                {
                    Sn = ProjectModel.Sn,
                    IdList = new List<string>() { ProjectModel.ID }
                });
                ServerGlobal.DataBase.AddT<ProjectResultDict>(projectResultDict);
                ServerGlobal.DataBase.SaveTs<ResultData>(resultControl.ResultDatas, ProjectModel);

                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        timer1.Enabled = progressBar1.Visible = false;
                        btn_save.Enabled = true;
                    }));
                }
                MessageBox.Show("数据保存完成。");
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
            }
        }
    }
}
