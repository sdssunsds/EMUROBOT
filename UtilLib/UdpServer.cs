using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EMU.Util
{
    public class UdpServer
    {
        private Socket server;
        private Thread thread;
        public event Action<string, EndPoint> ReciveData;
        public event Action<string, EndPoint> ReciveUtf8;
        public event Action<string, EndPoint> ReciveAscii;
        public event Action<string, EndPoint> ReciveDefault;

        public UdpServer(string ip, int port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            thread = new Thread(new ThreadStart(ReciveMsg));
            thread.Start();
        }

        public void Close()
        {
            thread.Abort();
            server.Close();
        }

        /// <summary>
        /// 向特定ip的主机的端口发送数据
        /// </summary>
        public void sendMsg(string msg, Encoding encoder, string ip, int port)
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            server.SendTo(encoder.GetBytes(msg), point);
        }

        /// <summary>
        /// 接收发送给本机ip对应端口号的数据
        /// </summary>
        private void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[1024];
                int length = server.ReceiveFrom(buffer, ref point);
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
