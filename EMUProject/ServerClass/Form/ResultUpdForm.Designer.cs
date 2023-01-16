
namespace Project.ServerClass
{
    partial class ResultUpdForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_old = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_data = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.cb = new System.Windows.Forms.ComboBox();
            this.tb = new System.Windows.Forms.TextBox();
            this.btn = new System.Windows.Forms.Button();
            this.cb_Normal = new System.Windows.Forms.CheckBox();
            this.cb_Abnormal = new System.Windows.Forms.CheckBox();
            this.cb_None = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lb_old);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(350, 76);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "原始检测数据";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lb_old
            // 
            this.lb_old.Location = new System.Drawing.Point(118, 0);
            this.lb_old.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_old.Name = "lb_old";
            this.lb_old.Size = new System.Drawing.Size(221, 20);
            this.lb_old.TabIndex = 3;
            this.lb_old.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.lb_data);
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(2, 22);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(338, 53);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "上次修正数据";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lb_data
            // 
            this.lb_data.Location = new System.Drawing.Point(118, 0);
            this.lb_data.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_data.Name = "lb_data";
            this.lb_data.Size = new System.Drawing.Size(215, 20);
            this.lb_data.TabIndex = 2;
            this.lb_data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "本次修正数据";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.cb);
            this.flowLayoutPanel4.Controls.Add(this.tb);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(118, 22);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(217, 24);
            this.flowLayoutPanel4.TabIndex = 4;
            // 
            // cb
            // 
            this.cb.FormattingEnabled = true;
            this.cb.Location = new System.Drawing.Point(2, 2);
            this.cb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(114, 20);
            this.cb.TabIndex = 1;
            this.cb.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(120, 2);
            this.tb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(87, 21);
            this.tb.TabIndex = 2;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(284, 78);
            this.btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(56, 20);
            this.btn.TabIndex = 1;
            this.btn.Text = "保存";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // cb_Normal
            // 
            this.cb_Normal.AutoSize = true;
            this.cb_Normal.Location = new System.Drawing.Point(4, 81);
            this.cb_Normal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Normal.Name = "cb_Normal";
            this.cb_Normal.Size = new System.Drawing.Size(48, 16);
            this.cb_Normal.TabIndex = 2;
            this.cb_Normal.Text = "正常";
            this.cb_Normal.UseVisualStyleBackColor = true;
            this.cb_Normal.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cb_Abnormal
            // 
            this.cb_Abnormal.AutoSize = true;
            this.cb_Abnormal.Location = new System.Drawing.Point(53, 81);
            this.cb_Abnormal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Abnormal.Name = "cb_Abnormal";
            this.cb_Abnormal.Size = new System.Drawing.Size(48, 16);
            this.cb_Abnormal.TabIndex = 3;
            this.cb_Abnormal.Text = "异常";
            this.cb_Abnormal.UseVisualStyleBackColor = true;
            this.cb_Abnormal.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cb_None
            // 
            this.cb_None.AutoSize = true;
            this.cb_None.Location = new System.Drawing.Point(102, 81);
            this.cb_None.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_None.Name = "cb_None";
            this.cb_None.Size = new System.Drawing.Size(48, 16);
            this.cb_None.TabIndex = 4;
            this.cb_None.Text = "未知";
            this.cb_None.UseVisualStyleBackColor = true;
            this.cb_None.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // ResultUpdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 99);
            this.Controls.Add(this.cb_None);
            this.Controls.Add(this.cb_Abnormal);
            this.Controls.Add(this.cb_Normal);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResultUpdForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结果修正";
            this.Load += new System.EventHandler(this.ResultUpdForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_old;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_data;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.ComboBox cb;
        private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.CheckBox cb_Normal;
        private System.Windows.Forms.CheckBox cb_Abnormal;
        private System.Windows.Forms.CheckBox cb_None;
    }
}