using EMU.Parameter;
using System;

namespace EMU.Interface
{
    public interface IHomePage
    {
        string Title { get; set; }
        void CheckHeadDetection();
        void RunTask(TaskName task, Action complete);
    }
}
