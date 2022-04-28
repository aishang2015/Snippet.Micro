namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class RemoveOrgMemberInputModel
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
    }
}