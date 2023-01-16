
namespace Project.ServerClass
{
    partial class ProjectControl
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
            ServerGlobal.RobotTaskEvent -= ServerGlobal_RobotTaskEvent;
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Button();
            this.lb_date = new System.Windows.Forms.Label();
            this.lb_state = new System.Windows.Forms.Label();
            this.lb_text = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_address = new System.Windows.Forms.Label();
            this.lb_rode = new System.Windows.Forms.Label();
            this.lb_point = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lb_start_date = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lb_end_date = new System.Windows.Forms.Label();
            this.lb_mode = new System.Windows.Forms.Label();
            this.lb_sn = new System.Windows.Forms.Label();
            this.lb_robot = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lb_count = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lb_job = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 9999;
            this.label1.Text = "计划日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 9999;
            this.label2.Text = "计划状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 9999;
            this.label3.Text = "计划说明";
            // 
            // btn
            // 
            this.btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn.Location = new System.Drawing.Point(422, 188);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(60, 60);
            this.btn.TabIndex = 3;
            this.btn.Text = "修改\r\n计划";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // lb_date
            // 
            this.lb_date.AutoSize = true;
            this.lb_date.Location = new System.Drawing.Point(111, 15);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(89, 19);
            this.lb_date.TabIndex = 9999;
            this.lb_date.Text = "2020-1-1";
            // 
            // lb_state
            // 
            this.lb_state.AutoSize = true;
            this.lb_state.Location = new System.Drawing.Point(111, 45);
            this.lb_state.Name = "lb_state";
            this.lb_state.Size = new System.Drawing.Size(28, 19);
            this.lb_state.TabIndex = 9999;
            this.lb_state.Text = "无";
            // 
            // lb_text
            // 
            this.lb_text.AutoSize = true;
            this.lb_text.Location = new System.Drawing.Point(111, 75);
            this.lb_text.Name = "lb_text";
            this.lb_text.Size = new System.Drawing.Size(0, 19);
            this.lb_text.TabIndex = 9999;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 9999;
            this.label4.Text = "检修库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 9999;
            this.label5.Text = "检修车道";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 19);
            this.label6.TabIndex = 9999;
            this.label6.Text = "检修位";
            // 
            // lb_address
            // 
            this.lb_address.AutoSize = true;
            this.lb_address.Location = new System.Drawing.Point(92, 105);
            this.lb_address.Name = "lb_address";
            this.lb_address.Size = new System.Drawing.Size(0, 19);
            this.lb_address.TabIndex = 9999;
            // 
            // lb_rode
            // 
            this.lb_rode.AutoSize = true;
            this.lb_rode.Location = new System.Drawing.Point(316, 105);
            this.lb_rode.Name = "lb_rode";
            this.lb_rode.Size = new System.Drawing.Size(0, 19);
            this.lb_rode.TabIndex = 9999;
            // 
            // lb_point
            // 
            this.lb_point.AutoSize = true;
            this.lb_point.Location = new System.Drawing.Point(92, 135);
            this.lb_point.Name = "lb_point";
            this.lb_point.Size = new System.Drawing.Size(0, 19);
            this.lb_point.TabIndex = 9999;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 9999;
            this.label7.Text = "检修时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 19);
            this.label8.TabIndex = 9999;
            this.label8.Text = "车型";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 19);
            this.label9.TabIndex = 9999;
            this.label9.Text = "车号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 19);
            this.label10.TabIndex = 9999;
            this.label10.Text = "机器人编号";
            // 
            // lb_start_date
            // 
            this.lb_start_date.AutoSize = true;
            this.lb_start_date.Location = new System.Drawing.Point(316, 135);
            this.lb_start_date.Name = "lb_start_date";
            this.lb_start_date.Size = new System.Drawing.Size(0, 19);
            this.lb_start_date.TabIndex = 9999;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(225, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 19);
            this.label11.TabIndex = 9999;
            this.label11.Text = "完成时间";
            // 
            // lb_end_date
            // 
            this.lb_end_date.AutoSize = true;
            this.lb_end_date.Location = new System.Drawing.Point(316, 165);
            this.lb_end_date.Name = "lb_end_date";
            this.lb_end_date.Size = new System.Drawing.Size(0, 19);
            this.lb_end_date.TabIndex = 9999;
            // 
            // lb_mode
            // 
            this.lb_mode.AutoSize = true;
            this.lb_mode.Location = new System.Drawing.Point(73, 165);
            this.lb_mode.Name = "lb_mode";
            this.lb_mode.Size = new System.Drawing.Size(0, 19);
            this.lb_mode.TabIndex = 9999;
            // 
            // lb_sn
            // 
            this.lb_sn.AutoSize = true;
            this.lb_sn.Location = new System.Drawing.Point(73, 195);
            this.lb_sn.Name = "lb_sn";
            this.lb_sn.Size = new System.Drawing.Size(0, 19);
            this.lb_sn.TabIndex = 9999;
            // 
            // lb_robot
            // 
            this.lb_robot.AutoSize = true;
            this.lb_robot.Location = new System.Drawing.Point(130, 225);
            this.lb_robot.Name = "lb_robot";
            this.lb_robot.Size = new System.Drawing.Size(0, 19);
            this.lb_robot.TabIndex = 9999;
            // 
            // btn_start
            // 
            this.btn_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_start.Location = new System.Drawing.Point(356, 188);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(60, 60);
            this.btn_start.TabIndex = 23;
            this.btn_start.Text = "开始\r\n检修";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_del
            // 
            this.btn_del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_del.Location = new System.Drawing.Point(382, 4);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(100, 30);
            this.btn_del.TabIndex = 24;
            this.btn_del.Text = "删除计划";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 19);
            this.label12.TabIndex = 10000;
            this.label12.Text = "车厢数";
            // 
            // lb_count
            // 
            this.lb_count.AutoSize = true;
            this.lb_count.Location = new System.Drawing.Point(297, 195);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(0, 19);
            this.lb_count.TabIndex = 10001;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(485, 251);
            this.progressBar.TabIndex = 10002;
            // 
            // lb_job
            // 
            this.lb_job.AutoSize = true;
            this.lb_job.Location = new System.Drawing.Point(225, 45);
            this.lb_job.Name = "lb_job";
            this.lb_job.Size = new System.Drawing.Size(0, 19);
            this.lb_job.TabIndex = 10003;
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_job);
            this.Controls.Add(this.lb_count);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.lb_robot);
            this.Controls.Add(this.lb_sn);
            this.Controls.Add(this.lb_mode);
            this.Controls.Add(this.lb_end_date);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lb_start_date);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lb_point);
            this.Controls.Add(this.lb_rode);
            this.Controls.Add(this.lb_address);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_text);
            this.Controls.Add(this.lb_state);
            this.Controls.Add(this.lb_date);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(485, 251);
            this.Load += new System.EventHandler(this.ProjectControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ProjectControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Label lb_state;
        private System.Windows.Forms.Label lb_text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_address;
        private System.Windows.Forms.Label lb_rode;
        private System.Windows.Forms.Label lb_point;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lb_start_date;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lb_end_date;
        private System.Windows.Forms.Label lb_mode;
        private System.Windows.Forms.Label lb_sn;
        private System.Windows.Forms.Label lb_robot;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_job;
    }
}
