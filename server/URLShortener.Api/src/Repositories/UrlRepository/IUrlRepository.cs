using URLShortener.Api.Entities;

namespace URLShortener.Api.Repositories;

public interface IUrlRepository
{
    Task<Url> GetByShortUrlAsync(string shortUrl);
    Task<Url> GetByLongUrlAsync(string longUrl);
    Task<Url> CreateAsync(Url url);
}
