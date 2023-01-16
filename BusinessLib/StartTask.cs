using EMU.Interface;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class StartTask : ITask
    {
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            complete = false;
            Programme1(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            complete = true;
        }

        private void Programme1(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            RunTask(TopTask.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            RunTask(ClearTask.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            RunTask(HeadDetection.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            RunTask(ForwardTask.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            RunTask(BackTask.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            RunTask(EndTask.taskIndex, task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
        }

        private void RunTask(int index, IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            tasks[index].TaskStart(task, appServer, cameraControl, laserControl, lightControl, rgvControl, robotControl, uploadImage, taskControl, tasks);
            while (!tasks[index].TaskComplete())
            {
                Thread.Sleep(50);
            }
        }
    }
}
