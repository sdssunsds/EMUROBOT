using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rgv
{
    public class RgvModCtrlHelper : IRgvControl
    {
        private RgvModCtrlHelper() { }
        private static RgvModCtrlHelper instance;
        public static RgvModCtrlHelper Instance
        {
            get
            {
                return instance ?? (instance = new RgvModCtrlHelper());
            }
        }

        private string ip = EMU.Parameter.Properties.Settings.Default.RgvIP;
        private int port = EMU.Parameter.Properties.Settings.Default.RgvPort;

        /// <summary>
        /// Rgv创建网络客户端的sock连接
        /// </summary>
        private TcpClientSocket myRgvClientSock = null;
        /// <summary>
        /// 车辆设备信息
        /// </summary>
        private RgvGlobalInfo myRgvGlobalInfo = RgvGlobalInfo.Instance;

        public bool IsEnable = false;
        public event RgvModCmd SetRgvModCmd;
        public event RgvModInfo GetRgvModInfo;

        /// <summary>
        /// 定时查询车的数据
        /// </summary>
        private void InitTimer()
        {
            Task.Run(() =>
            {
                while (IsEnable)
                {
                    byte[] sendby = RgvModCtrlFun.RgvModCtrlCmd_QureyStat();
                    myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                    Thread.Sleep(50);
                }
            });
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void RgvModRecvByteData(byte[] ReceiveBuf, int len)
        {
            Task.Run(new Action(() =>
            {
                try
                {
                    int r = len;

                    int backdata = 0;
                    int shedingshuju = 0;

                    if (r > 31 && r % 31 == 0)
                    {
                        r = 31;
                    }

                    //解析报文
                    if (r == 12)
                    {
                        if (ReceiveBuf[2] == 0x0 && ReceiveBuf[3] == 0x00 && ReceiveBuf[4] == 0x00 && ReceiveBuf[5] == 0x06 && ReceiveBuf[6] == 0x01 && ReceiveBuf[7] == 0x06 && ReceiveBuf[8] == 0x00)//写单个寄存器
                        {
                            int x = ReceiveBuf[9];
                            switch (x)
                            {
                                case 0x01:
                                    {
                                        if (ReceiveBuf[11] == 0x0)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "停车成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x01)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "控制小车正向运动成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x02)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "控制小车反向运动成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x03)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "返回零点成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x05)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "定位到指定位置成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x06)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "紧急停止成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x07)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "清除报警成功";
                                            myRgvGlobalInfo.RgvIsAlarm = 0; //管理Rgv的异常状态
                                        }
                                        else if (ReceiveBuf[11] == 0x08)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "智能返回成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x09)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "刹车盘开启成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x0a)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "刹车盘关闭成功";
                                        }
                                        else if (ReceiveBuf[11] == 0x0b)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "开始充电";
                                        }
                                        else if (ReceiveBuf[11] == 0x0c)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "停止充电";
                                        }
                                    }
                                    break;
                                case 04: //设定运动目标位置
                                    {
                                        backdata = ReceiveBuf[10];
                                        backdata = (backdata << 8) + ReceiveBuf[11];

                                        shedingshuju = myRgvGlobalInfo.RgvTargetRunDistance;
                                        if (backdata == shedingshuju)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "设定目标位置成功";
                                        }
                                    }
                                    break;

                                case 02: //设定运动轨道长度
                                    {
                                        backdata = ReceiveBuf[10];
                                        backdata = (backdata << 8) + ReceiveBuf[11];

                                        shedingshuju = myRgvGlobalInfo.RgvTrackLength;
                                        if (backdata == shedingshuju)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "设定轨道长度成功";
                                        }
                                    }
                                    break;

                                case 10: //设定目标速度
                                    {
                                        backdata = ReceiveBuf[10];
                                        backdata = (backdata << 8) + ReceiveBuf[11];

                                        shedingshuju = myRgvGlobalInfo.RgvTargetRunSpeed;
                                        if (backdata == shedingshuju)
                                        {
                                            myRgvGlobalInfo.RgvCurrentCmdSetStat = "设定目标速度成功";
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    else if (r == 31)
                    {
                        int temp1 = 0;
                        int temp2 = 0;
                        int temp3 = 0;
                        int temp4 = 0;

                        int temp = 0;
                        String str = @"";

                        //00 1e 00 00 00 19 01 03 16 00 00 00 00 00 00 13 89 00 64 02 bc 00 1a 00 01 00 00 00 00 00 00 
                        if (ReceiveBuf[2] == 0x00 && ReceiveBuf[3] == 0x00
                                && ReceiveBuf[4] == 0x00 && ReceiveBuf[5] == 0x19
                                && ReceiveBuf[6] == 0x01 && ReceiveBuf[7] == 0x03
                                && ReceiveBuf[8] == 0x16)
                        {
                            //得到速度
                            temp1 = ReceiveBuf[9];
                            temp2 = ReceiveBuf[10];
                            temp3 = ReceiveBuf[11];
                            temp4 = ReceiveBuf[12];
                            temp = (temp1 << 24) + (temp2 << 16) + (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentRunSpeed = temp;

                            //得到当前位置(mm)
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp1 = ReceiveBuf[13];
                            temp2 = ReceiveBuf[14];
                            temp3 = ReceiveBuf[15];
                            temp4 = ReceiveBuf[16];
                            temp = (temp1 << 24) + (temp2 << 16) + (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentRunDistacnce = temp;

                            //得到当前电量
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[17];
                            temp4 = ReceiveBuf[18];
                            temp = (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentPowerElectricity = temp;

                            //得到当前电流(mA)
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[19];
                            temp4 = ReceiveBuf[20];
                            temp = (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentPowerCurrent = temp;

                            //得到当前电池温度
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[21];
                            temp4 = ReceiveBuf[22];
                            temp = (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentPowerTempture = temp;

                            //得到当前模式
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[23];
                            temp4 = ReceiveBuf[24];
                            temp = (temp3 << 8) + temp4;
                            if (temp == 1)
                            {
                                myRgvGlobalInfo.RgvCurrentMode = "自动模式";
                            }
                            else
                            {
                                myRgvGlobalInfo.RgvCurrentMode = "手动模式";
                            }

                            //得到当前状态,待机状态位标识：0表示待机，1表示正在运行
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[25];
                            temp4 = ReceiveBuf[26];
                            temp = (temp3 << 8) + temp4;

                            if (temp == 0)
                            {
                                str += "待机状态";
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x0001) == 1)
                            {
                                str += "急停状态";
                                myRgvGlobalInfo.RgvIsAlarm = 1;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x2) == 2)
                            {
                                str += "位置数据异常";
                                myRgvGlobalInfo.RgvIsAlarm = 2;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x0004) == 4)
                            {
                                str += "前限位异常";
                                myRgvGlobalInfo.RgvIsAlarm = 4;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x0008) == 8)
                            {
                                str += "后限位异常";
                                myRgvGlobalInfo.RgvIsAlarm = 8;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x10) == 0x10)
                            {
                                str += "驱动电机异常";
                                myRgvGlobalInfo.RgvIsAlarm = 10;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x20) == 0x20)
                            {
                                str += "前减速光电异常";
                                myRgvGlobalInfo.RgvIsAlarm = 20;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x40) == 0x40)
                            {
                                str += "后减速光电异常";
                                myRgvGlobalInfo.RgvIsAlarm = 40;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x80) == 0x80)
                            {
                                str += "前障碍物报警";
                                myRgvGlobalInfo.RgvIsAlarm = 80;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x100) == 0x100)
                            {
                                str += "前障碍物停车";
                                myRgvGlobalInfo.RgvIsAlarm = 100;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x200) == 0x200)
                            {
                                str += "后障碍物报警";
                                myRgvGlobalInfo.RgvIsAlarm = 1;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x0400) == 0x400)
                            {
                                str += "后障碍物停车";
                                myRgvGlobalInfo.RgvIsAlarm = 1;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x0800) == 0x800)
                            {
                                str += "上位机紧急停车";
                                myRgvGlobalInfo.RgvIsAlarm = 1;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x1000) == 0x1000)
                            {
                                str += "运行参数设置异常";
                                myRgvGlobalInfo.RgvIsAlarm = 1;
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.STOP;
                            }
                            if ((temp & 0x2000) == 0x2000)
                            {
                                str += "正向运动过程中";
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.RUN;
                            }
                            if ((temp & 0x4000) == 0x4000)
                            {
                                str += "反向运动过程中";
                                myRgvGlobalInfo.RgvRunStatMonitor = EquipmentStatus.RUN;
                            }
                            myRgvGlobalInfo.RgvCurrentStat = str;

                            //得到设定参数状态
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[27];
                            temp4 = ReceiveBuf[28];
                            temp = (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentParaSetStat = temp;

                            //得到当前电池状态
                            temp1 = 0; temp2 = 0; temp3 = 0; temp4 = 0; temp = 0;
                            temp3 = ReceiveBuf[29];
                            temp4 = ReceiveBuf[30];
                            temp = (temp3 << 8) + temp4;
                            myRgvGlobalInfo.RgvCurrentPowerStat = temp;
                        }
                    }

                    //发送事件
                    GetRgvModInfo?.Invoke(myRgvGlobalInfo);
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }));
        }

        /// <summary>
        /// Rgv执行任务
        /// </summary>
        private bool DoRgvOpCmdHandle(RgvCmd rgvcmd)
        {
            try
            {
                byte[] sendby;
                switch (rgvcmd)
                {
                    //连接RGV
                    case RgvCmd.CONNECT:
                        RgvConnect();
                        break;
                    //断开RGV
                    case RgvCmd.DISCONNECT:
                        RgvDisConnect();
                        break;
                    //正常停止
                    case RgvCmd.NORMALSTOP:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_NormalStop();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //正向运动
                    case RgvCmd.FORWARDMOTOR:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_ForwardMotor();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //反向运动
                    case RgvCmd.BACKWARDMOTOR:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_BackwardMotor();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //清除报警
                    case RgvCmd.CLEARALARM:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_ClearAlarm();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //开始充电
                    case RgvCmd.STARTPOWERCHARGE:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_StartPowerCharge();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //停止充电
                    case RgvCmd.STOPPOWERCHARGE:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_StopPowerCharge();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //设置目标速度
                    case RgvCmd.SETTARGETSPEED:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_SetTargetSpeed(myRgvGlobalInfo.RgvTargetRunSpeed);
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //设置目标距离
                    case RgvCmd.SETTARGETDISATNCE:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_SetTargetDistance(myRgvGlobalInfo.RgvTargetRunDistance);
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //设置轨道长度
                    case RgvCmd.SETTRACKLENGTH:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_SetTrackLength(myRgvGlobalInfo.RgvTrackLength);
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                    //运动到指定位置
                    case RgvCmd.RUNAPPOINTDISTANCE:
                        sendby = RgvModCtrlFun.RgvModCtrlCmd_RunAppointDistance();
                        myRgvClientSock?.SendByteAsync(sendby, sendby.Length);
                        break;
                }
                SetRgvModCmd?.Invoke(rgvcmd);
                return true;
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
                return false;
            }
        }

        public bool RgvConnect()
        {
            try
            {
                //创建Socket连接
                if (myRgvClientSock != null && IsEnable == true)
                {
                    return true;
                }

                myRgvClientSock = new TcpClientSocket(ip, port);
                myRgvClientSock.recvByteEvent += new Action<byte[], int>(RgvModRecvByteData);
                SetRgvModCmd?.Invoke(RgvCmd.INVALID);
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
                return false;
            }

        reset:
            try
            {
                bool ret = myRgvClientSock.Start(1);
                if (ret == true)
                {
                    //创建定时器:50ms间隔的定时器
                    InitTimer();

                    //更新状态
                    IsEnable = true;
                    myRgvGlobalInfo.ModWorkMsg = "Rgv小车已连接";
                    myRgvGlobalInfo.ModWorkCode = 0;

                    myRgvGlobalInfo.RgvCurrentRunDistacnce = 0;
                    myRgvGlobalInfo.RgvCurrentRunSpeed = 0;
                    myRgvGlobalInfo.RgvCurrentPowerElectricity = 100;

                    myRgvGlobalInfo.RgvTargetRunSpeed = 500;
                    myRgvGlobalInfo.RgvTrackLength = 1000000;

                    //发送事件
                    GetRgvModInfo?.Invoke(myRgvGlobalInfo);
                    SetRgvModCmd?.Invoke(RgvCmd.CONNECT);
                    myRgvGlobalInfo.ModWorkMsg.AddLog(LogType.RgvLog);
                }
                else
                {
                    //更新状态
                    myRgvGlobalInfo.ModWorkMsg = "Rgv已断开或网络异常";
                    myRgvGlobalInfo.ModWorkCode = 0;

                    myRgvGlobalInfo.RgvCurrentRunDistacnce = 0;
                    myRgvGlobalInfo.RgvCurrentRunSpeed = 0;
                    myRgvGlobalInfo.RgvCurrentPowerElectricity = 100;

                    //发送事件
                    GetRgvModInfo?.Invoke(myRgvGlobalInfo);
                    SetRgvModCmd?.Invoke(RgvCmd.DISCONNECT);
                    myRgvGlobalInfo.ModWorkMsg.AddLog(LogType.RgvLog);

                    "RGV尝试重连...".AddLog(LogType.RgvLog);
                    Thread.Sleep(3000);
                    goto reset;
                }
            }
            catch (Exception e)
            {
                e.StackTrace.AddLog(LogType.ErrorLog);
                SetRgvModCmd?.Invoke(RgvCmd.DISCONNECT);
                "RGV尝试重连...".AddLog(LogType.RgvLog);
                Thread.Sleep(3000);
                goto reset;
            }
            return true;
        }

        public bool RgvDisConnect()
        {
            if (myRgvClientSock != null || (IsEnable == true))
            {
                try
                {
                    //关闭socket
                    myRgvClientSock.CloseClientSocket();
                }
                catch (Exception e)
                {
                    e.StackTrace.AddLog(LogType.ErrorLog);
                }
            }

            //更新状态
            IsEnable = false;
            myRgvGlobalInfo.ModWorkMsg = "Rgv已断开或网络异常";
            myRgvGlobalInfo.ModWorkCode = 0;

            myRgvGlobalInfo.RgvCurrentRunDistacnce = 0;
            myRgvGlobalInfo.RgvCurrentRunSpeed = 0;
            myRgvGlobalInfo.RgvCurrentPowerElectricity = 100;

            //发送事件
            GetRgvModInfo?.Invoke(myRgvGlobalInfo);
            SetRgvModCmd?.Invoke(RgvCmd.DISCONNECT);
            return true;
        }

        public bool RgvForwardMove()
        {
            return DoRgvOpCmdHandle(RgvCmd.FORWARDMOTOR);
        }

        public bool RgvBackMove()
        {
            return DoRgvOpCmdHandle(RgvCmd.BACKWARDMOTOR);
        }

        public bool RgvNormalStop()
        {
            return DoRgvOpCmdHandle(RgvCmd.NORMALSTOP);
        }

        public bool RgvEmergeStop()
        {
            return DoRgvOpCmdHandle(RgvCmd.NORMALSTOP);
        }

        public bool RgvClearAlarm()
        {
            return DoRgvOpCmdHandle(RgvCmd.CLEARALARM);
        }

        public bool RgvIntelligentCharging()
        {
            return DoRgvOpCmdHandle(RgvCmd.STARTPOWERCHARGE);
        }

        public bool RgvTerminateCharging()
        {
            return DoRgvOpCmdHandle(RgvCmd.STOPPOWERCHARGE);
        }

        public bool RgvSetSpeed(int speed)
        {
            myRgvGlobalInfo.RgvTargetRunSpeed = speed;
            return DoRgvOpCmdHandle(RgvCmd.SETTARGETSPEED);
        }

        public bool RgvSetDistance(int distance)
        {
            myRgvGlobalInfo.RgvTargetRunDistance = distance;
            return DoRgvOpCmdHandle(RgvCmd.SETTARGETDISATNCE);
        }

        public bool RgvSetLength(int length)
        {
            myRgvGlobalInfo.RgvTrackLength = length;
            return DoRgvOpCmdHandle(RgvCmd.SETTRACKLENGTH);
        }

        public bool RgvRunDistance()
        {
            return DoRgvOpCmdHandle(RgvCmd.RUNAPPOINTDISTANCE);
        }
    }
}
