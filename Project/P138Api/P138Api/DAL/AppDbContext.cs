using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P138Api.Configurations;
using P138Api.Entities;

namespace P138Api.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Computer", IsMain = true},
                new Category { Id = 2, Name = "Phone", IsMain = true},
                new Category { Id = 3, Name = "Aksesuar", IsMain = true},
                new Category { Id = 4, Name = "Laptop", IsMain = true }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1},
                new Product { Id = 2, CategoryId = 1 },
                new Product { Id = 3, CategoryId = 1 },
                new Product { Id = 4, CategoryId = 1 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
