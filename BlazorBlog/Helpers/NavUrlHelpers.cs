using HtmlAgilityPack;

namespace Blazor.Blog.Helpers
{
    public class NavUrlHelpers
    {
        #region MemberVariables
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment Environment;

        public string Env { get; set; } = string.Empty;
        public bool IsDevelopment { get; private set; } = false;
        public bool IsProduction { get; private set; } = false;

        public static readonly string development = "Development";
        public static readonly string production = "Production";

        public static readonly string BaseUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("CustomSettings").GetSection("ProdBlogUrl").Value!;
        public static readonly string BaseUrlWithTail = BaseUrl + "/";
        public static readonly string PostUrlStub = "/post/";
        public static readonly string PostUrl = BaseUrl + PostUrlStub;
        public static readonly string CategoryUrlStub = "/category/";
        public static readonly string CategoryUrl = BaseUrl + CategoryUrlStub;
        public static readonly string EditPostStub = "/editpost/";
        public static readonly string EditPostUrl = BaseUrl + EditPostStub;
        public static readonly string CreatePostUrlStub = "/createpost";
        public static readonly string CreatePostUrl = BaseUrl + CreatePostUrlStub;
        public static readonly string LoginUrlStub = "/account/login";
        public static readonly string LogoutUrlStub = "/account/logout";
        public static readonly string ManagePostsUrlStub = "/manageposts";
        public static readonly string ManagePostsUrl = BaseUrl + ManagePostsUrlStub;
        public static readonly string EditCategoryUrlStub = "/editcategory/";
        public static readonly string EditCategoryUrl = BaseUrl + EditCategoryUrlStub;
        public static readonly string CreateCategoryUrlStub = "/createcategory";
        public static readonly string CreateCategoryUrl = BaseUrl + CreateCategoryUrlStub;
        public static readonly string ManageCategoriesUrlStub = "/managecategories";
        public static readonly string ManageCategoriesUrl = BaseUrl + ManageCategoriesUrlStub;
        public static readonly string EditAuthorUrlStub = "/editauthor/";
        public static readonly string EditAuthorUrl = BaseUrl + EditAuthorUrlStub;
        public static readonly string ManageAuthorsUrlStub = "/manageauthors";
        public static readonly string ManageAuthorsUrl = BaseUrl + ManageAuthorsUrlStub;
        public static readonly string CreateAuthorUrlStub = "/createauthor";
        public static readonly string CreateAuthorUrl = BaseUrl + CreateAuthorUrlStub;
        public static readonly string TwitterUrl = "https://twitter.com/intent/tweet?url=";
        public static readonly string RedditUrl = "https://www.reddit.com/submit?url=";
        public static readonly string LinkedinUrl = "https://www.linkedin.com/sharing/share-offsite/?url=";
        public static readonly string FacebookUrl = "https://www.facebook.com/sharer.php?u=";
        public static readonly string PreviewUrlStub = "/preview/";
        public static readonly string PreviewUrl = BaseUrl + PreviewUrlStub;
        #endregion

        /// <summary>
        /// Initializes the environment and configuration variables
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public NavUrlHelpers(IConfiguration configuration, IWebHostEnvironment environment) {
            Configuration = configuration;
            Environment = environment;
            Env = Environment.EnvironmentName;
            IsDevelopment = Env == development;
            IsProduction = Env == production;
        }

       /// <summary>
        /// Gets the BlogUrl depending on the environment
        /// </summary>
        /// <returns>string blogUrl</returns>
        public string GetBlogUrl()
        {
            if (IsDevelopment) return Configuration["CustomSettings:DevBlogUrl"]!;
            else if (IsProduction) return Configuration["CustomSettings:ProdBlogUrl"]!;
            return string.Empty;
        }

        /// <summary>
        /// Takes a string and produces a url friendly version of it for SEO purposes
        /// </summary>
        /// <param name="postTitle"></param>
        /// <returns>string postUrl</returns>
        public static string NormalizePostTitleForUrl(string postTitle)
        {
            postTitle = postTitle.Trim();
            var characters = postTitle.Where(c => (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || c == '-')).ToArray();
            var normalized = string.Empty;
            char prev = '/';
            foreach (var c in characters)
            {
                if (Char.IsWhiteSpace(c) && prev != '-')
                {
                    normalized += "-";
                    prev = '-';
                }
                else if(Char.IsWhiteSpace(c) && prev == '-')
                {
                    prev = '-';
                }
                else
                {
                    normalized += c.ToString().ToLower();
                    prev = c;
                }
            }
            return normalized;
        }

        /// <summary>
        /// Parses the supplied html to extract the image url and returns the thumbnail version
        /// </summary>
        /// <param name="Html"></param>
        /// <returns>string thumbnailUrl</returns>
        public static string GetThumbnailUrl(string Html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            var link = doc.DocumentNode.SelectSingleNode("//img");
            if (link != null)
                return link.Attributes["src"].Value.Replace("/image/", "/image/thumb/small-");
            else return string.Empty;
        }
    }
}