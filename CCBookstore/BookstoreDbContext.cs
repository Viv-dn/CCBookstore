using CCBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace CCBookstore;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; }
}