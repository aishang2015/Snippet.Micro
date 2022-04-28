namespace Snippet.Micro.Common.Models
{
    public class PagedOutputModel<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}