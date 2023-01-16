
namespace Project
{
    partial class PictureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_sign = new System.Windows.Forms.CheckBox();
            this.tb_pars = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_pars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_sign);
            this.panel1.Controls.Add(this.tb_pars);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 39);
            this.panel1.TabIndex = 7;
            // 
            // cb_sign
            // 
            this.cb_sign.AutoSize = true;
            this.cb_sign.Checked = true;
            this.cb_sign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_sign.Location = new System.Drawing.Point(669, 3);
            this.cb_sign.Name = "cb_sign";
            this.cb_sign.Size = new System.Drawing.Size(59, 19);
            this.cb_sign.TabIndex = 8;
            this.cb_sign.Text = "标记";
            this.cb_sign.UseVisualStyleBackColor = true;
            this.cb_sign.CheckedChanged += new System.EventHandler(this.cb_sign_CheckedChanged);
            // 
            // tb_pars
            // 
            this.tb_pars.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_pars.Location = new System.Drawing.Point(0, 0);
            this.tb_pars.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_pars.Maximum = 255;
            this.tb_pars.Name = "tb_pars";
            this.tb_pars.Size = new System.Drawing.Size(664, 39);
            this.tb_pars.TabIndex = 7;
            this.tb_pars.TickFrequency = 10;
            this.tb_pars.Scroll += new System.EventHandler(this.tb_pars_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(760, 656);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PictureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部件预览";
            this.Load += new System.EventHandler(this.PictureForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_pars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_sign;
        private System.Windows.Forms.TrackBar tb_pars;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}