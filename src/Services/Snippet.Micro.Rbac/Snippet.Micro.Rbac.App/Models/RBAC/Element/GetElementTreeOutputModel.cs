namespace Snippet.Micro.Rbac.App.Models.RBAC.Element
{
    public class GetElementTreeOutputModel
    {
        /// <summary>
        /// 元素树主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 元素树标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 元素树类型
        /// </summary>
        public int Type { get; set; }

        public List<GetElementTreeOutputModel> Children { get; set; }
    }
}