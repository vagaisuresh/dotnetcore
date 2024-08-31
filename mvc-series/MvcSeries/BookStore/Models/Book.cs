using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Book Title is required.")]
        public string? Title { get; set; }

        public string? Genre { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, 1000)]
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Publish Date is required.")]
        public DateTime PublishDate { get; set; }

        public required ICollection<BookAuthor> BookAuthors { get; set; }
    }
}