namespace Snippet.Micro.Common.Models
{
    public class PagedInputModel
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public int TakeCount { get => Size; }

        public int SkipCount { get => Size * (Page - 1); }
    }
}