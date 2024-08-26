using System.ComponentModel.DataAnnotations;

namespace GettingStarted.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Book Title is required.")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "The Title length should be between 2 and 20.")]
        public string? Title { get; set; }

        public string? Genre { get; set; }
        public List<string> Authors { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, 100)]
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Publish Date is required.")]
        public DateTime PublishDate { get; set; }
    }
}