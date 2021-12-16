namespace Snippet.Micro.RBAC.Models.Common
{
    public class PagedOutputModel<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}