
namespace UpdateService
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_add_skin = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_add_files = new System.Windows.Forms.Button();
            this.btn_add_file = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_version = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_skin = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_now = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_up = new System.Windows.Forms.Label();
            this.skinEngine = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_add_skin);
            this.groupBox1.Controls.Add(this.btn_update);
            this.groupBox1.Controls.Add(this.btn_add_files);
            this.groupBox1.Controls.Add(this.btn_add_file);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.btn_open);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "升级服务";
            // 
            // btn_add_skin
            // 
            this.btn_add_skin.Location = new System.Drawing.Point(12, 129);
            this.btn_add_skin.Name = "btn_add_skin";
            this.btn_add_skin.Size = new System.Drawing.Size(182, 29);
            this.btn_add_skin.TabIndex = 5;
            this.btn_add_skin.Text = "追加新皮肤";
            this.btn_add_skin.UseVisualStyleBackColor = true;
            this.btn_add_skin.Click += new System.EventHandler(this.btn_add_skin_Click);
            // 
            // btn_update
            // 
            this.btn_update.Enabled = false;
            this.btn_update.Location = new System.Drawing.Point(12, 409);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(182, 29);
            this.btn_update.TabIndex = 4;
            this.btn_update.Text = "发布升级内容";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_add_files
            // 
            this.btn_add_files.Enabled = false;
            this.btn_add_files.Location = new System.Drawing.Point(12, 94);
            this.btn_add_files.Name = "btn_add_files";
            this.btn_add_files.Size = new System.Drawing.Size(182, 29);
            this.btn_add_files.TabIndex = 3;
            this.btn_add_files.Text = "添加升级文件夹";
            this.btn_add_files.UseVisualStyleBackColor = true;
            this.btn_add_files.Click += new System.EventHandler(this.btn_add_files_Click);
            // 
            // btn_add_file
            // 
            this.btn_add_file.Enabled = false;
            this.btn_add_file.Location = new System.Drawing.Point(12, 59);
            this.btn_add_file.Name = "btn_add_file";
            this.btn_add_file.Size = new System.Drawing.Size(182, 29);
            this.btn_add_file.TabIndex = 2;
            this.btn_add_file.Text = "添加升级文件";
            this.btn_add_file.UseVisualStyleBackColor = true;
            this.btn_add_file.Click += new System.EventHandler(this.btn_add_file_Click);
            // 
            // btn_close
            // 
            this.btn_close.Enabled = false;
            this.btn_close.Location = new System.Drawing.Point(104, 24);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(90, 29);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "关闭服务";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(12, 24);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(90, 29);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "打开服务";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_version);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_skin);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(200, 350);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // tb_version
            // 
            this.tb_version.Location = new System.Drawing.Point(79, 24);
            this.tb_version.Name = "tb_version";
            this.tb_version.Size = new System.Drawing.Size(171, 25);
            this.tb_version.TabIndex = 4;
            this.tb_version.TextChanged += new System.EventHandler(this.tb_version_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "版本号：";
            // 
            // btn_skin
            // 
            this.btn_skin.Location = new System.Drawing.Point(6, 59);
            this.btn_skin.Name = "btn_skin";
            this.btn_skin.Size = new System.Drawing.Size(90, 29);
            this.btn_skin.TabIndex = 2;
            this.btn_skin.Text = "皮肤设置";
            this.btn_skin.UseVisualStyleBackColor = true;
            this.btn_skin.Click += new System.EventHandler(this.btn_skin_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(200, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 350);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "升级项目";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel2);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Location = new System.Drawing.Point(253, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 326);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "本次升级内容";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.lb_now);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 302);
            this.panel2.TabIndex = 1;
            // 
            // lb_now
            // 
            this.lb_now.AutoSize = true;
            this.lb_now.Location = new System.Drawing.Point(10, 10);
            this.lb_now.Name = "lb_now";
            this.lb_now.Size = new System.Drawing.Size(0, 15);
            this.lb_now.TabIndex = 0;
            this.lb_now.TextChanged += new System.EventHandler(this.lb_now_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(3, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 326);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "上次升级内容";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lb_up);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 302);
            this.panel1.TabIndex = 1;
            // 
            // lb_up
            // 
            this.lb_up.AutoSize = true;
            this.lb_up.Location = new System.Drawing.Point(10, 10);
            this.lb_up.Name = "lb_up";
            this.lb_up.Size = new System.Drawing.Size(0, 15);
            this.lb_up.TabIndex = 0;
            // 
            // skinEngine
            // 
            this.skinEngine.SerialNumber = "";
            this.skinEngine.SkinFile = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "升级服务";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_skin;
        private Sunisoft.IrisSkin.SkinEngine skinEngine;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lb_now;
        private System.Windows.Forms.Label lb_up;
        private System.Windows.Forms.Button btn_add_files;
        private System.Windows.Forms.Button btn_add_file;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_version;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_add_skin;
    }
}

