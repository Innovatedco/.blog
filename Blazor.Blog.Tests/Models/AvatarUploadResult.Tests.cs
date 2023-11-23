using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;

namespace Blazor.Blog.Tests.Models
{
    public class AvatarUploadResultTests : TestContext
    {
        [Fact(DisplayName = "Should validate a valid model")]
        public void ShouldValidateAValidModel()
        {
            // Arrange
            var model = new ImageUploadResult();
            model.url = "c.com";

            // Act
            var errorCount = ModelPropertyValidator.Validate(model).Count;

            // Assert
            Assert.True(errorCount == 0);
        }
    }
}
