using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Entities;
using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class UrlRepository(AppDbContext db) : IUrlRepository
{
    public async Task<Url> GetByShortUrlAsync(string shortUrl)
    {
        return await db.Urls.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
    }

    public async Task<bool> ExistsShortUrlAsync(string shortUrl)
    {
        return await db.Urls.AnyAsync(x => x.ShortUrl == shortUrl);
    }

    public async Task<Url> CreateAsync(Url url)
    {
        db.Urls.Add(url);
        return url;
    }
}
