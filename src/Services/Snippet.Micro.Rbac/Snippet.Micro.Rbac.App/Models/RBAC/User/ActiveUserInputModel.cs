namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class ActiveUserInputModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}