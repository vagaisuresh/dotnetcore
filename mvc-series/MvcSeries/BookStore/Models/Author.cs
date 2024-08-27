using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "Author Name")]
        [Required(ErrorMessage = "Author Name is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string? Name { get; set; }
    }
}