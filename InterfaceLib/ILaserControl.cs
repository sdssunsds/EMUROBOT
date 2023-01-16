using EMU.Parameter;

namespace EMU.Interface
{
    /// <summary>
    /// 获取激光数据的委托
    /// </summary>
    /// <param name="data">激光数据</param>
    public delegate void LaserData(string data, LaserName laser);
    /// <summary>
    /// 获取激光数据的委托
    /// </summary>
    /// <param name="obj">激光数据</param>
    public delegate void LaserObject(object obj, LaserName laser);
    /// <summary>
    /// 获取激光数据的委托扩展方法
    /// </summary>
    /// <param name="exPars">其它数据</param>
    public delegate void LaserObjectEx(LaserName laser, params object[] exPars);
    /// <summary>
    /// 激光控制接口
    /// </summary>
    public interface ILaserControl
    {
        /// <summary>
        /// 激光连接
        /// </summary>
        /// <param name="laser">激光目标</param>
        bool LaserConnect(LaserName laser);
        /// <summary>
        /// 激光断开连接
        /// </summary>
        /// <param name="light">光源目标</param>
        bool LaserDisConnect(LaserName laser);
        /// <summary>
        /// 获取激光数据的回调事件
        /// </summary>
        event LaserData GetLaserData;
        /// <summary>
        /// 获取激光数据的回调事件
        /// </summary>
        event LaserObject GetLaserObj;
        // <summary>
        /// 获取激光数据的回调事件
        /// </summary>
        event LaserObjectEx GetLaserObjEx;
    }
}
