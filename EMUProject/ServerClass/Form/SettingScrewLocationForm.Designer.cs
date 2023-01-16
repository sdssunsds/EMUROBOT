
namespace Project.ServerClass
{
    partial class SettingScrewLocationForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.加载整车图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载部件配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载螺丝定位文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_mode = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_id = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.cb_sn = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pb_one = new System.Windows.Forms.PictureBox();
            this.cb_part = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pb_pars = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_one)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_pars)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载整车图片ToolStripMenuItem,
            this.加载部件配置文件ToolStripMenuItem,
            this.加载螺丝定位文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 加载整车图片ToolStripMenuItem
            // 
            this.加载整车图片ToolStripMenuItem.Name = "加载整车图片ToolStripMenuItem";
            this.加载整车图片ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.加载整车图片ToolStripMenuItem.Text = "加载整车图片";
            this.加载整车图片ToolStripMenuItem.Click += new System.EventHandler(this.加载整车图片ToolStripMenuItem_Click);
            // 
            // 加载部件配置文件ToolStripMenuItem
            // 
            this.加载部件配置文件ToolStripMenuItem.Name = "加载部件配置文件ToolStripMenuItem";
            this.加载部件配置文件ToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.加载部件配置文件ToolStripMenuItem.Text = "加载部件配置文件";
            this.加载部件配置文件ToolStripMenuItem.Click += new System.EventHandler(this.加载部件配置文件ToolStripMenuItem_Click);
            // 
            // 加载螺丝定位文件ToolStripMenuItem
            // 
            this.加载螺丝定位文件ToolStripMenuItem.Name = "加载螺丝定位文件ToolStripMenuItem";
            this.加载螺丝定位文件ToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.加载螺丝定位文件ToolStripMenuItem.Text = "加载螺丝定位文件";
            this.加载螺丝定位文件ToolStripMenuItem.Click += new System.EventHandler(this.加载螺丝定位文件ToolStripMenuItem_Click);
            // 
            // cb_mode
            // 
            this.cb_mode.FormattingEnabled = true;
            this.cb_mode.Location = new System.Drawing.Point(3, 3);
            this.cb_mode.Name = "cb_mode";
            this.cb_mode.Size = new System.Drawing.Size(100, 23);
            this.cb_mode.TabIndex = 1;
            this.cb_mode.SelectedIndexChanged += new System.EventHandler(this.cb_mode_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_id);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.cb_sn);
            this.panel1.Controls.Add(this.cb_mode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 30);
            this.panel1.TabIndex = 2;
            // 
            // cb_id
            // 
            this.cb_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_id.FormattingEnabled = true;
            this.cb_id.Items.AddRange(new object[] {
            "一车（车头）",
            "二车",
            "三车",
            "四车",
            "五车",
            "六车",
            "七车",
            "八车"});
            this.cb_id.Location = new System.Drawing.Point(215, 3);
            this.cb_id.Name = "cb_id";
            this.cb_id.Size = new System.Drawing.Size(150, 23);
            this.cb_id.TabIndex = 4;
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Location = new System.Drawing.Point(688, 2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(100, 25);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "保存配置";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cb_sn
            // 
            this.cb_sn.FormattingEnabled = true;
            this.cb_sn.Location = new System.Drawing.Point(109, 3);
            this.cb_sn.Name = "cb_sn";
            this.cb_sn.Size = new System.Drawing.Size(100, 23);
            this.cb_sn.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 58);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pb_one);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cb_part);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.pb_pars);
            this.splitContainer1.Size = new System.Drawing.Size(800, 392);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 3;
            // 
            // pb_one
            // 
            this.pb_one.Location = new System.Drawing.Point(0, 0);
            this.pb_one.Name = "pb_one";
            this.pb_one.Size = new System.Drawing.Size(237, 389);
            this.pb_one.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_one.TabIndex = 0;
            this.pb_one.TabStop = false;
            this.pb_one.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb_one.MouseLeave += new System.EventHandler(this.pb_MouseLeave);
            this.pb_one.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb_one.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // cb_part
            // 
            this.cb_part.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_part.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_part.FormattingEnabled = true;
            this.cb_part.Location = new System.Drawing.Point(255, 0);
            this.cb_part.Name = "cb_part";
            this.cb_part.Size = new System.Drawing.Size(295, 23);
            this.cb_part.TabIndex = 2;
            this.cb_part.SelectedIndexChanged += new System.EventHandler(this.cb_part_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(255, 392);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Name";
            this.Column1.HeaderText = "部件名称";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Count";
            this.Column2.HeaderText = "螺丝数量";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // pb_pars
            // 
            this.pb_pars.Location = new System.Drawing.Point(261, 29);
            this.pb_pars.Name = "pb_pars";
            this.pb_pars.Size = new System.Drawing.Size(286, 241);
            this.pb_pars.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_pars.TabIndex = 1;
            this.pb_pars.TabStop = false;
            this.pb_pars.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb_pars.MouseLeave += new System.EventHandler(this.pb_MouseLeave);
            this.pb_pars.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb_pars.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // SettingScrewLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SettingScrewLocationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置螺丝定位";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SettingScrewLocationForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_one)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_pars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 加载整车图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载部件配置文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载螺丝定位文件ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_mode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_sn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pb_one;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pb_pars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ComboBox cb_part;
        private System.Windows.Forms.ComboBox cb_id;
    }
}