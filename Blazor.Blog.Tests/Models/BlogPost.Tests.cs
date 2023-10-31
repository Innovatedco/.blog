using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;
using System;

namespace Blazor.Blog.Tests.Models
{
    public class BlogPostTest
    {
        [Fact(DisplayName = "Should validate a valid model")]
        public void ShouldValidateAValidModel()
        {
            // Arrange
            var model = new BlogPost();
            model.Title = "Title";
            model.Post = "Blog Post";
            model.BlogCategoryId = new Guid();

            // Act
            var errorCount = ModelPropertyValidator.Validate(model).Count;

            // Assert
            Assert.True(errorCount == 0);
        }

        [Fact(DisplayName = "Should not validate a invalid model")]
        public void ShouldNotValidateAnInvalidModel()
        {
            // Arrange
            var model = new BlogPost();
            
            // Act
            var errorCount = ModelPropertyValidator.Validate(model).Count;

            // Assert
            Assert.True(errorCount > 0);
        }
    }
}
