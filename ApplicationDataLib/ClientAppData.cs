namespace EMU.ApplicationData
{
    public class ClientAppData
    {
        public string train_mode { get; set; }
        public string train_sn { get; set; }
        public string robot_id { get; set; }
    }
    public class AppDeviceFrame
    {
        #region
        /// <summary>
        /// 功能字:开始作业
        /// </summary>
        public const string HostCmd_StartWork = "start_work";

        /// <summary>
        /// 功能字:停止作业
        /// </summary>
        public const string HostCmd_StopWork = "stop_work";

        /// <summary>
        /// 功能字:清除报警
        /// </summary>
        public const string HostCmd_DeviceClearAlarm = "device_clear_alarm";

        /// <summary>
        /// 功能字:紧急停止
        /// </summary>
        public const string HostCmd_DeviceStop = "device_stop";

        /// <summary>
        /// 功能字:查询状态
        /// </summary>
        public const string HostCmd_DeviceQueryStat = "device_query_stat";

        /// <summary>
        /// 功能字:暂停作业
        /// </summary>
        public const string HostCmd_SuspendWork = "suspend_work";

        /// <summary>
        /// 功能字:继续作业
        /// </summary>
        public const string HostCmd_ResumeWork = "resume_work";
        
        /// <summary>
        /// 功能字:正向相对运动几米(当前距离+相对距离)
        /// </summary>
        public const string HostCmd_MoveForwardDistance = "forward_move_relative_distance";

        /// <summary>
        /// 功能字:反向相对运动几米(当前距离-相对距离)
        /// </summary>
        public const string HostCmd_MoveBackwardDistance = "backward_move_relative_distance";

        /// <summary>
        /// 功能字:设备自检
        /// </summary>
        public const string HostCmd_DeviceCheckSelf = "device_checkself";

        /// <summary>
        /// 功能字:设备回到原点
        /// </summary>
        public const string HostCmd_DeviceRZSite = "device_rz_site";

        /// <summary>
        /// 功能字:设备开始充电
        /// </summary>
        public const string HostCmd_DevicePowerCharge = "device_powercharge";

        /// <summary>
        /// 功能字:设备关机
        /// </summary>
        public const string HostCmd_DevicePowerOff = "device_poweroff";
        #endregion

        /// <summary>
        /// 功能字
        /// </summary>
        public string cmd = " ";

        /// <summary>
        /// 设备参数信息
        /// </summary>
        public ClientAppData para { get; set; } = new ClientAppData();
    }
    public class AppTrainRspMsg
    {
        public string msg_code { get; set; }
        public string msg_content { get; set; }
    }
    public class AppRspFrame
    {
        /// <summary>
        /// 功能字
        /// </summary>
        public string cmd = " ";

        /// <summary>
        /// 设备参数信息
        /// </summary>
        public AppTrainRspMsg para = new AppTrainRspMsg();
    }
}
