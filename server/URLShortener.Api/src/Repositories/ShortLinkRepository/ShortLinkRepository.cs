using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;
using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class ShortLinkRepository(AppDbContext db) : IShortLinkRepository
{
    public async Task<ShortLink> GetByShortCodeAsync(string shortCode)
    {
        return await db.ShortLinks.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
    }

    public async Task<bool> ExistsShortCodeAsync(string shortCode)
    {
        return await db.ShortLinks.AnyAsync(x => x.ShortCode == shortCode);
    }

    public async Task<ShortLink> CreateAsync(ShortLink shortLink)
    {
        db.ShortLinks.Add(shortLink);
        return shortLink;
    }
}