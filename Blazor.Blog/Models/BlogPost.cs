using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Blog.Models
{
    [PrimaryKey(nameof(BlogPostId))]
    public class BlogPost
    {
        [Required]
        public Guid BlogPostId { get; set; } = new Guid();
        [Required(ErrorMessage = "Blog post title is a required field")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "The title should be at least 6 characters long")]
        public string Title { get; set; } = default!;
        public string NormalizedTitle { get; set; } = default!;
        [Required(ErrorMessage = "Blog post is a required field")]
        public string Post { get; set; } = default!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        [ForeignKey(nameof(BlogCategoryId))]
        [Required(ErrorMessage = "A category is required")]
        public Guid BlogCategoryId { get; set; }
        public BlogCategory? Category { get; set; }
        public string ThumbnailUrl { get; set; } = default!;
        public bool Archived { get; set; } = default!;
        public bool Published { get; set; } = true;
        [ForeignKey(nameof(AuthorId))]
        [Required(ErrorMessage = "An author is required")]
        public int AuthorId { get; set; }        
        public Author? Author { get; set; }
    }
}
