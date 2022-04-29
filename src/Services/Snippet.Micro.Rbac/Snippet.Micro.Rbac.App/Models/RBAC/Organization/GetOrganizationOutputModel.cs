namespace Snippet.Micro.Rbac.App.Models.RBAC.Organization
{
    public class GetOrganizationOutputModel
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 组织上级id
        /// </summary>
        public int? UpId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组织代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}