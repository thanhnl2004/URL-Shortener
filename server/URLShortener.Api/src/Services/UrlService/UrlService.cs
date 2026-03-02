using URLShortener.Api.Entities;
using URLShortener.Api.Repositories;

namespace URLShortener.Api.Services;

public class UrlService(IUnitOfWork unitOfWork) : IUrlService
{
    public async Task<Url> GetByShortUrlAsync(string shortUrl)
    {
        return await unitOfWork.Urls.GetByShortUrlAsync(shortUrl);
    }

    public async Task<bool> ExistsShortUrlAsync(string shortUrl)
    {
        return await unitOfWork.Urls.ExistsShortUrlAsync(shortUrl);
    }

    public async Task<Url> CreateAsync(Url url)
    {
        Url newUrl = await unitOfWork.Urls.CreateAsync(url);
        await unitOfWork.SaveChangesAsync();
        return newUrl;
    }
}
