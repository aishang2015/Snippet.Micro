namespace Snippet.Micro.Rbac.App.Models.RBAC.Role
{
    public class GetRoleOutputModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int[] Rights { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsActive { get; set; }
    }
}