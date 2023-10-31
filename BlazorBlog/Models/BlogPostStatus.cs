namespace Blazor.Blog.Models
{
    public class BlogPostStatus
    {
        public const string All = "All";
        public const string Published = "Published";
        public const string Deleted = "Deleted";
        public const string Drafts = "Drafts";
        public static readonly List<string> StatusTypes = new() { All, Published, Deleted, Drafts };
        
    }
}
