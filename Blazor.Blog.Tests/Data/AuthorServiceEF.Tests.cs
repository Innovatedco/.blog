using Blazor.Blog.Data;
using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;
using System.Linq;
using System.Threading.Tasks;
using static Blazor.Blog.Tests.HelperClasses.BlogPostHelpers;

namespace Blazor.Blog.Tests.Data
{
    public class AuthorServiceEFTests
    {
        [Fact(DisplayName = "Should get all Authors")]
        public async Task ShouldGetAllAuthors()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();

            // Act
            var initialCount = arrangeResult.RandomPostDto.List.Select(x => x.Author).Distinct().Count();
            var result = await arrangeResult.AuthorServiceEF.GetAllAuthors();
            var finalCount = result.Count();

            // Assert
            Assert.Equal(initialCount, finalCount);
        }

        [Fact(DisplayName = "Should create an Author")]
        public async Task ShouldCreateAnAuthor()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var newAuthor = new Author()
            {
                AuthorName = "Test",
                AuthorIcon = "Test Icon"
            };

            // Act
            var initialCount = arrangeResult.RandomPostDto.List.Select(x => x.Author).Distinct().Count();
            await arrangeResult.AuthorServiceEF.CreateAuthor(newAuthor);

            var result = await arrangeResult.AuthorServiceEF.GetAllAuthors();


            // Assert
            Assert.NotEqual(initialCount, result.Count());
        }
    }
}
