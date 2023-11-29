using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class BlogCategoryServiceEF : IBlogCategoryService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public BlogCategoryServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        /// <summary>
        /// Creates blog categories with the provided list parameter
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>Task</returns>
        public async Task CreateBlogCategories(IEnumerable<BlogCategory> categories)
        {
            foreach (var category in categories)
            {
                _context.BlogCategory.Add(category);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a blog category with the provided category parameter
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Task</returns>
        public async Task CreateBlogCategory(BlogCategory category)
        {
            _context.BlogCategory.Add(category);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a list of all available categories ordered by category name
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BlogCategory>> GetAllCategories()
        {
            return await _context.BlogCategory
            .OrderBy(x => x.CategoryName)
            .ToListAsync();
        }

        /// <summary>
        /// Retrieves a blog category or null using the provided guid parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogCategory> GetCategoryById(Guid id)
        {
            return await _context.BlogCategory
            .FirstOrDefaultAsync(x => x.BlogCategoryId == id);
        }

        /// <summary>
        /// Rerieves a blog category or null using the provided normalized category name parameter
        /// </summary>
        /// <param name="normalizedCategoryName"></param>
        /// <returns>Task<BlogCategory> or Null</returns>
        public async Task<BlogCategory>? GetCategoryByUrl(string normalizedCategoryName)
        {
            return await _context.BlogCategory
                .Include(x => x.BlogPosts)
                .FirstOrDefaultAsync(x => x.CategoryNameNormalized == normalizedCategoryName);
        }

        /// <summary>
        /// Updates a blog category using the provided blog category parameter
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Task</returns>
        public async Task UpdateBlogCategory(BlogCategory category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            await _context.Entry(category).ReloadAsync();
        }

        /// <summary>
        /// Deletes a blog category using the provided blog category parameter
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task DeleteBlogCategory(BlogCategory category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
            await _context.Entry(category).ReloadAsync();
        }
    }
}
