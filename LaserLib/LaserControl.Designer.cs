
namespace Laser
{
    partial class LaserControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_open1 = new System.Windows.Forms.Button();
            this.btn_close1 = new System.Windows.Forms.Button();
            this.btn_close2 = new System.Windows.Forms.Button();
            this.btn_open2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btn_clear1 = new System.Windows.Forms.Button();
            this.btn_clear2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_clear1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_close1);
            this.groupBox1.Controls.Add(this.btn_open1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 600);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "定位激光";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_clear2);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.btn_close2);
            this.groupBox2.Controls.Add(this.btn_open2);
            this.groupBox2.Location = new System.Drawing.Point(456, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 600);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测距激光";
            // 
            // btn_open1
            // 
            this.btn_open1.Location = new System.Drawing.Point(6, 24);
            this.btn_open1.Name = "btn_open1";
            this.btn_open1.Size = new System.Drawing.Size(75, 25);
            this.btn_open1.TabIndex = 0;
            this.btn_open1.Text = "连接";
            this.btn_open1.UseVisualStyleBackColor = true;
            this.btn_open1.Click += new System.EventHandler(this.btn_open1_Click);
            // 
            // btn_close1
            // 
            this.btn_close1.Location = new System.Drawing.Point(87, 24);
            this.btn_close1.Name = "btn_close1";
            this.btn_close1.Size = new System.Drawing.Size(75, 25);
            this.btn_close1.TabIndex = 1;
            this.btn_close1.Text = "断开";
            this.btn_close1.UseVisualStyleBackColor = true;
            this.btn_close1.Click += new System.EventHandler(this.btn_close1_Click);
            // 
            // btn_close2
            // 
            this.btn_close2.Location = new System.Drawing.Point(87, 24);
            this.btn_close2.Name = "btn_close2";
            this.btn_close2.Size = new System.Drawing.Size(75, 25);
            this.btn_close2.TabIndex = 3;
            this.btn_close2.Text = "断开";
            this.btn_close2.UseVisualStyleBackColor = true;
            this.btn_close2.Click += new System.EventHandler(this.btn_close2_Click);
            // 
            // btn_open2
            // 
            this.btn_open2.Location = new System.Drawing.Point(6, 24);
            this.btn_open2.Name = "btn_open2";
            this.btn_open2.Size = new System.Drawing.Size(75, 25);
            this.btn_open2.TabIndex = 2;
            this.btn_open2.Text = "连接";
            this.btn_open2.UseVisualStyleBackColor = true;
            this.btn_open2.Click += new System.EventHandler(this.btn_open2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 55);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(438, 539);
            this.textBox1.TabIndex = 2;
            this.textBox1.Tag = "9999";
            // 
            // textBox2
            // 
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox2.Location = new System.Drawing.Point(6, 55);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(438, 539);
            this.textBox2.TabIndex = 4;
            this.textBox2.Tag = "9999";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.ForeColor = System.Drawing.Color.Red;
            this.textBox3.Location = new System.Drawing.Point(6, 606);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(894, 137);
            this.textBox3.TabIndex = 3;
            this.textBox3.Tag = "9999";
            // 
            // btn_clear1
            // 
            this.btn_clear1.Location = new System.Drawing.Point(168, 24);
            this.btn_clear1.Name = "btn_clear1";
            this.btn_clear1.Size = new System.Drawing.Size(120, 25);
            this.btn_clear1.TabIndex = 3;
            this.btn_clear1.Text = "清空日志";
            this.btn_clear1.UseVisualStyleBackColor = true;
            this.btn_clear1.Click += new System.EventHandler(this.btn_clear1_Click);
            // 
            // btn_clear2
            // 
            this.btn_clear2.Location = new System.Drawing.Point(168, 24);
            this.btn_clear2.Name = "btn_clear2";
            this.btn_clear2.Size = new System.Drawing.Size(120, 25);
            this.btn_clear2.TabIndex = 5;
            this.btn_clear2.Text = "清空日志";
            this.btn_clear2.UseVisualStyleBackColor = true;
            this.btn_clear2.Click += new System.EventHandler(this.btn_clear2_Click);
            // 
            // LaserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LaserControl";
            this.Size = new System.Drawing.Size(913, 746);
            this.Load += new System.EventHandler(this.LaserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_close1;
        private System.Windows.Forms.Button btn_open1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_close2;
        private System.Windows.Forms.Button btn_open2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btn_clear1;
        private System.Windows.Forms.Button btn_clear2;
    }
}
