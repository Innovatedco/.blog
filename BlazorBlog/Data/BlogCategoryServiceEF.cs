using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class BlogCategoryServiceEF : IBlogCategoryService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        public BlogCategoryServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }
        public async Task CreateBlogCategories(IEnumerable<BlogCategory> categories)
        {
            foreach (var category in categories)
            {
                _context.BlogCategory.Add(category);
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateBlogCategory(BlogCategory category)
        {
            _context.BlogCategory.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogCategory>> GetAllCategories()
        {
            return await _context.BlogCategory
            .OrderBy(x => x.CategoryName)
            .ToListAsync();
        }

        public async Task<BlogCategory> GetCategoryById(Guid id)
        {
            return await _context.BlogCategory
            .FirstAsync(x => x.BlogCategoryId == id);
        }

        public async Task<BlogCategory>? GetCategoryByUrl(string normalizedCategoryName)
        {
            return await _context.BlogCategory
                .Include(x => x.BlogPosts)
                .FirstOrDefaultAsync(x => x.CategoryNameNormalized == normalizedCategoryName);
        }

        public async Task UpdateBlogCategory(BlogCategory category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            await _context.Entry(category).ReloadAsync();
        }

        public async Task DeleteBlogCategory(BlogCategory category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
            await _context.Entry(category).ReloadAsync();
        }
    }
}
