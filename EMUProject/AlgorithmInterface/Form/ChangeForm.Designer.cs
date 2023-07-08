namespace Project
{
    partial class ChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pb_todo = new System.Windows.Forms.PictureBox();
            this.pb_revoke = new System.Windows.Forms.PictureBox();
            this.pb_ok = new System.Windows.Forms.PictureBox();
            this.pb_del = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tb_h = new System.Windows.Forms.TextBox();
            this.tb_w = new System.Windows.Forms.TextBox();
            this.tb_y = new System.Windows.Forms.TextBox();
            this.tb_x = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pb_save = new System.Windows.Forms.PictureBox();
            this.lb_num = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_todo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_revoke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_save)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.pb_todo);
            this.panel1.Controls.Add(this.pb_revoke);
            this.panel1.Controls.Add(this.pb_ok);
            this.panel1.Controls.Add(this.pb_del);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.tb_h);
            this.panel1.Controls.Add(this.tb_w);
            this.panel1.Controls.Add(this.tb_y);
            this.panel1.Controls.Add(this.tb_x);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(668, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(132, 450);
            this.panel1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(6, 376);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(123, 71);
            this.treeView1.TabIndex = 15;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // pb_todo
            // 
            this.pb_todo.Image = ((System.Drawing.Image)(resources.GetObject("pb_todo.Image")));
            this.pb_todo.Location = new System.Drawing.Point(68, 86);
            this.pb_todo.Name = "pb_todo";
            this.pb_todo.Size = new System.Drawing.Size(30, 30);
            this.pb_todo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_todo.TabIndex = 14;
            this.pb_todo.TabStop = false;
            this.pb_todo.Click += new System.EventHandler(this.btn_redo_Click);
            // 
            // pb_revoke
            // 
            this.pb_revoke.Image = ((System.Drawing.Image)(resources.GetObject("pb_revoke.Image")));
            this.pb_revoke.Location = new System.Drawing.Point(37, 86);
            this.pb_revoke.Name = "pb_revoke";
            this.pb_revoke.Size = new System.Drawing.Size(30, 30);
            this.pb_revoke.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_revoke.TabIndex = 13;
            this.pb_revoke.TabStop = false;
            this.pb_revoke.Click += new System.EventHandler(this.btn_revoke_Click);
            // 
            // pb_ok
            // 
            this.pb_ok.Image = ((System.Drawing.Image)(resources.GetObject("pb_ok.Image")));
            this.pb_ok.Location = new System.Drawing.Point(6, 86);
            this.pb_ok.Name = "pb_ok";
            this.pb_ok.Size = new System.Drawing.Size(30, 30);
            this.pb_ok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_ok.TabIndex = 12;
            this.pb_ok.TabStop = false;
            this.pb_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // pb_del
            // 
            this.pb_del.Image = ((System.Drawing.Image)(resources.GetObject("pb_del.Image")));
            this.pb_del.Location = new System.Drawing.Point(99, 86);
            this.pb_del.Name = "pb_del";
            this.pb_del.Size = new System.Drawing.Size(30, 30);
            this.pb_del.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_del.TabIndex = 11;
            this.pb_del.TabStop = false;
            this.pb_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(123, 20);
            this.comboBox1.TabIndex = 9;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 126);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(123, 244);
            this.checkedListBox1.TabIndex = 8;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // tb_h
            // 
            this.tb_h.Location = new System.Drawing.Point(89, 33);
            this.tb_h.Name = "tb_h";
            this.tb_h.Size = new System.Drawing.Size(40, 21);
            this.tb_h.TabIndex = 4;
            // 
            // tb_w
            // 
            this.tb_w.Location = new System.Drawing.Point(27, 33);
            this.tb_w.Name = "tb_w";
            this.tb_w.Size = new System.Drawing.Size(40, 21);
            this.tb_w.TabIndex = 3;
            // 
            // tb_y
            // 
            this.tb_y.Location = new System.Drawing.Point(89, 9);
            this.tb_y.Name = "tb_y";
            this.tb_y.Size = new System.Drawing.Size(40, 21);
            this.tb_y.TabIndex = 2;
            // 
            // tb_x
            // 
            this.tb_x.Location = new System.Drawing.Point(27, 9);
            this.tb_x.Name = "tb_x";
            this.tb_x.Size = new System.Drawing.Size(40, 21);
            this.tb_x.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "  X         Y\r\n\r\n 宽        高";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pb_save);
            this.panel2.Controls.Add(this.lb_num);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 418);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 32);
            this.panel2.TabIndex = 1;
            // 
            // pb_save
            // 
            this.pb_save.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pb_save.Image = ((System.Drawing.Image)(resources.GetObject("pb_save.Image")));
            this.pb_save.Location = new System.Drawing.Point(402, 1);
            this.pb_save.Name = "pb_save";
            this.pb_save.Size = new System.Drawing.Size(30, 30);
            this.pb_save.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_save.TabIndex = 12;
            this.pb_save.TabStop = false;
            this.pb_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lb_num
            // 
            this.lb_num.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lb_num.AutoSize = true;
            this.lb_num.Location = new System.Drawing.Point(262, 10);
            this.lb_num.Name = "lb_num";
            this.lb_num.Size = new System.Drawing.Size(11, 12);
            this.lb_num.TabIndex = 1;
            this.lb_num.Text = "0";
            // 
            // ChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ChangeForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人工修正检测结果";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangeForm_FormClosing);
            this.Load += new System.EventHandler(this.ChangeForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_todo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_revoke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_del)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_save)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_h;
        private System.Windows.Forms.TextBox tb_w;
        private System.Windows.Forms.TextBox tb_y;
        private System.Windows.Forms.TextBox tb_x;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lb_num;
        private System.Windows.Forms.PictureBox pb_todo;
        private System.Windows.Forms.PictureBox pb_revoke;
        private System.Windows.Forms.PictureBox pb_ok;
        private System.Windows.Forms.PictureBox pb_del;
        private System.Windows.Forms.PictureBox pb_save;
        private System.Windows.Forms.TreeView treeView1;
    }
}