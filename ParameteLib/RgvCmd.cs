namespace EMU.Parameter
{
    public enum RgvCmd
    {
        /// <summary>
        /// 无效命令
        /// </summary>
        INVALID = 0xFFFF,
        /// <summary>
        /// 连接设备
        /// </summary>
        CONNECT = 0xF000,
        /// <summary>
        /// 断开设备
        /// </summary>
        DISCONNECT = 0xF001,
        /// <summary>
        /// 正常停止
        /// </summary>
        NORMALSTOP = 0x0000,
        /// <summary>
        /// 正向运动
        /// </summary>
        FORWARDMOTOR = 0x0001,
        /// <summary>
        /// 反向运动
        /// </summary>
        BACKWARDMOTOR = 0x0002,
        /// <summary>
        /// 停止智能充电
        /// </summary>
        STOPPOWERCHARGE = 0x000C,
        /// <summary>
        /// 清除报警
        /// </summary>
        CLEARALARM = 0x0007,
        /// <summary>
        /// 开始智能充电
        /// </summary>
        STARTPOWERCHARGE = 0x000B,
        /// <summary>
        /// 运动到指定距离
        /// </summary>
        RUNAPPOINTDISTANCE = 0x0005,
        /// <summary>
        /// 设置目标运行速度
        /// </summary>
        SETTARGETSPEED = 0x000D,
        /// <summary>
        /// 设置目标运行距离
        /// </summary>
        SETTARGETDISATNCE = 0x000E,
        /// <summary>
        /// 设置轨道长度
        /// </summary>
        SETTRACKLENGTH = 0x000F
    }
}
