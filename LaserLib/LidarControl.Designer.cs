
namespace Laser
{
    partial class LidarControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_received = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.角度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.距离ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.图片导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片缩放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像半径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_send);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 569);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送指令";
            // 
            // tb_send
            // 
            this.tb_send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_send.Location = new System.Drawing.Point(3, 21);
            this.tb_send.Multiline = true;
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(224, 513);
            this.tb_send.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 534);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 32);
            this.panel1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(115, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 19);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "显示坐标系";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 25);
            this.button3.TabIndex = 13;
            this.button3.Text = "启动";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(59, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 25);
            this.button2.TabIndex = 12;
            this.button2.Text = "停止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_received);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(230, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 569);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接受的全部数据";
            // 
            // tb_received
            // 
            this.tb_received.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_received.Location = new System.Drawing.Point(3, 21);
            this.tb_received.Multiline = true;
            this.tb_received.Name = "tb_received";
            this.tb_received.Size = new System.Drawing.Size(224, 545);
            this.tb_received.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(460, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 569);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "未解析有效数据";
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(3, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(224, 545);
            this.listBox1.TabIndex = 7;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制数据ToolStripMenuItem,
            this.数据排序ToolStripMenuItem,
            this.数据导出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 76);
            // 
            // 复制数据ToolStripMenuItem
            // 
            this.复制数据ToolStripMenuItem.Name = "复制数据ToolStripMenuItem";
            this.复制数据ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.复制数据ToolStripMenuItem.Text = "复制数据";
            this.复制数据ToolStripMenuItem.Click += new System.EventHandler(this.复制数据ToolStripMenuItem_Click);
            // 
            // 数据排序ToolStripMenuItem
            // 
            this.数据排序ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编号ToolStripMenuItem,
            this.角度ToolStripMenuItem,
            this.距离ToolStripMenuItem});
            this.数据排序ToolStripMenuItem.Name = "数据排序ToolStripMenuItem";
            this.数据排序ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.数据排序ToolStripMenuItem.Text = "数据排序";
            // 
            // 编号ToolStripMenuItem
            // 
            this.编号ToolStripMenuItem.Name = "编号ToolStripMenuItem";
            this.编号ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.编号ToolStripMenuItem.Text = "编号";
            this.编号ToolStripMenuItem.Click += new System.EventHandler(this.编号ToolStripMenuItem_Click);
            // 
            // 角度ToolStripMenuItem
            // 
            this.角度ToolStripMenuItem.Name = "角度ToolStripMenuItem";
            this.角度ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.角度ToolStripMenuItem.Text = "角度";
            this.角度ToolStripMenuItem.Click += new System.EventHandler(this.角度ToolStripMenuItem_Click);
            // 
            // 距离ToolStripMenuItem
            // 
            this.距离ToolStripMenuItem.Name = "距离ToolStripMenuItem";
            this.距离ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.距离ToolStripMenuItem.Text = "距离";
            this.距离ToolStripMenuItem.Click += new System.EventHandler(this.距离ToolStripMenuItem_Click);
            // 
            // 数据导出ToolStripMenuItem
            // 
            this.数据导出ToolStripMenuItem.Name = "数据导出ToolStripMenuItem";
            this.数据导出ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.数据导出ToolStripMenuItem.Text = "数据导出";
            this.数据导出ToolStripMenuItem.Click += new System.EventHandler(this.数据导出ToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(690, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 569);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "解析后有效数据";
            // 
            // listBox2
            // 
            this.listBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(3, 21);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(224, 545);
            this.listBox2.TabIndex = 8;
            this.listBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip2;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(920, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 569);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片导出ToolStripMenuItem,
            this.图片缩放ToolStripMenuItem,
            this.图像半径ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(139, 76);
            // 
            // 图片导出ToolStripMenuItem
            // 
            this.图片导出ToolStripMenuItem.Name = "图片导出ToolStripMenuItem";
            this.图片导出ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.图片导出ToolStripMenuItem.Text = "图片导出";
            this.图片导出ToolStripMenuItem.Click += new System.EventHandler(this.图片导出ToolStripMenuItem_Click);
            // 
            // 图片缩放ToolStripMenuItem
            // 
            this.图片缩放ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ToolStripMenuItem,
            this.缩小ToolStripMenuItem});
            this.图片缩放ToolStripMenuItem.Name = "图片缩放ToolStripMenuItem";
            this.图片缩放ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.图片缩放ToolStripMenuItem.Text = "图片缩放";
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.放大ToolStripMenuItem_Click);
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.缩小ToolStripMenuItem.Text = "缩小";
            this.缩小ToolStripMenuItem.Click += new System.EventHandler(this.缩小ToolStripMenuItem_Click);
            // 
            // 图像半径ToolStripMenuItem
            // 
            this.图像半径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.图像半径ToolStripMenuItem.Name = "图像半径ToolStripMenuItem";
            this.图像半径ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.图像半径ToolStripMenuItem.Text = "图像半径";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem2.Text = "400";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem3.Text = "800";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem4.Text = "1000";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem5.Text = "2000";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem6.Text = "4000";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem7.Text = "8000";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(137, 26);
            this.toolStripMenuItem8.Text = "10000";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMeAnuItem_Click);
            // 
            // LidarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LidarControl";
            this.Size = new System.Drawing.Size(1155, 569);
            this.Load += new System.EventHandler(this.LidarControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_received;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据排序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 角度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 距离ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据导出ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 图片导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片缩放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像半径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
    }
}
