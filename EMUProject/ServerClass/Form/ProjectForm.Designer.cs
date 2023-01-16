
namespace Project.ServerClass
{
    partial class ProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_address = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_rode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_two = new System.Windows.Forms.RadioButton();
            this.rb_one = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_num = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_mode = new System.Windows.Forms.ComboBox();
            this.cb_sn = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_robot = new System.Windows.Forms.ComboBox();
            this.btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tb = new System.Windows.Forms.TextBox();
            this.cb_head = new System.Windows.Forms.ComboBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "检修库";
            // 
            // cb_address
            // 
            this.cb_address.FormattingEnabled = true;
            this.cb_address.Location = new System.Drawing.Point(78, 12);
            this.cb_address.Name = "cb_address";
            this.cb_address.Size = new System.Drawing.Size(317, 23);
            this.cb_address.TabIndex = 2;
            this.cb_address.SelectedIndexChanged += new System.EventHandler(this.cb_address_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "检修车道";
            // 
            // cb_rode
            // 
            this.cb_rode.FormattingEnabled = true;
            this.cb_rode.Location = new System.Drawing.Point(93, 47);
            this.cb_rode.Name = "cb_rode";
            this.cb_rode.Size = new System.Drawing.Size(90, 23);
            this.cb_rode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "检修位";
            // 
            // rb_two
            // 
            this.rb_two.AutoSize = true;
            this.rb_two.Checked = true;
            this.rb_two.Location = new System.Drawing.Point(329, 48);
            this.rb_two.Name = "rb_two";
            this.rb_two.Size = new System.Drawing.Size(66, 19);
            this.rb_two.TabIndex = 7;
            this.rb_two.TabStop = true;
            this.rb_two.Text = "2列位";
            this.rb_two.UseVisualStyleBackColor = true;
            // 
            // rb_one
            // 
            this.rb_one.AutoSize = true;
            this.rb_one.Location = new System.Drawing.Point(257, 48);
            this.rb_one.Name = "rb_one";
            this.rb_one.Size = new System.Drawing.Size(66, 19);
            this.rb_one.TabIndex = 8;
            this.rb_one.Text = "1列位";
            this.rb_one.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "车厢数量";
            // 
            // cb_num
            // 
            this.cb_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_num.FormattingEnabled = true;
            this.cb_num.Items.AddRange(new object[] {
            "8 车",
            "16车"});
            this.cb_num.Location = new System.Drawing.Point(93, 82);
            this.cb_num.Name = "cb_num";
            this.cb_num.Size = new System.Drawing.Size(90, 23);
            this.cb_num.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "车型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "车号";
            // 
            // cb_mode
            // 
            this.cb_mode.FormattingEnabled = true;
            this.cb_mode.Location = new System.Drawing.Point(93, 117);
            this.cb_mode.Name = "cb_mode";
            this.cb_mode.Size = new System.Drawing.Size(90, 23);
            this.cb_mode.TabIndex = 13;
            this.cb_mode.SelectedIndexChanged += new System.EventHandler(this.cb_mode_SelectedIndexChanged);
            // 
            // cb_sn
            // 
            this.cb_sn.FormattingEnabled = true;
            this.cb_sn.Location = new System.Drawing.Point(264, 117);
            this.cb_sn.Name = "cb_sn";
            this.cb_sn.Size = new System.Drawing.Size(90, 23);
            this.cb_sn.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "机器人";
            // 
            // cb_robot
            // 
            this.cb_robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_robot.FormattingEnabled = true;
            this.cb_robot.Location = new System.Drawing.Point(264, 82);
            this.cb_robot.Name = "cb_robot";
            this.cb_robot.Size = new System.Drawing.Size(131, 23);
            this.cb_robot.TabIndex = 16;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(329, 255);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 25);
            this.btn.TabIndex = 17;
            this.btn.Text = "确认";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "计划说明";
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(93, 187);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(302, 62);
            this.tb.TabIndex = 19;
            // 
            // cb_head
            // 
            this.cb_head.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_head.FormattingEnabled = true;
            this.cb_head.Items.AddRange(new object[] {
            "00",
            "01"});
            this.cb_head.Location = new System.Drawing.Point(264, 152);
            this.cb_head.Name = "cb_head";
            this.cb_head.Size = new System.Drawing.Size(90, 23);
            this.cb_head.TabIndex = 23;
            // 
            // cb_type
            // 
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(93, 152);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(90, 23);
            this.cb_type.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "车头号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "检测类型";
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 286);
            this.Controls.Add(this.cb_head);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.cb_robot);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_sn);
            this.Controls.Add(this.cb_mode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_num);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rb_one);
            this.Controls.Add(this.rb_two);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_rode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_address);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检修计划";
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_address;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_rode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_two;
        private System.Windows.Forms.RadioButton rb_one;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_num;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_mode;
        private System.Windows.Forms.ComboBox cb_sn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_robot;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.ComboBox cb_head;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}