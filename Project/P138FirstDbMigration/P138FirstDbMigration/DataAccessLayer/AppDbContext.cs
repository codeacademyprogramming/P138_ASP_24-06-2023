using Microsoft.EntityFrameworkCore;
using P138FirstDbMigration.Models;

namespace P138FirstDbMigration.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}