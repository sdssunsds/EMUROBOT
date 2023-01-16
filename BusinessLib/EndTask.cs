using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class EndTask : ITask
    {
        public const int taskIndex = 6;
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = false;
            Global.RunStat = false;
            while (!rgvControl.RgvIntelligentCharging())
            {
                Thread.Sleep(50);
            }
            "<---任务结束--->".AddLog(LogType.ProcessLog);
            complete = true;
        }
    }
}
