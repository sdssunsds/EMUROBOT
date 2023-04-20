using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class DetectionResultHistoryControl : UserControl
    {
        private List<ProjectResultDict> list = null;

        public DetectionResultHistoryControl()
        {
            InitializeComponent();
        }

        private void DetectionResultHistoryControl_Load(object sender, EventArgs e)
        {
            dataGridView3D.AutoGenerateColumns = false;
            list = ServerGlobal.DataBase.GetTs<ProjectResultDict>();
            if (list != null)
            {
                foreach (ProjectResultDict item in list)
                {
                    lb_mode.Items.Add(item.Mode);
                }
                if (lb_mode.Items.Count > 0)
                {
                    lb_mode.SelectedIndex = 0;
                }
            }
        }

        private void lb_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = lb_mode.SelectedIndex;
                ProjectResultDict item = list[i];
                if (item.SnList != null)
                {
                    lb_sn.Items.Clear();
                    foreach (ModeSnDict modeSnDict in item.SnList)
                    {
                        lb_sn.Items.Add(modeSnDict.Sn);
                    }
                    if (lb_sn.Items.Count > 0)
                    {
                        lb_sn.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void lb_sn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = lb_mode.SelectedIndex;
                int j = lb_sn.SelectedIndex;
                List<string> ids = list[i].SnList[j].IdList;
                List<string> times = new List<string>();
                if (ServerGlobal.ProjectModels.Count == 0)
                {
                    ServerGlobal.ProjectModels = ServerGlobal.DataBase.GetTs<ProjectModel>();
                }
                foreach (string id in ids)
                {
                    ProjectModel project = ServerGlobal.ProjectModels.Find(p => p.ID == id);
                    if (project != null)
                    {
                        times.Add(project.ProjectDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        times.Add("计划被删除");
                    }
                }
                lb_time.Items.Clear();
                lb_time.Items.AddRange(times.ToArray());
                if (lb_time.Items.Count > 0)
                {
                    lb_time.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void lb_time_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = lb_mode.SelectedIndex;
                int j = lb_sn.SelectedIndex;
                int k = lb_time.SelectedIndex;
                string id = list[i].SnList[j].IdList[k];
                ProjectModel project = new ProjectModel() { ID = id, Mode = list[i].Mode, Sn = list[i].SnList[j].Sn };
                List<ResultData> results = ServerGlobal.DataBase.GetTs<ResultData>(null, project);
                
                dataGridView.DataSource = null;
                dataGridView.DataSource = new ResultStatisticsCollection(results.FindAll(r => r.ResultType != ResultType.ThrD)).BindResultDatas;
                try
                {
                    ServerGlobal.SetDataGridViewHead(dataGridView);
                }
                catch (Exception) { }

                dataGridView3D.DataSource = null;
                dataGridView3D.DataSource = results.FindAll(r => r.ResultType == ResultType.ThrD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
