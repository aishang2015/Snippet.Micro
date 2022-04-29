namespace Snippet.Micro.Rbac.App.Models.RBAC.Organization
{
    public class GetOrganizationTreeOutputModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        public List<GetOrganizationTreeOutputModel> Children { get; set; }
    }
}