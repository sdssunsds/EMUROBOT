using System.Windows.Forms;

namespace EMU.RolePermissions
{
    public class Jurisdiction
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 控制范围
        /// </summary>
        public ControlRange ControlRange { get; set; }
        /// <summary>
        /// 上级权限
        /// </summary>
        public Jurisdiction Parent { get; set; }
        /// <summary>
        /// 给控件设置权限
        /// </summary>
        public void SetControl(Control control, bool isHave)
        {
            string[] names = Path.Split('.');
            if (CheckName(control, names, names.Length - 1))
            {
                switch (ControlRange)
                {
                    case ControlRange.Enable:
                        control.Enabled = isHave;
                        break;
                    case ControlRange.Visible:
                        control.Visible = isHave;
                        break;
                }
            }
        }
        /// <summary>
        /// 读取控件路径
        /// </summary>
        public void ReadControl(Control control)
        {
            Path = AddName(control);
        }
        private string AddName(Control control)
        {
            if (control.Parent != null)
            {
                return AddName(control.Parent) + "." + control.Name;
            }
            return control.Name;
        }
        private bool CheckName(Control control, string[] names, int pathIndex)
        {
            if (pathIndex == 0 && control.Parent == null)
            {
                return control.Name == names[0];
            }
            else if (pathIndex > 0 && pathIndex < names.Length && control.Parent != null)
            {
                if (control.Name == names[pathIndex])
                {
                    return CheckName(control.Parent, names, pathIndex--);
                }
            }
            return false;
        }
    }
}
