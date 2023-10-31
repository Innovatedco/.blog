using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class BlogPostServiceEF : IBlogPostService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        public BlogPostServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        public async Task CreateBlogPost(BlogPost blogPost)
        {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await _context.BlogPost
                .Where(x => !x.Archived && x.Published)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsForEditor()
        {
            return await _context.BlogPost
                .Include(x => x.Category)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostByID(Guid id)
        {
            return await _context.BlogPost
            .Include(x => x.Category)
            .Include(x => x.Author)
            .FirstAsync(x => x.BlogPostId == id);
        }

        public async Task<BlogPost>? GetBlogPostByUrl(string normalizedTitle)
        {
            return await _context.BlogPost
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Where(x => x.NormalizedTitle == normalizedTitle)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsByCategory(string category)
        {
            var result = await _context.BlogPost
            .Where(x => !x.Archived && x.Published)
            .Include(x => x.Category)
            .OrderByDescending(x => x.Created)
            .Where(x => x.Category!.CategoryNameNormalized == category)
            .ToListAsync();
            return result;          
        }

        public async Task<Tuple<IEnumerable<BlogPost>, BlogPost>> GetBlogPostsForHomePage()
        {
            var all = await _context.BlogPost
                .Where(x => !x.Archived && x.Published)
                .Include(x => x.Category)
                .Include(x => x.Author)
                .OrderByDescending(x => x.Created)
                .Take(10).ToListAsync();
            var recent = all.Skip(1).Take(9);
            var featured = all.FirstOrDefault();
            var returnType = Tuple.Create(recent, featured);
            return returnType;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostSlice(int take, int skip)
        {
            return await _context.BlogPost
            .Where(x => !x.Archived && x.Published)
            .Include(x => x.Category)
            .OrderByDescending(x => x.Created)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }

        public async Task UpdateBlogPost(BlogPost blogPost)
        {
            _context.BlogPost.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        public async Task ArchiveBlogPost(BlogPost blogPost)
        {
            blogPost.Archived = true;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        public async Task DeArchiveBlogPost(BlogPost blogPost)
        {
            blogPost.Archived = false;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        public async Task PublishBlogPost(BlogPost blogPost)
        {
            blogPost.Published = true;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        public async Task DePublishBlogPost(BlogPost blogPost)
        {
            blogPost.Published = false;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }
    }
}
