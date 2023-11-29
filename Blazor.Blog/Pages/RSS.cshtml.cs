using Blazor.Blog.Data;
using Blazor.Blog.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace Blazor.Blog.Pages
{
    public class RSSModel : PageModel
    {
        #region Member Variables
        public readonly string baseUrl;
        public readonly IBlogPostService _blogPostService;
        public readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly string postRoute = "post/";
        private string SiteName { get; set; } = default!;
        private string TagLine { get; set; } = default!;
        #endregion

        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="blogPostService"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public RSSModel(IBlogPostService blogPostService, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _blogPostService = blogPostService;
            _configuration = configuration;
            _webHostEnvironment = environment;
            var helper = new NavUrlHelpers(_configuration, _webHostEnvironment);
            baseUrl = helper.GetBlogUrl() + "/";
            SiteName = _configuration["CustomSettings:SiteName"]!;
            TagLine = _configuration["CustomSettings:TagLine"]!;
        }

        /// <summary>
        /// Handles the get request for the RSS feed
        /// </summary>
        /// <returns>formatted rss document</returns>
        public async Task<IActionResult> OnGet()
        {
            // gets a list of published blog posts
            var blogPosts = await _blogPostService.GetAllBlogPosts();
            // selects out Syndication items from the list
            var items = blogPosts.Select(x => new SyndicationItem(
                    x.Title,
                    TextHelpers.FormatStub(x.Post, 50),
                    new Uri(baseUrl + postRoute + x.NormalizedTitle),
                    x.NormalizedTitle,
                    x.Created
                    ));
            var feed = new SyndicationFeed(
                SiteName,
                TagLine,
                new Uri(baseUrl),
                items
            );

            XNamespace atom = "http://www.w3.org/2005/Atom";
            feed.ElementExtensions.Add(
                new XElement(atom + "link",
                new XAttribute("href", $"{baseUrl}rss"),
                new XAttribute("rel", "self"),
                new XAttribute("type", "application/rss+xml")));

            // Create the XML Writer with it's settings
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = false,
                Indent = true, // Makes it easier to read for humans
                Async = true, // You can omit this if you don't use the async API
            };

            using var stream = new MemoryStream();
            await using var xmlWriter = XmlWriter.Create(stream, settings);
            // Create the RSS Feed
            var rssFormatter = new Rss20FeedFormatter(feed, false);
            rssFormatter.WriteTo(xmlWriter);
            await xmlWriter.FlushAsync();

            return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
        }
    }
}
