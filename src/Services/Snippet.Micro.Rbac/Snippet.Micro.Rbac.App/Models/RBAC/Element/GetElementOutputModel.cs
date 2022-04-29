namespace Snippet.Micro.Rbac.App.Models.RBAC.Element
{
    public class GetElementOutputModel
    {
        /// <summary>
        /// 元素id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 元素名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元素类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 元素标识
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 访问的api
        /// </summary>
        public string AccessApi { get; set; }
    }
}