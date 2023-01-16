using EMU.ApplicationData;
using EMU.ImageTransmission;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class ForwardTask : ITask, ISign
    {
        public const int taskIndex = 4;
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
            task.SetTaskLocation("车底检测流程", taskIndex);
            "开始线扫拍照...".AddLog(LogType.ProcessLog);
            Global.ForwardShotCount = 0;
            Programme1(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            complete = true;
        }

        /// <summary>
        /// 方案1
        /// </summary>
        private void Programme1(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            if (Global.LineCameraIndex < 0)
            {
                "未找到线扫相机".AddLog(LogType.ProcessLog);
                return;
            }

            Global.RunBack = false;
            cameraControl[Global.LineCameraIndex].ContinuousShot();
            int CarriageLengthCount = 0;
            int ShotCount = 0;
            int index = 1;
            foreach (FowardDataModel item in Global.FowardDataModels)
            {
                if (!Global.RunStat)
                {
                    "任务终止".AddLog(LogType.ProcessLog);
                    return;
                }

                CarriageLengthCount += item.CarriageLength;
                ShotCount += item.ShotCount;
                while (item.RgvGlobalInfo.RgvCurrentRunDistacnce < CarriageLengthCount && Global.ForwardShotCount < ShotCount)
                {
                    if (!Global.RunStat)
                    {
                        "任务终止".AddLog(LogType.ProcessLog);
                        return;
                    }
                    Thread.Sleep(100);
                }

                SetSign(string.Format("拍照总数：{0}\r\n位置：{1}", Global.ForwardShotCount, item.RgvGlobalInfo.RgvCurrentRunDistacnce), false);
                if (index > 1)
                {
                    SetSign(string.Format("第{0}节", index));
                }
                string.Format("已完成第{0}节>>当前位置：{1}，拍照数量：{2}", index, item.RgvGlobalInfo.RgvCurrentRunDistacnce, Global.ForwardShotCount).AddLog(LogType.ProcessLog);
                index++;
            }
            string.Format("Rgv当前位置: {0}", RgvGlobalInfo.Instance.RgvCurrentRunDistacnce).AddLog(LogType.ProcessLog);
            cameraControl[Global.LineCameraIndex].Stop();
            "线扫结束".AddLog(LogType.ProcessLog);
            rgvControl.RgvNormalStop();
            while (true)
            {
                if (!Global.RunStat)
                {
                    "任务终止".AddLog(LogType.ProcessLog);
                    return;
                }
                if (RgvGlobalInfo.Instance.RgvRunStatMonitor != EquipmentStatus.RUN)
                {
                    string.Format("Rgv停车位置: {0}", RgvGlobalInfo.Instance.RgvCurrentRunDistacnce).AddLog(LogType.ProcessLog);
                    break;
                }
                Thread.Sleep(50);
            }
            (uploadImage as UploadImages).UploadComplete();
        }
    }
}
