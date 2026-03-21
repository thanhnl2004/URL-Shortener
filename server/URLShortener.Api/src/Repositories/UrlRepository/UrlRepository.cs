using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;
using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class UrlRepository(AppDbContext db) : IUrlRepository
{
    public async Task<Url?> GetByShortUrlAsync(string shortUrl)
    {
        return await db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
    }

    public async Task<Url?> GetByLongUrlAndOwnerAsync(string longUrl, string ownerUserId)
    {
        return await db.Urls.FirstOrDefaultAsync(x => x.LongUrl == longUrl && x.OwnerUserId == ownerUserId);
    }

    public async Task<IReadOnlyList<Url>> GetByOwnerUserIdAsync(string ownerUserId)
    {
        return await db.Urls
            .Where(x => x.OwnerUserId == ownerUserId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Url> CreateAsync(Url url)
    {
        await db.Urls.AddAsync(url);
        return url;
    }
}
