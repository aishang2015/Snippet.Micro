namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class SearchUserOutputModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleInfo[] UserRoles { get; set; }

        /// <summary>
        /// 用户组织信息
        /// </summary>
        public UserOrg[] UserOrgs { get; set; }
    }

    public class RoleInfo
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class UserOrg
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName { get; set; }
    }
}