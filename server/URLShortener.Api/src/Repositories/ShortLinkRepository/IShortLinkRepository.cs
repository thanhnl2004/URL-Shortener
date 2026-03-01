using URLShortener.Api.Entities;

namespace URLShortener.Api.Repositories;

public interface IShortLinkRepository
{
    Task<ShortLink> GetByShortCodeAsync(string shortCode);
    Task<bool> ExistsShortCodeAsync(string shortCode);
    Task<ShortLink> CreateAsync(ShortLink shortLink);
}