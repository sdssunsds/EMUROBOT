using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EMU.Util
{
    public class UdpClient
    {
        private string IP;
        private int Port;
        private Socket client;
        private Thread thread;
        public event Action<string, EndPoint> ReciveData;
        public event Action<string, EndPoint> ReciveUtf8;
        public event Action<string, EndPoint> ReciveAscii;
        public event Action<string, EndPoint> ReciveDefault;
        public UdpClient(string ip, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            thread = new Thread(new ThreadStart(ReciveMsg));
            IP = ip;
            Port = port;
            thread.Start();
        }

        public void Close()
        {
            thread.Abort();
            client.Close();
        }

        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        public void sendMsg(string msg, Encoding encoder, string ip = "", int port = 0)
        {
            if (!string.IsNullOrEmpty(ip))
            {
                ip = IP;
            }
            if (port == 0)
            {
                port = Port;
            }
            EndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            client.SendTo(encoder.GetBytes(msg), point);
        }

        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        private void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[1024];
                int length = client.ReceiveFrom(buffer, ref point);
                if (length > 0)
                {
                    if (ReciveData != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (byte item in buffer)
                        {
                            stringBuilder.Append(((int)item).ToString("X2") + " ");
                        }
                        string message = stringBuilder.ToString();
                        ReciveData(message, point);
                    }

                    if (ReciveUtf8 != null)
                    {
                        ReciveUtf8(Encoding.UTF8.GetString(buffer), point);
                    }

                    if (ReciveAscii != null)
                    {
                        ReciveAscii(Encoding.ASCII.GetString(buffer), point);
                    }

                    if (ReciveDefault != null)
                    {
                        ReciveDefault(Encoding.Default.GetString(buffer), point);
                    }
                }
            }
        }
    }
}
