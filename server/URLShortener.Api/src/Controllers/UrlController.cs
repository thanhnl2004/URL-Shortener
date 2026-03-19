using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.DTOs;
using URLShortener.Api.Entities;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/tinyurl")]
public class UrlController(IUrlService urlService) : ControllerBase
{   
    [HttpPost]
    [Route("shorten")]
    public async Task<ActionResult<UrlResponse>> ShortenAsync([FromBody] CreateUrlRequest request)
    {
        var url = await urlService.ShortenAsync(request.LongUrl);
        return new UrlResponse
        {
            ShortUrl = url.ShortUrl,
            LongUrl = url.LongUrl
        };
    }

    [HttpGet]
    [Route("{shortUrl}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RedirectAsync([FromRoute] string shortUrl)
    {
        var url = await urlService.GetByShortUrlAsync(shortUrl);
        return Redirect(url.LongUrl);
    }
}
