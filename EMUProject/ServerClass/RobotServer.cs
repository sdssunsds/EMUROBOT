using EMU.ApplicationData;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Project.ServerClass
{
    internal class RobotServer
    {
        private static RobotServer instance = null;
        public static RobotServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RobotServer();
                }
                return instance;
            }
        }

        private object lockLink = new object();
        private TcpServiceSocket server = null;
        private Dictionary<string, bool> links = null;
        private Dictionary<string, Socket> clients = null;
        private Dictionary<Cmd, string> CmdRecvDict = null;
        /// <summary>
        /// 超时等待时间，单位秒
        /// </summary>
        public int OutTime { get; set; } = 30;
        public event Action<string> LinkEvent;
        public event Action<SocketRgvInfo> SetInfoEvent;
        private RobotServer()
        {
            links = new Dictionary<string, bool>();
            clients = new Dictionary<string, Socket>();
            CmdRecvDict = new Dictionary<Cmd, string>();
            string[] vs = Properties.Settings.Default.控制服务地址.Split(':');
            server = new TcpServiceSocket(vs[0], int.Parse(vs[1]), 10);
            server.recvMessageEvent = Recv;
            server.Start();
            ThreadManager.BackTask((int i, ThreadEventArgs threadEventArgs) =>
            {
                if (i % 10 > 0)
                {
                    return;
                }
                lock (lockLink)
                {
                    List<string> noLinkID = new List<string>();
                    foreach (KeyValuePair<string, bool> item in links)
                    {
                        if (item.Value)
                        {
                            noLinkID.Add(item.Key);
                        }
                        else
                        {
                            ServerGlobal.LinkRobotList.Remove(item.Key);
                        }
                    }
                    foreach (string item in noLinkID)
                    {
                        links[item] = false;
                    }
                }
            });
        }
        private void Recv(Socket socket, string msg)
        {
            string[] vs = msg.Split('/');
            if (msg.Contains("/robot_online/"))
            {
                if (!clients.ContainsKey(vs[2]))
                {
                    clients.Add(vs[2], null);
                }
                if (!links.ContainsKey(vs[2]))
                {
                    lock (lockLink)
                    {
                        links.Add(vs[2], true);  
                    }
                }
                else
                {
                    links[vs[2]] = true;
                }
                clients[vs[2]] = socket;
                LinkEvent?.Invoke(vs[2]);
            }
            else if (msg.Contains("/robot_state/"))
            {
                SocketRgvInfo info = JsonManager.JsonToObject<SocketRgvInfo>(vs[2]);
                if (!clients.ContainsKey(info.ID))
                {
                    clients.Add(info.ID, socket);
                }
                clients[info.ID] = socket;
                if (links.ContainsKey(info.ID))
                {
                    links[info.ID] = true;
                }
                if (ServerGlobal.RgvJob.ContainsKey(info.ID))
                {
                    ServerGlobal.RgvJob[info.ID] = info.Job;
                }
                else
                {
                    ServerGlobal.RgvJob.Add(info.ID, info.Job);
                }
                if (ServerGlobal.RgvList.ContainsKey(info.ID))
                {
                    ServerGlobal.RgvList[info.ID] = info;
                }
                else
                {
                    ServerGlobal.RgvList.Add(info.ID, info);
                }
                SetInfoEvent?.Invoke(info);
                LinkEvent?.Invoke(info.ID);
            }
            else
            {
                Cmd cmd = (Cmd)Enum.Parse(typeof(Cmd), vs[1]);
                if (!CmdRecvDict.ContainsKey(cmd))
                {
                    CmdRecvDict.Add(cmd, "");
                }
                CmdRecvDict[cmd] = vs[2];
            }
        }
        public void Command(Cmd cmd, string mode = null, string sn = null, string robotID = null, string pars = null)
        {
            if (string.IsNullOrEmpty(robotID))
            {
                robotID = ServerGlobal.SelectRobotID;
            }
            if (!clients.ContainsKey(robotID))
            {
                return;
            }
            if (CmdRecvDict.ContainsKey(cmd))
            {
                CmdRecvDict[cmd] = "";
            }
            try
            {
            links:
                if (clients[robotID].Connected)
                {
                    string content = cmd.ToString();
                    if (cmd == Cmd.start_work)
                    {
                        content += "/" + robotID + "," + mode + "," + sn;
                    }
                    else if (cmd == Cmd.robot_linedata)
                    {
                        content += "/" + JsonManager.ObjectToJson(ServerGlobal.DataBase.GetTs<FowardDataModel>(null, mode + "_" + sn));
                    }
                    else if (cmd == Cmd.robot_backdata)
                    {
                        List<BackDataModel> list = ServerGlobal.DataBase.GetTs<BackDataModel>(null, mode + "_" + sn);
                        List<BackDataExtend> tmp = new List<BackDataExtend>();
                        foreach (BackDataModel item in list)
                        {
                            tmp.Add(new BackDataExtend(item));
                        }
                        content += "/" + JsonManager.ObjectToJson(tmp);
                    }
                    else if (!string.IsNullOrEmpty(pars))
                    {
                        content += "/" + pars;
                    }
                    server.SendAsync(clients[robotID], "START/" + content + "/END"); 
                }
                else
                {
                    goto links;
                }
            }
            catch (Exception e)
            {
                e.Message.AddLog(EMU.Parameter.LogType.ErrorLog);
            }
        }
        public void GetRecv(Cmd cmd, Action recvAct, Action<string> error)
        {
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                int len = OutTime * 10;
                for (int i = 0; i < len; i++)
                {
                    string msg = GetRecv(cmd);
                    if (string.IsNullOrEmpty(msg))
                    {
                        Thread.Sleep(100);
                    }
                    else if (bool.TryParse(msg, out _))
                    {
                        recvAct?.Invoke();
                        break;
                    }
                    else
                    {
                        error?.Invoke(msg);
                    }
                }
            });
        }
        public string GetRecv(Cmd cmd)
        {
            if (CmdRecvDict.ContainsKey(cmd))
            {
                return CmdRecvDict[cmd];
            }
            return "";
        }
    }
    internal enum Cmd
    {
        start_work,
        stop_work,
        home_work,
        forward_start,
        backward_start,
        powercharge,
        powerstop,
        clear_alarm,
        robot_zero,
        robot_check,
        forward,
        backward,
        rgv_run,
        rgv_stop,
        led_power_on,
        led_power_off,
        front_camera_power_on,
        front_camera_power_off,
        front_sliding_power_on,
        front_sliding_power_off,
        back_camera_power_on,
        back_camera_power_off,
        back_sliding_power_on,
        back_sliding_power_off,
        line_camera_power_on,
        line_camera_power_off,
        set_speed,
        set_distance,
        set_length,
        powerdown,
        robot_linedata,
        robot_backdata,
        robot_outtime_alarm,
        robot_driver_alarm,
        robot_plc_alarm,
        robot_camera_alarm,
        robot_upload_error
    }
    public class SocketRgvInfo
    {
        #region RGV
        public string ID { get; set; }
        public int RgvCurrentRunSpeed { get; set; }
        public int RgvCurrentRunDistacnce { get; set; }
        public int RgvCurrentPowerStat { get; set; }
        public int RgvCurrentPowerElectricity { get; set; }
        public int RgvCurrentPowerCurrent { get; set; }
        public int RgvCurrentPowerTempture { get; set; }
        public string RgvCurrentMode { get; set; }
        public string RgvCurrentStat { get; set; }
        public string RgvCurrentCmdSetStat { get; set; }
        public int RgvCurrentParaSetStat { get; set; }
        public int RgvIsAlarm { get; set; }
        public int RgvIsStandby { get; set; }
        public int RgvTargetRunSpeed { get; set; }
        public int RgvTargetRunDistance { get; set; }
        public int RgvTrackLength { get; set; }
        public string RgvRunStatMonitor { get; set; }
        #endregion
        #region 线阵
        public int DataLine { get; set; }
        public int CarriageId { get; set; }
        public int CarriageType { get; set; }
        public int RgvCheckMinDistacnce { get; set; }
        #endregion
        #region 面阵
        public int Rgv_Distance { get; set; }
        public string Front_Parts_Id { get; set; }
        public string FrontComponentType { get; set; }
        public string FrontRobot_J1 { get; set; }
        public string FrontRobot_J2 { get; set; }
        public string FrontRobot_J3 { get; set; }
        public string FrontRobot_J4 { get; set; }
        public string FrontRobot_J5 { get; set; }
        public string FrontRobot_J6 { get; set; }
        public string FrontComponentId { get; set; }
        public int Front3d_Id { get; set; }
        public string Back_Parts_Id { get; set; }
        public string BackComponentType { get; set; }
        public string BackRobot_J1 { get; set; }
        public string BackRobot_J2 { get; set; }
        public string BackRobot_J3 { get; set; }
        public string BackRobot_J4 { get; set; }
        public string BackRobot_J5 { get; set; }
        public string BackRobot_J6 { get; set; }
        public string BackComponentId { get; set; }
        public int Back3d_Id { get; set; }
        #endregion
        #region 通用信息
        public string TrainMode { get; set; }
        public string TrainSn { get; set; }
        public int TrainCurrentHeadDistance { get; set; }
        public bool FrontRobotConnStat { get; set; }
        public string FrontRobotRunStatMonitor { get; set; }
        public bool BackRobotConnStat { get; set; }
        public string BackRobotRunStatMonitor { get; set; }
        public string Log { get; set; }
        public string Job { get; set; }
        #endregion
    }
}
