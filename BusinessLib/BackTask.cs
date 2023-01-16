using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class BackTask : ITask, ISign
    {
        public const int taskIndex = 5;
        private bool complete = false;

        public event SetSign SetSign;
        public event Action<int, int> SetAlarm;
        public event Action ClearSign;
        public event Action EndSign;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = false;
            task.SetTaskLocation("部件检测流程", taskIndex);
            "开始面阵拍照...".AddLog(LogType.ProcessLog);
            Global.BackShotCount = 0;
            Programme1(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            complete = true;
        }

        private void Programme1(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            if (Global.FrontCameraIndex < 0)
            {
                "未找到前机械臂相机".AddLog(LogType.ProcessLog);
                return;
            }
            if (Global.BackCameraIndex < 0)
            {
                "未找到后机械臂相机".AddLog(LogType.ProcessLog);
                return;
            }

            Global.RunBack = true;
            bool isTop = true;
            bool frontStart = true;
            bool backStart = true;
            int groupId = -1;
            int frontId = -2;
            int backId = -3;
            int location = 0;
            string groupName = "";
            List<BackDataModel> frontList = Global.BackDataModels.FindAll(b => b.RobotName == RobotName.Front);
            List<BackDataModel> backList = Global.BackDataModels.FindAll(b => b.RobotName == RobotName.Back);

            Task.Run(() =>
            {
                while (frontStart || backStart)
                {
                    if (!Global.RunStat)
                    {
                        "任务终止".AddLog(LogType.ProcessLog);
                        return;
                    }
                    while (Global.RunWait)
                    {
                        Thread.Sleep(100);
                    }
                    while (RgvGlobalInfo.Instance.RgvRunStatMonitor == EquipmentStatus.RUN)
                    {
                        Thread.Sleep(50);
                    }
                    if (groupId < frontId && groupId < backId)
                    {
                        groupId = frontId;
                        while (!rgvControl.RgvSetDistance(location))
                        {
                            Thread.Sleep(50);
                        }
                        while (!robotControl.RobotZeroPosition(RobotName.Front) || !robotControl.RobotZeroPosition(RobotName.Back))
                        {
                            Thread.Sleep(50);
                        }
                        while (RobotGlobalInfo.Instance.FrontRobotRunStatMonitor == EquipmentStatus.RUN ||
                               RobotGlobalInfo.Instance.BackRobotRunStatMonitor == EquipmentStatus.RUN)
                        {
                            Thread.Sleep(100);
                        }
                        while (!rgvControl.RgvRunDistance())
                        {
                            Thread.Sleep(50);
                        }
                        while (RgvGlobalInfo.Instance.RgvRunStatMonitor == EquipmentStatus.RUN)
                        {
                            Thread.Sleep(50);
                        }
                        if (isTop)
                        {
                            SetSign("", true);
                        }
                        else
                        {
                            SetSign(groupName, false);
                        }
                    }
                }
            });
            Task.Run(() =>
            {
                foreach (BackDataModel item in frontList)
                {
                    if (item.RobotLocation.j1 == "0.00" && item.RobotLocation.j2 == "0.00" &&
                        item.RobotLocation.j3 == "0.00" && item.RobotLocation.j4 == "0.00" &&
                        item.RobotLocation.j5 == "0.00" && item.RobotLocation.j6 == "0.00")
                    {
                        continue;
                    }
                Shot:
                    if (!Global.RunStat)
                    {
                        "任务终止".AddLog(LogType.ProcessLog);
                        return;
                    }
                    while (Global.RunWait)
                    {
                        Thread.Sleep(100);
                    }
                    if (item.RgvRunDistacnce == location)
                    {
                        while (!robotControl.RobotMovePosition(RobotName.Front, item.RobotLocation))
                        {
                            Thread.Sleep(50);
                        }
                        while (RobotGlobalInfo.Instance.FrontRobotRunStatMonitor == EquipmentStatus.RUN)
                        {
                            Thread.Sleep(100);
                        }
                        if (item.CanSort)
                        {
                            switch (item.CameraType)
                            {
                                case CameraType.BaslerLineCamera:
                                    cameraControl[Global.FrontCameraIndex].ContinuousShot();
                                    break;
                                case CameraType.BaslerCamera:
                                    cameraControl[Global.FrontCameraIndex].OneShot();
                                    break;
                                case CameraType.Cognext3DCamera:
                                    tasks[_3DTask.taskIndex].TaskStart(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
                                    while (!tasks[_3DTask.taskIndex].TaskComplete())
                                    {
                                        Thread.Sleep(50);
                                    }
                                    break;
                                case CameraType.MovedCamera:
                                    tasks[MovedCameraTask.taskIndex].TaskStart(task, appServer, new ICameraControl[] { cameraControl[Global.FrontCameraIndex] }, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
                                    while (!tasks[MovedCameraTask.taskIndex].TaskComplete())
                                    {
                                        Thread.Sleep(50);
                                    }
                                    break;
                                case CameraType.HikCamera:
                                    cameraControl[Global.FrontCameraIndex].OneShot();
                                    break;
                            } 
                        }
                    }
                    else
                    {
                        frontId = item.GroupID;
                        location = item.RgvRunDistacnce;
                        groupName = item.GroupName;
                        Thread.Sleep(500);
                        goto Shot;
                    }
                }
                frontStart = false;
            });
            Task.Run(() =>
            {
                foreach (BackDataModel item in backList)
                {
                    if (item.RobotLocation.j1 == "0.00" && item.RobotLocation.j2 == "0.00" &&
                        item.RobotLocation.j3 == "0.00" && item.RobotLocation.j4 == "0.00" &&
                        item.RobotLocation.j5 == "0.00" && item.RobotLocation.j6 == "0.00")
                    {
                        continue;
                    }
                Shot:
                    if (!Global.RunStat)
                    {
                        "任务终止".AddLog(LogType.ProcessLog);
                        return;
                    }
                    while (Global.RunWait)
                    {
                        Thread.Sleep(100);
                    }
                    if (item.RgvRunDistacnce == location)
                    {
                        while (!robotControl.RobotMovePosition(RobotName.Back, item.RobotLocation))
                        {
                            Thread.Sleep(50);
                        }
                        while (RobotGlobalInfo.Instance.BackRobotRunStatMonitor == EquipmentStatus.RUN)
                        {
                            Thread.Sleep(100);
                        }
                        if (item.CanSort)
                        {
                            switch (item.CameraType)
                            {
                                case CameraType.BaslerLineCamera:
                                    cameraControl[Global.BackCameraIndex].ContinuousShot();
                                    break;
                                case CameraType.BaslerCamera:
                                    cameraControl[Global.BackCameraIndex].OneShot();
                                    break;
                                case CameraType.Cognext3DCamera:
                                    tasks[_3DTask.taskIndex].TaskStart(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
                                    while (!tasks[_3DTask.taskIndex].TaskComplete())
                                    {
                                        Thread.Sleep(50);
                                    }
                                    break;
                                case CameraType.MovedCamera:
                                    tasks[MovedCameraTask.taskIndex].TaskStart(task, appServer, new ICameraControl[] { cameraControl[Global.BackCameraIndex] }, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
                                    while (!tasks[MovedCameraTask.taskIndex].TaskComplete())
                                    {
                                        Thread.Sleep(50);
                                    }
                                    break;
                                case CameraType.HikCamera:
                                    cameraControl[Global.BackCameraIndex].OneShot();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        backId = item.GroupID;
                        Thread.Sleep(500);
                        goto Shot;
                    }
                }
                backStart = false;
            });
        }
    }
}
