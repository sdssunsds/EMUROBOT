
namespace Project.AGV
{
    partial class RunAgvLocationForm
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
            this.lb_left = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_right = new System.Windows.Forms.ListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_internal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_left
            // 
            this.lb_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_left.FormattingEnabled = true;
            this.lb_left.ItemHeight = 12;
            this.lb_left.Location = new System.Drawing.Point(0, 0);
            this.lb_left.Name = "lb_left";
            this.lb_left.Size = new System.Drawing.Size(120, 541);
            this.lb_left.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_internal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_del);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(120, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 541);
            this.panel1.TabIndex = 1;
            // 
            // lb_right
            // 
            this.lb_right.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_right.FormattingEnabled = true;
            this.lb_right.ItemHeight = 12;
            this.lb_right.Location = new System.Drawing.Point(155, 0);
            this.lb_right.Name = "lb_right";
            this.lb_right.Size = new System.Drawing.Size(120, 541);
            this.lb_right.TabIndex = 2;
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_add.Location = new System.Drawing.Point(3, 175);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(29, 23);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = ">>";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_del
            // 
            this.btn_del.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_del.Location = new System.Drawing.Point(3, 308);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(29, 23);
            this.btn_del.TabIndex = 1;
            this.btn_del.Text = "<<";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "运行\r\n间隔";
            // 
            // tb_internal
            // 
            this.tb_internal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tb_internal.Location = new System.Drawing.Point(3, 249);
            this.tb_internal.Name = "tb_internal";
            this.tb_internal.Size = new System.Drawing.Size(29, 21);
            this.tb_internal.TabIndex = 3;
            this.tb_internal.Text = "0";
            this.tb_internal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "毫秒";
            // 
            // RunAgvLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 541);
            this.Controls.Add(this.lb_right);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_left);
            this.Name = "RunAgvLocationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量运行点位";
            this.Load += new System.EventHandler(this.RunAgvLocationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_left;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ListBox lb_right;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_internal;
    }
}