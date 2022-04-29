namespace Snippet.Micro.Rbac.App.Models.RBAC.Role
{
    public class ActiveRoleInputModel
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}