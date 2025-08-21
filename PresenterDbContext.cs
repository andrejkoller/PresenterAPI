using Microsoft.EntityFrameworkCore;
using PresenterAPI.Interfaces;
using PresenterAPI.Models;

namespace PresenterAPI
{
    public class PresenterDbContext(DbContextOptions<PresenterDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Presentation>()
                .ToTable("Presentations")
                .HasOne(p => p.Owner)
                .WithMany(u => u.Presentations)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Slide>()
                .ToTable("Slides")
                .HasOne(s => s.Presentation)
                .WithMany(p => p.Slides)
                .HasForeignKey(s => s.PresentationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

    }
}
