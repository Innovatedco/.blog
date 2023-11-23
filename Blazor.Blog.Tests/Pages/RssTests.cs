using Blazor.Blog.Pages;
using Blazor.Blog.Tests.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blazor.Blog.Tests.Pages
{
    public class RssTests
    {
        [Fact(DisplayName = "Should render")]
        public async Task ShouldRender()
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
            var rssModel = new RSSModel(arrangeResult.BlogPostServiceEF, config, env);
            IActionResult result = await rssModel.OnGet();
            var contentResult = (FileContentResult)result;

            // Assert
            Assert.True(contentResult.ContentType == "application/rss+xml; charset=utf-8");
            Assert.NotNull(contentResult.FileContents);
        }
    }
}
