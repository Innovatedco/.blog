using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class IConfigurationHelper
    {
        public static readonly string ProdBlogUrl = "https://blazor.blog/";
        public static readonly string DevBlogUrl = "http://localhost:55955/";
        public static readonly string SiteName = "Blazor Blog";
        public static IConfiguration Configuration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                { "CustomSettings:ProdBlogUrl", ProdBlogUrl },
                { "CustomSettings:DevBlogUrl", DevBlogUrl },
                { "CustomSettings:SiteName",  SiteName }
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();
            return configuration;
        }
    }
}
