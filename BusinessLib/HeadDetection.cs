using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class HeadDetection : IHeadDetection
    {
        public const int taskIndex = 3;
        private double height = 0;
        private HeadDetection()
        {
            Laser.LaserManager.Instance.GetLaserData += Instance_GetLaserData;
        }
        private static HeadDetection instance;
        public static HeadDetection Instance
        {
            get
            {
                return instance ?? (instance = new HeadDetection());
            }
        }

        public int GetHeadPosition(IMainTask task)
        {
            task.SetTaskLocation("车头位置检测", taskIndex);
            "开始车头检测...".AddLog(LogType.ProcessLog);
            return Programme1();
        }

        /// <summary>
        /// 方案1
        /// </summary>
        private int Programme1()
        {
            int TrainCurrentHeadDistance = 0;
            while (true)
            {
                if (!Global.RunStat)
                {
                    "任务终止".AddLog(LogType.ProcessLog);
                    break;
                }

                if (height > 0.6 && height < 3)
                {
                    TrainCurrentHeadDistance = RgvGlobalInfo.Instance.RgvCurrentRunDistacnce;
                    ("记录车头位置：" + TrainCurrentHeadDistance).AddLog(LogType.ProcessLog);
                    break;
                }
                Thread.Sleep(1);
            }
            return TrainCurrentHeadDistance;
        }

        private void Instance_GetLaserData(string data, LaserName laser)
        {
            if (laser == LaserName.RangingLaser)
            {
                if (double.TryParse(data, out _))
                {
                    height = double.Parse(data);
                }
            }
        }
    }

    public class HeadTask : ITask, ISign
    {
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
            while (!rgvControl.RgvSetSpeed(RgvGlobalInfo.Instance.RgvTargetRunSpeed) ||
                   !rgvControl.RgvSetLength(RgvGlobalInfo.Instance.RgvTrackLength) ||
                   !rgvControl.RgvSetDistance(RgvGlobalInfo.Instance.RgvTargetRunDistance))
            {
                Thread.Sleep(50);
            }
            while (!rgvControl.RgvRunDistance())
            {
                Thread.Sleep(50);
            }
            Global.HeadDistance = HeadDetection.Instance.GetHeadPosition(task);
            SetSign("车头");
            complete = true;
        }
    }
}
