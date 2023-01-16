using EMU.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class _3DTask : ITask
    {
        public const int taskIndex = 7;
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            
        }
    }
}
