using Blazor.Blog.Models;

namespace Blazor.Blog.Data
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPosts();
        Task<Tuple<IEnumerable<BlogPost>, BlogPost>> GetBlogPostsForHomePage(); 
        Task<IEnumerable<BlogPost>> GetBlogPostSlice(int take, int skip);
        Task<IEnumerable<BlogPost>> GetBlogPostsByCategory(string category);
        Task<BlogPost>? GetBlogPostByUrl(string normalizedTitle);
        Task<BlogPost> GetBlogPostByID(Guid id);
        Task CreateBlogPost(BlogPost blogPost);
        Task UpdateBlogPost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsForEditor();
        Task ArchiveBlogPost(BlogPost blogPost);
        Task DeArchiveBlogPost(BlogPost blogPost);
        Task PublishBlogPost(BlogPost blogPost);
        Task DePublishBlogPost(BlogPost blogPost);
    }
}
