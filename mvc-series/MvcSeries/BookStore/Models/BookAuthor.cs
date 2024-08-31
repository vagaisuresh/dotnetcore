using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}