
namespace Project
{
    partial class PagesControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_home = new System.Windows.Forms.Button();
            this.btn_top = new System.Windows.Forms.Button();
            this.cb_page = new System.Windows.Forms.ComboBox();
            this.cb_count = new System.Windows.Forms.ComboBox();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_last = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.flowLayoutPanel1.Controls.Add(this.btn_home);
            this.flowLayoutPanel1.Controls.Add(this.btn_top);
            this.flowLayoutPanel1.Controls.Add(this.cb_page);
            this.flowLayoutPanel1.Controls.Add(this.cb_count);
            this.flowLayoutPanel1.Controls.Add(this.pb);
            this.flowLayoutPanel1.Controls.Add(this.btn_next);
            this.flowLayoutPanel1.Controls.Add(this.btn_last);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(489, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_home
            // 
            this.btn_home.Location = new System.Drawing.Point(3, 3);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(40, 25);
            this.btn_home.TabIndex = 0;
            this.btn_home.Text = "<<";
            this.btn_home.UseVisualStyleBackColor = true;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // btn_top
            // 
            this.btn_top.Location = new System.Drawing.Point(49, 3);
            this.btn_top.Name = "btn_top";
            this.btn_top.Size = new System.Drawing.Size(30, 25);
            this.btn_top.TabIndex = 1;
            this.btn_top.Text = "<";
            this.btn_top.UseVisualStyleBackColor = true;
            this.btn_top.Click += new System.EventHandler(this.btn_top_Click);
            // 
            // cb_page
            // 
            this.cb_page.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_page.FormattingEnabled = true;
            this.cb_page.Location = new System.Drawing.Point(85, 3);
            this.cb_page.Name = "cb_page";
            this.cb_page.Size = new System.Drawing.Size(100, 23);
            this.cb_page.TabIndex = 2;
            this.cb_page.SelectedIndexChanged += new System.EventHandler(this.cb_page_SelectedIndexChanged);
            // 
            // cb_count
            // 
            this.cb_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_count.FormattingEnabled = true;
            this.cb_count.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50"});
            this.cb_count.Location = new System.Drawing.Point(191, 3);
            this.cb_count.Name = "cb_count";
            this.cb_count.Size = new System.Drawing.Size(50, 23);
            this.cb_count.TabIndex = 3;
            this.cb_count.SelectedIndexChanged += new System.EventHandler(this.cb_count_SelectedIndexChanged);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(247, 3);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(156, 25);
            this.pb.TabIndex = 4;
            this.pb.Visible = false;
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(409, 3);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(30, 25);
            this.btn_next.TabIndex = 5;
            this.btn_next.Text = ">";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_last
            // 
            this.btn_last.Location = new System.Drawing.Point(445, 3);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(40, 25);
            this.btn_last.TabIndex = 6;
            this.btn_last.Text = ">>";
            this.btn_last.UseVisualStyleBackColor = true;
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PagesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "PagesControl";
            this.Size = new System.Drawing.Size(489, 30);
            this.Load += new System.EventHandler(this.PageControl_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Button btn_top;
        private System.Windows.Forms.ComboBox cb_page;
        private System.Windows.Forms.ComboBox cb_count;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_last;
        private System.Windows.Forms.Timer timer1;
    }
}
