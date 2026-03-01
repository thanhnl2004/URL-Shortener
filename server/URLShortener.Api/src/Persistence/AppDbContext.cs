using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;

namespace URLShortener.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ShortLink> ShortLinks => Set<ShortLink>();
}