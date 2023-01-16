
namespace Rgv
{
    partial class RgvTestControl
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
            this.rgvInfoControl1 = new Rgv.RgvInfoControl();
            this.rgvControl1 = new Rgv.RgvControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_stopPower = new System.Windows.Forms.Button();
            this.btn_power = new System.Windows.Forms.Button();
            this.btn_estop = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_speed = new System.Windows.Forms.Button();
            this.btn_distance = new System.Windows.Forms.Button();
            this.btn_length = new System.Windows.Forms.Button();
            this.btn_move = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rgvInfoControl1
            // 
            this.rgvInfoControl1.Location = new System.Drawing.Point(6, 24);
            this.rgvInfoControl1.Name = "rgvInfoControl1";
            this.rgvInfoControl1.Size = new System.Drawing.Size(263, 282);
            this.rgvInfoControl1.TabIndex = 1;
            // 
            // rgvControl1
            // 
            this.rgvControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rgvControl1.Location = new System.Drawing.Point(0, 324);
            this.rgvControl1.Name = "rgvControl1";
            this.rgvControl1.RgvColor = System.Drawing.Color.Empty;
            this.rgvControl1.Size = new System.Drawing.Size(935, 80);
            this.rgvControl1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_move);
            this.groupBox1.Controls.Add(this.btn_length);
            this.groupBox1.Controls.Add(this.btn_distance);
            this.groupBox1.Controls.Add(this.btn_speed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.btn_stopPower);
            this.groupBox1.Controls.Add(this.btn_power);
            this.groupBox1.Controls.Add(this.btn_estop);
            this.groupBox1.Controls.Add(this.btn_stop);
            this.groupBox1.Controls.Add(this.btn_back);
            this.groupBox1.Controls.Add(this.btn_forward);
            this.groupBox1.Controls.Add(this.btn_close);
            this.groupBox1.Controls.Add(this.btn_connect);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 312);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "速度";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(48, 179);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 25);
            this.textBox1.TabIndex = 9;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(6, 148);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(110, 25);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.Text = "清除报警";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_stopPower
            // 
            this.btn_stopPower.Location = new System.Drawing.Point(122, 117);
            this.btn_stopPower.Name = "btn_stopPower";
            this.btn_stopPower.Size = new System.Drawing.Size(110, 25);
            this.btn_stopPower.TabIndex = 7;
            this.btn_stopPower.Text = "停止充电";
            this.btn_stopPower.UseVisualStyleBackColor = true;
            this.btn_stopPower.Click += new System.EventHandler(this.btn_stopPower_Click);
            // 
            // btn_power
            // 
            this.btn_power.Location = new System.Drawing.Point(6, 117);
            this.btn_power.Name = "btn_power";
            this.btn_power.Size = new System.Drawing.Size(110, 25);
            this.btn_power.TabIndex = 6;
            this.btn_power.Text = "充电";
            this.btn_power.UseVisualStyleBackColor = true;
            this.btn_power.Click += new System.EventHandler(this.btn_power_Click);
            // 
            // btn_estop
            // 
            this.btn_estop.Location = new System.Drawing.Point(122, 86);
            this.btn_estop.Name = "btn_estop";
            this.btn_estop.Size = new System.Drawing.Size(110, 25);
            this.btn_estop.TabIndex = 5;
            this.btn_estop.Text = "急停";
            this.btn_estop.UseVisualStyleBackColor = true;
            this.btn_estop.Click += new System.EventHandler(this.btn_estop_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(6, 86);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(110, 25);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(122, 55);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(110, 25);
            this.btn_back.TabIndex = 3;
            this.btn_back.Text = "反向运动";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.Location = new System.Drawing.Point(6, 55);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(110, 25);
            this.btn_forward.TabIndex = 2;
            this.btn_forward.Text = "正向运动";
            this.btn_forward.UseVisualStyleBackColor = true;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(122, 24);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(110, 25);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "断开";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(6, 24);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(110, 25);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "连接";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rgvInfoControl1);
            this.groupBox2.Location = new System.Drawing.Point(259, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 312);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "距离";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(48, 210);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(68, 25);
            this.textBox2.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(48, 241);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(68, 25);
            this.textBox3.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "轨长";
            // 
            // btn_speed
            // 
            this.btn_speed.Location = new System.Drawing.Point(122, 179);
            this.btn_speed.Name = "btn_speed";
            this.btn_speed.Size = new System.Drawing.Size(110, 25);
            this.btn_speed.TabIndex = 18;
            this.btn_speed.Text = "设置速度";
            this.btn_speed.UseVisualStyleBackColor = true;
            this.btn_speed.Click += new System.EventHandler(this.btn_speed_Click);
            // 
            // btn_distance
            // 
            this.btn_distance.Location = new System.Drawing.Point(122, 210);
            this.btn_distance.Name = "btn_distance";
            this.btn_distance.Size = new System.Drawing.Size(110, 25);
            this.btn_distance.TabIndex = 19;
            this.btn_distance.Text = "设置运动距离";
            this.btn_distance.UseVisualStyleBackColor = true;
            this.btn_distance.Click += new System.EventHandler(this.btn_distance_Click);
            // 
            // btn_length
            // 
            this.btn_length.Location = new System.Drawing.Point(122, 241);
            this.btn_length.Name = "btn_length";
            this.btn_length.Size = new System.Drawing.Size(110, 25);
            this.btn_length.TabIndex = 20;
            this.btn_length.Text = "设置轨道长度";
            this.btn_length.UseVisualStyleBackColor = true;
            this.btn_length.Click += new System.EventHandler(this.btn_length_Click);
            // 
            // btn_move
            // 
            this.btn_move.Location = new System.Drawing.Point(48, 272);
            this.btn_move.Name = "btn_move";
            this.btn_move.Size = new System.Drawing.Size(184, 25);
            this.btn_move.TabIndex = 21;
            this.btn_move.Text = "运动到设置距离";
            this.btn_move.UseVisualStyleBackColor = true;
            this.btn_move.Click += new System.EventHandler(this.btn_move_Click);
            // 
            // RgvTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rgvControl1);
            this.Name = "RgvTestControl";
            this.Size = new System.Drawing.Size(935, 404);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private RgvInfoControl rgvInfoControl1;
        private RgvControl rgvControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_stopPower;
        private System.Windows.Forms.Button btn_power;
        private System.Windows.Forms.Button btn_estop;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_move;
        private System.Windows.Forms.Button btn_length;
        private System.Windows.Forms.Button btn_distance;
        private System.Windows.Forms.Button btn_speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}
