using URLShortener.Api.Entities;

namespace URLShortener.Api.Services;

public interface IUrlService
{
    Task<Url> GetByShortUrlAsync(string shortUrl);
    Task<bool> ExistsShortUrlAsync(string shortUrl);
    Task<Url> CreateAsync(Url url);
}
