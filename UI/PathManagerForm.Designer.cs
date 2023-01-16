
namespace EMU.UI
{
    partial class PathManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathManagerForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_task = new System.Windows.Forms.Button();
            this.tb_task = new System.Windows.Forms.TextBox();
            this.btn_data = new System.Windows.Forms.Button();
            this.tb_data = new System.Windows.Forms.TextBox();
            this.btn_img = new System.Windows.Forms.Button();
            this.tb_img = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_3d = new System.Windows.Forms.Button();
            this.tb_3d = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 44);
            this.panel1.TabIndex = 0;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(196, 7);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 25);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(277, 7);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 25);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_3d);
            this.panel2.Controls.Add(this.tb_3d);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btn_task);
            this.panel2.Controls.Add(this.tb_task);
            this.panel2.Controls.Add(this.btn_data);
            this.panel2.Controls.Add(this.tb_data);
            this.panel2.Controls.Add(this.btn_img);
            this.panel2.Controls.Add(this.tb_img);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(364, 134);
            this.panel2.TabIndex = 1;
            // 
            // btn_task
            // 
            this.btn_task.Location = new System.Drawing.Point(307, 70);
            this.btn_task.Name = "btn_task";
            this.btn_task.Size = new System.Drawing.Size(45, 25);
            this.btn_task.TabIndex = 8;
            this.btn_task.Text = "...";
            this.btn_task.UseVisualStyleBackColor = true;
            this.btn_task.Click += new System.EventHandler(this.btn_task_Click);
            // 
            // tb_task
            // 
            this.tb_task.Location = new System.Drawing.Point(74, 70);
            this.tb_task.Name = "tb_task";
            this.tb_task.Size = new System.Drawing.Size(227, 25);
            this.tb_task.TabIndex = 7;
            // 
            // btn_data
            // 
            this.btn_data.Location = new System.Drawing.Point(307, 40);
            this.btn_data.Name = "btn_data";
            this.btn_data.Size = new System.Drawing.Size(45, 25);
            this.btn_data.TabIndex = 6;
            this.btn_data.Text = "...";
            this.btn_data.UseVisualStyleBackColor = true;
            this.btn_data.Click += new System.EventHandler(this.btn_data_Click);
            // 
            // tb_data
            // 
            this.tb_data.Location = new System.Drawing.Point(74, 40);
            this.tb_data.Name = "tb_data";
            this.tb_data.Size = new System.Drawing.Size(227, 25);
            this.tb_data.TabIndex = 5;
            // 
            // btn_img
            // 
            this.btn_img.Location = new System.Drawing.Point(307, 10);
            this.btn_img.Name = "btn_img";
            this.btn_img.Size = new System.Drawing.Size(45, 25);
            this.btn_img.TabIndex = 4;
            this.btn_img.Text = "...";
            this.btn_img.UseVisualStyleBackColor = true;
            this.btn_img.Click += new System.EventHandler(this.btn_img_Click);
            // 
            // tb_img
            // 
            this.tb_img.Location = new System.Drawing.Point(74, 10);
            this.tb_img.Name = "tb_img";
            this.tb_img.Size = new System.Drawing.Size(227, 25);
            this.tb_img.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "流程缓存";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据缓存";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片缓存";
            // 
            // btn_3d
            // 
            this.btn_3d.Location = new System.Drawing.Point(307, 101);
            this.btn_3d.Name = "btn_3d";
            this.btn_3d.Size = new System.Drawing.Size(45, 25);
            this.btn_3d.TabIndex = 11;
            this.btn_3d.Text = "...";
            this.btn_3d.UseVisualStyleBackColor = true;
            this.btn_3d.Click += new System.EventHandler(this.btn_3d_Click);
            // 
            // tb_3d
            // 
            this.tb_3d.Location = new System.Drawing.Point(74, 101);
            this.tb_3d.Name = "tb_3d";
            this.tb_3d.Size = new System.Drawing.Size(227, 25);
            this.tb_3d.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "3D开发包";
            // 
            // PathManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 178);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PathManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缓存设置";
            this.Load += new System.EventHandler(this.PathManagerForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_task;
        private System.Windows.Forms.TextBox tb_task;
        private System.Windows.Forms.Button btn_data;
        private System.Windows.Forms.TextBox tb_data;
        private System.Windows.Forms.Button btn_img;
        private System.Windows.Forms.TextBox tb_img;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_3d;
        private System.Windows.Forms.TextBox tb_3d;
        private System.Windows.Forms.Label label4;
    }
}