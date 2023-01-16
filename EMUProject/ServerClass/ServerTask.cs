using AlgorithmLib;
using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UploadImageServer;

namespace Project.ServerClass
{
    internal class ServerTask : ITask, ISign
    {
        private int xzDataIndex = 0;
        private IMainTask mainTask = null;

        public IProject Project;
        public event SetSign SetSign;
        public event Action<int, int> SetAlarm;
        public event Action ClearSign;
        public event Action EndSign;

        public bool TaskComplete()
        {
            return true;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            mainTask = task;
            appServer.CreateServer(Properties.Settings.Default.ServerPath);
            appServer.AcceptClient += AppServer_AcceptClient;
            RobotServer.Instance.SetInfoEvent += Instance_SetInfoEvent;
        }

        private void Instance_SetInfoEvent(SocketRgvInfo info)
        {
            string log = info.Log;
            if (!string.IsNullOrEmpty(log))
            {
                log.AddLog(LogType.GeneralLog);
            }
            if (ServerGlobal.SelectRobotID == info.ID)
            {
                mainTask.SetLog(log);
                if (ServerGlobal.StartRobotList.Count > 0)
                {
                    mainTask.SetTaskLocation(info.Job, ServerGlobal.StartRobotList.IndexOf(info.ID));  
                }
            }

            RgvGlobalInfo rgvGlobalInfo = new RgvGlobalInfo();
            rgvGlobalInfo.ID = info.ID;
            rgvGlobalInfo.RgvCurrentStat = info.RgvCurrentStat;
            rgvGlobalInfo.RgvCurrentRunDistacnce = info.RgvCurrentRunDistacnce;
            rgvGlobalInfo.RgvCurrentRunSpeed = info.RgvCurrentRunSpeed;
            rgvGlobalInfo.RgvTargetRunSpeed = info.RgvTargetRunSpeed;
            rgvGlobalInfo.RgvCurrentPowerElectricity = info.RgvCurrentPowerElectricity;
            rgvGlobalInfo.RgvCurrentPowerCurrent = info.RgvCurrentPowerCurrent;
            rgvGlobalInfo.RgvCurrentPowerTempture = info.RgvCurrentPowerTempture;
            rgvGlobalInfo.RgvTargetRunDistance = info.RgvTargetRunDistance;
            rgvGlobalInfo.RgvTrackLength = info.RgvTrackLength;
            rgvGlobalInfo.RgvIsAlarm = info.RgvIsAlarm;
            rgvGlobalInfo.RgvRunStatMonitor = (EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), info.RgvRunStatMonitor);
            mainTask.SetRgvInfo(rgvGlobalInfo);
            (Project.rgv as RgvTask).ActEvent();

            if (info.DataLine > -1)
            {
                if (info.TrainCurrentHeadDistance <= rgvGlobalInfo.RgvCurrentRunDistacnce)
                {
                    if (xzDataIndex != info.DataLine)
                    {
                        xzDataIndex = info.DataLine;
                        if (xzDataIndex == 0)
                        {
                            SetSignForward(info.ID + "," + xzDataIndex + "," + info.TrainCurrentHeadDistance, true);
                        }
                        else
                        {
                            SetSignForward(info.ID + "," + (xzDataIndex - 1) + "," + rgvGlobalInfo.RgvCurrentRunDistacnce, false);
                            SetSignForward(info.ID + "," + xzDataIndex + "," + rgvGlobalInfo.RgvCurrentRunDistacnce, true);
                        }
                    }
                }
            }
            else
            {
                ClearSign?.Invoke();
                xzDataIndex = -1;
            }

            RobotGlobalInfo robotGlobalInfo = null;
            if (info.Rgv_Distance > 0)
            {
                robotGlobalInfo = new RobotGlobalInfo();
                robotGlobalInfo.RobotID = info.ID;
                robotGlobalInfo.FrontRobotConnStat = info.FrontRobotConnStat;
                robotGlobalInfo.FrontRobotRunStatMonitor = (EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), info.FrontRobotRunStatMonitor);
                robotGlobalInfo.BackRobotConnStat = info.BackRobotConnStat;
                robotGlobalInfo.BackRobotRunStatMonitor = (EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), info.BackRobotRunStatMonitor);
                robotGlobalInfo.FrontRobotSiteData.j1 = info.FrontRobot_J1;
                robotGlobalInfo.FrontRobotSiteData.j2 = info.FrontRobot_J2;
                robotGlobalInfo.FrontRobotSiteData.j3 = info.FrontRobot_J3;
                robotGlobalInfo.FrontRobotSiteData.j4 = info.FrontRobot_J4;
                robotGlobalInfo.FrontRobotSiteData.j5 = info.FrontRobot_J5;
                robotGlobalInfo.FrontRobotSiteData.j6 = info.FrontRobot_J6;
                robotGlobalInfo.BackRobotSiteData.j1 = info.BackRobot_J1;
                robotGlobalInfo.BackRobotSiteData.j2 = info.BackRobot_J2;
                robotGlobalInfo.BackRobotSiteData.j3 = info.BackRobot_J3;
                robotGlobalInfo.BackRobotSiteData.j4 = info.BackRobot_J4;
                robotGlobalInfo.BackRobotSiteData.j5 = info.BackRobot_J5;
                robotGlobalInfo.BackRobotSiteData.j6 = info.BackRobot_J6;
            }
            if (robotGlobalInfo != null)
            {
                mainTask.SetRobotInfo(robotGlobalInfo); 
            }
        }

        private void AppServer_AcceptClient(AppDeviceFrame msg)
        {
            if (ServerGlobal.StartProjectDict.ContainsKey(msg.para.robot_id))
            {
                string key = ServerGlobal.StartProjectDict[msg.para.robot_id].ID;
                if (msg.cmd == AppServer.Data3D)
                {
                    AppDeviceFrameEx2 app = msg as AppDeviceFrameEx2;
                    PartsType partsType = PartsDict.GetPartsType(app.ParsID);
                    List<AlgorithmParemeter> paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, msg.para.train_mode + "_" + msg.para.train_sn);
                    List<input_task> inputList = null;
                    if (paremeters != null)
                    {
                        inputList = paremeters[0].InputTasks;
                    }
                    paremeters = null;
                    ServerGlobal.AddResultData(key, new ResultData()
                    {
                        ID = Convert.ToString((int)partsType, 16).ToHexData(4),
                        LengthID = app.ParsID,
                        Name = partsType.ToString(),
                        Data = app.Data,
                        ResultType = ResultType.ThrD
                    }, inputList);
                }
                else if (msg.cmd == AppServer.ImageMz)
                {
                    AppDeviceFrameEx1 app = msg as AppDeviceFrameEx1;
                    mainTask.SetImage(app.Img, app.RobotName == RobotName.Front ? CameraName.Front : CameraName.Back);
                }
                else if (msg.cmd == AppServer.ImageXz)
                {
                    AppDeviceFrameEx app = msg as AppDeviceFrameEx;
                    mainTask.SetImage(app.Img, CameraName.Line);
                } 
            }
        }

        private void SetSignForward(string txt, bool isStart)
        {
            SetSign?.Invoke(txt, isStart);
        }
    }
}
