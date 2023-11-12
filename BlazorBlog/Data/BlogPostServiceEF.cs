using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class BlogPostServiceEF : IBlogPostService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public BlogPostServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        /// <summary>
        /// Creates a blog category using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task CreateBlogPost(BlogPost blogPost)
        {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a list of all blog posts which are published and not archived
        /// </summary>
        /// <returns>Task<IEnumerable<BlogPost>></returns>
        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await _context.BlogPost
                .Where(x => !x.Archived && x.Published)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a list of all blog posts regardless of the published & archived status
        /// </summary>
        /// <returns>Task<IEnumerable<BlogPost>></returns>
        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsForEditor()
        {
            return await _context.BlogPost
                .Include(x => x.Category)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a blog post or null using the provided blog post Id guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<BlogPost>?</returns>
        public async Task<BlogPost>? GetBlogPostByID(Guid id)
        {
            var result = await _context.BlogPost
            .Include(x => x.Category)
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.BlogPostId == id);
            return result;
        }

        /// <summary>
        /// Retrieves a blog post or null using the provided normalized blog post name 
        /// </summary>
        /// <param name="normalizedTitle"></param>
        /// <returns></returns>
        public async Task<BlogPost>? GetBlogPostByUrl(string normalizedTitle)
        {
            var result = await _context.BlogPost
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Where(x => x.NormalizedTitle == normalizedTitle)
            .FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Retrives a list of blog posts contained in a category using the provided normalized category name parameter
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Task<IEnumerable<BlogPost>></returns>
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

        /// <summary>
        /// Gets a list of blog posts for use on the home page, featuring the most recent post
        /// </summary>
        /// <returns>Task<Tuple<IEnumerable<BlogPost>, BlogPost>></returns>
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

        /// <summary>
        /// Gets a slice of published not-archived blog posts using the provided take and skip parameters
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns>Task<IEnumerable<BlogPost>></returns>
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

        /// <summary>
        /// Updates a blog post using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task UpdateBlogPost(BlogPost blogPost)
        {
            _context.BlogPost.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        /// <summary>
        /// Archives (deletes) a blog post using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task ArchiveBlogPost(BlogPost blogPost)
        {
            blogPost.Archived = true;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        /// <summary>
        /// De-arvives a blog post using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task DeArchiveBlogPost(BlogPost blogPost)
        {
            blogPost.Archived = false;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        /// <summary>
        /// Publishes a blog post using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task PublishBlogPost(BlogPost blogPost)
        {
            blogPost.Published = true;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }

        /// <summary>
        /// Depublishes a blog post using the provided blog post parameter
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns>Task</returns>
        public async Task DePublishBlogPost(BlogPost blogPost)
        {
            blogPost.Published = false;
            _context.Update(blogPost);
            await _context.SaveChangesAsync();
            await _context.Entry(blogPost).ReloadAsync();
        }
    }
}
