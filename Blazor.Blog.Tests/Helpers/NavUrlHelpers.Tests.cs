using Blazor.Blog.Helpers;
using Blazor.Blog.Tests.HelperClasses;

namespace Blazor.Blog.Tests.Helpers
{
    public class NavUrlHelpersTests : TestContext
    {
        [Fact(DisplayName = "Should configure the env variables for dev")]
        public void ShouldConfigureTheEnvironmentVariablesForDev()
        {
            // Arrange
            var config = IConfigurationHelper.Configuration();
            var hostingEnv = IWebHostEnvironmentHelper.MockIWebHostEnvironmentDev();

            // Act
            var navHelper = new NavUrlHelpers(config, hostingEnv.Object);

            // Assert
            Assert.True(navHelper.Env == NavUrlHelpers.development);
            Assert.True(navHelper.IsDevelopment);
            Assert.False(navHelper.IsProduction);
        }

        [Fact(DisplayName = "Should configure the env variables for dev")]
        public void ShouldConfigureTheEnvironmentVariablesForProd()
        {
            // Arrange
            var config = IConfigurationHelper.Configuration();
            var hostingEnv = IWebHostEnvironmentHelper.MockIWebHostEnvironmentProd();

            // Act
            var navHelper = new NavUrlHelpers(config, hostingEnv.Object);

            // Assert
            Assert.True(navHelper.Env == NavUrlHelpers.production);
            Assert.True(navHelper.IsProduction);
            Assert.False(navHelper.IsDevelopment);
        }

        [Fact(DisplayName = "Should get the blog url for prod")]
        public void ShouldGetTheBlogUrlForProd()
        {
            // Arrange
            var config = IConfigurationHelper.Configuration();
            var hostingEnv = IWebHostEnvironmentHelper.MockIWebHostEnvironmentProd();

            // Act
            var navHelper = new NavUrlHelpers(config, hostingEnv.Object);
            var blogUrl = navHelper.GetBlogUrl();

            // Assert
            Assert.True(blogUrl == IConfigurationHelper.ProdBlogUrl);
            Assert.True(navHelper.IsProduction);
            Assert.False(navHelper.IsDevelopment);
        }

        [Fact(DisplayName = "Should get the blog url for dev")]
        public void ShouldGetTheBlogUrlForDev()
        {
            // Arrange
            var config = IConfigurationHelper.Configuration();
            var hostingEnv = IWebHostEnvironmentHelper.MockIWebHostEnvironmentDev();

            // Act
            var navHelper = new NavUrlHelpers(config, hostingEnv.Object);
            var blogUrl = navHelper.GetBlogUrl();

            // Assert
            Assert.True(blogUrl == IConfigurationHelper.DevBlogUrl);
            Assert.True(navHelper.IsDevelopment);
            Assert.False(navHelper.IsProduction);
        }

        [Fact(DisplayName = "Should get empty blog url for unhandled env")]
        public void ShouldGetEmptyBlogUrlForUnhandledEnv()
        {
            // Arrange
            var config = IConfigurationHelper.Configuration();
            var hostingEnv = IWebHostEnvironmentHelper.MockIWebHostEnvironmentMystery();

            // Act
            var navHelper = new NavUrlHelpers(config, hostingEnv.Object);
            var blogUrl = navHelper.GetBlogUrl();

            // Assert
            Assert.True(string.IsNullOrEmpty(blogUrl));
            Assert.False(navHelper.IsDevelopment);
            Assert.False(navHelper.IsProduction);
        }

        [Fact(DisplayName = "Should get thumbnail url")]
        public void ShouldGetThumbnailUrl()
        {
            // Act
            var thumbNail = NavUrlHelpers.GetThumbnailUrl(HtmlHelpers.MockHtmlWithImage);

            // Assert
            Assert.False(string.IsNullOrEmpty(thumbNail));
            Assert.Contains("/thumb/", thumbNail);
        }

        [Fact(DisplayName = "Should not get thumbnail url if html does not contain an image")]
        public void ShouldNotGetThumbnailUrlIfHtmlDoesNotContainImage()
        {
            // Act
            var thumbNail = NavUrlHelpers.GetThumbnailUrl(HtmlHelpers.MockHtmlWithoutImage);

            // Assert
            Assert.True(string.IsNullOrEmpty(thumbNail));
        }

        [Fact(DisplayName = "Should remove non number & letter characters except whitespace and dash")]
        public void ShouldRemoveNonNumberLetterCharactersExceptWhitespaceAndDash()
        {
            // Arrange/Act
            var result = NavUrlHelpers.NormalizePostTitleForUrl(HtmlHelpers.TitleScrambled);

            // Assert
            Assert.Equal("hello-my-name-is-string-yes-really", result);
        }
    }
}
