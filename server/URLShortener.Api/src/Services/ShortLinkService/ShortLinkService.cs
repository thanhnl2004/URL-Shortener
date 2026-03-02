using URLShortener.Api.Entities;
using URLShortener.Api.Repositories;

namespace URLShortener.Api.Services;

public class ShortLinkService(IUnitOfWork unitOfWork) : IShortLinkService
{
    public async Task<ShortLink> GetByShortCodeAsync(string shortCode)
    {
        return await unitOfWork.ShortLinks.GetByShortCodeAsync(shortCode);
    }

    public async Task<bool> ExistsShortCodeAsync(string shortCode)
    {
        return await unitOfWork.ShortLinks.ExistsShortCodeAsync(shortCode);
    }
    
    

    public async Task<ShortLink> CreateAsync(ShortLink shortLink)
    {
        ShortLink newShortLink =  await unitOfWork.ShortLinks.CreateAsync(shortLink);
        await unitOfWork.SaveChangesAsync();
        return newShortLink;
    }
}