using EMU.ApplicationData;
using EMU.Util;
using System;
using System.Windows.Forms;

namespace EMU.BusinessManager
{
    public partial class HttpServerControl : UserControl
    {
        public HttpServerControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpServerHelper.Instance.CreateServer("");
            HttpServerHelper.Instance.AcceptClient += Instance_AcceptClient;
            HttpServerHelper.Instance.SendClient += Instance_SendClient;
        }

        private void Instance_SendClient(AppRspFrame msg)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    textBox2.Text += JsonManager.ObjectToJson(msg);
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.ScrollToCaret();
                }));
            }
            catch (Exception) { }
        }

        private void Instance_AcceptClient(AppDeviceFrame msg)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    textBox1.Text += JsonManager.ObjectToJson(msg);
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                }));
            }
            catch (Exception) { }
        }
    }
}
