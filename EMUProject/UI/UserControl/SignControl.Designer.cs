
namespace EMU.UI
{
    partial class SignControl
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
            this.paneltop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.paneltop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paneltop.Location = new System.Drawing.Point(0, 0);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(400, 50);
            this.paneltop.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 50);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(400, 50);
            this.panelBottom.TabIndex = 1;
            // 
            // SignControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paneltop);
            this.Controls.Add(this.panelBottom);
            this.Name = "SignControl";
            this.Size = new System.Drawing.Size(400, 100);
            this.Load += new System.EventHandler(this.SignControl1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paneltop;
        private System.Windows.Forms.Panel panelBottom;
    }
}
