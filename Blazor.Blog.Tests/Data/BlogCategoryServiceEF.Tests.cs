using Blazor.Blog.Tests.HelperClasses;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Blazor.Blog.Tests.HelperClasses.BlogCategoryHelpers;
namespace Blazor.Blog.Tests.Data
{
    public class BlogCategoryServiceEFTests
    {
        [Fact(DisplayName = "Should Create Blog Categories")]
        public async Task ShouldCreateBlogCategories()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var categoriesToAdd = BuildBlogCategoriesFixture(3);

            // Act
            var initialCount = arrangeResult.RandomPostDto.List.Select(x => x.Category).Distinct().Count(); 
            await arrangeResult.BlogCategoryServiceEF.CreateBlogCategories(categoriesToAdd);
            var result = await arrangeResult.BlogCategoryServiceEF.GetAllCategories();
            var finalCount = result.Count();

            // Assert
            Assert.Equal(initialCount + 3, finalCount);
        }

        [Fact(DisplayName = "Should Create a Blog Category")]
        public async Task ShouldCreateABlogCategory()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var categoryToAdd = BuildBlogCategoryFixture();

            // Act
            var initialCount = arrangeResult.RandomPostDto.List.Select(x => x.Category).Distinct().Count();
            await arrangeResult.BlogCategoryServiceEF.CreateBlogCategory(categoryToAdd);
            var result = await arrangeResult.BlogCategoryServiceEF.GetAllCategories();
            var finalCount = result.Count();

            // Assert
            Assert.Equal(initialCount + 1, finalCount);
        }

        [Fact(DisplayName = "Should get all Blog Categories")]
        public async Task ShouldGetAllBlogCategories()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var categoryToAdd = BuildBlogCategoryFixture();

            // Act
            var initialCount = arrangeResult.RandomPostDto.List.Select(x => x.Category).Distinct().Count();
            var result = await arrangeResult.BlogCategoryServiceEF.GetAllCategories();
            var finalCount = result.Count();

            // Assert
            Assert.Equal(initialCount, finalCount);
        }

        [Fact(DisplayName = "Should get Blog Category by ID")]
        public async Task ShouldGetBlogCategoryByID()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var category = arrangeResult.RandomPostDto.List.Select(x => x.Category).First();
            var guid = category!.BlogCategoryId;

            // Act
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryById(guid);

            // Assert
            Assert.Equal(category, result);
        }

        [Fact(DisplayName = "Should not return a Blog Category if an invalid ID is provided")]
        public async Task ShouldNotReturnABlogCategoryIfAnInvalidIdIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var guid = new Guid();

            // Act
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryById(guid);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Should get Blog Category by Url")]
        public async Task ShouldGetBlogCategoryByUrl()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var category = arrangeResult.RandomPostDto.List.Select(x => x.Category).First();
            var url = category!.CategoryNameNormalized;

            // Act
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryByUrl(url);

            // Assert
            Assert.Equal(category, result);
        }

        [Fact(DisplayName = "Should not return a Blog Category if an invalid url is provided")]
        public async Task ShouldNotReturnABlogCategoryIfAnInvalidUrlIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var url = "oops";

            // Act
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryByUrl(url);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Should update Blog Category")]
        public async Task ShouldUpdateBlogCategory()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var category = arrangeResult.RandomPostDto.List.Select(x => x.Category).First();
            category!.CategoryName = "Test Category";
            category.Icon = "Test category Icon";
            var id = category!.BlogCategoryId;

            // Act
            await arrangeResult.BlogCategoryServiceEF.UpdateBlogCategory(category);
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryById(id); 

            // Assert
            Assert.Equal(category, result);
        }

        [Fact(DisplayName = "Should delete Blog Category")]
        public async Task ShouldDeleteBlogCategory()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var category = arrangeResult.RandomPostDto.List.Select(x => x.Category).First();
            var id = category!.BlogCategoryId;

            // Act
            await arrangeResult.BlogCategoryServiceEF.DeleteBlogCategory(category);
            var result = await arrangeResult.BlogCategoryServiceEF.GetCategoryById(id);

            // Assert
            Assert.Null(result);
        }
    }
}
