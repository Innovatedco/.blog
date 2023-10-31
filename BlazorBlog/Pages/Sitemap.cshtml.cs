using Blazor.Blog.Data;
using Blazor.Blog.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Blazor.Blog.Pages
{
    public class SitemapModel : PageModel
    {
        #region Member Variables
        public readonly string baseUrl;
        public readonly IBlogPostService _blogPostService;
        public readonly IBlogCategoryService _blogCategoryService;
        public readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly string postRoute = "post/";
        public readonly string categoryRoute = "category/";
        #endregion

        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="blogCategoryService"></param>
        /// <param name="blogPostService"></param>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public SitemapModel(IBlogCategoryService blogCategoryService, IBlogPostService blogPostService, IConfiguration configuration, IWebHostEnvironment environment)
        {
            this._blogCategoryService = blogCategoryService;
            this._blogPostService = blogPostService;
            this._configuration = configuration;
            this._webHostEnvironment = environment;
            var helper = new NavUrlHelpers(_configuration, _webHostEnvironment);
            baseUrl = helper.GetBlogUrl() + "/";
        }
        
        /// <summary>
        /// Outputs an xml site map
        /// </summary>
        /// <returns>application/xml</returns>
        public IActionResult OnGet()
        {
            var blogPosts = _blogPostService.GetAllBlogPosts().Result;
            var categories = blogPosts.Select(x => x.Category).Distinct();//_blogCategoryService.GetAllCategories().Result;
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8' ?><urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            sb.Append("<url><loc>" + baseUrl + "</loc><lastmod>" + date + "</lastmod><priority> 0.8</priority></url>");
            if (blogPosts != null && blogPosts.Any())
            {
                foreach (var post in blogPosts)
                {
                    string mDate = post.Updated.ToString("yyyy-MM-dd");
                    sb.Append("<url><loc>" + baseUrl + postRoute + post.NormalizedTitle + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
                }
                if(categories != null && categories.Any())
                {
                    foreach (var category in categories)
                    {

                        _ = sb.Append("<url><loc>" + baseUrl + categoryRoute + category!.CategoryNameNormalized + "</loc><lastmod>" + date + "</lastmod><priority> 0.8</priority></url>");
                    }
                }
            }

            sb.Append("</urlset>");

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };
        }
    }
}
