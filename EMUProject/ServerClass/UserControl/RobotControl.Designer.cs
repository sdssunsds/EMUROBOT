
namespace Project.ServerClass
{
    partial class RobotControl
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
            this.trainBar = new System.Windows.Forms.ProgressBar();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_home = new System.Windows.Forms.Button();
            this.btn_stop_power = new System.Windows.Forms.Button();
            this.btn_clear_alarm = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.btn_backward = new System.Windows.Forms.Button();
            this.btn_rgv_stop = new System.Windows.Forms.Button();
            this.btn_zero = new System.Windows.Forms.Button();
            this.btn_power_down = new System.Windows.Forms.Button();
            this.cameraControl1 = new Basler.CameraControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.tb_length = new System.Windows.Forms.TextBox();
            this.tb_distacnce = new System.Windows.Forms.TextBox();
            this.tb_speed = new System.Windows.Forms.TextBox();
            this.cb_distacnce = new System.Windows.Forms.CheckBox();
            this.cb_length = new System.Windows.Forms.CheckBox();
            this.cb_speed = new System.Windows.Forms.CheckBox();
            this.btn_setting = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.frontRobotControl1 = new Robot.FrontRobotControl();
            this.backRobotControl1 = new Robot.BackRobotControl();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rgvInfoControl1 = new Project.ServerClass.RgvInfoControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trainBar
            // 
            this.trainBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trainBar.Location = new System.Drawing.Point(0, 558);
            this.trainBar.Name = "trainBar";
            this.trainBar.Size = new System.Drawing.Size(2408, 20);
            this.trainBar.TabIndex = 1;
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.Dock = System.Windows.Forms.DockStyle.Left;
            this.flp.Location = new System.Drawing.Point(0, 0);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(110, 558);
            this.flp.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.btn_start);
            this.flowLayoutPanel1.Controls.Add(this.btn_stop);
            this.flowLayoutPanel1.Controls.Add(this.btn_home);
            this.flowLayoutPanel1.Controls.Add(this.btn_stop_power);
            this.flowLayoutPanel1.Controls.Add(this.btn_clear_alarm);
            this.flowLayoutPanel1.Controls.Add(this.btn_forward);
            this.flowLayoutPanel1.Controls.Add(this.btn_backward);
            this.flowLayoutPanel1.Controls.Add(this.btn_rgv_stop);
            this.flowLayoutPanel1.Controls.Add(this.btn_zero);
            this.flowLayoutPanel1.Controls.Add(this.btn_power_down);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(110, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(110, 558);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(3, 3);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(80, 80);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "开始\r\n作业";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(3, 89);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(80, 80);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "停止\r\n作业";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_home
            // 
            this.btn_home.Location = new System.Drawing.Point(3, 175);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(80, 80);
            this.btn_home.TabIndex = 6;
            this.btn_home.Text = "返回\r\n充电";
            this.btn_home.UseVisualStyleBackColor = true;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // btn_stop_power
            // 
            this.btn_stop_power.Location = new System.Drawing.Point(3, 261);
            this.btn_stop_power.Name = "btn_stop_power";
            this.btn_stop_power.Size = new System.Drawing.Size(80, 80);
            this.btn_stop_power.TabIndex = 7;
            this.btn_stop_power.Text = "停止\r\n充电";
            this.btn_stop_power.UseVisualStyleBackColor = true;
            this.btn_stop_power.Click += new System.EventHandler(this.btn_stop_power_Click);
            // 
            // btn_clear_alarm
            // 
            this.btn_clear_alarm.Location = new System.Drawing.Point(3, 347);
            this.btn_clear_alarm.Name = "btn_clear_alarm";
            this.btn_clear_alarm.Size = new System.Drawing.Size(80, 80);
            this.btn_clear_alarm.TabIndex = 2;
            this.btn_clear_alarm.Text = "清除\r\n报警";
            this.btn_clear_alarm.UseVisualStyleBackColor = true;
            this.btn_clear_alarm.Click += new System.EventHandler(this.btn_clear_alarm_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.Location = new System.Drawing.Point(3, 433);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(80, 80);
            this.btn_forward.TabIndex = 3;
            this.btn_forward.Text = "正向\r\n移动";
            this.btn_forward.UseVisualStyleBackColor = true;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // btn_backward
            // 
            this.btn_backward.Location = new System.Drawing.Point(3, 519);
            this.btn_backward.Name = "btn_backward";
            this.btn_backward.Size = new System.Drawing.Size(80, 80);
            this.btn_backward.TabIndex = 4;
            this.btn_backward.Text = "反向\r\n移动";
            this.btn_backward.UseVisualStyleBackColor = true;
            this.btn_backward.Click += new System.EventHandler(this.btn_backward_Click);
            // 
            // btn_rgv_stop
            // 
            this.btn_rgv_stop.Location = new System.Drawing.Point(3, 605);
            this.btn_rgv_stop.Name = "btn_rgv_stop";
            this.btn_rgv_stop.Size = new System.Drawing.Size(80, 80);
            this.btn_rgv_stop.TabIndex = 5;
            this.btn_rgv_stop.Text = "正常\r\n停车";
            this.btn_rgv_stop.UseVisualStyleBackColor = true;
            this.btn_rgv_stop.Click += new System.EventHandler(this.btn_rgv_stop_Click);
            // 
            // btn_zero
            // 
            this.btn_zero.Location = new System.Drawing.Point(3, 691);
            this.btn_zero.Name = "btn_zero";
            this.btn_zero.Size = new System.Drawing.Size(80, 80);
            this.btn_zero.TabIndex = 8;
            this.btn_zero.Text = "回收\r\n机械臂";
            this.btn_zero.UseVisualStyleBackColor = true;
            this.btn_zero.Click += new System.EventHandler(this.btn_zero_Click);
            // 
            // btn_power_down
            // 
            this.btn_power_down.Location = new System.Drawing.Point(3, 777);
            this.btn_power_down.Name = "btn_power_down";
            this.btn_power_down.Size = new System.Drawing.Size(80, 80);
            this.btn_power_down.TabIndex = 9;
            this.btn_power_down.Text = "关闭\r\n机器人";
            this.btn_power_down.UseVisualStyleBackColor = true;
            this.btn_power_down.Click += new System.EventHandler(this.btn_power_down_Click);
            // 
            // cameraControl1
            // 
            this.cameraControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cameraControl1.Location = new System.Drawing.Point(220, 0);
            this.cameraControl1.Name = "cameraControl1";
            this.cameraControl1.Size = new System.Drawing.Size(758, 558);
            this.cameraControl1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_run);
            this.groupBox1.Controls.Add(this.tb_length);
            this.groupBox1.Controls.Add(this.tb_distacnce);
            this.groupBox1.Controls.Add(this.tb_speed);
            this.groupBox1.Controls.Add(this.cb_distacnce);
            this.groupBox1.Controls.Add(this.cb_length);
            this.groupBox1.Controls.Add(this.cb_speed);
            this.groupBox1.Controls.Add(this.btn_setting);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 278);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "机器人控制";
            // 
            // btn_run
            // 
            this.btn_run.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_run.Location = new System.Drawing.Point(24, 229);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(208, 50);
            this.btn_run.TabIndex = 13;
            this.btn_run.Text = "运动到指定位置";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // tb_length
            // 
            this.tb_length.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_length.Location = new System.Drawing.Point(76, 131);
            this.tb_length.Name = "tb_length";
            this.tb_length.Size = new System.Drawing.Size(100, 30);
            this.tb_length.TabIndex = 7;
            this.tb_length.Text = "300000";
            // 
            // tb_distacnce
            // 
            this.tb_distacnce.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_distacnce.Location = new System.Drawing.Point(76, 81);
            this.tb_distacnce.Name = "tb_distacnce";
            this.tb_distacnce.Size = new System.Drawing.Size(100, 30);
            this.tb_distacnce.TabIndex = 4;
            this.tb_distacnce.Text = "8000";
            // 
            // tb_speed
            // 
            this.tb_speed.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_speed.Location = new System.Drawing.Point(76, 31);
            this.tb_speed.Name = "tb_speed";
            this.tb_speed.Size = new System.Drawing.Size(100, 30);
            this.tb_speed.TabIndex = 1;
            this.tb_speed.Text = "800";
            // 
            // cb_distacnce
            // 
            this.cb_distacnce.AutoSize = true;
            this.cb_distacnce.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_distacnce.Location = new System.Drawing.Point(6, 75);
            this.cb_distacnce.Name = "cb_distacnce";
            this.cb_distacnce.Size = new System.Drawing.Size(69, 42);
            this.cb_distacnce.TabIndex = 12;
            this.cb_distacnce.Text = "位置\r\n控制";
            this.cb_distacnce.UseVisualStyleBackColor = true;
            // 
            // cb_length
            // 
            this.cb_length.AutoSize = true;
            this.cb_length.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_length.Location = new System.Drawing.Point(6, 125);
            this.cb_length.Name = "cb_length";
            this.cb_length.Size = new System.Drawing.Size(69, 42);
            this.cb_length.TabIndex = 11;
            this.cb_length.Text = "轨道\r\n长度";
            this.cb_length.UseVisualStyleBackColor = true;
            // 
            // cb_speed
            // 
            this.cb_speed.AutoSize = true;
            this.cb_speed.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_speed.Location = new System.Drawing.Point(6, 25);
            this.cb_speed.Name = "cb_speed";
            this.cb_speed.Size = new System.Drawing.Size(69, 42);
            this.cb_speed.TabIndex = 10;
            this.cb_speed.Text = "速度\r\n控制";
            this.cb_speed.UseVisualStyleBackColor = true;
            // 
            // btn_setting
            // 
            this.btn_setting.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_setting.Location = new System.Drawing.Point(24, 173);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(208, 50);
            this.btn_setting.TabIndex = 9;
            this.btn_setting.Text = "确 认 设 置";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(182, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 50);
            this.label5.TabIndex = 8;
            this.label5.Text = "毫米";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(182, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 50);
            this.label3.TabIndex = 5;
            this.label3.Text = "毫米";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(182, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 50);
            this.label2.TabIndex = 2;
            this.label2.Text = "毫米";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frontRobotControl1
            // 
            this.frontRobotControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.frontRobotControl1.Location = new System.Drawing.Point(252, 280);
            this.frontRobotControl1.Name = "frontRobotControl1";
            this.frontRobotControl1.Size = new System.Drawing.Size(170, 278);
            this.frontRobotControl1.TabIndex = 7;
            // 
            // backRobotControl1
            // 
            this.backRobotControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.backRobotControl1.Location = new System.Drawing.Point(422, 280);
            this.backRobotControl1.Name = "backRobotControl1";
            this.backRobotControl1.Size = new System.Drawing.Size(170, 278);
            this.backRobotControl1.TabIndex = 8;
            // 
            // tb_log
            // 
            this.tb_log.BackColor = System.Drawing.Color.White;
            this.tb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_log.Location = new System.Drawing.Point(592, 280);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_log.Size = new System.Drawing.Size(838, 278);
            this.tb_log.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tb_log);
            this.panel1.Controls.Add(this.backRobotControl1);
            this.panel1.Controls.Add(this.frontRobotControl1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.rgvInfoControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(978, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1430, 558);
            this.panel1.TabIndex = 10;
            // 
            // rgvInfoControl1
            // 
            this.rgvInfoControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rgvInfoControl1.Location = new System.Drawing.Point(0, 0);
            this.rgvInfoControl1.Name = "rgvInfoControl1";
            this.rgvInfoControl1.Size = new System.Drawing.Size(1430, 280);
            this.rgvInfoControl1.TabIndex = 5;
            // 
            // RobotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cameraControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flp);
            this.Controls.Add(this.trainBar);
            this.Name = "RobotControl";
            this.Size = new System.Drawing.Size(1458, 578);
            this.Load += new System.EventHandler(this.RobotControl_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar trainBar;
        private Basler.CameraControl cameraControl1;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_clear_alarm;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.Button btn_backward;
        private System.Windows.Forms.Button btn_rgv_stop;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Button btn_stop_power;
        private System.Windows.Forms.Button btn_zero;
        private System.Windows.Forms.Button btn_power_down;
        private RgvInfoControl rgvInfoControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_speed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_length;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_distacnce;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_setting;
        private Robot.FrontRobotControl frontRobotControl1;
        private Robot.BackRobotControl backRobotControl1;
        private System.Windows.Forms.CheckBox cb_distacnce;
        private System.Windows.Forms.CheckBox cb_length;
        private System.Windows.Forms.CheckBox cb_speed;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_run;
    }
}
