using EMU.Parameter;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EMU.Interface
{
    public interface Iinit
    {
        void Setup(IProject project, UserControl userControl);
        void Setup(out Dictionary<int, ITask> taskDict);
        void Setup(out Dictionary<TaskName, ITask> taskDict);
    }
}
