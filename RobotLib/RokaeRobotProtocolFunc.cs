namespace Robot
{
    public class RokaeRobotProtocolFunc
    {
        /// <summary>
        /// 控制机械臂运动
        /// </summary>
        public static void RobotSetCtrlCmd(string databuf, out string cmdbuf)
        {
            RobotData pack = new RobotData();
            pack.RobotSendPck(databuf, RobotData.RobotSetCtrlFunCode, out cmdbuf);
        }

        /// <summary>
        /// 获取机械臂运动数据
        /// </summary>
        public static void RobotGetStatCmd(string databuf, out string cmdbuf)
        {
            RobotData pack = new RobotData();
            pack.RobotSendPck(databuf, RobotData.RobotGetDataFunCode, out cmdbuf);
        }

        /// <summary>
        /// 获取机械臂运动数据
        /// </summary>
        public static void RobotBackZeroCmd(string databuf, out string cmdbuf)
        {
            RobotData pack = new RobotData();
            pack.RobotSendPck(databuf, RobotData.RobotBackZeroFunCode, out cmdbuf);
        }

        /// <summary>
        /// 获取机械臂运动数据
        /// </summary>
        public static void RobotCloseCmd(string databuf, out string cmdbuf)
        {
            RobotData pack = new RobotData();
            pack.RobotSendPck(databuf, RobotData.RobotCloseSocketFunCode, out cmdbuf);
        }
    }
}
