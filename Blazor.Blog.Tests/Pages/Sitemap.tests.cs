using Blazor.Blog.Pages;
using Blazor.Blog.Tests.HelperClasses;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Blog.Tests.Pages
{
    public class SitemapTests
    {
        [Fact(DisplayName = "Should render")]
        public void ShouldRender()
        {
            // Arrange
            var arrangeResult = ArrangeHelpers.ArrangeWithResult(null, new ArrangeHelpers.ArrangeConfig
            {
                UseData = true,
                UseWebHostEnvironment = ArrangeHelpers.ArrangeConfig.WebHostEnvironment.Prod,
                UseConfiguration = true,
            });
            var config = IConfigurationHelper.Configuration();
            var env = IWebHostEnvironmentHelper.MockIWebHostEnvironmentDev().Object;

            // Act
            var siteMapModel = new SitemapModel(arrangeResult.BlogCategoryServiceEF, arrangeResult.BlogPostServiceEF, config, env);
            IActionResult result = siteMapModel.OnGet();
            var contentResult = (ContentResult) result;

            // Assert
            Assert.True(contentResult.StatusCode == 200);
            Assert.True(contentResult.ContentType == "application/xml");
            Assert.NotNull(contentResult.Content);
        }
    }
}
