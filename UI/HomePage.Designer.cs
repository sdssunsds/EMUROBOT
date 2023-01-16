
namespace EMU.UI
{
    partial class HomePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.tb_log = new System.Windows.Forms.TextBox();
            this.rgvControl = new Rgv.RgvControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_alarm = new System.Windows.Forms.PictureBox();
            this.lb = new System.Windows.Forms.Label();
            this.btn_check = new System.Windows.Forms.Button();
            this.btn_head = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.cameraControl = new Basler.CameraControl();
            this.frontRobotControl = new Robot.FrontRobotControl();
            this.backRobotControl = new Robot.BackRobotControl();
            this.rgvInfoControl = new Rgv.RgvInfoControl();
            this.signControl = new EMU.UI.SignControl();
            this.btn_wait = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_alarm)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_log
            // 
            this.tb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_log.Location = new System.Drawing.Point(1140, 0);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(133, 544);
            this.tb_log.TabIndex = 12;
            // 
            // rgvControl
            // 
            this.rgvControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rgvControl.Location = new System.Drawing.Point(0, 644);
            this.rgvControl.Name = "rgvControl";
            this.rgvControl.RgvColor = global::EMU.UI.Properties.Settings.Default.Color;
            this.rgvControl.Size = new System.Drawing.Size(1273, 80);
            this.rgvControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_wait);
            this.panel1.Controls.Add(this.pb_alarm);
            this.panel1.Controls.Add(this.lb);
            this.panel1.Controls.Add(this.btn_check);
            this.panel1.Controls.Add(this.btn_head);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Controls.Add(this.btn_forward);
            this.panel1.Controls.Add(this.btn_stop);
            this.panel1.Controls.Add(this.btn_start);
            this.panel1.Controls.Add(this.cameraControl);
            this.panel1.Controls.Add(this.frontRobotControl);
            this.panel1.Controls.Add(this.backRobotControl);
            this.panel1.Controls.Add(this.rgvInfoControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 544);
            this.panel1.TabIndex = 15;
            // 
            // pb_alarm
            // 
            this.pb_alarm.Image = ((System.Drawing.Image)(resources.GetObject("pb_alarm.Image")));
            this.pb_alarm.Location = new System.Drawing.Point(736, 0);
            this.pb_alarm.Name = "pb_alarm";
            this.pb_alarm.Size = new System.Drawing.Size(25, 25);
            this.pb_alarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_alarm.TabIndex = 26;
            this.pb_alarm.TabStop = false;
            this.pb_alarm.Visible = false;
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(7, 10);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(67, 15);
            this.lb.TabIndex = 25;
            this.lb.Text = "当前流程";
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(1036, 253);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(100, 30);
            this.btn_check.TabIndex = 24;
            this.btn_check.Text = "设备检测";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // btn_head
            // 
            this.btn_head.Location = new System.Drawing.Point(1036, 79);
            this.btn_head.Name = "btn_head";
            this.btn_head.Size = new System.Drawing.Size(100, 30);
            this.btn_head.TabIndex = 23;
            this.btn_head.Text = "车头检测";
            this.btn_head.UseVisualStyleBackColor = true;
            this.btn_head.Click += new System.EventHandler(this.btn_head_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(1036, 224);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(100, 30);
            this.btn_clear.TabIndex = 22;
            this.btn_clear.Text = "清除报警";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(1036, 166);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(100, 30);
            this.btn_back.TabIndex = 21;
            this.btn_back.Text = "反向流程";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.Location = new System.Drawing.Point(1036, 137);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(100, 30);
            this.btn_forward.TabIndex = 20;
            this.btn_forward.Text = "正向流程";
            this.btn_forward.UseVisualStyleBackColor = true;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(1036, 108);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(100, 30);
            this.btn_stop.TabIndex = 19;
            this.btn_stop.Text = "终止流程";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(1036, 0);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(100, 80);
            this.btn_start.TabIndex = 18;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // cameraControl
            // 
            this.cameraControl.Location = new System.Drawing.Point(4, 28);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = new System.Drawing.Size(757, 504);
            this.cameraControl.TabIndex = 17;
            // 
            // frontRobotControl
            // 
            this.frontRobotControl.Location = new System.Drawing.Point(767, 292);
            this.frontRobotControl.Name = "frontRobotControl";
            this.frontRobotControl.Size = new System.Drawing.Size(170, 240);
            this.frontRobotControl.TabIndex = 16;
            // 
            // backRobotControl
            // 
            this.backRobotControl.Location = new System.Drawing.Point(943, 292);
            this.backRobotControl.Name = "backRobotControl";
            this.backRobotControl.Size = new System.Drawing.Size(170, 240);
            this.backRobotControl.TabIndex = 15;
            // 
            // rgvInfoControl
            // 
            this.rgvInfoControl.Location = new System.Drawing.Point(767, 4);
            this.rgvInfoControl.Name = "rgvInfoControl";
            this.rgvInfoControl.Size = new System.Drawing.Size(263, 282);
            this.rgvInfoControl.TabIndex = 14;
            // 
            // signControl
            // 
            this.signControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.signControl.Location = new System.Drawing.Point(0, 544);
            this.signControl.Name = "signControl";
            this.signControl.Size = new System.Drawing.Size(1273, 100);
            this.signControl.TabIndex = 14;
            // 
            // btn_wait
            // 
            this.btn_wait.Location = new System.Drawing.Point(1036, 195);
            this.btn_wait.Name = "btn_wait";
            this.btn_wait.Size = new System.Drawing.Size(100, 30);
            this.btn_wait.TabIndex = 27;
            this.btn_wait.Text = "正常停车";
            this.btn_wait.UseVisualStyleBackColor = true;
            this.btn_wait.Click += new System.EventHandler(this.btn_wait_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.signControl);
            this.Controls.Add(this.rgvControl);
            this.Name = "HomePage";
            this.Size = new System.Drawing.Size(1273, 724);
            this.Load += new System.EventHandler(this.HomePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_alarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_log;
        public Rgv.RgvControl rgvControl;
        public SignControl signControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Button btn_head;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_start;
        public Basler.CameraControl cameraControl;
        public Robot.FrontRobotControl frontRobotControl;
        public Robot.BackRobotControl backRobotControl;
        public Rgv.RgvInfoControl rgvInfoControl;
        private System.Windows.Forms.PictureBox pb_alarm;
        private System.Windows.Forms.Button btn_wait;
    }
}
