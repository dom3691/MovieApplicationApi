using Microsoft.EntityFrameworkCore;
using Movie_Application_Backend.Models;

namespace Movie_Application_Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MovieItem> Movies => Set<MovieItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieItem>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Genre).HasMaxLength(100).IsRequired();
            entity.Property(x => x.CreatedUtc).IsRequired();
            entity.Property(x => x.UpdatedUtc).IsRequired();
        });
    }
}
