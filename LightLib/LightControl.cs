using EMU.Parameter;
using System;
using System.Windows.Forms;

namespace Light
{
    public partial class LightControl : UserControl
    {
        public LightControl()
        {
            InitializeComponent();
        }

        private void LightControl_Load(object sender, EventArgs e)
        {
            textBox2.Text = EMU.Parameter.Properties.Settings.Default.LightFrontHigh.ToString();
            textBox3.Text = EMU.Parameter.Properties.Settings.Default.LightBackHigh.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LightManager.Instance.PowerOn(GetName(sender));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LightManager.Instance.PowerOff(GetName(sender));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LightManager.Instance.LightConnect(GetName(sender));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LightManager.Instance.LightDisConnect(GetName(sender));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LightManager.Instance.LightOn(GetName(sender));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LightManager.Instance.LightOff(GetName(sender));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                LightName name = GetName(sender);
                int value = 100;
                switch (name)
                {
                    case LightName.FrontRobotLight:
                        value = int.Parse(textBox2.Text);
                        break;
                    case LightName.BackRobotLight:
                        value = int.Parse(textBox3.Text);
                        break;
                    case LightName.LineCameraLight:
                        value = int.Parse(textBox1.Text);
                        break;
                }
                LightManager.Instance.SetLightBrightness(name, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private LightName GetName(object sender)
        {
            Button btn = sender as Button;
            int i = (int)btn.Tag;
            switch (i)
            {
                case 1:
                    return LightName.LineCameraLight;
                case 2:
                    return LightName.FrontRobotLight;
                case 3:
                    return LightName.BackRobotLight;
                default:
                    return LightName.LineCameraLight;
            }
        }
    }
}
