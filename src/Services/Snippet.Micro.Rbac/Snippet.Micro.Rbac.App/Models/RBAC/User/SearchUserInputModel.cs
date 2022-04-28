using Snippet.Micro.Common.Models;

namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class SearchUserInputModel : PagedInputModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int? Role { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public int? Org { get; set; }
    }
}