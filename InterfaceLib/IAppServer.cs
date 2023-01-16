using EMU.ApplicationData;

namespace EMU.Interface
{
    /// <summary>
    /// 接收业务端消息委托
    /// </summary>
    /// <param name="msg">消息</param>
    public delegate void AcceptClient(AppDeviceFrame msg);
    /// <summary>
    /// 发送业务端消息委托
    /// </summary>
    /// <param name="msg">消息</param>
    public delegate void SendClient(AppRspFrame msg);
    public interface IAppServer
    {
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="serverPath">服务地址</param>
        bool CreateServer(string serverPath);
        /// <summary>
        /// 接收业务端消息事件
        /// </summary>
        event AcceptClient AcceptClient;
        /// <summary>
        /// 发送业务端消息事件
        /// </summary>
        event SendClient SendClient;
    }
}
