using Blazor.Blog.Models;

namespace Blazor.Blog.Data
{
    public interface IAuthorService
    {
        Task CreateAuthor(Author author);
        Task UpdateAuthor(Author author);
        Task<Author> GetAuthorByID(int id);
        Task<IEnumerable<Author>> GetAllAuthors();
    }
}
