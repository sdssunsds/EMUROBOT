
namespace Project.AGV
{
    partial class TestAGVForm
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
            this.testControl1 = new Project.AGV.TestControl();
            this.btn_open_log = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testControl1
            // 
            this.testControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testControl1.Location = new System.Drawing.Point(0, 0);
            this.testControl1.Margin = new System.Windows.Forms.Padding(2);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(940, 444);
            this.testControl1.TabIndex = 0;
            // 
            // btn_open_log
            // 
            this.btn_open_log.Location = new System.Drawing.Point(0, 45);
            this.btn_open_log.Name = "btn_open_log";
            this.btn_open_log.Size = new System.Drawing.Size(75, 23);
            this.btn_open_log.TabIndex = 1;
            this.btn_open_log.Text = "打开日志";
            this.btn_open_log.UseVisualStyleBackColor = true;
            this.btn_open_log.Click += new System.EventHandler(this.btn_open_log_Click);
            // 
            // TestAGVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 444);
            this.Controls.Add(this.btn_open_log);
            this.Controls.Add(this.testControl1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TestAGVForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestAGVForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.TestAGVForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private TestControl testControl1;
        private System.Windows.Forms.Button btn_open_log;
    }
}