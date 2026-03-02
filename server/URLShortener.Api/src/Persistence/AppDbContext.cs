using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;

namespace URLShortener.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Url> Urls => Set<Url>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Url>()
            .Property(u => u.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Url>()
            .HasIndex(u => u.ShortUrl)
            .IsUnique();
    }
}