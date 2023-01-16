using System.Collections.Generic;

namespace EMU.RolePermissions
{
    public interface IRoleData
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        bool AddRole(Role role);
        /// <summary>
        /// 添加用户
        /// </summary>
        bool AddUser(User user);
        /// <summary>
        /// 清理权限
        /// </summary>
        bool ClearJurisdiction();
        /// <summary>
        /// 删除角色
        /// </summary>
        bool DelRole(Role role);
        /// <summary>
        /// 删除用户
        /// </summary>
        bool DelUser(User user);
        /// <summary>
        /// 获取用户数量
        /// </summary>
        int GetUserCount();
        /// <summary>
        /// 获取角色数量
        /// </summary>
        int GetRoleCount();
        /// <summary>
        /// 获取权限数量
        /// </summary>
        int GetJurisdiction();
        /// <summary>
        /// 获取权限数据
        /// </summary>
        List<Jurisdiction> GetJurisdictionDatas();
        /// <summary>
        /// 获取角色数据
        /// </summary>
        List<Role> GetRoleDatas();
        /// <summary>
        /// 获取用户数据
        /// </summary>
        List<User> GetUserDatas();
        /// <summary>
        /// 用户登陆
        /// </summary>
        User Login(string loginName, string LoginPwd, ref string errorStr);
        /// <summary>
        /// 修改角色
        /// </summary>
        bool UpdRole(Role role);
        /// <summary>
        /// 修改用户
        /// </summary>
        bool UpdUser(User user);
        /// <summary>
        /// 存储权限
        /// </summary>
        bool SaveJurisdiction(List<Jurisdiction> jurisdictions);
    }
}
