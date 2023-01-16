using EMU.ApplicationData;
using EMU.Parameter;

namespace EMU.Interface
{
    /// <summary>
    /// 控制RGV时的委托
    /// </summary>
    public delegate void RgvModCmd(RgvCmd rgvCmd);
    /// <summary>
    /// 获取RGV当前状态的委托
    /// </summary>
    /// <param name="rgvinfo">RGV信息</param>
    public delegate void RgvModInfo(RgvGlobalInfo rgvinfo);
    public interface IRgvControl
    {
        /// <summary>
        /// 连接RGV
        /// </summary>
        bool RgvConnect();
        /// <summary>
        /// 断开RGV
        /// </summary>
        bool RgvDisConnect();
        /// <summary>
        /// 正向运动
        /// </summary>
        bool RgvForwardMove();
        /// <summary>
        /// 反向运动
        /// </summary>
        bool RgvBackMove();
        /// <summary>
        /// 正常停止
        /// </summary>
        bool RgvNormalStop();
        /// <summary>
        /// 紧急停止
        /// </summary>
        bool RgvEmergeStop();
        /// <summary>
        /// 清除报警
        /// </summary>
        bool RgvClearAlarm();
        /// <summary>
        /// 智能充电
        /// </summary>
        bool RgvIntelligentCharging();
        /// <summary>
        /// 终止充电
        /// </summary>
        bool RgvTerminateCharging();
        /// <summary>
        /// 设置速度
        /// </summary>
        bool RgvSetSpeed(int speed);
        /// <summary>
        /// 设置位置
        /// </summary>
        bool RgvSetDistance(int distance);
        /// <summary>
        /// 设置轨道长度
        /// </summary>
        bool RgvSetLength(int length);
        /// <summary>
        /// 运动到位置
        /// </summary>
        bool RgvRunDistance();
        /// <summary>
        /// 获取RGV状态回调事件
        /// </summary>
        event RgvModInfo GetRgvModInfo;
    }
}
