using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;

namespace Blazor.Blog.Tests.Models
{
    public class InputModelTests : TestContext
    {
        [Fact(DisplayName = "Should validate a valid model")]
        public void ShouldValidateAValidModel()
        {
            // Arrange
            var model = new InputModel();
            model.Email = "email";
            model.Password = "password";

            // Act
            var errorCount = ModelPropertyValidator.Validate(model).Count;

            // Assert
            Assert.True(errorCount == 0);
        }

        [Fact(DisplayName = "Should not validate a invalid model")]
        public void ShouldNotValidateAnInvalidModel()
        {
            // Arrange
            var model = new InputModel();

            // Act
            var errorCount = ModelPropertyValidator.Validate(model).Count;

            // Assert
            Assert.True(errorCount > 0);
        }
    }
}
