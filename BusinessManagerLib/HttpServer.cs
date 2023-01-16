using EMU.Util;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace EMU.BusinessManager
{
    public abstract class HttpServer
    {
        protected int port;
        TcpListener listener;
        bool is_active = true;

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void listen()
        {
            try
            {
                listener = new TcpListener(port);
                listener.Start();
                while (is_active)
                {
                    TcpClient s = listener.AcceptTcpClient();
                    HttpProcessor processor = new HttpProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                e.Message.AddLog(Parameter.LogType.ErrorLog);
            }
        }

        public abstract void handleGETRequest(HttpProcessor p);

        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }
}
