
namespace Project.AGV
{
    partial class TestControl
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_car = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加载地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空点位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原缩放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.自动导航ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导航一次ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重复导航ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置导航ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存点位数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载点位数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.批量执行点位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.执行一轮ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重复执行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置批量点位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_cancel_move = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_shutdown = new System.Windows.Forms.Button();
            this.btn_setting = new System.Windows.Forms.Button();
            this.tb_turn = new System.Windows.Forms.TextBox();
            this.tb_speed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.btn_backward = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.lb_error = new System.Windows.Forms.Label();
            this.btn_start_server = new System.Windows.Forms.Button();
            this.btn_close_server = new System.Windows.Forms.Button();
            this.pb_map = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.记录当前AGV位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_car)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_car);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 80);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(717, 395);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "车辆列表";
            // 
            // dgv_car
            // 
            this.dgv_car.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_car.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_car.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_car.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_car.Location = new System.Drawing.Point(2, 16);
            this.dgv_car.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_car.Name = "dgv_car";
            this.dgv_car.ReadOnly = true;
            this.dgv_car.RowHeadersVisible = false;
            this.dgv_car.RowHeadersWidth = 51;
            this.dgv_car.RowTemplate.Height = 27;
            this.dgv_car.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_car.Size = new System.Drawing.Size(713, 377);
            this.dgv_car.TabIndex = 0;
            this.dgv_car.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_car_DataError);
            this.dgv_car.SelectionChanged += new System.EventHandler(this.dgv_car_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载地图ToolStripMenuItem,
            this.清空点位ToolStripMenuItem,
            this.还原缩放ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.记录当前AGV位置ToolStripMenuItem,
            this.自动导航ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 142);
            // 
            // 加载地图ToolStripMenuItem
            // 
            this.加载地图ToolStripMenuItem.Name = "加载地图ToolStripMenuItem";
            this.加载地图ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.加载地图ToolStripMenuItem.Text = "加载地图";
            this.加载地图ToolStripMenuItem.Click += new System.EventHandler(this.加载地图ToolStripMenuItem_Click);
            // 
            // 清空点位ToolStripMenuItem
            // 
            this.清空点位ToolStripMenuItem.Name = "清空点位ToolStripMenuItem";
            this.清空点位ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空点位ToolStripMenuItem.Text = "清空点位";
            this.清空点位ToolStripMenuItem.Click += new System.EventHandler(this.清空点位ToolStripMenuItem_Click);
            // 
            // 还原缩放ToolStripMenuItem
            // 
            this.还原缩放ToolStripMenuItem.Name = "还原缩放ToolStripMenuItem";
            this.还原缩放ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.还原缩放ToolStripMenuItem.Text = "还原缩放";
            this.还原缩放ToolStripMenuItem.Click += new System.EventHandler(this.还原缩放ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // 自动导航ToolStripMenuItem
            // 
            this.自动导航ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导航一次ToolStripMenuItem,
            this.重复导航ToolStripMenuItem,
            this.配置导航ToolStripMenuItem});
            this.自动导航ToolStripMenuItem.Name = "自动导航ToolStripMenuItem";
            this.自动导航ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.自动导航ToolStripMenuItem.Text = "自动导航";
            // 
            // 导航一次ToolStripMenuItem
            // 
            this.导航一次ToolStripMenuItem.Name = "导航一次ToolStripMenuItem";
            this.导航一次ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导航一次ToolStripMenuItem.Text = "导航一次";
            this.导航一次ToolStripMenuItem.Click += new System.EventHandler(this.执行一轮ToolStripMenuItem_Click);
            // 
            // 重复导航ToolStripMenuItem
            // 
            this.重复导航ToolStripMenuItem.Name = "重复导航ToolStripMenuItem";
            this.重复导航ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.重复导航ToolStripMenuItem.Text = "重复导航";
            this.重复导航ToolStripMenuItem.Click += new System.EventHandler(this.重复执行ToolStripMenuItem_Click);
            // 
            // 配置导航ToolStripMenuItem
            // 
            this.配置导航ToolStripMenuItem.Name = "配置导航ToolStripMenuItem";
            this.配置导航ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.配置导航ToolStripMenuItem.Text = "配置导航";
            this.配置导航ToolStripMenuItem.Click += new System.EventHandler(this.配置批量点位ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 80);
            this.panel1.TabIndex = 6;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.flp);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(717, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(275, 80);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "位置列表";
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.ContextMenuStrip = this.contextMenuStrip2;
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.Location = new System.Drawing.Point(2, 16);
            this.flp.Margin = new System.Windows.Forms.Padding(2);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(271, 62);
            this.flp.TabIndex = 0;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存点位数据ToolStripMenuItem,
            this.加载点位数据ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.批量执行点位ToolStripMenuItem,
            this.配置批量点位ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(149, 98);
            // 
            // 保存点位数据ToolStripMenuItem
            // 
            this.保存点位数据ToolStripMenuItem.Name = "保存点位数据ToolStripMenuItem";
            this.保存点位数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存点位数据ToolStripMenuItem.Text = "保存点位数据";
            this.保存点位数据ToolStripMenuItem.Click += new System.EventHandler(this.保存点位数据ToolStripMenuItem_Click);
            // 
            // 加载点位数据ToolStripMenuItem
            // 
            this.加载点位数据ToolStripMenuItem.Name = "加载点位数据ToolStripMenuItem";
            this.加载点位数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载点位数据ToolStripMenuItem.Text = "加载点位数据";
            this.加载点位数据ToolStripMenuItem.Click += new System.EventHandler(this.加载点位数据ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // 批量执行点位ToolStripMenuItem
            // 
            this.批量执行点位ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.执行一轮ToolStripMenuItem,
            this.重复执行ToolStripMenuItem});
            this.批量执行点位ToolStripMenuItem.Name = "批量执行点位ToolStripMenuItem";
            this.批量执行点位ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.批量执行点位ToolStripMenuItem.Text = "批量执行点位";
            // 
            // 执行一轮ToolStripMenuItem
            // 
            this.执行一轮ToolStripMenuItem.Name = "执行一轮ToolStripMenuItem";
            this.执行一轮ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.执行一轮ToolStripMenuItem.Text = "执行一轮";
            this.执行一轮ToolStripMenuItem.Click += new System.EventHandler(this.执行一轮ToolStripMenuItem_Click);
            // 
            // 重复执行ToolStripMenuItem
            // 
            this.重复执行ToolStripMenuItem.Name = "重复执行ToolStripMenuItem";
            this.重复执行ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.重复执行ToolStripMenuItem.Text = "重复执行";
            this.重复执行ToolStripMenuItem.Click += new System.EventHandler(this.重复执行ToolStripMenuItem_Click);
            // 
            // 配置批量点位ToolStripMenuItem
            // 
            this.配置批量点位ToolStripMenuItem.Name = "配置批量点位ToolStripMenuItem";
            this.配置批量点位ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.配置批量点位ToolStripMenuItem.Text = "配置批量点位";
            this.配置批量点位ToolStripMenuItem.Click += new System.EventHandler(this.配置批量点位ToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_cancel_move);
            this.groupBox3.Controls.Add(this.btn_close);
            this.groupBox3.Controls.Add(this.btn_shutdown);
            this.groupBox3.Controls.Add(this.btn_setting);
            this.groupBox3.Controls.Add(this.tb_turn);
            this.groupBox3.Controls.Add(this.tb_speed);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btn_stop);
            this.groupBox3.Controls.Add(this.btn_right);
            this.groupBox3.Controls.Add(this.btn_backward);
            this.groupBox3.Controls.Add(this.btn_left);
            this.groupBox3.Controls.Add(this.btn_forward);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(295, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(422, 80);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AGV控制";
            // 
            // btn_cancel_move
            // 
            this.btn_cancel_move.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel_move.ForeColor = System.Drawing.Color.Orange;
            this.btn_cancel_move.Location = new System.Drawing.Point(140, 29);
            this.btn_cancel_move.Margin = new System.Windows.Forms.Padding(2);
            this.btn_cancel_move.Name = "btn_cancel_move";
            this.btn_cancel_move.Size = new System.Drawing.Size(46, 44);
            this.btn_cancel_move.TabIndex = 12;
            this.btn_cancel_move.Text = "取消导航";
            this.btn_cancel_move.UseVisualStyleBackColor = true;
            this.btn_cancel_move.Click += new System.EventHandler(this.btn_cancel_move_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_close.ForeColor = System.Drawing.Color.Black;
            this.btn_close.Location = new System.Drawing.Point(356, 47);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(62, 29);
            this.btn_close.TabIndex = 11;
            this.btn_close.Text = "关闭设备";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_shutdown
            // 
            this.btn_shutdown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_shutdown.ForeColor = System.Drawing.Color.Black;
            this.btn_shutdown.Location = new System.Drawing.Point(356, 14);
            this.btn_shutdown.Margin = new System.Windows.Forms.Padding(2);
            this.btn_shutdown.Name = "btn_shutdown";
            this.btn_shutdown.Size = new System.Drawing.Size(62, 29);
            this.btn_shutdown.TabIndex = 10;
            this.btn_shutdown.Text = "关机";
            this.btn_shutdown.UseVisualStyleBackColor = true;
            this.btn_shutdown.Click += new System.EventHandler(this.btn_shutdown_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_setting.ForeColor = System.Drawing.Color.Black;
            this.btn_setting.Location = new System.Drawing.Point(298, 29);
            this.btn_setting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(45, 44);
            this.btn_setting.TabIndex = 9;
            this.btn_setting.Text = "设置";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // tb_turn
            // 
            this.tb_turn.Location = new System.Drawing.Point(245, 53);
            this.tb_turn.Margin = new System.Windows.Forms.Padding(2);
            this.tb_turn.Name = "tb_turn";
            this.tb_turn.Size = new System.Drawing.Size(50, 21);
            this.tb_turn.TabIndex = 8;
            this.tb_turn.Text = "0";
            this.tb_turn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_turn.TextChanged += new System.EventHandler(this.tb_turn_TextChanged);
            // 
            // tb_speed
            // 
            this.tb_speed.Location = new System.Drawing.Point(245, 28);
            this.tb_speed.Margin = new System.Windows.Forms.Padding(2);
            this.tb_speed.Name = "tb_speed";
            this.tb_speed.Size = new System.Drawing.Size(50, 21);
            this.tb_speed.TabIndex = 7;
            this.tb_speed.Text = "0";
            this.tb_speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_speed.TextChanged += new System.EventHandler(this.tb_speed_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "转向速度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "行驶速度";
            // 
            // btn_stop
            // 
            this.btn_stop.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stop.ForeColor = System.Drawing.Color.Red;
            this.btn_stop.Location = new System.Drawing.Point(84, 29);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(52, 44);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_right
            // 
            this.btn_right.Location = new System.Drawing.Point(58, 38);
            this.btn_right.Margin = new System.Windows.Forms.Padding(2);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(22, 24);
            this.btn_right.TabIndex = 3;
            this.btn_right.Text = "→";
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // btn_backward
            // 
            this.btn_backward.Location = new System.Drawing.Point(32, 56);
            this.btn_backward.Margin = new System.Windows.Forms.Padding(2);
            this.btn_backward.Name = "btn_backward";
            this.btn_backward.Size = new System.Drawing.Size(22, 24);
            this.btn_backward.TabIndex = 2;
            this.btn_backward.Text = "↓";
            this.btn_backward.UseVisualStyleBackColor = true;
            this.btn_backward.Click += new System.EventHandler(this.btn_backward_Click);
            // 
            // btn_left
            // 
            this.btn_left.Location = new System.Drawing.Point(4, 38);
            this.btn_left.Margin = new System.Windows.Forms.Padding(2);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(22, 24);
            this.btn_left.TabIndex = 1;
            this.btn_left.Text = "←";
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_forward
            // 
            this.btn_forward.Location = new System.Drawing.Point(32, 17);
            this.btn_forward.Margin = new System.Windows.Forms.Padding(2);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(22, 24);
            this.btn_forward.TabIndex = 0;
            this.btn_forward.Text = "↑";
            this.btn_forward.UseVisualStyleBackColor = true;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(295, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务端设置";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.tb_ip);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.tb_port);
            this.flowLayoutPanel2.Controls.Add(this.lb_error);
            this.flowLayoutPanel2.Controls.Add(this.btn_start_server);
            this.flowLayoutPanel2.Controls.Add(this.btn_close_server);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 16);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(291, 62);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tb_ip
            // 
            this.tb_ip.Location = new System.Drawing.Point(59, 2);
            this.tb_ip.Margin = new System.Windows.Forms.Padding(2);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(84, 21);
            this.tb_ip.TabIndex = 1;
            this.tb_ip.Text = "192.168.100.106";
            this.tb_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务端口";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(204, 2);
            this.tb_port.Margin = new System.Windows.Forms.Padding(2);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(84, 21);
            this.tb_port.TabIndex = 3;
            this.tb_port.Text = "22222";
            this.tb_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_error
            // 
            this.lb_error.ForeColor = System.Drawing.Color.Red;
            this.lb_error.Location = new System.Drawing.Point(2, 25);
            this.lb_error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_error.Name = "lb_error";
            this.lb_error.Size = new System.Drawing.Size(140, 20);
            this.lb_error.TabIndex = 6;
            this.lb_error.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btn_start_server
            // 
            this.btn_start_server.Location = new System.Drawing.Point(146, 27);
            this.btn_start_server.Margin = new System.Windows.Forms.Padding(2);
            this.btn_start_server.Name = "btn_start_server";
            this.btn_start_server.Size = new System.Drawing.Size(68, 24);
            this.btn_start_server.TabIndex = 4;
            this.btn_start_server.Text = "启动服务";
            this.btn_start_server.UseVisualStyleBackColor = true;
            this.btn_start_server.Click += new System.EventHandler(this.btn_start_server_Click);
            // 
            // btn_close_server
            // 
            this.btn_close_server.Enabled = false;
            this.btn_close_server.Location = new System.Drawing.Point(218, 27);
            this.btn_close_server.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close_server.Name = "btn_close_server";
            this.btn_close_server.Size = new System.Drawing.Size(68, 24);
            this.btn_close_server.TabIndex = 5;
            this.btn_close_server.Text = "关闭服务";
            this.btn_close_server.UseVisualStyleBackColor = true;
            this.btn_close_server.Click += new System.EventHandler(this.btn_close_server_Click);
            // 
            // pb_map
            // 
            this.pb_map.ContextMenuStrip = this.contextMenuStrip1;
            this.pb_map.Location = new System.Drawing.Point(721, 80);
            this.pb_map.Margin = new System.Windows.Forms.Padding(2);
            this.pb_map.Name = "pb_map";
            this.pb_map.Size = new System.Drawing.Size(269, 395);
            this.pb_map.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_map.TabIndex = 7;
            this.pb_map.TabStop = false;
            this.pb_map.LocationChanged += new System.EventHandler(this.pb_map_LocationChanged);
            this.pb_map.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDoubleClick);
            this.pb_map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseDown);
            this.pb_map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseMove);
            this.pb_map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_map_MouseUp);
            // 
            // groupBox4
            // 
            this.groupBox4.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(717, 80);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 395);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "地图";
            // 
            // 记录当前AGV位置ToolStripMenuItem
            // 
            this.记录当前AGV位置ToolStripMenuItem.Name = "记录当前AGV位置ToolStripMenuItem";
            this.记录当前AGV位置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.记录当前AGV位置ToolStripMenuItem.Text = "记录当前AGV位置";
            this.记录当前AGV位置ToolStripMenuItem.Click += new System.EventHandler(this.记录当前AGV位置ToolStripMenuItem_Click);
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pb_map);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(992, 475);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_car)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_car;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.TextBox tb_turn;
        private System.Windows.Forms.TextBox tb_speed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Button btn_backward;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label lb_error;
        private System.Windows.Forms.Button btn_start_server;
        private System.Windows.Forms.Button btn_close_server;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_shutdown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 加载地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空点位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还原缩放ToolStripMenuItem;
        private System.Windows.Forms.Button btn_cancel_move;
        private System.Windows.Forms.PictureBox pb_map;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 保存点位数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载点位数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 批量执行点位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置批量点位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 执行一轮ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重复执行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 自动导航ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导航一次ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重复导航ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置导航ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记录当前AGV位置ToolStripMenuItem;
    }
}
