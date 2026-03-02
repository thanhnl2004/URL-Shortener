using URLShortener.Api.Entities;
using URLShortener.Api.Repositories;

namespace URLShortener.Api.Services;

public class UrlService(IUnitOfWork unitOfWork, IHashService hashService, IIdGeneratorService idGeneratorService) 
    : IUrlService
{
    public async Task<Url> GetByShortUrlAsync(string shortUrl)
    {
        return await unitOfWork.Urls.GetByShortUrlAsync(shortUrl);
    }

    public async Task<Url> ShortenAsync(string longUrl)
    {
        var existing = await unitOfWork.Urls.GetByLongUrlAsync(longUrl);
        if (existing != null) return existing;

        var uniqueId = idGeneratorService.NextId();
        var shortUrl = hashService.Encode(BitConverter.GetBytes(uniqueId));

        var url = new Url
        {
            Id = uniqueId,
            LongUrl = longUrl,
            ShortUrl = shortUrl
        };

        await unitOfWork.Urls.CreateAsync(url);
        await unitOfWork.SaveChangesAsync();

        return url;
    }
}
