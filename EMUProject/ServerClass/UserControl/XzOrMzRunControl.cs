using EMU.ApplicationData;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class XzOrMzRunControl : UserControl
    {
        public bool IsXz { private get; set; }

        public XzOrMzRunControl()
        {
            InitializeComponent();
        }

        public void SetStartEnd(int start, int end)
        {
            tb_start.Text = start.ToString();
            tb_end.Text = end.ToString();
        }

        public void SetMzData(List<BackDataExtend> list)
        {
            dgv.DataSource = null;
            dgv.DataSource = list;
        }

        private void XzOrMzRunControl_Load(object sender, EventArgs e)
        {

        }

        private void XzOrMzRunControl_SizeChanged(object sender, EventArgs e)
        {
            dgv.Size = new System.Drawing.Size(flowLayoutPanel1.Width - 6, flowLayoutPanel1.Height - 32);
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_start.Text, out _))
            {
                MessageBox.Show("起始位置数据非法");
                tb_start.Focus();
                return;
            }
            if (!int.TryParse(tb_end.Text, out _))
            {
                MessageBox.Show("结束位置数据非法");
                tb_end.Focus();
                return;
            }

            if (IsXz)
            {
                RobotServer.Instance.Command(Cmd.forward_start, pars: int.Parse(tb_start.Text) + "," + int.Parse(tb_end.Text));
            }
            else
            {
                string json = JsonManager.ObjectToJson(dgv.DataSource as List<BackDataExtend>);
                RobotServer.Instance.Command(Cmd.backward_start, pars: json);
            }
        }
    }
}
