namespace Robot
{
    public class RobotData
    {
        #region
        /// <summary>
        /// Robot功能字:"1"-回到安全点
        /// </summary>
        public const string RobotBackZeroFunCode = "1.00";

        /// <summary>
        /// Robot功能字:"2"-控制运动
        /// </summary>
        public const string RobotSetCtrlFunCode = "2.00";

        /// <summary>
        /// Robot功能字:"3"-获取坐标状态
        /// </summary>
        public const string RobotGetDataFunCode = "3.00";

        /// <summary>
        /// Robot功能字:"4"-获取机器臂工作状态(预留)
        /// </summary>
        public const string RobotGetStatFunCode = "4.00";

        /// <summary>
        /// Robot功能字:"5"-关闭机械臂socket
        /// </summary>
        public const string RobotCloseSocketFunCode = "5.00";

        public const string RAKAE_ROBOT_DATA_SPLIT = ",";

        /// <summary>
        /// Roake Robot的协议末尾需要携带\r
        /// </summary>
        public const byte RAKAE_ROBOT_DATA_TAIL = 0x0d;
        #endregion

        /// <summary>
        /// 机械臂Id
        /// </summary>
        public byte mRobotDeviceId;

        /// <summary>
        /// 机械臂功能字
        /// </summary>
        public string mRobotFunc;

        /// <summary>
        /// 坐标数据
        /// </summary>
        public string mCoordinateVal;

        /// <summary>
        /// 发送命令:命令字,数据内容,\r
        /// 例子："2.00,1.00，2.00，3.00,4.00,5.00,6.00"'\r'
        /// </summary>
        public void RobotSendPck(string databuf, string cmd, out string cmdbuf)
        {
            //报文数据缓冲区
            string SendDataBuf = "";

            //a.功能字
            SendDataBuf += cmd;
            SendDataBuf += RAKAE_ROBOT_DATA_SPLIT;

            //b.拷贝Robot的数据内容(坐标)
            SendDataBuf += databuf;

            //c.在尾巴上加上1个字节‘\r’
            cmdbuf = SendDataBuf;
        }
    }
}
