using Project.ServerClass.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ProjectControl : UserControl
    {
        public ProjectModel ProjectModel { get; private set; }
        public Action<ProjectModel> DeleteProject;
        public Action<ProjectModel> UpdateProject;
        public Action<ProjectModel> ShowProjectResult;
        public Action<ProjectModel> StartProject;

        public ProjectControl()
        {
            InitializeComponent();
        }

        private void ProjectControl_Load(object sender, EventArgs e)
        {
            ServerGlobal.RobotTaskEvent += ServerGlobal_RobotTaskEvent;
        }

        private void ServerGlobal_RobotTaskEvent(string id, bool isStart)
        {
            if (ProjectModel?.Robot == id)
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        btn_start.Enabled = btn_del.Enabled = !isStart;
                    }));
                }
            }
        }

        public void SetModel(ProjectModel model)
        {
            ProjectModel = model;
            if (model.Enable)
            {
                RobotServer.Instance.SetInfoEvent += Instance_SetInfoEvent;
            }
            ProjectModel.SetPropertyEvent += ProjectModel_SetPropertyEvent;
            SetWindow();
            ServerGlobal.ProgressProjectDict.Add(model.ID, (int i, int max) =>
            {
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar.Maximum = max;
                        if (i <= max)
                        {
                            progressBar.Value = i; 
                        }
                    }));
                }
            });
        }

        private void Instance_SetInfoEvent(SocketRgvInfo obj)
        {
            if (obj.ID == ProjectModel.Robot)
            {
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        lb_job.Text = obj.Job;
                    }));
                } 
            }
        }

        private void ProjectModel_SetPropertyEvent()
        {
            SetWindow();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (btn_del.Text == "结束计划")
            {
                ProjectModel.Enable = false;
                UpdateProject?.Invoke(ProjectModel);
            }
            else
            {
                DeleteProject?.Invoke(ProjectModel); 
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (ServerGlobal.StartProjectDict.ContainsKey(ProjectModel.Robot))
            {
                MessageBox.Show("机器人" + ProjectModel.Robot + "正在作业！");
                return;
            }
            if (btn_start.Text != "重启\r\n计划")
            {
                btn_start.Text = "重启\r\n计划";
                btn.Text = "检修\r\n结果";
                btn_del.Text = "结束计划";
            }
            StartProject?.Invoke(ProjectModel);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (btn.Text == "检修\r\n结果")
            {
                ShowProjectResult?.Invoke(ProjectModel); 
            }
            else
            {
                UpdateProject?.Invoke(ProjectModel);
            }
        }

        private void ProjectControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Global.MainColor, 3), new Rectangle(0, 0, Width - 4, Height - 4));
        }

        private void SetWindow()
        {
            if (!this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    lb_date.Text = ProjectModel.ProjectDate.ToString("yyyy-MM-dd HH:mm:ss");
                    lb_state.Text = ProjectModel.ProjectState.ToString();
                    lb_text.Text = ProjectModel.ProjectText;
                    lb_address.Text = ProjectModel.TestAddress;
                    lb_rode.Text = ProjectModel.TestRode;
                    lb_point.Text = ProjectModel.TestPoint;
                    lb_start_date.Text= ProjectModel.TestStart?.ToString("yyyy-MM-dd HH:mm:ss");
                    lb_end_date.Text = ProjectModel.TestEnd?.ToString("yyyy-MM-dd HH:mm:ss");
                    lb_mode.Text = ProjectModel.Mode;
                    lb_sn.Text = ProjectModel.Sn;
                    lb_robot.Text = ProjectModel.Robot;
                    lb_count.Text = ProjectModel.Count.ToString();
                    btn_del.Visible = btn_start.Visible = ProjectModel.Enable;
                    if (ProjectModel.ProjectState != ProjectState.计划中)
                    {
                        btn_start.Text = "重启\r\n计划";
                        btn.Text = "检修\r\n结果";
                        btn_del.Text = "结束计划";
                    }
                }));
            }
        }
    }
}
