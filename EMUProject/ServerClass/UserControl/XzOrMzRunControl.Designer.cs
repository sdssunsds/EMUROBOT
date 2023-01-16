
namespace Project.ServerClass
{
    partial class XzOrMzRunControl
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
            this.tb_start = new System.Windows.Forms.TextBox();
            this.tb_end = new System.Windows.Forms.TextBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tb_start);
            this.flowLayoutPanel1.Controls.Add(this.tb_end);
            this.flowLayoutPanel1.Controls.Add(this.btn_run);
            this.flowLayoutPanel1.Controls.Add(this.dgv);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(208, 118);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tb_start
            // 
            this.tb_start.Location = new System.Drawing.Point(3, 3);
            this.tb_start.Name = "tb_start";
            this.tb_start.Size = new System.Drawing.Size(50, 25);
            this.tb_start.TabIndex = 0;
            // 
            // tb_end
            // 
            this.tb_end.Location = new System.Drawing.Point(59, 3);
            this.tb_end.Name = "tb_end";
            this.tb_end.Size = new System.Drawing.Size(50, 25);
            this.tb_end.TabIndex = 1;
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(115, 3);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 25);
            this.btn_run.TabIndex = 2;
            this.btn_run.Text = "执行";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // dgv
            // 
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 34);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 27;
            this.dgv.Size = new System.Drawing.Size(202, 82);
            this.dgv.TabIndex = 3;
            // 
            // XzOrMzRunControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "XzOrMzRunControl";
            this.Size = new System.Drawing.Size(208, 118);
            this.Load += new System.EventHandler(this.XzOrMzRunControl_Load);
            this.SizeChanged += new System.EventHandler(this.XzOrMzRunControl_SizeChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox tb_start;
        private System.Windows.Forms.TextBox tb_end;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.DataGridView dgv;
    }
}
