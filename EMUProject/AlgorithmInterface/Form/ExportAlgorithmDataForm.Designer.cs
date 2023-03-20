
namespace Project
{
    partial class ExportAlgorithmDataForm
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
            this.components = new System.ComponentModel.Container();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.tb_export_path = new System.Windows.Forms.TextBox();
            this.btn_path = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.cb_type = new System.Windows.Forms.CheckedListBox();
            this.cb_pars = new System.Windows.Forms.CheckedListBox();
            this.cb_image = new System.Windows.Forms.CheckedListBox();
            this.cb_result = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全不选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp
            // 
            this.dtp.Location = new System.Drawing.Point(12, 12);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(120, 21);
            this.dtp.TabIndex = 0;
            // 
            // tb_export_path
            // 
            this.tb_export_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_export_path.BackColor = System.Drawing.SystemColors.Control;
            this.tb_export_path.Location = new System.Drawing.Point(219, 12);
            this.tb_export_path.Name = "tb_export_path";
            this.tb_export_path.Size = new System.Drawing.Size(211, 21);
            this.tb_export_path.TabIndex = 2;
            this.tb_export_path.TextChanged += new System.EventHandler(this.tb_export_path_TextChanged);
            // 
            // btn_path
            // 
            this.btn_path.Location = new System.Drawing.Point(138, 11);
            this.btn_path.Name = "btn_path";
            this.btn_path.Size = new System.Drawing.Size(75, 23);
            this.btn_path.TabIndex = 3;
            this.btn_path.Text = "导出路径";
            this.btn_path.UseVisualStyleBackColor = true;
            this.btn_path.Click += new System.EventHandler(this.btn_path_Click);
            // 
            // btn_export
            // 
            this.btn_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_export.Location = new System.Drawing.Point(436, 11);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 4;
            this.btn_export.Text = "导出数据";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // cb_type
            // 
            this.cb_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_type.ContextMenuStrip = this.contextMenuStrip1;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(12, 39);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(120, 404);
            this.cb_type.TabIndex = 5;
            this.cb_type.MouseEnter += new System.EventHandler(this.cb_type_MouseEnter);
            // 
            // cb_pars
            // 
            this.cb_pars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_pars.ContextMenuStrip = this.contextMenuStrip1;
            this.cb_pars.FormattingEnabled = true;
            this.cb_pars.Items.AddRange(new object[] {
            "车型车号",
            "部件编号",
            "识别任务",
            "模板坐标"});
            this.cb_pars.Location = new System.Drawing.Point(138, 40);
            this.cb_pars.Name = "cb_pars";
            this.cb_pars.Size = new System.Drawing.Size(120, 404);
            this.cb_pars.TabIndex = 6;
            this.cb_pars.MouseEnter += new System.EventHandler(this.cb_pars_MouseEnter);
            // 
            // cb_image
            // 
            this.cb_image.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_image.ContextMenuStrip = this.contextMenuStrip1;
            this.cb_image.FormattingEnabled = true;
            this.cb_image.Items.AddRange(new object[] {
            "模板图片",
            "往次图片",
            "结果图片"});
            this.cb_image.Location = new System.Drawing.Point(264, 40);
            this.cb_image.Name = "cb_image";
            this.cb_image.Size = new System.Drawing.Size(120, 404);
            this.cb_image.TabIndex = 7;
            this.cb_image.MouseEnter += new System.EventHandler(this.cb_image_MouseEnter);
            // 
            // cb_result
            // 
            this.cb_result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_result.ContextMenuStrip = this.contextMenuStrip1;
            this.cb_result.FormattingEnabled = true;
            this.cb_result.Items.AddRange(new object[] {
            "算法结果"});
            this.cb_result.Location = new System.Drawing.Point(390, 40);
            this.cb_result.Name = "cb_result";
            this.cb_result.Size = new System.Drawing.Size(120, 404);
            this.cb_result.TabIndex = 8;
            this.cb_result.MouseEnter += new System.EventHandler(this.cb_result_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.全不选ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 48);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 全不选ToolStripMenuItem
            // 
            this.全不选ToolStripMenuItem.Name = "全不选ToolStripMenuItem";
            this.全不选ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.全不选ToolStripMenuItem.Text = "全不选";
            this.全不选ToolStripMenuItem.Click += new System.EventHandler(this.全不选ToolStripMenuItem_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(12, 424);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(497, 19);
            this.pb.TabIndex = 10;
            this.pb.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ExportAlgorithmDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 450);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.cb_result);
            this.Controls.Add(this.cb_image);
            this.Controls.Add(this.cb_pars);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.btn_path);
            this.Controls.Add(this.tb_export_path);
            this.Controls.Add(this.dtp);
            this.Name = "ExportAlgorithmDataForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导出";
            this.Load += new System.EventHandler(this.ExportAlgorithmDataForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.TextBox tb_export_path;
        private System.Windows.Forms.Button btn_path;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.CheckedListBox cb_type;
        private System.Windows.Forms.CheckedListBox cb_pars;
        private System.Windows.Forms.CheckedListBox cb_image;
        private System.Windows.Forms.CheckedListBox cb_result;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全不选ToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Timer timer1;
    }
}