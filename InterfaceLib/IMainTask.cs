using EMU.ApplicationData;
using EMU.Parameter;
using System;
using System.Drawing;

namespace EMU.Interface
{
    public interface IMainTask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task">任务名称</param>
        /// <param name="complete">执行完成委托</param>
        void RunTask(TaskName task, Action complete);
        /// <summary>
        /// 设置显示的图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="camera">对应的相机</param>
        void SetImage(Image image, CameraName camera);
        /// <summary>
        /// 设置显示的日志
        /// </summary>
        /// <param name="log">日志</param>
        void SetLog(string log);
        /// <summary>
        /// 设置Rgv信息
        /// </summary>
        /// <param name="rgv">Rgv信息</param>
        void SetRgvInfo(RgvGlobalInfo rgv);
        /// <summary>
        /// 设置机械臂信息
        /// </summary>
        /// <param name="robot">机械臂信息</param>
        void SetRobotInfo(RobotGlobalInfo robot);
        /// <summary>
        /// 设置任务执行位置
        /// </summary>
        /// <param name="taskContext">任务内容</param>
        /// <param name="taskProgress">任务进度</param>
        void SetTaskLocation(string taskContext, int taskProgress);
    }
}
