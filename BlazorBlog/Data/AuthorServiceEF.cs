using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class AuthorServiceEF : IAuthorService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public AuthorServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        /// <summary>
        /// Creates an author with the provided author parameter
        /// </summary>
        /// <param name="author"></param>
        /// <returns>Task</returns>
        public async Task CreateAuthor(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an author or null with the provided author id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Author>? GetAuthorByID(int id)
        {
            var result = await _context.Author.Where(x => x.AuthorId == id).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Updates an author with the provided author object
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public async Task UpdateAuthor(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a list of available authors
        /// </summary>
        /// <returns>Task<IEnumerable<Author>></returns>
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Author
                .ToListAsync();
        }
    }
}
