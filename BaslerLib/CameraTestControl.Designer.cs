
namespace Basler
{
    partial class CameraTestControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraTestControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_continuousshot = new System.Windows.Forms.Button();
            this.btn_oneshot = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cameraControl1 = new Basler.CameraControl();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 500);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tb_log);
            this.panel2.Controls.Add(this.btn_stop);
            this.panel2.Controls.Add(this.btn_continuousshot);
            this.panel2.Controls.Add(this.btn_oneshot);
            this.panel2.Controls.Add(this.btn_close);
            this.panel2.Controls.Add(this.btn_open);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 350);
            this.panel2.TabIndex = 1;
            // 
            // tb_log
            // 
            this.tb_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_log.BackColor = System.Drawing.Color.White;
            this.tb_log.Location = new System.Drawing.Point(17, 126);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.Size = new System.Drawing.Size(165, 216);
            this.tb_log.TabIndex = 5;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(107, 86);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 25);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "停止拍图";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_continuousshot
            // 
            this.btn_continuousshot.Location = new System.Drawing.Point(17, 86);
            this.btn_continuousshot.Name = "btn_continuousshot";
            this.btn_continuousshot.Size = new System.Drawing.Size(75, 25);
            this.btn_continuousshot.TabIndex = 3;
            this.btn_continuousshot.Text = "连续拍图";
            this.btn_continuousshot.UseVisualStyleBackColor = true;
            this.btn_continuousshot.Click += new System.EventHandler(this.btn_continuousshot_Click);
            // 
            // btn_oneshot
            // 
            this.btn_oneshot.Location = new System.Drawing.Point(17, 46);
            this.btn_oneshot.Name = "btn_oneshot";
            this.btn_oneshot.Size = new System.Drawing.Size(75, 25);
            this.btn_oneshot.TabIndex = 2;
            this.btn_oneshot.Text = "单帧拍图";
            this.btn_oneshot.UseVisualStyleBackColor = true;
            this.btn_oneshot.Click += new System.EventHandler(this.btn_oneshot_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(107, 6);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 25);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "关闭相机";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(17, 6);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 25);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "打开相机";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机列表";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(194, 126);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl1.Location = new System.Drawing.Point(200, 0);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(700, 500);
            this.cameraControl1.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "icon_connect.png");
            this.imageList.Images.SetKeyName(1, "icon_disconnect.png");
            // 
            // CameraTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cameraControl1);
            this.Controls.Add(this.panel1);
            this.Name = "CameraTestControl";
            this.Size = new System.Drawing.Size(900, 500);
            this.Load += new System.EventHandler(this.CameraTestControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CameraControl cameraControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_oneshot;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_continuousshot;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.ImageList imageList;
    }
}
