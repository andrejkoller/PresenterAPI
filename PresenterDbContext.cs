using Microsoft.EntityFrameworkCore;
using PresenterAPI.Models;

namespace PresenterAPI
{
    public class PresenterDbContext(DbContextOptions<PresenterDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
