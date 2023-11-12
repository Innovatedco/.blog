using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Blog.Tests.Data
{
    public class BlogPostServiceEFTests
    {
        [Fact(DisplayName = "Should return all publishable blog posts")]
        public async Task ShouldReturnAllPublishableBlogPosts()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetAllBlogPosts();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(arrangeResult.RandomPostDto.PublishableCount, result.Count());
        }

        [Fact(DisplayName = "Should return all blog posts")]
        public async Task ShouldReturnAllBlogPosts()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetAllBlogPostsForEditor();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(arrangeResult.RandomPostDto.Total, result.Count());
        }

        [Fact(DisplayName = "Should return a blog post if a valid guid is provided")]
        public async Task ShouldReturnABlogPostIfAValidGuidIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;

            var guid = arrangeResult.RandomPostDto.List.Where(x => x.Published).Select(x => x.BlogPostId).Take(1).First();

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostByID(guid);

            // Assert
            Assert.Equal(guid, result.BlogPostId);
        }

        [Fact(DisplayName = "Should not return a blog post if an invalid guid is provided")]
        public async Task ShouldNotReturnABlogPostIfAnInvalidGuidIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();
            var guid = new System.Guid();

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostByID(guid);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Should return a blog post if a valid url is provided")]
        public async Task ShouldReturnABlogPostIfAValidUrlIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;

            var url = arrangeResult.RandomPostDto.List.Where(x => x.Published).Select(x => x.NormalizedTitle).Take(1).First();

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostByUrl(url);

            // Assert
            Assert.Equal(url, result.NormalizedTitle);
        }

        [Fact(DisplayName = "Should not return a blog post if an invalid url is provided")]
        public async Task ShouldNotReturnABlogPostIfAnInvalidUrlIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;

            var url = "oops";

            // Act/Assert
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostByUrl(url);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Should create a blog post if a valid blog post is provided")]
        public async Task ShouldCreateABlogPostIfAValidBlogPostIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => x.Published).Take(1).First();
            var newBlogPost = BlogPostHelpers.BuildPublishableBlogPostFixture();
            newBlogPost.BlogCategoryId = blogPost.BlogCategoryId;
            newBlogPost.Category = blogPost.Category;

            // Act
            await arrangeResult.BlogPostServiceEF.CreateBlogPost(newBlogPost);
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostByID(newBlogPost.BlogPostId);

            // Assert
            Assert.Equal(newBlogPost, result);
        }

        [Fact(DisplayName = "Should get blog posts by category")]
        public async Task ShouldGetBlogPostsByCategory()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => x.Published).Take(1).First();
            var categoryName = blogPost.Category!.CategoryNameNormalized;

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostsByCategory(categoryName);

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(blogPost, result);
        }

        [Fact(DisplayName = "Should not return blog posts by category in an invalid category name is provided")]
        public async Task ShouldNotReturnBlogPostsIfAnInvalidCategoryIsProvided()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var categoryName = "oops";

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostsByCategory(categoryName);

            // Assert
            Assert.Empty(result);
        }

        [Fact(DisplayName = "Should get blog posts for home page")]
        public async Task ShouldGetBlogPostsForHomePage()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostsForHomePage();

            // Assert
            Assert.NotEmpty(result.Item1);
            Assert.IsType<BlogPost>(result.Item2);
        }

        [Fact(DisplayName = "Should get blog post slice")]
        public async Task ShouldGetBlogPostSlice()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;

            // Act
            var result = await arrangeResult.BlogPostServiceEF.GetBlogPostSlice(2, 0);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact(DisplayName = "Should update a blog post")]
        public async Task ShouldUpdateABlogPost()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => x.Published).Take(1).First();
            blogPost.Title = "Test Title";
            blogPost.Post = "Test Post";

            // Act
            await arrangeResult.BlogPostServiceEF.UpdateBlogPost(blogPost);
            var updateblogPost = arrangeResult.RandomPostDto.List.Where(x => x.Published).Take(1).First();

            // Assert
            Assert.Equal(blogPost.Title, updateblogPost.Title);
            Assert.Equal(blogPost.Post, updateblogPost.Post);
        }

        [Fact(DisplayName = "Should archive a blog post")]
        public async Task ShouldArchiveABlogPost()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => !x.Archived).Take(1).First();
            var id = blogPost.BlogPostId;

            // Act
            await arrangeResult.BlogPostServiceEF.ArchiveBlogPost(blogPost);
            var updateblogPost = arrangeResult.RandomPostDto.List.Where(x => x.BlogPostId == id).First();

            // Assert
            Assert.True(updateblogPost.Archived);
        }

        [Fact(DisplayName = "Should de-archive a blog post")]
        public async Task ShouldDeArchiveABlogPost()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => x.Archived).Take(1).First();
            var id = blogPost.BlogPostId;

            // Act
            await arrangeResult.BlogPostServiceEF.DeArchiveBlogPost(blogPost);
            var updateblogPost = arrangeResult.RandomPostDto.List.Where(x => x.BlogPostId == id).First();

            // Assert
            Assert.False(updateblogPost.Archived);
        }

        [Fact(DisplayName = "Should publish a blog post")]
        public async Task ShouldPublishABlogPost()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => !x.Published).Take(1).First();
            var id = blogPost.BlogPostId;

            // Act
            await arrangeResult.BlogPostServiceEF.PublishBlogPost(blogPost);
            var updateblogPost = arrangeResult.RandomPostDto.List.Where(x => x.BlogPostId == id).First();

            // Assert
            Assert.True(updateblogPost.Published);
        }

        [Fact(DisplayName = "Should de-publish a blog post")]
        public async Task ShouldDePublishABlogPost()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult();;
            var blogPost = arrangeResult.RandomPostDto.List.Where(x => x.Published).Take(1).First();
            var id = blogPost.BlogPostId;

            // Act
            await arrangeResult.BlogPostServiceEF.DePublishBlogPost(blogPost);
            var updateblogPost = arrangeResult.RandomPostDto.List.Where(x => x.BlogPostId == id).First();

            // Assert
            Assert.False(updateblogPost.Published);
        }
    }
}