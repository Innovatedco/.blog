using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class SiteSettingsServiceEf : ISiteSettingsService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly DataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public SiteSettingsServiceEf(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.CreateDbContext();
        }

        /// <summary>
        /// Gets site settings
        /// </summary>
        /// <returns>SiteSettings</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SiteSettings>? GetSiteSettings()
        {
            return await _context.SiteSetings.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="siteSettings"></param>
        /// <returns>Task</returns>
        public async Task UpdateSiteSettings(SiteSettings siteSettings)
        {
            _context.SiteSetings.Update(siteSettings);
            await _context.SaveChangesAsync();
            await _context.Entry(siteSettings).ReloadAsync();
        }
    }
}
