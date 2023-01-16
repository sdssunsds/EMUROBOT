using EMU.Interface;
using Rgv;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class ClearAlarmTask : ITask
    {
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = RgvModCtrlHelper.Instance.RgvClearAlarm();
        }
    }
}
