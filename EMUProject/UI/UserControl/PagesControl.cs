using EMU.Util;
using System;
using System.Windows.Forms;

namespace Project
{
    public partial class PagesControl : UserControl
    {
        private int index = 0;
        private int count = 10;
        private int maxNumber = 0;
        /// <summary>
        /// 数据数量
        /// </summary>
        public int MaxDataNumber
        {
            get { return maxNumber; }
            set
            {
                maxNumber = value;
                InitPageCount();
            }
        }
        /// <summary>
        /// 翻页事件
        /// </summary>
        public event Action<int, int> PageChanged;
        /// <summary>
        /// 翻页进度事件
        /// </summary>
        public event Func<PageProgress> PageProgressBar;

        public PagesControl()
        {
            InitializeComponent();
        }

        public void PageChanging()
        {
            PageChange();
        }

        private void PageControl_Load(object sender, EventArgs e)
        {
            cb_count.SelectedIndex = 0;
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            if (cb_page.Items.Count > 0)
            {
                cb_page.SelectedIndex = 0;
            }
            btn_home.Enabled = btn_top.Enabled = cb_page.SelectedIndex > 0;
        }

        private void btn_top_Click(object sender, EventArgs e)
        {
            if (cb_page.SelectedIndex > 0)
            {
                cb_page.SelectedIndex--;
            }
            btn_home.Enabled = btn_top.Enabled = cb_page.SelectedIndex > 0;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (cb_page.SelectedIndex < cb_page.Items.Count - 1)
            {
                cb_page.SelectedIndex++;
            }
            btn_last.Enabled = btn_next.Enabled = cb_page.SelectedIndex < cb_page.Items.Count - 1;
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            if (cb_page.SelectedIndex < cb_page.Items.Count - 1)
            {
                cb_page.SelectedIndex = cb_page.Items.Count - 1;
            }
            btn_last.Enabled = btn_next.Enabled = cb_page.SelectedIndex < cb_page.Items.Count - 1;
        }

        private void cb_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = cb_page.SelectedIndex * count;
            btn_home.Enabled = btn_top.Enabled = cb_page.SelectedIndex > 0;
            btn_last.Enabled = btn_next.Enabled = cb_page.SelectedIndex < cb_page.Items.Count - 1;
            PageChange();
        }

        private void cb_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            count = (cb_count.SelectedIndex + 1) * 10;
            InitPageCount();
            PageChange();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PageProgress pp = PageProgressBar.Invoke();
            if (pp != null)
            {
                pb.Maximum = pp.Max;
                pb.Value = pp.Index;
            }
        }

        private void InitPageCount()
        {
            int length = MaxDataNumber / count;
            if (MaxDataNumber % count > 0)
            {
                length++;
            }

            int select = cb_page.SelectedIndex;
            cb_page.SelectedIndexChanged -= cb_page_SelectedIndexChanged;
            cb_page.Items.Clear();
            for (int i = 0; i < length; i++)
            {
                cb_page.Items.Add($"第{(i + 1)}页");
            }
            if (length > 0 && select < 0)
            {
                cb_page.SelectedIndex = 0;
            }
            cb_page.SelectedIndexChanged += cb_page_SelectedIndexChanged;
            if (length > select && select > -1)
            {
                cb_page.SelectedIndex = select;
            }
        }

        private void PageChange()
        {
            cb_count.Visible = cb_page.Visible = false;
            timer1.Enabled = pb.Visible = true;
            pb.Value = 0;
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                PageChanged?.Invoke(index, count);
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        timer1.Enabled = pb.Visible = false;
                        cb_count.Visible = cb_page.Visible = true;
                    }));
                }
            });
        }
    }

    public class PageProgress
    {
        public int Index { get; set; }
        public int Max { get; set; }
    }
}
