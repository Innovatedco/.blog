using Blazor.Blog.Models;

namespace Blazor.Blog.Data
{
    public interface ISiteSettingsService
    {
        Task<SiteSettings> GetSiteSettings();
        Task UpdateSiteSettings(SiteSettings siteSettings);

    }
}
