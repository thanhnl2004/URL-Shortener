using URLShortener.Api.Entities;

namespace URLShortener.Api.Repositories;

public interface IUrlRepository
{
    Task<Url?> GetByShortUrlAsync(string shortUrl);
    Task<Url?> GetByLongUrlAndOwnerAsync(string longUrl, string ownerUserId);
    Task<IReadOnlyList<Url>> GetByOwnerUserIdAsync(string ownerUserId);
    Task<Url> CreateAsync(Url url);
}
