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
    public async Task<ActionResult<Url>> ShortenAsync([FromBody] CreateUrlRequest request)
    {
        return await urlService.ShortenAsync(request.LongUrl);
    }

    [HttpGet]
    [Route("{shortUrl}")]
    public async Task<IActionResult> RedirectAsync(string shortUrl)
    {
        var url = await urlService.GetByShortUrlAsync(shortUrl);
        return Redirect(url.LongUrl);
    }
}
