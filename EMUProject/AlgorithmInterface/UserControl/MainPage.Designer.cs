
namespace Project
{
    partial class MainPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_redis_output = new System.Windows.Forms.TextBox();
            this.tb_redis_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_redis_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_redis_report = new System.Windows.Forms.TextBox();
            this.tb_redis = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_redis_internal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_redis_result = new System.Windows.Forms.TextBox();
            this.btn_link = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_link);
            this.groupBox1.Controls.Add(this.tb_redis_output);
            this.groupBox1.Controls.Add(this.tb_redis_input);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_redis_url);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1091, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Redis接口";
            // 
            // tb_redis_output
            // 
            this.tb_redis_output.Enabled = false;
            this.tb_redis_output.Location = new System.Drawing.Point(394, 44);
            this.tb_redis_output.Name = "tb_redis_output";
            this.tb_redis_output.Size = new System.Drawing.Size(252, 21);
            this.tb_redis_output.TabIndex = 5;
            this.tb_redis_output.Leave += new System.EventHandler(this.tb_redis_output_Leave);
            // 
            // tb_redis_input
            // 
            this.tb_redis_input.Enabled = false;
            this.tb_redis_input.Location = new System.Drawing.Point(67, 44);
            this.tb_redis_input.Name = "tb_redis_input";
            this.tb_redis_input.Size = new System.Drawing.Size(252, 21);
            this.tb_redis_input.TabIndex = 4;
            this.tb_redis_input.Leave += new System.EventHandler(this.tb_redis_input_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "输出接口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入接口";
            // 
            // tb_redis_url
            // 
            this.tb_redis_url.Location = new System.Drawing.Point(67, 17);
            this.tb_redis_url.Name = "tb_redis_url";
            this.tb_redis_url.Size = new System.Drawing.Size(579, 21);
            this.tb_redis_url.TabIndex = 1;
            this.tb_redis_url.Leave += new System.EventHandler(this.tb_redis_url_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "接口地址";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_redis_report);
            this.groupBox2.Controls.Add(this.tb_redis);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(541, 365);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Redis接口定时输入";
            // 
            // tb_redis_report
            // 
            this.tb_redis_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_redis_report.Location = new System.Drawing.Point(3, 76);
            this.tb_redis_report.Multiline = true;
            this.tb_redis_report.Name = "tb_redis_report";
            this.tb_redis_report.ReadOnly = true;
            this.tb_redis_report.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_redis_report.Size = new System.Drawing.Size(353, 286);
            this.tb_redis_report.TabIndex = 4;
            // 
            // tb_redis
            // 
            this.tb_redis.Dock = System.Windows.Forms.DockStyle.Right;
            this.tb_redis.Location = new System.Drawing.Point(356, 76);
            this.tb_redis.Multiline = true;
            this.tb_redis.Name = "tb_redis";
            this.tb_redis.ReadOnly = true;
            this.tb_redis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_redis.Size = new System.Drawing.Size(182, 286);
            this.tb_redis.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tb_redis_internal);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 59);
            this.panel1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "过程监控";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "请求结果";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "毫秒";
            // 
            // tb_redis_internal
            // 
            this.tb_redis_internal.Location = new System.Drawing.Point(73, 7);
            this.tb_redis_internal.Name = "tb_redis_internal";
            this.tb_redis_internal.Size = new System.Drawing.Size(41, 21);
            this.tb_redis_internal.TabIndex = 5;
            this.tb_redis_internal.Text = "100";
            this.tb_redis_internal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_redis_internal.Leave += new System.EventHandler(this.tb_redis_internal_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "请求间隔";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_redis_result);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(541, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(550, 365);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "返回给Redis的数据";
            // 
            // tb_redis_result
            // 
            this.tb_redis_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_redis_result.Location = new System.Drawing.Point(3, 17);
            this.tb_redis_result.Multiline = true;
            this.tb_redis_result.Name = "tb_redis_result";
            this.tb_redis_result.ReadOnly = true;
            this.tb_redis_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_redis_result.Size = new System.Drawing.Size(544, 345);
            this.tb_redis_result.TabIndex = 5;
            // 
            // btn_link
            // 
            this.btn_link.Location = new System.Drawing.Point(671, 17);
            this.btn_link.Name = "btn_link";
            this.btn_link.Size = new System.Drawing.Size(85, 48);
            this.btn_link.TabIndex = 6;
            this.btn_link.Text = "连接Redis";
            this.btn_link.UseVisualStyleBackColor = true;
            this.btn_link.Click += new System.EventHandler(this.btn_link_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(1091, 441);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_redis_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_redis_output;
        private System.Windows.Forms.TextBox tb_redis_input;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_redis_report;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_redis_internal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_redis_result;
        private System.Windows.Forms.TextBox tb_redis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_link;
    }
}
