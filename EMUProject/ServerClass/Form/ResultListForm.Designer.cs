
namespace Project.ServerClass
{
    partial class ResultListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultListForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_exp3 = new System.Windows.Forms.Button();
            this.btn_exp2 = new System.Windows.Forms.Button();
            this.btn_exp1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pagesControl1 = new Project.PagesControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_exp3);
            this.panel1.Controls.Add(this.btn_exp2);
            this.panel1.Controls.Add(this.btn_exp1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 34);
            this.panel1.TabIndex = 0;
            // 
            // btn_exp3
            // 
            this.btn_exp3.Location = new System.Drawing.Point(256, 4);
            this.btn_exp3.Name = "btn_exp3";
            this.btn_exp3.Size = new System.Drawing.Size(140, 25);
            this.btn_exp3.TabIndex = 2;
            this.btn_exp3.Text = "导出机检记录单";
            this.btn_exp3.UseVisualStyleBackColor = true;
            this.btn_exp3.Click += new System.EventHandler(this.btn_exp3_Click);
            // 
            // btn_exp2
            // 
            this.btn_exp2.Location = new System.Drawing.Point(110, 4);
            this.btn_exp2.Name = "btn_exp2";
            this.btn_exp2.Size = new System.Drawing.Size(140, 25);
            this.btn_exp2.TabIndex = 1;
            this.btn_exp2.Text = "导出故障复核单";
            this.btn_exp2.UseVisualStyleBackColor = true;
            this.btn_exp2.Click += new System.EventHandler(this.btn_exp2_Click);
            // 
            // btn_exp1
            // 
            this.btn_exp1.Location = new System.Drawing.Point(4, 4);
            this.btn_exp1.Name = "btn_exp1";
            this.btn_exp1.Size = new System.Drawing.Size(100, 25);
            this.btn_exp1.TabIndex = 0;
            this.btn_exp1.Text = "导出故障单";
            this.btn_exp1.UseVisualStyleBackColor = true;
            this.btn_exp1.Click += new System.EventHandler(this.btn_exp1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(482, 689);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pagesControl1
            // 
            this.pagesControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagesControl1.Location = new System.Drawing.Point(0, 723);
            this.pagesControl1.MaxDataNumber = 0;
            this.pagesControl1.Name = "pagesControl1";
            this.pagesControl1.Size = new System.Drawing.Size(482, 30);
            this.pagesControl1.TabIndex = 2;
            this.pagesControl1.PageProgressBar += new System.Func<Project.PageProgress>(this.pagesControl1_PageProgressBar);
            // 
            // ResultListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 753);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pagesControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ResultListForm";
            this.Text = "异常列表";
            this.Load += new System.EventHandler(this.ResultListForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_exp1;
        private System.Windows.Forms.Button btn_exp2;
        private System.Windows.Forms.Button btn_exp3;
        private PagesControl pagesControl1;
    }
}