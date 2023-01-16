using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Interface
{
    public interface ITask
    {
        /// <summary>
        /// 任务开始
        /// </summary>
        /// <param name="task">主任务对象</param>
        void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks);
        /// <summary>
        /// 任务完成
        /// </summary>
        /// <returns>返回是否已经完成</returns>
        bool TaskComplete();
    }
}
