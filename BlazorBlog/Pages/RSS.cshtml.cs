using Blazor.Blog.Data;
using Blazor.Blog.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System;
using System.Text;
using Blazor.Blog.Models;
using Microsoft.AspNetCore.Components;
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
        private string blogUrlBase { get; set; } = default!;
        private string postUrlBase { get; set; } = default!;
        private string categoryUrlBase { get; set; } = default!;
        private string siteName { get; set; } = default!;
        private string siteNameStub { get; set; } = default!;
        private string tagLine { get; set; } = default!;
        private string tagLineStub { get; set; } = default!;
        #endregion

        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="blogPostService"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public RSSModel(IBlogPostService blogPostService, IConfiguration configuration, IWebHostEnvironment environment)
        {
            this._blogPostService = blogPostService;
            this._configuration = configuration;
            this._webHostEnvironment = environment;
            var helper = new NavUrlHelpers(_configuration, _webHostEnvironment);
            baseUrl = helper.GetBlogUrl() + "/";
            blogUrlBase = this._configuration["CustomSettings:ProdBlogUrl"]!;
            postUrlBase = blogUrlBase + NavUrlHelpers.PostUrlStub;
            categoryUrlBase = blogUrlBase + NavUrlHelpers.CategoryUrlStub;
            siteName = this._configuration["CustomSettings:SiteName"]!;
            siteNameStub = siteName + " | ";
            tagLine = this._configuration["CustomSettings:TagLine"]!;
            tagLineStub = siteNameStub + tagLine;
        }

        public async Task<IActionResult> OnGet()
        {
            var blogPosts = await _blogPostService.GetAllBlogPosts();
            var items = blogPosts.Select(x => new SyndicationItem(
                    x.Title,
                    TextHelpers.FormatStub(x.Post, 50),
                    new Uri(baseUrl + postRoute + x.NormalizedTitle),
                    x.NormalizedTitle,
                    x.Created
                    ));
            var feed = new SyndicationFeed(
                siteName,
                tagLine,
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
        //    public IActionResult OnGet()
        //    {
        //        var blogPosts = _blogPostService.GetAllBlogPosts().Result;
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<?xml version=\"1.0\"?>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<rss xmlns:atom=\"http://www.w3.org/2005/Atom\" version=\"2.0\">");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<channel>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append($"<title>{siteName}</title>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append($"<link>{baseUrl}</link>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append($"<description>{tagLineStub}</description>");
        //        sb.Append(Environment.NewLine);
        //        var lastPost = blogPosts.FirstOrDefault();
        //        var pubDate = lastPost != null ? lastPost.Created : DateTime.Now;
        //        var utcPubDate = ToRFC822DateString(pubDate);
        //        sb.Append($"<pubDate>{utcPubDate}</pubDate>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append($"<lastBuildDate>{utcPubDate}</lastBuildDate>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<docs>https://www.rssboard.org/rss-specification</docs>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append($"<atom:link href=\"{baseUrl}rss\" rel=\"self\" type=\"application/rss+xml\"/>");
        //        sb.Append(Environment.NewLine);
        //        foreach ( var blogPost in blogPosts )
        //        {
        //            var url = baseUrl + postRoute + blogPost.NormalizedTitle;
        //            var description = TextHelpers.FormatStub(blogPost.Post, 200);
        //            sb.Append("<item>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append($"<title>{blogPost.Title}</title>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append($"<link>{url}</link>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append($"<description>{description}</description>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append($"<pubDate>{ToRFC822DateString(blogPost.Created)}</pubDate>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append($"<guid>{url}</guid>");
        //            sb.Append(Environment.NewLine);
        //            sb.Append("</item>");
        //            sb.Append(Environment.NewLine);
        //        }
        //        sb.Append("</channel>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("</rss>");
        //        return new ContentResult
        //        {
        //            ContentType = "application/rss+xml",
        //            Content = sb.ToString(),
        //            StatusCode = 200
        //        };
        //    }

        //    /// <summary>
        //    /// Converts a regular DateTime to a RFC822 date string.
        //    /// </summary>
        //    /// <returns>The specified date formatted as a RFC822 date string.</returns>
        //    private string ToRFC822DateString(DateTime date)
        //    {
        //        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(date);
        //        return utcDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss") + " GMT";
        //    }
        //}
    }
}
