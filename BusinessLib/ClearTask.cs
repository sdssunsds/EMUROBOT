using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.IO;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Business
{
    public class ClearTask : ITask
    {
        public const int taskIndex = 2;
        private bool complete = false;

        public bool TaskComplete()
        {
            return complete;
        }

        public void TaskStart(IMainTask task, IAppServer appServer, ICameraControl[] cameraControl, ILaserControl laserControl, ILightControl lightControl, IRgvControl rgvControl, IRobotControl robotControl, IService uploadImage, UserControl taskControl, params ITask[] tasks)
        {
            if (!Global.RunStat)
            {
                complete = true;
                return;
            }
            complete = false;
            "开始清理缓存图片".AddLog(LogType.ProcessLog);
            string taskPath = Properties.Settings.Default.ImgSavePath + "\\task_data";
            string testPath = Properties.Settings.Default.ImgSavePath + "\\test_data";
            string workPath = Properties.Settings.Default.ImgSavePath + "\\work_data";
            var deleteFiles = new Action<DirectoryInfo>((DirectoryInfo dir) => { });
            deleteFiles = new Action<DirectoryInfo>((DirectoryInfo dir) =>
            {
                foreach (FileInfo item in dir.GetFiles())
                {
                    try
                    {
                        File.Delete(item.FullName);
                    }
                    catch (Exception) { }
                }
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    deleteFiles(item);
                }
            });
            var deleteFile = new Action<string>((string path) =>
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                deleteFiles(directory);
            });
            ">>清理task_data文件夹下的缓存图片".AddLog(LogType.ProcessLog);
            deleteFile(taskPath);
            ">>清理test_data文件夹下的缓存图片".AddLog(LogType.ProcessLog);
            deleteFile(testPath);
            complete = true;
        }
    }
}
