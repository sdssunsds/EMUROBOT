
namespace Project.ServerClass
{
    partial class TaskProjectControl
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_frash = new System.Windows.Forms.Button();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb_none = new System.Windows.Forms.CheckBox();
            this.cb_doing = new System.Windows.Forms.CheckBox();
            this.cb_complate = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_none_count = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_doing_count = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_complate_count = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_count = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_insert);
            this.flowLayoutPanel1.Controls.Add(this.btn_frash);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.dt_start);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.dt_end);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1111, 100);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_insert
            // 
            this.btn_insert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_insert.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_insert.Location = new System.Drawing.Point(3, 3);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(95, 95);
            this.btn_insert.TabIndex = 0;
            this.btn_insert.Text = "添加检\r\n修计划";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_frash
            // 
            this.btn_frash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_frash.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_frash.Location = new System.Drawing.Point(104, 3);
            this.btn_frash.Name = "btn_frash";
            this.btn_frash.Size = new System.Drawing.Size(95, 95);
            this.btn_frash.TabIndex = 2;
            this.btn_frash.Text = "刷新检\r\n修计划";
            this.btn_frash.UseVisualStyleBackColor = true;
            this.btn_frash.Click += new System.EventHandler(this.btn_frash_Click);
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.Location = new System.Drawing.Point(0, 100);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(1111, 541);
            this.flp.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.cb_none);
            this.flowLayoutPanel3.Controls.Add(this.cb_doing);
            this.flowLayoutPanel3.Controls.Add(this.cb_complate);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(205, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(113, 100);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // cb_none
            // 
            this.cb_none.AutoSize = true;
            this.cb_none.Checked = true;
            this.cb_none.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_none.Location = new System.Drawing.Point(3, 3);
            this.cb_none.Name = "cb_none";
            this.cb_none.Size = new System.Drawing.Size(104, 19);
            this.cb_none.TabIndex = 0;
            this.cb_none.Text = "未开始计划";
            this.cb_none.UseVisualStyleBackColor = true;
            // 
            // cb_doing
            // 
            this.cb_doing.AutoSize = true;
            this.cb_doing.Checked = true;
            this.cb_doing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_doing.Location = new System.Drawing.Point(3, 28);
            this.cb_doing.Name = "cb_doing";
            this.cb_doing.Size = new System.Drawing.Size(104, 19);
            this.cb_doing.TabIndex = 1;
            this.cb_doing.Text = "未完成计划";
            this.cb_doing.UseVisualStyleBackColor = true;
            // 
            // cb_complate
            // 
            this.cb_complate.AutoSize = true;
            this.cb_complate.Checked = true;
            this.cb_complate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_complate.Location = new System.Drawing.Point(3, 53);
            this.cb_complate.Name = "cb_complate";
            this.cb_complate.Size = new System.Drawing.Size(104, 19);
            this.cb_complate.TabIndex = 2;
            this.cb_complate.Text = "已完成计划";
            this.cb_complate.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.label1);
            this.flowLayoutPanel4.Controls.Add(this.lb_none_count);
            this.flowLayoutPanel4.Controls.Add(this.label2);
            this.flowLayoutPanel4.Controls.Add(this.lb_doing_count);
            this.flowLayoutPanel4.Controls.Add(this.label3);
            this.flowLayoutPanel4.Controls.Add(this.lb_complate_count);
            this.flowLayoutPanel4.Controls.Add(this.label4);
            this.flowLayoutPanel4.Controls.Add(this.lb_count);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(324, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(233, 100);
            this.flowLayoutPanel4.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "未开始计划数量：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_none_count
            // 
            this.lb_none_count.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_none_count.Location = new System.Drawing.Point(174, 0);
            this.lb_none_count.Name = "lb_none_count";
            this.lb_none_count.Size = new System.Drawing.Size(50, 25);
            this.lb_none_count.TabIndex = 1;
            this.lb_none_count.Text = "0";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "未完成计划数量：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_doing_count
            // 
            this.lb_doing_count.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_doing_count.Location = new System.Drawing.Point(174, 25);
            this.lb_doing_count.Name = "lb_doing_count";
            this.lb_doing_count.Size = new System.Drawing.Size(50, 25);
            this.lb_doing_count.TabIndex = 3;
            this.lb_doing_count.Text = "0";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "已完成计划数量：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_complate_count
            // 
            this.lb_complate_count.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_complate_count.Location = new System.Drawing.Point(174, 50);
            this.lb_complate_count.Name = "lb_complate_count";
            this.lb_complate_count.Size = new System.Drawing.Size(50, 25);
            this.lb_complate_count.TabIndex = 5;
            this.lb_complate_count.Text = "0";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "计划总数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_count
            // 
            this.lb_count.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_count.Location = new System.Drawing.Point(174, 75);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(50, 25);
            this.lb_count.TabIndex = 7;
            this.lb_count.Text = "0";
            // 
            // dt_start
            // 
            this.dt_start.Location = new System.Drawing.Point(645, 3);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(155, 25);
            this.dt_start.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(563, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 23);
            this.label5.TabIndex = 6;
            this.label5.Text = "计划日期";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(806, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "至";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dt_end
            // 
            this.dt_end.Location = new System.Drawing.Point(845, 3);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(155, 25);
            this.dt_end.TabIndex = 8;
            // 
            // TaskProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flp);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TaskProjectControl";
            this.Size = new System.Drawing.Size(1111, 641);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_frash;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox cb_none;
        private System.Windows.Forms.CheckBox cb_doing;
        private System.Windows.Forms.CheckBox cb_complate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_none_count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_doing_count;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_complate_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dt_end;
    }
}
