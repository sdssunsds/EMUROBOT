using EMU.Interface;
using EMU.Parameter;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class StopTask : ITask
    {
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = false;
            if (Global.RunStat)
            {
                Global.RunStat = false;
                Global.RunWait = false;
                while (!rgvControl.RgvEmergeStop())
                {
                    Thread.Sleep(50);
                }
                while (!robotControl.RobotZeroPosition(RobotName.Front))
                {
                    Thread.Sleep(50);
                }
                while (!robotControl.RobotZeroPosition(RobotName.Back))
                {
                    Thread.Sleep(50);
                } 
            }
            else
            {
                while (!robotControl.RobotZeroPosition(RobotName.Front))
                {
                    Thread.Sleep(50);
                }
                while (!robotControl.RobotZeroPosition(RobotName.Back))
                {
                    Thread.Sleep(50);
                }
                while (!rgvControl.RgvIntelligentCharging())
                {
                    Thread.Sleep(50);
                }
            }
            complete = true;
        }
    }
}
