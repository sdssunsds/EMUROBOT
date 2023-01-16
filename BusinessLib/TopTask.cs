using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class TopTask : ITask
    {
        public const int taskIndex = 1;
        private bool complete = false;
        private bool frontRobot = false;
        private bool backRobot = false;

        public bool TaskComplete()
        {
            return complete && frontRobot && backRobot;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            if (!Global.RunStat)
            {
                complete = frontRobot = backRobot = true;
                return;
            }
            complete = frontRobot = backRobot = false;
            "<---任务开始--->".AddLog(LogType.ProcessLog);
            Global.Train_Mode = Global.TrainPara.train_mode;
            Global.Train_Sn = Global.TrainPara.train_sn;
            robotControl.GetRobotModInfo += Robot_GetRobotModInfo;
            ("动车参数：" + JsonManager.ObjectToJson(Global.TrainPara)).AddLog(LogType.OtherLog);
            "停止充电".AddLog(LogType.ProcessLog);
            if (!rgvControl.RgvTerminateCharging())
            {
                Thread.Sleep(50);
            }
            "前机械臂回归原点".AddLog(LogType.ProcessLog);
            while (!robotControl.RobotZeroPosition(RobotName.Front))
            {
                Thread.Sleep(50);
            }
            "后机械臂回归原点".AddLog(LogType.ProcessLog);
            while (!robotControl.RobotZeroPosition(RobotName.Back))
            {
                Thread.Sleep(50);
            }
        }

        private void Robot_GetRobotModInfo(RobotGlobalInfo robotinfo, RobotName robot)
        {
            switch (robot)
            {
                case RobotName.Front:
                    if (robotinfo.FrontRobotRunStatMonitor != EquipmentStatus.RUN)
                    {
                        frontRobot = true;
                    }
                    break;
                case RobotName.Back:
                    if (robotinfo.BackRobotRunStatMonitor != EquipmentStatus.RUN)
                    {
                        backRobot = true;
                    }
                    break;
            }
        }
    }
}
