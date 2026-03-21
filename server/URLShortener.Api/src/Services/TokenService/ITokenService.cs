using URLShortener.Api.Entities;

namespace URLShortener.Api.Services;

public interface ITokenService
{
    Task<string> CreateAccessTokenAsync(AppUser user);
}
