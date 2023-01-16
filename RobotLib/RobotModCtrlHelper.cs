using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Robot
{
    public class RobotModCtrlHelper : IRobotControl
    {
        private static RobotModCtrlHelper instance;
        private RobotModCtrlHelper() { }
        public static RobotModCtrlHelper Instance
        {
            get
            {
                return instance ?? (instance = new RobotModCtrlHelper());
            }
        }

        private const byte FRONT_ROBOT_DEVICE_ID = 0x01;
        private const byte BACK_ROBOT_DEVICE_ID = 0x02;

        private object sendLock = new object();
        private RobotGlobalInfo myRobotGlobalInfo = RobotGlobalInfo.Instance;
        /// <summary>
        /// 机械臂连接对象
        /// </summary>
        private Dictionary<byte, TcpClientSocket> RobotSocket = new Dictionary<byte, TcpClientSocket>();

        public bool this[RobotName robot]
        {
            get
            {
                byte id = 0x00;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        break;
                }
                return RobotSocket.ContainsKey(id) && RobotSocket[id].connectSocket != null ? RobotSocket[id].connectSocket.Connected : false;
            }
        }
        public event RobotModInfo GetRobotModInfo;

        /// <summary>
        /// 接收数据
        /// </summary>
        private void RobotFrontRecvData(string message)
        {
            RobotServerRecvData(message, FRONT_ROBOT_DEVICE_ID);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void RobotBackRecvData(string message)
        {
            RobotServerRecvData(message, BACK_ROBOT_DEVICE_ID);
        }

        private void RobotServerRecvData(string message, byte id)
        {
            Task.Run(new Action(() =>
            {
                byte cmd = 0;
                try
                {
                    //解析报文
                    if (message.Contains("START,") && (message.Contains(",END")))
                    {
                        //按照','字符进行分割
                        string[] subArray = message.Split(',');
                        if (subArray[1].Contains("OK"))
                        {
                            cmd = 1;//命令控制报文
                        }
                        else
                        {
                            if (subArray.Length == 8)
                            {
                                cmd = 2;//获取数据报文 
                            }
                        }

                        //处理数据
                        switch (cmd)
                        {
                            case 1:
                                //获取客户端ip信息
                                switch (id)
                                {
                                    case FRONT_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.FrontRobotRspMsg = "设置成功";

                                        //表明机械臂执行完毕
                                        if (myRobotGlobalInfo.FrontRobotRunStatMonitor == EquipmentStatus.RUN)
                                        {
                                            myRobotGlobalInfo.FrontRobotRunStatMonitor = EquipmentStatus.STOP;
                                        }
                                        break;
                                    case BACK_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.BackRobotRspMsg = "设置成功";

                                        //表明机械臂执行完毕
                                        if (myRobotGlobalInfo.BackRobotRunStatMonitor == EquipmentStatus.RUN)
                                        {
                                            myRobotGlobalInfo.BackRobotRunStatMonitor = EquipmentStatus.STOP;
                                        }
                                        break;
                                }
                                break;

                            case 2:
                                switch (id)
                                {
                                    case FRONT_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.FrontRobotRspMsg = "获取数据成功";
                                        myRobotGlobalInfo.FrontRobotSiteData.j1 = subArray[1];
                                        myRobotGlobalInfo.FrontRobotSiteData.j2 = subArray[2];
                                        myRobotGlobalInfo.FrontRobotSiteData.j3 = subArray[3];
                                        myRobotGlobalInfo.FrontRobotSiteData.j4 = subArray[4];
                                        myRobotGlobalInfo.FrontRobotSiteData.j5 = subArray[5];
                                        myRobotGlobalInfo.FrontRobotSiteData.j6 = subArray[6];
                                        break;
                                    case BACK_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.BackRobotRspMsg = "获取数据成功";
                                        myRobotGlobalInfo.BackRobotSiteData.j1 = subArray[1];
                                        myRobotGlobalInfo.BackRobotSiteData.j2 = subArray[2];
                                        myRobotGlobalInfo.BackRobotSiteData.j3 = subArray[3];
                                        myRobotGlobalInfo.BackRobotSiteData.j4 = subArray[4];
                                        myRobotGlobalInfo.BackRobotSiteData.j5 = subArray[5];
                                        myRobotGlobalInfo.BackRobotSiteData.j6 = subArray[6];
                                        break;
                                }
                                break;

                            default:
                                switch (id)
                                {
                                    case FRONT_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.FrontRobotRspMsg = "数据协议异常";
                                        break;
                                    case BACK_ROBOT_DEVICE_ID:
                                        myRobotGlobalInfo.BackRobotRspMsg = "数据协议异常";
                                        break;
                                }
                                break;
                        }

                        myRobotGlobalInfo.ModWorkCode = 0;
                    }
                    else
                    {
                        myRobotGlobalInfo.ModWorkMsg = "设置失败,请重新设置";
                        myRobotGlobalInfo.ModWorkCode = -1;
                    }

                    //发送事件
                    switch (id)
                    {
                        case FRONT_ROBOT_DEVICE_ID:
                            GetRobotModInfo(myRobotGlobalInfo, RobotName.Front);
                            break;
                        case BACK_ROBOT_DEVICE_ID:
                            GetRobotModInfo(myRobotGlobalInfo, RobotName.Back);
                            break;
                    }
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                }
            }));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        private void RobotSendFront(int len)
        {
            RobotSendBack(len, FRONT_ROBOT_DEVICE_ID);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        private void RobotSendBack(int len)
        {
            RobotSendBack(len, BACK_ROBOT_DEVICE_ID);
        }

        private void RobotSendBack(int len, byte id)
        {
            (id.ToString() + "[Send]: " + len).AddLog(LogType.RobotLog);
        }

        private void DoRobotOpCmdHandle(byte devid, string cmd, RobotDataPack data)
        {
            lock (sendLock)
            {
                string sendpack = "";
                string val = data.j1 + RobotData.RAKAE_ROBOT_DATA_SPLIT +
                             data.j2 + RobotData.RAKAE_ROBOT_DATA_SPLIT +
                             data.j3 + RobotData.RAKAE_ROBOT_DATA_SPLIT +
                             data.j4 + RobotData.RAKAE_ROBOT_DATA_SPLIT +
                             data.j5 + RobotData.RAKAE_ROBOT_DATA_SPLIT +
                             data.j6;
                //构造数据包
                if (cmd == RobotData.RobotSetCtrlFunCode)
                {
                    RokaeRobotProtocolFunc.RobotSetCtrlCmd(val, out sendpack);
                }
                else if (cmd == RobotData.RobotGetDataFunCode)
                {
                    RokaeRobotProtocolFunc.RobotGetStatCmd(val, out sendpack);
                }
                else if (cmd == RobotData.RobotBackZeroFunCode)
                {
                    RokaeRobotProtocolFunc.RobotBackZeroCmd(val, out sendpack);
                }
                else if (cmd == RobotData.RobotCloseSocketFunCode)
                {
                    RokaeRobotProtocolFunc.RobotCloseCmd(val, out sendpack);
                }
                else
                {
                    return;
                }
                ("机械臂指令：" + sendpack).AddLog(LogType.RobotLog);

                byte[] senddataby = System.Text.Encoding.Default.GetBytes(sendpack);
                byte[] sendpackby = new byte[senddataby.Length + 1];
                Array.Copy(senddataby, 0, sendpackby, 0, senddataby.Length);

                sendpackby[senddataby.Length] = RobotData.RAKAE_ROBOT_DATA_TAIL;

                //发送数据
                RobotSocket[devid].SendAsync(System.Text.Encoding.UTF8.GetString(sendpackby));
            }
        }

        public bool RobotConnect(RobotName robot)
        {
            try
            {
                byte id = 0x00;
                string ip = "";
                int port = 0;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        ip = EMU.Parameter.Properties.Settings.Default.RobotFrontIP;
                        port = EMU.Parameter.Properties.Settings.Default.RobotFrontPort;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        ip = EMU.Parameter.Properties.Settings.Default.RobotBackIP;
                        port = EMU.Parameter.Properties.Settings.Default.RobotBackPort;
                        break;
                }
                RobotSocket.Add(id, new TcpClientSocket(ip, port));
                switch (robot)
                {
                    case RobotName.Front:
                        RobotSocket[id].recvMessageEvent = new Action<string>(RobotFrontRecvData);
                        RobotSocket[id].sendResultEvent = new Action<int>(RobotSendFront);
                        myRobotGlobalInfo.FrontRobotConnStat = true;
                        break;
                    case RobotName.Back:
                        RobotSocket[id].recvMessageEvent = new Action<string>(RobotBackRecvData);
                        RobotSocket[id].sendResultEvent = new Action<int>(RobotSendBack);
                        myRobotGlobalInfo.BackRobotConnStat = true;
                        break;
                }
                RobotSocket[id].Start(0);
                myRobotGlobalInfo.ModWorkMsg = "客户端创建成功";
                myRobotGlobalInfo.ModWorkCode = 0;
                GetRobotModInfo(myRobotGlobalInfo, robot);
                return RobotSelected(robot);
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }

        public bool RobotDisConnect(RobotName robot)
        {
            try
            {
                byte id = 0x00;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.FrontRobotConnStat = false;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.BackRobotConnStat = false;
                        break;
                }
                DoRobotOpCmdHandle(id, RobotData.RobotCloseSocketFunCode, new RobotDataPack());
                RobotSocket[id].CloseClientSocket();
                RobotSocket.Remove(id);
                GetRobotModInfo(myRobotGlobalInfo, robot);
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }

        public bool RobotZeroPosition(RobotName robot)
        {
            try
            {
                byte id = 0x00;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.FrontRobotRunStatMonitor = EquipmentStatus.RUN;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.BackRobotRunStatMonitor = EquipmentStatus.RUN;
                        break;
                }
                DoRobotOpCmdHandle(id, RobotData.RobotBackZeroFunCode, new RobotDataPack());
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }

        public bool RobotMovePosition(RobotName robot, RobotDataPack robotDataPack)
        {
            try
            {
                byte id = 0x00;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.FrontRobotRunStatMonitor = EquipmentStatus.RUN;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        myRobotGlobalInfo.BackRobotRunStatMonitor = EquipmentStatus.RUN;
                        break;
                }
                DoRobotOpCmdHandle(id, RobotData.RobotSetCtrlFunCode, robotDataPack);
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }

        public bool RobotSelected(RobotName robot)
        {
            try
            {
                byte id = 0x00;
                switch (robot)
                {
                    case RobotName.Front:
                        id = FRONT_ROBOT_DEVICE_ID;
                        break;
                    case RobotName.Back:
                        id = BACK_ROBOT_DEVICE_ID;
                        break;
                }
                DoRobotOpCmdHandle(id, RobotData.RobotGetDataFunCode, new RobotDataPack());
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }
    }
}
