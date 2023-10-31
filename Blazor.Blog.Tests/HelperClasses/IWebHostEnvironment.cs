using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class IWebHostEnvironmentHelper
    {
        /// <summary>
        /// Mock IWebHostEnvironment configured to return the Development environment name
        /// </summary>
        /// <returns>Mock<IWebHostEnvironment> mockEnvironment</returns>
        public static Mock<IWebHostEnvironment> MockIWebHostEnvironmentDev()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            
            mockEnvironment
            .Setup(m => m.EnvironmentName)
            .Returns("Development");
            return mockEnvironment;
        }

        /// <summary>
        /// Mock IWebHostEnvironment configured to return the Production environment name
        /// </summary>
        /// <returns>Mock<IWebHostEnvironment> mockEnvironment</returns>
        public static Mock<IWebHostEnvironment> MockIWebHostEnvironmentProd()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();

            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Production");
            return mockEnvironment;
        }

        /// <summary>
        /// Mock IWebHostEnvironment configured to return the mystery environment name
        /// </summary>
        /// <returns>Mock<IWebHostEnvironment> mockEnvironment</returns>
        public static Mock<IWebHostEnvironment> MockIWebHostEnvironmentMystery()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();

            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Mystery");
            return mockEnvironment;
        }
    }
}
