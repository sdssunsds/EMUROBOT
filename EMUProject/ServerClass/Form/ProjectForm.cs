using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ProjectForm : Form
    {
        private DataList dataList = DataList.Instance;

        public ProjectModel ProjectModel { get; set; }

        public ProjectForm()
        {
            InitializeComponent();
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            if (dataList != null)
            {
                cb_address.Items.AddRange(dataList.addressList?.ToArray());
                cb_mode.Items.AddRange(dataList.modeList?.ToArray()); 
            }
            cb_robot.Items.AddRange(ServerGlobal.LinkRobotList?.ToArray());
            cb_type.Items.AddRange(Enum.GetNames(typeof(ProjectType)));
            cb_num.SelectedIndex = cb_type.SelectedIndex = cb_head.SelectedIndex = 0;

            if (ProjectModel != null)
            {
                cb_address.Text = ProjectModel.TestAddress;
                cb_mode.Text = ProjectModel.Mode;
                cb_num.SelectedIndex = ProjectModel.Count == 8 ? 0 : 1;
                cb_robot.Text = ProjectModel.Robot;
                cb_rode.Text = ProjectModel.TestRode;
                cb_sn.Text = ProjectModel.Sn;
                cb_type.Text = ProjectModel.TestType.ToString();
                cb_head.Text = ProjectModel.Head;
                if (ProjectModel.TestPoint == rb_one.Text)
                {
                    rb_one.Checked = true;
                } 
            }
        }

        private void cb_address_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataList.rodeList.ContainsKey(cb_address.Text))
            {
                cb_rode.Items.Clear();
                cb_rode.Items.AddRange(dataList.rodeList[cb_address.Text]?.ToArray());
            }
        }

        private void cb_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataList.snList.ContainsKey(cb_mode.Text))
            {
                cb_sn.Items.Clear();
                cb_sn.Items.AddRange(dataList.snList[cb_mode.Text]?.ToArray());
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (ProjectModel == null)
            {
                ProjectModel = new ProjectModel();
            }

            ProjectModel.TestAddress = cb_address.Text;
            ProjectModel.Mode = cb_mode.Text;
            ProjectModel.Count = cb_num.SelectedIndex == 0 ? 8 : 16;
            ProjectModel.Robot = cb_robot.Text;
            ProjectModel.TestRode = cb_rode.Text;
            ProjectModel.Sn = cb_sn.Text;
            ProjectModel.TestPoint = rb_one.Checked ? rb_one.Text : rb_two.Text;
            ProjectModel.ProjectText = tb.Text;

            WriteList();
            this.Close();
        }

        private void WriteList()
        {
            if (dataList.addressList.IndexOf(cb_address.Text) < 0)
            {
                dataList.addressList.Add(cb_address.Text);
            }
            if (!dataList.rodeList.ContainsKey(cb_address.Text))
            {
                dataList.rodeList.Add(cb_address.Text, new List<string>());
            }
            if (dataList.rodeList[cb_address.Text].IndexOf(cb_rode.Text) < 0)
            {
                dataList.rodeList[cb_address.Text].Add(cb_rode.Text);
            }
            if (dataList.modeList.IndexOf(cb_mode.Text) < 0)
            {
                dataList.modeList.Add(cb_mode.Text);
            }
            if (!dataList.snList.ContainsKey(cb_mode.Text))
            {
                dataList.snList.Add(cb_mode.Text, new List<string>());
            }
            if (dataList.snList[cb_mode.Text].IndexOf(cb_sn.Text) < 0)
            {
                dataList.snList[cb_mode.Text].Add(cb_sn.Text);
            }
            dataList.Save();
        }
    }
}
