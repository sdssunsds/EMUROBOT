
namespace Project.ServerClass
{
    partial class SettingDataForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_select = new System.Windows.Forms.Button();
            this.cb_mode = new System.Windows.Forms.ComboBox();
            this.cb_sn = new System.Windows.Forms.ComboBox();
            this.cb_num = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_select);
            this.flowLayoutPanel1.Controls.Add(this.cb_mode);
            this.flowLayoutPanel1.Controls.Add(this.cb_sn);
            this.flowLayoutPanel1.Controls.Add(this.cb_num);
            this.flowLayoutPanel1.Controls.Add(this.btn_save);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(3, 3);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(75, 25);
            this.btn_select.TabIndex = 0;
            this.btn_select.Text = "···";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // cb_mode
            // 
            this.cb_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_mode.FormattingEnabled = true;
            this.cb_mode.Location = new System.Drawing.Point(84, 3);
            this.cb_mode.Name = "cb_mode";
            this.cb_mode.Size = new System.Drawing.Size(100, 23);
            this.cb_mode.TabIndex = 3;
            this.cb_mode.SelectedIndexChanged += new System.EventHandler(this.cb_mode_SelectedIndexChanged);
            // 
            // cb_sn
            // 
            this.cb_sn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sn.FormattingEnabled = true;
            this.cb_sn.Location = new System.Drawing.Point(190, 3);
            this.cb_sn.Name = "cb_sn";
            this.cb_sn.Size = new System.Drawing.Size(100, 23);
            this.cb_sn.TabIndex = 4;
            this.cb_sn.SelectedIndexChanged += new System.EventHandler(this.cb_sn_SelectedIndexChanged);
            // 
            // cb_num
            // 
            this.cb_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_num.FormattingEnabled = true;
            this.cb_num.Items.AddRange(new object[] {
            "8节车",
            "16节车"});
            this.cb_num.Location = new System.Drawing.Point(296, 3);
            this.cb_num.Name = "cb_num";
            this.cb_num.Size = new System.Drawing.Size(100, 23);
            this.cb_num.TabIndex = 2;
            this.cb_num.SelectedIndexChanged += new System.EventHandler(this.cb_num_SelectedIndexChanged);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(402, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 25);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 420);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // SettingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SettingDataForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingDataForm";
            this.Load += new System.EventHandler(this.SettingDataForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.ComboBox cb_num;
        private System.Windows.Forms.ComboBox cb_mode;
        private System.Windows.Forms.ComboBox cb_sn;
        private System.Windows.Forms.Button btn_save;
    }
}