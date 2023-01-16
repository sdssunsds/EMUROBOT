
namespace Project.ServerClass
{
    partial class ResultListControl
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_data = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lb_remark = new System.Windows.Forms.Label();
            this.btn_upd = new System.Windows.Forms.Button();
            this.btn_ignore = new System.Windows.Forms.Button();
            this.btn_remark = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(250, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_name.Location = new System.Drawing.Point(4, 4);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(82, 24);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "label1";
            // 
            // lb_data
            // 
            this.lb_data.AutoSize = true;
            this.lb_data.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_data.Location = new System.Drawing.Point(31, 28);
            this.lb_data.Name = "lb_data";
            this.lb_data.Size = new System.Drawing.Size(82, 24);
            this.lb_data.TabIndex = 2;
            this.lb_data.Text = "label1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(198, 128);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 19);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "标记";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lb_remark
            // 
            this.lb_remark.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_remark.Location = new System.Drawing.Point(4, 52);
            this.lb_remark.Name = "lb_remark";
            this.lb_remark.Size = new System.Drawing.Size(240, 73);
            this.lb_remark.TabIndex = 4;
            this.lb_remark.Text = "label1";
            // 
            // btn_upd
            // 
            this.btn_upd.Location = new System.Drawing.Point(-1, 124);
            this.btn_upd.Name = "btn_upd";
            this.btn_upd.Size = new System.Drawing.Size(100, 25);
            this.btn_upd.TabIndex = 5;
            this.btn_upd.Text = "结果修正";
            this.btn_upd.UseVisualStyleBackColor = true;
            this.btn_upd.Click += new System.EventHandler(this.btn_upd_Click);
            // 
            // btn_ignore
            // 
            this.btn_ignore.Location = new System.Drawing.Point(97, 124);
            this.btn_ignore.Name = "btn_ignore";
            this.btn_ignore.Size = new System.Drawing.Size(50, 25);
            this.btn_ignore.TabIndex = 6;
            this.btn_ignore.Text = "忽略";
            this.btn_ignore.UseVisualStyleBackColor = true;
            this.btn_ignore.Click += new System.EventHandler(this.btn_ignore_Click);
            // 
            // btn_remark
            // 
            this.btn_remark.Location = new System.Drawing.Point(146, 124);
            this.btn_remark.Name = "btn_remark";
            this.btn_remark.Size = new System.Drawing.Size(50, 25);
            this.btn_remark.TabIndex = 7;
            this.btn_remark.Text = "备注";
            this.btn_remark.UseVisualStyleBackColor = true;
            this.btn_remark.Click += new System.EventHandler(this.btn_remark_Click);
            // 
            // ResultListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_remark);
            this.Controls.Add(this.btn_ignore);
            this.Controls.Add(this.btn_upd);
            this.Controls.Add(this.lb_remark);
            this.Controls.Add(this.lb_data);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox1);
            this.Name = "ResultListControl";
            this.Size = new System.Drawing.Size(400, 150);
            this.Load += new System.EventHandler(this.ResultListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_data;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lb_remark;
        private System.Windows.Forms.Button btn_upd;
        private System.Windows.Forms.Button btn_ignore;
        private System.Windows.Forms.Button btn_remark;
    }
}
