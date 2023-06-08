namespace Project
{
    partial class ChangeHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeHistoryForm));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_all = new System.Windows.Forms.Button();
            this.btn_sel = new System.Windows.Forms.Button();
            this.cb_state = new System.Windows.Forms.ComboBox();
            this.tb_sn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_mode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_partId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_list = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pb = new System.Windows.Forms.PictureBox();
            this.bar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_all);
            this.panel1.Controls.Add(this.btn_sel);
            this.panel1.Controls.Add(this.cb_state);
            this.panel1.Controls.Add(this.tb_sn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_mode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_partId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 25);
            this.panel1.TabIndex = 1;
            // 
            // btn_all
            // 
            this.btn_all.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_all.Location = new System.Drawing.Point(790, 0);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 25);
            this.btn_all.TabIndex = 9;
            this.btn_all.Text = "筛选所有";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // btn_sel
            // 
            this.btn_sel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_sel.Location = new System.Drawing.Point(715, 0);
            this.btn_sel.Name = "btn_sel";
            this.btn_sel.Size = new System.Drawing.Size(75, 25);
            this.btn_sel.TabIndex = 4;
            this.btn_sel.Text = "筛选当前";
            this.btn_sel.UseVisualStyleBackColor = true;
            this.btn_sel.Click += new System.EventHandler(this.btn_sel_Click);
            // 
            // cb_state
            // 
            this.cb_state.Dock = System.Windows.Forms.DockStyle.Left;
            this.cb_state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_state.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_state.FormattingEnabled = true;
            this.cb_state.Location = new System.Drawing.Point(565, 0);
            this.cb_state.Name = "cb_state";
            this.cb_state.Size = new System.Drawing.Size(150, 21);
            this.cb_state.TabIndex = 3;
            // 
            // tb_sn
            // 
            this.tb_sn.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_sn.Location = new System.Drawing.Point(515, 0);
            this.tb_sn.Name = "tb_sn";
            this.tb_sn.Size = new System.Drawing.Size(50, 21);
            this.tb_sn.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(485, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "车号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_mode
            // 
            this.tb_mode.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_mode.Location = new System.Drawing.Point(435, 0);
            this.tb_mode.Name = "tb_mode";
            this.tb_mode.Size = new System.Drawing.Size(50, 21);
            this.tb_mode.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(405, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "车型";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_partId
            // 
            this.tb_partId.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_partId.Location = new System.Drawing.Point(205, 0);
            this.tb_partId.Name = "tb_partId";
            this.tb_partId.Size = new System.Drawing.Size(200, 21);
            this.tb_partId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(125, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "部件编号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_list
            // 
            this.lb_list.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_list.FormattingEnabled = true;
            this.lb_list.ItemHeight = 12;
            this.lb_list.Location = new System.Drawing.Point(0, 25);
            this.lb_list.Name = "lb_list";
            this.lb_list.Size = new System.Drawing.Size(255, 425);
            this.lb_list.TabIndex = 2;
            this.lb_list.SelectedIndexChanged += new System.EventHandler(this.lb_list_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.pb);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(255, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(603, 395);
            this.panel2.TabIndex = 3;
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 50);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            // 
            // bar
            // 
            this.bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar.Location = new System.Drawing.Point(255, 435);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(603, 15);
            this.bar.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(255, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(603, 15);
            this.panel3.TabIndex = 4;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(90, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "人工修正结果";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(0, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "原算法结果";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ChangeHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.lb_list);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人工修正历史记录";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ChangeHistoryForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_partId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sel;
        private System.Windows.Forms.ComboBox cb_state;
        private System.Windows.Forms.ListBox lb_list;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_sn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_mode;
        private System.Windows.Forms.ProgressBar bar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}