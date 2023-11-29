using System.ComponentModel.DataAnnotations;

namespace Blazor.Blog.Models
{
    public class SiteSettings
    {
        [Key]
        public Guid SiteId { get; set; } = new Guid();
        [Required(ErrorMessage = "Site name is required")]
        public string SiteName { get; set; } = default!;
        public string SiteTagLine { get; set; } = default!;
        [Required]
        public string SiteDevUrl { get; set; } = default!;
        [Required]
        public string SiteProdUrl { get; set; } = default!;
        public string SiteLogo { get; set; } = default!;
        public string SiteLogoSmall { get; set; } = default!;
    }
}
