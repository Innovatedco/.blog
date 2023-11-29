using Blazor.Blog.Data;
using Blazor.Blog.Models;

namespace Blazor.Blog.Globals
{
    public class GlobalSiteSettings
    {
        private readonly ISiteSettingsService _siteSettingsService;
        public SiteSettings SiteSettings { get; set; } 
        public GlobalSiteSettings(ISiteSettingsService siteSettingsService) 
        {
            _siteSettingsService = siteSettingsService;
            SiteSettings = InitializeAsync().Result;
        }

        private async Task<SiteSettings> InitializeAsync()
        {
            return await _siteSettingsService.GetSiteSettings();
        }
    }
}
