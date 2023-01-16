using System.Collections.Generic;

namespace EMU.RolePermissions
{
    public class User
    {
        private Role role = null;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                Jurisdictions.Clear();
                Jurisdictions.AddRange(role.Jurisdictions);
            }
        }
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<Jurisdiction> Jurisdictions { get; private set; }
        public User()
        {
            Jurisdictions = new List<Jurisdiction>();
        }
    }
}
