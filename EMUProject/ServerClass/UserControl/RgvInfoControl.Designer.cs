
namespace Project.ServerClass
{
    partial class RgvInfoControl
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
            System.Windows.Forms.AGaugeRange aGaugeRange1 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange2 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange3 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange4 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange5 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange6 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange7 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange8 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange9 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange10 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange11 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange12 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange13 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange14 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange15 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange16 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange17 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange18 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange19 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange20 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange21 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange22 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange23 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange24 = new System.Windows.Forms.AGaugeRange();
            System.Windows.Forms.AGaugeRange aGaugeRange25 = new System.Windows.Forms.AGaugeRange();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_speed = new System.Windows.Forms.TextBox();
            this.aGauge_speed = new System.Windows.Forms.AGauge();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_power = new System.Windows.Forms.TextBox();
            this.aGauge_power = new System.Windows.Forms.AGauge();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_current = new System.Windows.Forms.TextBox();
            this.aGauge_current = new System.Windows.Forms.AGauge();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_tempture = new System.Windows.Forms.TextBox();
            this.aGauge_tempture = new System.Windows.Forms.AGauge();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tb_distacnce = new System.Windows.Forms.TextBox();
            this.aGauge_distacnce = new System.Windows.Forms.AGauge();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.tb_speed);
            this.groupBox1.Controls.Add(this.aGauge_speed);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "9999";
            this.groupBox1.Text = "Rgv运行速度";
            // 
            // tb_speed
            // 
            this.tb_speed.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tb_speed.BackColor = System.Drawing.Color.White;
            this.tb_speed.Location = new System.Drawing.Point(84, 246);
            this.tb_speed.Name = "tb_speed";
            this.tb_speed.ReadOnly = true;
            this.tb_speed.Size = new System.Drawing.Size(100, 25);
            this.tb_speed.TabIndex = 1;
            this.tb_speed.Tag = "9999";
            this.tb_speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_speed.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // aGauge_speed
            // 
            this.aGauge_speed.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge_speed.BaseArcRadius = 80;
            this.aGauge_speed.BaseArcStart = 135;
            this.aGauge_speed.BaseArcSweep = 270;
            this.aGauge_speed.BaseArcWidth = 2;
            this.aGauge_speed.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge_speed.GaugeAutoSize = false;
            aGaugeRange1.Color = System.Drawing.Color.Orange;
            aGaugeRange1.EndValue = 1600F;
            aGaugeRange1.InnerRadius = 70;
            aGaugeRange1.InRange = false;
            aGaugeRange1.Name = "speed_orange";
            aGaugeRange1.OuterRadius = 80;
            aGaugeRange1.StartValue = 1200F;
            aGaugeRange2.Color = System.Drawing.Color.Red;
            aGaugeRange2.EndValue = 2000F;
            aGaugeRange2.InnerRadius = 70;
            aGaugeRange2.InRange = false;
            aGaugeRange2.Name = "speed_red";
            aGaugeRange2.OuterRadius = 80;
            aGaugeRange2.StartValue = 1600F;
            this.aGauge_speed.GaugeRanges.Add(aGaugeRange1);
            this.aGauge_speed.GaugeRanges.Add(aGaugeRange2);
            this.aGauge_speed.Location = new System.Drawing.Point(3, 21);
            this.aGauge_speed.MaxValue = 2000F;
            this.aGauge_speed.MinValue = 0F;
            this.aGauge_speed.Name = "aGauge_speed";
            this.aGauge_speed.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge_speed.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge_speed.NeedleRadius = 80;
            this.aGauge_speed.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.aGauge_speed.NeedleWidth = 2;
            this.aGauge_speed.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge_speed.ScaleLinesInterInnerRadius = 73;
            this.aGauge_speed.ScaleLinesInterOuterRadius = 80;
            this.aGauge_speed.ScaleLinesInterWidth = 1;
            this.aGauge_speed.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge_speed.ScaleLinesMajorInnerRadius = 70;
            this.aGauge_speed.ScaleLinesMajorOuterRadius = 80;
            this.aGauge_speed.ScaleLinesMajorStepValue = 200F;
            this.aGauge_speed.ScaleLinesMajorWidth = 2;
            this.aGauge_speed.ScaleLinesMinorColor = System.Drawing.Color.Lime;
            this.aGauge_speed.ScaleLinesMinorInnerRadius = 75;
            this.aGauge_speed.ScaleLinesMinorOuterRadius = 80;
            this.aGauge_speed.ScaleLinesMinorTicks = 9;
            this.aGauge_speed.ScaleLinesMinorWidth = 1;
            this.aGauge_speed.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge_speed.ScaleNumbersFormat = null;
            this.aGauge_speed.ScaleNumbersRadius = 95;
            this.aGauge_speed.ScaleNumbersRotation = 0;
            this.aGauge_speed.ScaleNumbersStartScaleLine = 0;
            this.aGauge_speed.ScaleNumbersStepScaleLines = 1;
            this.aGauge_speed.Size = new System.Drawing.Size(274, 219);
            this.aGauge_speed.TabIndex = 0;
            this.aGauge_speed.Value = 0F;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_power);
            this.groupBox2.Controls.Add(this.aGauge_power);
            this.groupBox2.Location = new System.Drawing.Point(283, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 280);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "9999";
            this.groupBox2.Text = "Rgv电池电量";
            // 
            // tb_power
            // 
            this.tb_power.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tb_power.BackColor = System.Drawing.Color.White;
            this.tb_power.Location = new System.Drawing.Point(84, 246);
            this.tb_power.Name = "tb_power";
            this.tb_power.ReadOnly = true;
            this.tb_power.Size = new System.Drawing.Size(100, 25);
            this.tb_power.TabIndex = 1;
            this.tb_power.Tag = "9999";
            this.tb_power.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_power.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // aGauge_power
            // 
            this.aGauge_power.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge_power.BaseArcRadius = 80;
            this.aGauge_power.BaseArcStart = 135;
            this.aGauge_power.BaseArcSweep = 270;
            this.aGauge_power.BaseArcWidth = 2;
            this.aGauge_power.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge_power.GaugeAutoSize = false;
            aGaugeRange3.Color = System.Drawing.Color.Orange;
            aGaugeRange3.EndValue = 30F;
            aGaugeRange3.InnerRadius = 70;
            aGaugeRange3.InRange = false;
            aGaugeRange3.Name = "power_orange";
            aGaugeRange3.OuterRadius = 80;
            aGaugeRange3.StartValue = 10F;
            aGaugeRange4.Color = System.Drawing.Color.Red;
            aGaugeRange4.EndValue = 10F;
            aGaugeRange4.InnerRadius = 70;
            aGaugeRange4.InRange = false;
            aGaugeRange4.Name = "power_red";
            aGaugeRange4.OuterRadius = 80;
            aGaugeRange4.StartValue = 0F;
            this.aGauge_power.GaugeRanges.Add(aGaugeRange3);
            this.aGauge_power.GaugeRanges.Add(aGaugeRange4);
            this.aGauge_power.Location = new System.Drawing.Point(3, 21);
            this.aGauge_power.MaxValue = 100F;
            this.aGauge_power.MinValue = 0F;
            this.aGauge_power.Name = "aGauge_power";
            this.aGauge_power.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge_power.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge_power.NeedleRadius = 80;
            this.aGauge_power.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.aGauge_power.NeedleWidth = 2;
            this.aGauge_power.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge_power.ScaleLinesInterInnerRadius = 73;
            this.aGauge_power.ScaleLinesInterOuterRadius = 80;
            this.aGauge_power.ScaleLinesInterWidth = 1;
            this.aGauge_power.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge_power.ScaleLinesMajorInnerRadius = 70;
            this.aGauge_power.ScaleLinesMajorOuterRadius = 80;
            this.aGauge_power.ScaleLinesMajorStepValue = 10F;
            this.aGauge_power.ScaleLinesMajorWidth = 2;
            this.aGauge_power.ScaleLinesMinorColor = System.Drawing.Color.Lime;
            this.aGauge_power.ScaleLinesMinorInnerRadius = 75;
            this.aGauge_power.ScaleLinesMinorOuterRadius = 80;
            this.aGauge_power.ScaleLinesMinorTicks = 9;
            this.aGauge_power.ScaleLinesMinorWidth = 1;
            this.aGauge_power.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge_power.ScaleNumbersFormat = null;
            this.aGauge_power.ScaleNumbersRadius = 95;
            this.aGauge_power.ScaleNumbersRotation = 0;
            this.aGauge_power.ScaleNumbersStartScaleLine = 0;
            this.aGauge_power.ScaleNumbersStepScaleLines = 1;
            this.aGauge_power.Size = new System.Drawing.Size(274, 219);
            this.aGauge_power.TabIndex = 0;
            this.aGauge_power.Value = 0F;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_current);
            this.groupBox3.Controls.Add(this.aGauge_current);
            this.groupBox3.Location = new System.Drawing.Point(569, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 280);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "9999";
            this.groupBox3.Text = "Rgv电池电流";
            // 
            // tb_current
            // 
            this.tb_current.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tb_current.BackColor = System.Drawing.Color.White;
            this.tb_current.Location = new System.Drawing.Point(84, 246);
            this.tb_current.Name = "tb_current";
            this.tb_current.ReadOnly = true;
            this.tb_current.Size = new System.Drawing.Size(100, 25);
            this.tb_current.TabIndex = 1;
            this.tb_current.Tag = "9999";
            this.tb_current.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_current.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // aGauge_current
            // 
            this.aGauge_current.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge_current.BaseArcRadius = 80;
            this.aGauge_current.BaseArcStart = 135;
            this.aGauge_current.BaseArcSweep = 270;
            this.aGauge_current.BaseArcWidth = 2;
            this.aGauge_current.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge_current.GaugeAutoSize = false;
            aGaugeRange5.Color = System.Drawing.Color.Orange;
            aGaugeRange5.EndValue = 80F;
            aGaugeRange5.InnerRadius = 70;
            aGaugeRange5.InRange = false;
            aGaugeRange5.Name = "current_orange";
            aGaugeRange5.OuterRadius = 80;
            aGaugeRange5.StartValue = 50F;
            aGaugeRange6.Color = System.Drawing.Color.Red;
            aGaugeRange6.EndValue = 100F;
            aGaugeRange6.InnerRadius = 70;
            aGaugeRange6.InRange = false;
            aGaugeRange6.Name = "current_red";
            aGaugeRange6.OuterRadius = 80;
            aGaugeRange6.StartValue = 80F;
            this.aGauge_current.GaugeRanges.Add(aGaugeRange5);
            this.aGauge_current.GaugeRanges.Add(aGaugeRange6);
            this.aGauge_current.Location = new System.Drawing.Point(3, 21);
            this.aGauge_current.MaxValue = 100F;
            this.aGauge_current.MinValue = 0F;
            this.aGauge_current.Name = "aGauge_current";
            this.aGauge_current.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge_current.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge_current.NeedleRadius = 80;
            this.aGauge_current.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.aGauge_current.NeedleWidth = 2;
            this.aGauge_current.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge_current.ScaleLinesInterInnerRadius = 73;
            this.aGauge_current.ScaleLinesInterOuterRadius = 80;
            this.aGauge_current.ScaleLinesInterWidth = 1;
            this.aGauge_current.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge_current.ScaleLinesMajorInnerRadius = 70;
            this.aGauge_current.ScaleLinesMajorOuterRadius = 80;
            this.aGauge_current.ScaleLinesMajorStepValue = 10F;
            this.aGauge_current.ScaleLinesMajorWidth = 2;
            this.aGauge_current.ScaleLinesMinorColor = System.Drawing.Color.Lime;
            this.aGauge_current.ScaleLinesMinorInnerRadius = 75;
            this.aGauge_current.ScaleLinesMinorOuterRadius = 80;
            this.aGauge_current.ScaleLinesMinorTicks = 9;
            this.aGauge_current.ScaleLinesMinorWidth = 1;
            this.aGauge_current.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge_current.ScaleNumbersFormat = null;
            this.aGauge_current.ScaleNumbersRadius = 95;
            this.aGauge_current.ScaleNumbersRotation = 0;
            this.aGauge_current.ScaleNumbersStartScaleLine = 0;
            this.aGauge_current.ScaleNumbersStepScaleLines = 1;
            this.aGauge_current.Size = new System.Drawing.Size(274, 219);
            this.aGauge_current.TabIndex = 0;
            this.aGauge_current.Value = 0F;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_tempture);
            this.groupBox4.Controls.Add(this.aGauge_tempture);
            this.groupBox4.Location = new System.Drawing.Point(855, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 280);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "9999";
            this.groupBox4.Text = "Rgv电池温度";
            // 
            // tb_tempture
            // 
            this.tb_tempture.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tb_tempture.BackColor = System.Drawing.Color.White;
            this.tb_tempture.Location = new System.Drawing.Point(84, 246);
            this.tb_tempture.Name = "tb_tempture";
            this.tb_tempture.ReadOnly = true;
            this.tb_tempture.Size = new System.Drawing.Size(100, 25);
            this.tb_tempture.TabIndex = 1;
            this.tb_tempture.Tag = "9999";
            this.tb_tempture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_tempture.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // aGauge_tempture
            // 
            this.aGauge_tempture.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge_tempture.BaseArcRadius = 80;
            this.aGauge_tempture.BaseArcStart = 135;
            this.aGauge_tempture.BaseArcSweep = 270;
            this.aGauge_tempture.BaseArcWidth = 2;
            this.aGauge_tempture.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge_tempture.GaugeAutoSize = false;
            aGaugeRange7.Color = System.Drawing.Color.Gold;
            aGaugeRange7.EndValue = 0F;
            aGaugeRange7.InnerRadius = 70;
            aGaugeRange7.InRange = false;
            aGaugeRange7.Name = "tempture_down";
            aGaugeRange7.OuterRadius = 80;
            aGaugeRange7.StartValue = -100F;
            aGaugeRange8.Color = System.Drawing.Color.Gold;
            aGaugeRange8.EndValue = 400F;
            aGaugeRange8.InnerRadius = 70;
            aGaugeRange8.InRange = false;
            aGaugeRange8.Name = "tempture_up";
            aGaugeRange8.OuterRadius = 80;
            aGaugeRange8.StartValue = 70F;
            this.aGauge_tempture.GaugeRanges.Add(aGaugeRange7);
            this.aGauge_tempture.GaugeRanges.Add(aGaugeRange8);
            this.aGauge_tempture.Location = new System.Drawing.Point(3, 21);
            this.aGauge_tempture.MaxValue = 400F;
            this.aGauge_tempture.MinValue = -100F;
            this.aGauge_tempture.Name = "aGauge_tempture";
            this.aGauge_tempture.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge_tempture.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge_tempture.NeedleRadius = 80;
            this.aGauge_tempture.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.aGauge_tempture.NeedleWidth = 2;
            this.aGauge_tempture.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge_tempture.ScaleLinesInterInnerRadius = 73;
            this.aGauge_tempture.ScaleLinesInterOuterRadius = 80;
            this.aGauge_tempture.ScaleLinesInterWidth = 1;
            this.aGauge_tempture.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge_tempture.ScaleLinesMajorInnerRadius = 70;
            this.aGauge_tempture.ScaleLinesMajorOuterRadius = 80;
            this.aGauge_tempture.ScaleLinesMajorStepValue = 50F;
            this.aGauge_tempture.ScaleLinesMajorWidth = 2;
            this.aGauge_tempture.ScaleLinesMinorColor = System.Drawing.Color.Lime;
            this.aGauge_tempture.ScaleLinesMinorInnerRadius = 75;
            this.aGauge_tempture.ScaleLinesMinorOuterRadius = 80;
            this.aGauge_tempture.ScaleLinesMinorTicks = 9;
            this.aGauge_tempture.ScaleLinesMinorWidth = 1;
            this.aGauge_tempture.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge_tempture.ScaleNumbersFormat = null;
            this.aGauge_tempture.ScaleNumbersRadius = 95;
            this.aGauge_tempture.ScaleNumbersRotation = 0;
            this.aGauge_tempture.ScaleNumbersStartScaleLine = 0;
            this.aGauge_tempture.ScaleNumbersStepScaleLines = 1;
            this.aGauge_tempture.Size = new System.Drawing.Size(274, 219);
            this.aGauge_tempture.TabIndex = 0;
            this.aGauge_tempture.Value = 0F;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tb_distacnce);
            this.groupBox5.Controls.Add(this.aGauge_distacnce);
            this.groupBox5.Location = new System.Drawing.Point(1141, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 280);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "9999";
            this.groupBox5.Text = "Rgv当前位置";
            // 
            // tb_distacnce
            // 
            this.tb_distacnce.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tb_distacnce.BackColor = System.Drawing.Color.White;
            this.tb_distacnce.Location = new System.Drawing.Point(84, 246);
            this.tb_distacnce.Name = "tb_distacnce";
            this.tb_distacnce.ReadOnly = true;
            this.tb_distacnce.Size = new System.Drawing.Size(100, 25);
            this.tb_distacnce.TabIndex = 1;
            this.tb_distacnce.Tag = "9999";
            this.tb_distacnce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_distacnce.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // aGauge_distacnce
            // 
            this.aGauge_distacnce.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge_distacnce.BaseArcRadius = 80;
            this.aGauge_distacnce.BaseArcStart = 135;
            this.aGauge_distacnce.BaseArcSweep = 270;
            this.aGauge_distacnce.BaseArcWidth = 2;
            this.aGauge_distacnce.Dock = System.Windows.Forms.DockStyle.Top;
            this.aGauge_distacnce.GaugeAutoSize = false;
            aGaugeRange9.Color = System.Drawing.Color.Blue;
            aGaugeRange9.EndValue = 5F;
            aGaugeRange9.InnerRadius = 70;
            aGaugeRange9.InRange = false;
            aGaugeRange9.Name = "distacnce";
            aGaugeRange9.OuterRadius = 80;
            aGaugeRange9.StartValue = 0F;
            aGaugeRange10.Color = System.Drawing.Color.Empty;
            aGaugeRange10.EndValue = 0F;
            aGaugeRange10.InnerRadius = 70;
            aGaugeRange10.InRange = false;
            aGaugeRange10.Name = "GaugeRange1";
            aGaugeRange10.OuterRadius = 80;
            aGaugeRange10.StartValue = 0F;
            aGaugeRange11.Color = System.Drawing.Color.Empty;
            aGaugeRange11.EndValue = 0F;
            aGaugeRange11.InnerRadius = 70;
            aGaugeRange11.InRange = false;
            aGaugeRange11.Name = "GaugeRange2";
            aGaugeRange11.OuterRadius = 80;
            aGaugeRange11.StartValue = 0F;
            aGaugeRange12.Color = System.Drawing.Color.Empty;
            aGaugeRange12.EndValue = 0F;
            aGaugeRange12.InnerRadius = 70;
            aGaugeRange12.InRange = false;
            aGaugeRange12.Name = "GaugeRange3";
            aGaugeRange12.OuterRadius = 80;
            aGaugeRange12.StartValue = 0F;
            aGaugeRange13.Color = System.Drawing.Color.Empty;
            aGaugeRange13.EndValue = 0F;
            aGaugeRange13.InnerRadius = 70;
            aGaugeRange13.InRange = false;
            aGaugeRange13.Name = "GaugeRange4";
            aGaugeRange13.OuterRadius = 80;
            aGaugeRange13.StartValue = 0F;
            aGaugeRange14.Color = System.Drawing.Color.Empty;
            aGaugeRange14.EndValue = 0F;
            aGaugeRange14.InnerRadius = 70;
            aGaugeRange14.InRange = false;
            aGaugeRange14.Name = "GaugeRange5";
            aGaugeRange14.OuterRadius = 80;
            aGaugeRange14.StartValue = 0F;
            aGaugeRange15.Color = System.Drawing.Color.Empty;
            aGaugeRange15.EndValue = 0F;
            aGaugeRange15.InnerRadius = 70;
            aGaugeRange15.InRange = false;
            aGaugeRange15.Name = "GaugeRange6";
            aGaugeRange15.OuterRadius = 80;
            aGaugeRange15.StartValue = 0F;
            aGaugeRange16.Color = System.Drawing.Color.Empty;
            aGaugeRange16.EndValue = 0F;
            aGaugeRange16.InnerRadius = 70;
            aGaugeRange16.InRange = false;
            aGaugeRange16.Name = "GaugeRange7";
            aGaugeRange16.OuterRadius = 80;
            aGaugeRange16.StartValue = 0F;
            aGaugeRange17.Color = System.Drawing.Color.Empty;
            aGaugeRange17.EndValue = 0F;
            aGaugeRange17.InnerRadius = 70;
            aGaugeRange17.InRange = false;
            aGaugeRange17.Name = "GaugeRange8";
            aGaugeRange17.OuterRadius = 80;
            aGaugeRange17.StartValue = 0F;
            aGaugeRange18.Color = System.Drawing.Color.Empty;
            aGaugeRange18.EndValue = 0F;
            aGaugeRange18.InnerRadius = 70;
            aGaugeRange18.InRange = false;
            aGaugeRange18.Name = "GaugeRange9";
            aGaugeRange18.OuterRadius = 80;
            aGaugeRange18.StartValue = 0F;
            aGaugeRange19.Color = System.Drawing.Color.Empty;
            aGaugeRange19.EndValue = 0F;
            aGaugeRange19.InnerRadius = 70;
            aGaugeRange19.InRange = false;
            aGaugeRange19.Name = "GaugeRange10";
            aGaugeRange19.OuterRadius = 80;
            aGaugeRange19.StartValue = 0F;
            aGaugeRange20.Color = System.Drawing.Color.Empty;
            aGaugeRange20.EndValue = 0F;
            aGaugeRange20.InnerRadius = 70;
            aGaugeRange20.InRange = false;
            aGaugeRange20.Name = "GaugeRange11";
            aGaugeRange20.OuterRadius = 80;
            aGaugeRange20.StartValue = 0F;
            aGaugeRange21.Color = System.Drawing.Color.Empty;
            aGaugeRange21.EndValue = 0F;
            aGaugeRange21.InnerRadius = 70;
            aGaugeRange21.InRange = false;
            aGaugeRange21.Name = "GaugeRange12";
            aGaugeRange21.OuterRadius = 80;
            aGaugeRange21.StartValue = 0F;
            aGaugeRange22.Color = System.Drawing.Color.Empty;
            aGaugeRange22.EndValue = 0F;
            aGaugeRange22.InnerRadius = 70;
            aGaugeRange22.InRange = false;
            aGaugeRange22.Name = "GaugeRange13";
            aGaugeRange22.OuterRadius = 80;
            aGaugeRange22.StartValue = 0F;
            aGaugeRange23.Color = System.Drawing.Color.Empty;
            aGaugeRange23.EndValue = 0F;
            aGaugeRange23.InnerRadius = 70;
            aGaugeRange23.InRange = false;
            aGaugeRange23.Name = "GaugeRange14";
            aGaugeRange23.OuterRadius = 80;
            aGaugeRange23.StartValue = 0F;
            aGaugeRange24.Color = System.Drawing.Color.Empty;
            aGaugeRange24.EndValue = 0F;
            aGaugeRange24.InnerRadius = 70;
            aGaugeRange24.InRange = false;
            aGaugeRange24.Name = "GaugeRange15";
            aGaugeRange24.OuterRadius = 80;
            aGaugeRange24.StartValue = 0F;
            aGaugeRange25.Color = System.Drawing.Color.Empty;
            aGaugeRange25.EndValue = 0F;
            aGaugeRange25.InnerRadius = 70;
            aGaugeRange25.InRange = false;
            aGaugeRange25.Name = "GaugeRange16";
            aGaugeRange25.OuterRadius = 80;
            aGaugeRange25.StartValue = 0F;
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange9);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange10);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange11);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange12);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange13);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange14);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange15);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange16);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange17);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange18);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange19);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange20);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange21);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange22);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange23);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange24);
            this.aGauge_distacnce.GaugeRanges.Add(aGaugeRange25);
            this.aGauge_distacnce.Location = new System.Drawing.Point(3, 21);
            this.aGauge_distacnce.MaxValue = 300F;
            this.aGauge_distacnce.MinValue = 0F;
            this.aGauge_distacnce.Name = "aGauge_distacnce";
            this.aGauge_distacnce.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.aGauge_distacnce.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge_distacnce.NeedleRadius = 80;
            this.aGauge_distacnce.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.aGauge_distacnce.NeedleWidth = 2;
            this.aGauge_distacnce.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge_distacnce.ScaleLinesInterInnerRadius = 73;
            this.aGauge_distacnce.ScaleLinesInterOuterRadius = 80;
            this.aGauge_distacnce.ScaleLinesInterWidth = 1;
            this.aGauge_distacnce.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge_distacnce.ScaleLinesMajorInnerRadius = 70;
            this.aGauge_distacnce.ScaleLinesMajorOuterRadius = 80;
            this.aGauge_distacnce.ScaleLinesMajorStepValue = 20F;
            this.aGauge_distacnce.ScaleLinesMajorWidth = 2;
            this.aGauge_distacnce.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.aGauge_distacnce.ScaleLinesMinorInnerRadius = 75;
            this.aGauge_distacnce.ScaleLinesMinorOuterRadius = 80;
            this.aGauge_distacnce.ScaleLinesMinorTicks = 9;
            this.aGauge_distacnce.ScaleLinesMinorWidth = 1;
            this.aGauge_distacnce.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGauge_distacnce.ScaleNumbersFormat = null;
            this.aGauge_distacnce.ScaleNumbersRadius = 95;
            this.aGauge_distacnce.ScaleNumbersRotation = 0;
            this.aGauge_distacnce.ScaleNumbersStartScaleLine = 0;
            this.aGauge_distacnce.ScaleNumbersStepScaleLines = 1;
            this.aGauge_distacnce.Size = new System.Drawing.Size(274, 219);
            this.aGauge_distacnce.TabIndex = 0;
            this.aGauge_distacnce.Value = 0F;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RgvInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "RgvInfoControl";
            this.Size = new System.Drawing.Size(1429, 284);
            this.Load += new System.EventHandler(this.RgvInfoControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_speed;
        private System.Windows.Forms.AGauge aGauge_speed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_power;
        private System.Windows.Forms.AGauge aGauge_power;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_current;
        private System.Windows.Forms.AGauge aGauge_current;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_tempture;
        private System.Windows.Forms.AGauge aGauge_tempture;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tb_distacnce;
        private System.Windows.Forms.AGauge aGauge_distacnce;
        private System.Windows.Forms.Timer timer1;
    }
}
