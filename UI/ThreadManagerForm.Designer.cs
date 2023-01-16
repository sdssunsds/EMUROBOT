
namespace EMU.UI
{
    partial class ThreadManagerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_num1 = new System.Windows.Forms.Label();
            this.lb_num2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flp_back = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_front = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flp_back);
            this.groupBox1.Controls.Add(this.lb_num1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 659);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "后台线程";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flp_front);
            this.groupBox2.Controls.Add(this.lb_num2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(355, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 659);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "前台线程";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "线程总数";
            // 
            // lb_num1
            // 
            this.lb_num1.AutoSize = true;
            this.lb_num1.Location = new System.Drawing.Point(99, 24);
            this.lb_num1.Name = "lb_num1";
            this.lb_num1.Size = new System.Drawing.Size(15, 15);
            this.lb_num1.TabIndex = 1;
            this.lb_num1.Text = "0";
            // 
            // lb_num2
            // 
            this.lb_num2.AutoSize = true;
            this.lb_num2.Location = new System.Drawing.Point(100, 24);
            this.lb_num2.Name = "lb_num2";
            this.lb_num2.Size = new System.Drawing.Size(15, 15);
            this.lb_num2.TabIndex = 3;
            this.lb_num2.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "线程总数";
            // 
            // flp_back
            // 
            this.flp_back.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flp_back.Location = new System.Drawing.Point(3, 42);
            this.flp_back.Name = "flp_back";
            this.flp_back.Size = new System.Drawing.Size(349, 614);
            this.flp_back.TabIndex = 2;
            // 
            // flp_front
            // 
            this.flp_front.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flp_front.Location = new System.Drawing.Point(3, 42);
            this.flp_front.Name = "flp_front";
            this.flp_front.Size = new System.Drawing.Size(349, 614);
            this.flp_front.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ThreadManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 659);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThreadManagerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线程管理器";
            this.Load += new System.EventHandler(this.ThreadManagerForm_Load);
            this.Shown += new System.EventHandler(this.ThreadManagerForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_num1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_num2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flp_back;
        private System.Windows.Forms.FlowLayoutPanel flp_front;
        private System.Windows.Forms.Timer timer1;
    }
}