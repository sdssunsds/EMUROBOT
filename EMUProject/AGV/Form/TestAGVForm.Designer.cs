
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
            this.SuspendLayout();
            // 
            // testControl1
            // 
            this.testControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testControl1.Location = new System.Drawing.Point(0, 0);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(1254, 555);
            this.testControl1.TabIndex = 0;
            // 
            // TestAGVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 555);
            this.Controls.Add(this.testControl1);
            this.DoubleBuffered = true;
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
    }
}