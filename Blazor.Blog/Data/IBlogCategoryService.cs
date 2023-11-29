using Blazor.Blog.Models;

namespace Blazor.Blog.Data
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategory>> GetAllCategories();
        Task<BlogCategory> GetCategoryByUrl(string normalizedCategoryName);
        Task<BlogCategory> GetCategoryById(Guid id);
        Task UpdateBlogCategory(BlogCategory category);
        Task CreateBlogCategory(BlogCategory category);
        Task CreateBlogCategories(IEnumerable<BlogCategory> categories);
        Task DeleteBlogCategory(BlogCategory category);
    }
}
