using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace URLShortener.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Url> Urls => Set<Url>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Url>()
            .Property(u => u.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Url>()
            .HasIndex(u => u.ShortUrl)
            .IsUnique();

        modelBuilder.Entity<Url>()
            .HasIndex(u => new { u.OwnerUserId, u.LongUrl });

        modelBuilder.Entity<Url>()
            .HasOne(u => u.OwnerUser)
            .WithMany(u => u.OwnedUrls)
            .HasForeignKey(u => u.OwnerUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}