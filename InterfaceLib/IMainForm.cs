using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMU.Interface
{
    public interface IMainForm
    {
        void Load(string[] args);
        void Shown();
        void Closing();
    }
}
