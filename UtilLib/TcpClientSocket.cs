using EMU.Parameter;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMU.Util
{
    public class TcpClientSocket
    {
        //接收数据事件
        public Action<string> recvMessageEvent = null;
        public Action<byte[], int> recvByteEvent = null;

        //发送结果事件
        public Action<int> sendResultEvent = null;

        //连接socket
        public Socket connectSocket = null;

        //tcp服务器ip
        private string host = "";

        //tcp服务器端口
        private int port = 0;

        //socket缓冲区
        private int bufferSize = 4096000;

        public TcpClientSocket(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new Exception("IP地址非法【IP: " + host + "】");
            if (port < 1 || port > 65535)
                throw new Exception("端口号非法【Port: " + port + "】");

            this.host = host;
            this.port = port;
        }

        public bool Start(int protocol)
        {
            try
            {
                connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connectSocket.Connect(host, port);
                if (protocol == 0)
                {
                    RecvAsync();
                }
                else if (protocol == 1)
                {
                    RecvByteAsync();
                }
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
                return false;
            }
            return true;
        }

        private void RecvByteAsync()
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                byte[] buffer = new byte[bufferSize];
                try
                {
                    //表示收取数据成功
                    while (connectSocket != null &&
                          (len = connectSocket.Receive(buffer, bufferSize, SocketFlags.None)) > 0)
                    {
                        if (recvByteEvent != null)
                            recvByteEvent(buffer, len);
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                    Restart(1);
                }
            }));
        }

        private void RecvAsync()
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                byte[] buffer = new byte[bufferSize];
                try
                {
                    //表示收取数据成功
                    while (connectSocket != null &&
                          (len = connectSocket.Receive(buffer, bufferSize, SocketFlags.None)) > 0)
                    {
                        if (recvMessageEvent != null)
                            recvMessageEvent(Encoding.UTF8.GetString(buffer, 0, len));
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                    Restart(0);
                }
            }));
        }

        public void SendByteAsync(byte[] buffer, int bufferlen)
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                try
                {
                    if (connectSocket != null &&
                       (len = connectSocket.Send(buffer, bufferlen, SocketFlags.None)) > 0)
                    {
                        if (sendResultEvent != null)
                            sendResultEvent(len);
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                    Restart(1);
                }
            }));
        }

        public void SendAsync(string message)
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                try
                {
                    if (connectSocket != null &&
                       (len = connectSocket.Send(buffer, buffer.Length, SocketFlags.None)) > 0)
                    {
                        if (sendResultEvent != null)
                            sendResultEvent(len);
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                    Restart(0);
                }
            }));
        }

        public void CloseClientSocket()
        {
            try
            {
                connectSocket?.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }
            try
            {
                connectSocket?.Close();
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }
        }

        public void Restart(int protocol)
        {
            CloseClientSocket();
            while (!Start(protocol))
            {
                "Socket尝试重连...".AddLog(LogType.GeneralLog);
                Thread.Sleep(3000);
            }
        }
    }
}
