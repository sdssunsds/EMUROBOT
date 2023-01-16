using EMU.ApplicationData;
using EMU.Parameter;

namespace EMU.Interface
{
    /// <summary>
    /// 获取机械臂当前状态的委托
    /// </summary>
    /// <param name="robotinfo">机械臂信息</param>
    /// <param name="robot">机械臂目标</param>
    public delegate void RobotModInfo(RobotGlobalInfo robotinfo, RobotName robot);
    public interface IRobotControl
    {
        /// <summary>
        /// 机械臂连接
        /// </summary>
        /// <param name="robot">机械臂目标</param>
        bool RobotConnect(RobotName robot);
        /// <summary>
        /// 机械臂断开连接
        /// </summary>
        /// <param name="robot">机械臂目标</param>
        bool RobotDisConnect(RobotName robot);
        /// <summary>
        /// 机械臂回归原点
        /// </summary>
        /// <param name="robot">机械臂目标</param>
        bool RobotZeroPosition(RobotName robot);
        /// <summary>
        /// 机械臂移动到指定位置
        /// </summary>
        /// <param name="robot">机械臂目标</param>
        bool RobotMovePosition(RobotName robot, RobotDataPack robotDataPack);
        /// <summary>
        /// 查询机械臂状态
        /// </summary>
        /// <param name="robot">机械臂目标</param>
        bool RobotSelected(RobotName robot);
        /// <summary>
        /// 获取机械臂状态回调事件
        /// </summary>
        event RobotModInfo GetRobotModInfo;
    }
}
