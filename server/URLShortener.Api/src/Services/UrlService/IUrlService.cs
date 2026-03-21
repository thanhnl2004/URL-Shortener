using URLShortener.Api.Entities;

namespace URLShortener.Api.Services;

public interface IUrlService
{
    Task<Url> GetByShortUrlAsync(string shortUrl);
    Task<Url> ShortenAsync(string longUrl, string ownerUserId);
    Task<IReadOnlyList<Url>> GetMineAsync(string ownerUserId);
}
