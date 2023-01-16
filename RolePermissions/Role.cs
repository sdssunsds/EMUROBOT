using System.Collections.Generic;

namespace EMU.RolePermissions
{
    public class Role
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public List<Jurisdiction> Jurisdictions { get; private set; }
        public Role()
        {
            Jurisdictions = new List<Jurisdiction>();
        }
    }
}
