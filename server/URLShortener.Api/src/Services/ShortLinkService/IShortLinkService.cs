using URLShortener.Api.Entities;

namespace URLShortener.Api.Services;

public interface IShortLinkService
{
    Task<ShortLink> GetByShortCodeAsync(string shortCode);
    Task<bool> ExistsShortCodeAsync(string shortCode); 
    Task<ShortLink> CreateAsync(ShortLink shortLink);
}