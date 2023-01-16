using System.Collections.Generic;

namespace Project.ServerClass.Model
{
    public class ProjectResultDict
    {
        public string Mode { get; set; }
        public List<ModeSnDict> SnList { get; set; }
    }
    public class ModeSnDict
    {
        public string Sn { get; set; }
        public List<string> IdList { get; set; }
    }
}
