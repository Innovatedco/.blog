using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class AuthorServiceEF : IAuthorService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        public AuthorServiceEF(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        public async Task CreateAuthor(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author>? GetAuthorByID(int id)
        {
            var result = await _context.Author.Where(x => x.AuthorId == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAuthor(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Author
                .ToListAsync();
        }
    }
}
