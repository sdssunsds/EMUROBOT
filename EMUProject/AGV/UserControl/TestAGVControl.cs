using EMU.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class TestAGVControl : UserControl
    {
        private TcpServiceSocket server = null;
        private DataTable carTable = null;
        private Dictionary<string, Socket> clients = new Dictionary<string, Socket>();

        public TestAGVControl()
        {
            InitializeComponent();
        }

        private void TestAGVControl_Load(object sender, EventArgs e)
        {
            cb_loc.SelectedIndex = cb_point.SelectedIndex = 0;

            string host = Dns.GetHostName();
            IPHostEntry iPs = Dns.GetHostEntry(host);
            IPAddress[] addresses = iPs.AddressList;
            if (addresses != null)
            {
                for (int i = 0; i < addresses.Length; i++)
                {
                    byte[] vs = addresses[i].GetAddressBytes();
                    if (vs[3] > 0)
                    {
                        tb_ip.Text = vs[0] + "." + vs[1] + "." + vs[2] + "." + vs[3];
                        break;
                    }
                }
            }

            carTable = new DataTable();
            carTable.Columns.Add("AGV IP");
            carTable.Columns.Add("AGV编号");
            carTable.Columns.Add("AGV名称");
            dgv_car.DataSource = carTable;
        }

        private void btn_start_server_Click(object sender, EventArgs e)
        {
            server = new TcpServiceSocket(tb_ip.Text, int.Parse(tb_port.Text), 100);
            server.accpetInfoEvent = (Socket socket) =>
            {
                carTable.Rows.Add(socket.RemoteEndPoint.ToString(), "", "");
                Invoke(new Action(() =>
                {
                    dgv_car.DataSource = null;
                    dgv_car.DataSource = carTable;
                }));
            };
            server.recvMessageEvent = (Socket socket, string value) =>
            {
                string ip = socket.RemoteEndPoint.ToString();
                if (!clients.ContainsKey(ip))
                {
                    clients.Add(ip, socket);
                }
                else
                {
                    clients[ip] = socket;
                }
            };
            server.Start();
            lb_error.Text = "服务已启动";
        }

        private void ben_close_server_Click(object sender, EventArgs e)
        {
            server.CloseAllClientSocket();
            server = null;
            dgv_car.DataSource = null;
            carTable.Rows.Clear();
            lb_error.Text = "服务已关闭";
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            if (dgv_car.SelectedRows.Count > 0)
            {
                int i = dgv_car.SelectedRows[0].Index;
                string ip = carTable.Rows[i][0].ToString();
                if (clients.ContainsKey(ip))
                {
                    foreach (string item in lb_list.Items)
                    {
                        server.SendAsync(clients[ip], item, true);
                    }
                }
            }
            else
            {
                foreach (string item in lb_list.Items)
                {
                    server.SendMessageToAllClientsAsync(item, true);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectCmd("01", rb_forward.Checked ? "01" : "02", Convert10To16(int.Parse(tb_speed1.Text)), "0" + (cb_loc.SelectedIndex + 1), ConvertStrToStr(tb_box.Text, 2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectCmd("02", rb_left.Checked ? "01" : "02", Convert10To16(int.Parse(tb_speed2.Text)), "0" + (cb_point.SelectedIndex + 1));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectCmd("03", ConvertStrToStr(tb_length.Text, 2));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectCmd("04", ConvertStrToStr(tb_sleep.Text, 2));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SelectCmd("05", rb_input.Checked ? "01" : "02", ConvertStrToStr(tb_info_port.Text, 2), rb_true.Checked ? "01" : "00");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectCmd("06", ConvertStrToStr(tb_id.Text, 2));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SelectCmd("A0", ConvertStrToStr(tb_num.Text, 2));
        }

        private void 置顶指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lb_list.SelectedIndex >= 0)
            {
                int index = lb_list.SelectedIndex;
                string s = lb_list.Items[index].ToString();
                lb_list.Items.RemoveAt(index);
                lb_list.Items.Insert(0, s);
            }
        }

        private void 上移指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lb_list.SelectedIndex >= 0)
            {
                int index = lb_list.SelectedIndex;
                string s = lb_list.Items[index].ToString();
                lb_list.Items.RemoveAt(index);
                lb_list.Items.Insert(index - 1, s);
            }
        }

        private void 下移指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lb_list.SelectedIndex >= 0)
            {
                if (lb_list.SelectedIndex < lb_list.Items.Count - 1)
                {
                    int index = lb_list.SelectedIndex;
                    string s = lb_list.Items[index].ToString();
                    lb_list.Items.RemoveAt(index);
                    lb_list.Items.Insert(index + 1, s);
                }
            }
        }

        private void 置底指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lb_list.SelectedIndex >= 0)
            {
                if (lb_list.SelectedIndex < lb_list.Items.Count - 1)
                {
                    int index = lb_list.SelectedIndex;
                    string s = lb_list.Items[index].ToString();
                    lb_list.Items.RemoveAt(index);
                    lb_list.Items.Add(s);
                }
            }
        }

        private void 移除指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lb_list.SelectedIndex >= 0)
            {
                lb_list.Items.RemoveAt(lb_list.SelectedIndex);
            }
        }

        private void SelectCmd(string head, params string[] pars)
        {
            int length = 9;
            string value = head;
            for (int i = 0; i < length; i++)
            {
                if (pars != null && pars.Length > i)
                {
                    value += " " + pars[i];
                    if (pars[i].Length > 2)
                    {
                        length--;
                    }
                }
                else
                {
                    value += " 00";
                }
            }
            lb_list.Items.Add(value);
        }

        private string Convert10To16(int i)
        {
            string s = i.ToString("X4");
            return s.Substring(0, 2) + " " + s.Substring(2, 2);
        }

        public string ConvertStrToStr(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(length);
            }
            else if (str.Length < length)
            {
                for (int i = 0; i < length - str.Length; i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }
    }
}
