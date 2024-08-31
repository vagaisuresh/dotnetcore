using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext (DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthor { get; set; } = default!;
    }
}