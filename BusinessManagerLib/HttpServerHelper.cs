using EMU.ApplicationData;
using EMU.Interface;
using System;
using System.Threading.Tasks;

namespace EMU.BusinessManager
{
    public class HttpServerHelper : IAppServer
    {
        private static HttpServerHelper instance;
        private HttpServerHelper() { }
        public static HttpServerHelper Instance
        {
            get
            {
                return instance ?? (instance = new HttpServerHelper());
            }
        }

        public MyHttpServer httpServer = new MyHttpServer(Parameter.Properties.Settings.Default.HttpServerPort);

        public event AcceptClient AcceptClient;
        public event SendClient SendClient;

        public bool CreateServer(string serverPath)
        {
            Task.Run(new Action(() =>
            {
                httpServer.listen();
            }));
            return true;
        }

        public void Accept(AppDeviceFrame msg)
        {
            AcceptClient?.Invoke(msg);
        }

        public void Send(AppRspFrame msg)
        {
            SendClient?.Invoke(msg);
        }
    }
}
