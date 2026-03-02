using URLShortener.Api.Entities;

namespace URLShortener.Api.Repositories;

public interface IUrlRepository
{
    Task<Url> GetByShortUrlAsync(string shortUrl);
    Task<bool> ExistsShortUrlAsync(string shortUrl);
    Task<Url> CreateAsync(Url url);
}
