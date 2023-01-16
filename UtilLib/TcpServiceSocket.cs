using EMU.Parameter;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMU.Util
{
    public class TcpServiceSocket
    {
        //连接信息事件
        public Action<Socket> accpetInfoEvent = null;

        //接收数据事件
        public Action<Socket, string> recvMessageEvent = null;

        //发送结果事件
        public Action<int> sendResultEvent = null;

        //允许连接到tcp服务器的tcp客户端数量
        private int numConnections = 0;

        //连接socket
        private Socket listenSocket = null;

        //tcp服务器ip
        private string host = "";

        //tcp服务器端口
        private int port = 0;

        //控制tcp客户端连接数量的信号量
        private Semaphore maxNumberAcceptedClients = null;

        ///// <summary>
        ///// RelaytHandleTask取消条件
        ///// </summary>
        private CancellationTokenSource cancelTokenSource;

        //tcp连接缓冲区
        private int bufferSize = 4096000;

        //客户端session列表
        public List<Socket> clientSockets = null;

        /// <summary>
        /// 初始化socket参数
        /// </summary>
        public TcpServiceSocket(string host, int port, int numConnections)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host cannot be null");

            if (port < 1 || port > 65535)
                throw new ArgumentOutOfRangeException("port is out of range");

            if (numConnections <= 0 || numConnections > int.MaxValue)
                throw new ArgumentOutOfRangeException("_numConnections is out of range");

            this.host = host;
            this.port = port;
            this.numConnections = numConnections;
            clientSockets = new List<Socket>();
            maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        /// <summary>
        /// 创建TcpServer角色
        /// </summary>
        public void Start()
        {
            try
            {
                cancelTokenSource = new CancellationTokenSource();
                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(new IPEndPoint(IPAddress.Parse(host), port));
                listenSocket.Listen(numConnections);
                AcceptAsync();
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }
        }

        /// <summary>
        /// 接收socket连接
        /// </summary>
        private void AcceptAsync()
        {
            try
            {
                Task.Run(new Action(() =>
                {
                    while (!cancelTokenSource.IsCancellationRequested)
                    {
                        maxNumberAcceptedClients.WaitOne();

                        try
                        {
                            Socket acceptSocket = listenSocket.Accept();
                            if (acceptSocket == null)
                                continue;

                            //添加session
                            lock (clientSockets)
                            {
                                clientSockets.Add(acceptSocket);
                            }

                            //通知UI已经有UI连接进来
                            AccpetClientInfoAsync(acceptSocket);

                            //拉起任务
                            RecvAsync(acceptSocket);
                        }
                        catch (Exception e)
                        {
                            e.StackTrace.AddLog(LogType.ErrorLog);
                            try
                            {
                                maxNumberAcceptedClients.Release();
                            }
                            catch (Exception ex)
                            {
                                ex.StackTrace.AddLog(LogType.ErrorLog);
                            }
                        }
                    }
                }), cancelTokenSource.Token);
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }
        }

        /// <summary>
        /// 通知客户端连接信息
        /// </summary>
        private void AccpetClientInfoAsync(Socket acceptSocket)
        {
            Task.Run(new Action(() =>
            {
                try
                {
                    accpetInfoEvent?.Invoke(acceptSocket); //返回字符串
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }));
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void RecvAsync(Socket acceptSocket)
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                byte[] buffer = new byte[bufferSize];

                try
                {
                    //表示收取数据成功
                    while ((len = acceptSocket.Receive(buffer, bufferSize, SocketFlags.None)) > 0)
                    {
                        recvMessageEvent?.Invoke(acceptSocket, Encoding.UTF8.GetString(buffer, 0, len)); //返回字符串
                    }
                }
                catch (Exception e)
                {
                    CloseClientSocket(acceptSocket);
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void SendAsync(Socket acceptSocket, string message, bool isByte = false)
        {
            Task.Run(new Action(() =>
            {
                int len = 0;
                byte[] buffer = null;
                if (isByte)
                {
                    string[] vs = message.Split(' ');
                    buffer = new byte[vs.Length];
                    for (int i = 0; i < vs.Length; i++)
                    {
                        buffer[i] = (byte)Convert.ToInt32(vs[i], 16);
                    }
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(message);
                }
                try
                {
                    if ((len = acceptSocket.Send(buffer, buffer.Length, SocketFlags.None)) > 0)
                    {
                        sendResultEvent?.Invoke(len);
                    }
                }
                catch (Exception e)
                {
                    CloseClientSocket(acceptSocket);
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }));
        }

        /// <summary>
        /// 向所有客户端广播消息
        /// </summary>
        public void SendMessageToAllClientsAsync(string message, bool isByte = false)
        {
            Task.Run(new Action(() =>
            {
                lock (clientSockets)
                {
                    foreach (var socket in clientSockets)
                    {
                        SendAsync(socket, message, isByte);
                    }
                }
            }));
        }

        /// <summary>
        /// 关闭一个客户端连接
        /// </summary>
        private void CloseClientSocket(Socket acceptSocket)
        {
            try
            {
                acceptSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }

            try
            {
                acceptSocket.Close();
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }

            try
            {
                maxNumberAcceptedClients.Release();
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
            }
        }

        /// <summary>
        /// 关闭所有客户端连接
        /// </summary>
        public void CloseAllClientSocket()
        {
            lock (clientSockets)
            {
                try
                {
                    foreach (var socket in clientSockets)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }

                try
                {
                    foreach (var socket in clientSockets)
                    {
                        socket.Close();
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }

                try
                {
                    //释放掉任务
                    cancelTokenSource.Cancel();
                    listenSocket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }

                try
                {
                    listenSocket.Close();
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }

                try
                {
                    maxNumberAcceptedClients.Release(clientSockets.Count);
                    clientSockets.Clear();
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }
        }
    }
}
