using System;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class ResultUpdForm : Form
    {
        public ResultData Data { get; set; }

        public ResultUpdForm()
        {
            InitializeComponent();
        }

        private void ResultUpdForm_Load(object sender, EventArgs e)
        {
            if (Data.ResultType == ResultType.ThrD)
            {
                cb.Visible = false;
            }
            else
            {
                cb.Items.AddRange(ServerGlobal.GetResultDataArray());
                tb.Visible = false;
            }

            lb_old.Text = Data.OldData;
            if (Data.IsUpdData)
            {
                lb_data.Text = Data.Data; 
            }
            else
            {
                label2.Visible = lb_data.Visible = false;
            }

            switch (Data.ResultState)
            {
                case ResultState.Normal:
                    cb_Normal.Checked = true;
                    break;
                case ResultState.Abnormal:
                    cb_Abnormal.Checked = true;
                    break;
                case ResultState.None:
                    cb_None.Checked = true;
                    break;
            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb.SelectedIndex == 0)
            {
                cb_Normal.Checked = true;
            }
            else
            {
                cb_Abnormal.Checked = true;
            }
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                if (cb != cb_Abnormal)
                {
                    cb_Abnormal.Checked = false;
                }
                if (cb != cb_None)
                {
                    cb_None.Checked = false;
                }
                if (cb != cb_Normal)
                {
                    cb_Normal.Checked = false;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (cb.Visible)
            {
                Data.Data = cb.Text;
            }
            else
            {
                Data.Data = tb.Text;
            }
            if (string.IsNullOrEmpty(Data.Data))
            {
                Data.Data = Data.OldData;
            }
            Data.ResultState = cb_Normal.Checked ? ResultState.Normal : cb_Abnormal.Checked ? ResultState.Abnormal : ResultState.None;
            this.DialogResult = DialogResult.OK;
        }
    }
}
