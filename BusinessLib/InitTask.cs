using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class InitTask : ITask
    {
        public const int taskIndex = 0;
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = false;
            task.SetTaskLocation("硬件检测", taskIndex);
            Programme1(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
        }

        /// <summary>
        /// 方案1
        /// </summary>
        private void Programme1(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            Task.Run(() =>
            {
                try
                {
                    "服务初始化".AddLog(LogType.ProcessLog);
                    appServer.CreateServer(Parameter.Properties.Settings.Default.AppServerIP + ":" + Parameter.Properties.Settings.Default.AppServerPort);
                    appServer.AcceptClient += AppServer_AcceptClient;
                    appServer.SendClient += AppServer_SendClient;
                    "完成服务初始化".AddLog(LogType.ProcessLog);

                    "相机初始化".AddLog(LogType.ProcessLog);
                    ("找到的相机数量: " + cameraControl.Length).AddLog(LogType.ProcessLog);
                    "完成相机初始化".AddLog(LogType.ProcessLog);

                    "RGV初始化".AddLog(LogType.ProcessLog);
                    if (rgvControl.RgvConnect())
                    {
                        "完成RGV初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "RGV初始化失败".AddLog(LogType.ProcessLog);
                    }

                    "机械臂初始化".AddLog(LogType.ProcessLog);
                    if (robotControl.RobotConnect(RobotName.Front))
                    {
                        "完成前机械臂初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "前机械臂初始化失败".AddLog(LogType.ProcessLog);
                    }

                    if (robotControl.RobotConnect(RobotName.Back))
                    {
                        "完成后机械臂初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "后机械臂初始化失败".AddLog(LogType.ProcessLog);
                    }

                    "测距激光初始化".AddLog(LogType.ProcessLog);
                    if (laserControl.LaserConnect(LaserName.RangingLaser))
                    {
                        "完成测距激光初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "测距激光初始化失败".AddLog(LogType.ProcessLog);
                    }

                    "光源初始化".AddLog(LogType.ProcessLog);
                    lightControl.PowerOn(LightName.LineCameraLight);
                    lightControl.PowerOn(LightName.FrontRobotLight);
                    lightControl.PowerOn(LightName.BackRobotLight);
                    Thread.Sleep(1000);
                    if (lightControl.LightConnect(LightName.LineCameraLight))
                    {
                        "完成线阵光源初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "线阵光源初始化失败".AddLog(LogType.ProcessLog);
                    }
                    if (lightControl.LightConnect(LightName.FrontRobotLight))
                    {
                        "完成前机械臂光源初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "前机械臂光源初始化失败".AddLog(LogType.ProcessLog);
                    }
                    if (lightControl.LightConnect(LightName.BackRobotLight))
                    {
                        "完成后机械臂光源初始化".AddLog(LogType.ProcessLog);
                    }
                    else
                    {
                        "后机械臂光源初始化失败".AddLog(LogType.ProcessLog);
                    }

                    "数据初始化".AddLog(LogType.ProcessLog);
                    RgvGlobalInfo.Instance.ID = Parameter.Properties.Settings.Default.RobotID;
                    RgvGlobalInfo.Instance.RgvTargetRunSpeed = 800;
                    RgvGlobalInfo.Instance.RgvTrackLength = 300000;
                    RgvGlobalInfo.Instance.RgvTargetRunDistance = 250000;

                    string path = Properties.Settings.Default.DataJsonSavePath + "\\FowardDataModel.json";
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            Global.FowardDataModels = JsonManager.JsonToObject<List<FowardDataModel>>(sr.ReadToEnd());
                        }
                    }
                    else
                    {
                        Global.FowardDataModels = new List<FowardDataModel>();
                        for (int i = 0; i < 8; i++)
                        {
                            FowardDataModel model = new FowardDataModel()
                            {
                                CarriageLength = i == 0 ? 28500 : 27500,
                                ShotCount = i ==0 ? 38 : 33
                            };
                        } 
                    }

                    path = Properties.Settings.Default.DataJsonSavePath + "\\BackDataModel.json";
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            Global.BackDataModels = JsonManager.JsonToObject<List<BackDataModel>>(sr.ReadToEnd());
                        }
                    }
                    else
                    {
                        Global.BackDataModels = new List<BackDataModel>(); 
                    }

                    "完成数据初始化".AddLog(LogType.ProcessLog);

                    complete = true;
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                }
            });
        }

        private void AppServer_SendClient(AppRspFrame msg)
        {
            string s = JsonManager.ObjectToJson(msg);
            s.AddLog(LogType.OtherLog);
            using (StreamWriter sw = new StreamWriter(Properties.Settings.Default.TaskJsonSave + "\\SendData_" + DateTime.Now.ToString("yy_MM_dd_HH_mm_ss") + ".json"))
            {
                sw.Write(s);
            }
        }

        private void AppServer_AcceptClient(AppDeviceFrame msg)
        {
            string s = JsonManager.ObjectToJson(msg);
            s.AddLog(LogType.OtherLog);
            using (StreamWriter sw = new StreamWriter(Properties.Settings.Default.TaskJsonSave + "\\AcceptData_" + DateTime.Now.ToString("yy_MM_dd_HH_mm_ss") + ".json"))
            {
                sw.Write(s);
            }
        }
    }
}
