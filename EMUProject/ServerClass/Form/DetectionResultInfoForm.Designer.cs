﻿
namespace Project.ServerClass
{
    partial class DetectionResultInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectionResultInfoForm));
            this.detectionResultControl = new Project.ServerClass.DetectionResultControl();
            this.SuspendLayout();
            // 
            // detectionResultControl
            // 
            this.detectionResultControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detectionResultControl.Location = new System.Drawing.Point(0, 0);
            this.detectionResultControl.Name = "detectionResultControl";
            this.detectionResultControl.ProjectModel = null;
            this.detectionResultControl.Size = new System.Drawing.Size(1297, 721);
            this.detectionResultControl.TabIndex = 0;
            // 
            // DetectionResultInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 721);
            this.Controls.Add(this.detectionResultControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DetectionResultInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检修结果";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DetectionResultInfoForm_Load);
            this.Shown += new System.EventHandler(this.DetectionResultInfoForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public DetectionResultControl detectionResultControl;
    }
}