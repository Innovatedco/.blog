using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Blog.Models
{
    [PrimaryKey(nameof(AuthorId))]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Author name is required")]
        public string AuthorName { get; set; } = default!;
        [Required(ErrorMessage = "An Icon is required, please upload a square image at least 32px x 32px")]
        public string AuthorIcon { get; set; } = default!;

    }
}
