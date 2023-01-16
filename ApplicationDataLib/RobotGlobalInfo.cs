using EMU.Parameter;

namespace EMU.ApplicationData
{
    public class RobotGlobalInfo
    {
        private static RobotGlobalInfo instance;
        public static RobotGlobalInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RobotGlobalInfo();
                }
                return instance;
            }
        }

        public int ModWorkCode { get; set; }
        public string ModWorkMsg { get; set; }

        /// <summary>
        /// 机器人编号
        /// </summary>
        public string RobotID { get; set; }

        /// <summary>
        /// Robot应答数据
        /// </summary>
        public string FrontRobotRspMsg { get; set; }
        public bool FrontRobotConnStat { get; set; } = false;

        /// <summary>
        /// Robot应答数据
        /// </summary>
        public string BackRobotRspMsg { get; set; }
        public bool BackRobotConnStat { get; set; } = false;

        /// <summary>
        /// FrontRobot设备坐标数据
        /// </summary>
        public RobotDataPack FrontRobotSiteData { get; set; } = new RobotDataPack();

        /// <summary>
        /// BackRobot设备坐标数据
        /// </summary>
        public RobotDataPack BackRobotSiteData { get; set; } = new RobotDataPack();

        /// <summary>
        /// Robot运动指令执行状态监控
        /// </summary>
        public EquipmentStatus FrontRobotRunStatMonitor { get; set; } = EquipmentStatus.READY;

        /// <summary>
        /// Robot运动指令执行状态监控
        /// </summary>
        public EquipmentStatus BackRobotRunStatMonitor { get; set; } = EquipmentStatus.READY;
    }
}
