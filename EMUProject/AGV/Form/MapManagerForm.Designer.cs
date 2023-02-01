
namespace Project.AGV
{
    partial class MapManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapManagerForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载配置的地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存配置的地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预览配置的地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "单元半径";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(25, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "20";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.LocationChanged += new System.EventHandler(this.pictureBox1_LocationChanged);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 地图ToolStripMenuItem
            // 
            this.地图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载地图ToolStripMenuItem,
            this.加载配置的地图ToolStripMenuItem,
            this.保存配置的地图ToolStripMenuItem});
            this.地图ToolStripMenuItem.Name = "地图ToolStripMenuItem";
            this.地图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.地图ToolStripMenuItem.Text = "地图";
            // 
            // 加载地图ToolStripMenuItem
            // 
            this.加载地图ToolStripMenuItem.Name = "加载地图ToolStripMenuItem";
            this.加载地图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.加载地图ToolStripMenuItem.Text = "加载地图";
            this.加载地图ToolStripMenuItem.Click += new System.EventHandler(this.加载地图ToolStripMenuItem_Click);
            // 
            // 加载配置的地图ToolStripMenuItem
            // 
            this.加载配置的地图ToolStripMenuItem.Name = "加载配置的地图ToolStripMenuItem";
            this.加载配置的地图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.加载配置的地图ToolStripMenuItem.Text = "加载配置的地图";
            this.加载配置的地图ToolStripMenuItem.Click += new System.EventHandler(this.加载配置的地图ToolStripMenuItem_Click);
            // 
            // 保存配置的地图ToolStripMenuItem
            // 
            this.保存配置的地图ToolStripMenuItem.Name = "保存配置的地图ToolStripMenuItem";
            this.保存配置的地图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.保存配置的地图ToolStripMenuItem.Text = "保存配置的地图";
            this.保存配置的地图ToolStripMenuItem.Click += new System.EventHandler(this.保存配置的地图ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.预览配置的地图ToolStripMenuItem});
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.测试ToolStripMenuItem.Text = "测试";
            // 
            // 预览配置的地图ToolStripMenuItem
            // 
            this.预览配置的地图ToolStripMenuItem.Name = "预览配置的地图ToolStripMenuItem";
            this.预览配置的地图ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.预览配置的地图ToolStripMenuItem.Text = "预览配置的地图";
            this.预览配置的地图ToolStripMenuItem.Click += new System.EventHandler(this.预览配置的地图ToolStripMenuItem_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空配置ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 清空配置ToolStripMenuItem
            // 
            this.清空配置ToolStripMenuItem.Name = "清空配置ToolStripMenuItem";
            this.清空配置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.清空配置ToolStripMenuItem.Text = "清空配置";
            this.清空配置ToolStripMenuItem.Click += new System.EventHandler(this.清空配置ToolStripMenuItem_Click);
            // 
            // MapManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图编辑";
            this.Load += new System.EventHandler(this.MapManagerForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存配置的地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预览配置的地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载配置的地图ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}