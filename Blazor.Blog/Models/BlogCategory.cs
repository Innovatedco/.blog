using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Blog.Models
{
    [PrimaryKey(nameof(BlogCategoryId))]
    public class BlogCategory
    {
        public Guid BlogCategoryId { get; set; } = new Guid();
        [Required(ErrorMessage = "Blog category name is a required field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "The category name should be at least 2 characters long")]
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryNameNormalized { get; set; } = string.Empty;
        public ICollection<BlogPost>? BlogPosts { get; set; }
        [Required(ErrorMessage = "Blog category icon is a required field")]
        public string? Icon { get; set; }
    }
}
