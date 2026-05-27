using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", DisplayOrder = 1},
                new Category { Id = 2, Name = "Non-Fiction", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Science Fiction", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Biography", DisplayOrder = 4 }
            );
        }
    }
}
