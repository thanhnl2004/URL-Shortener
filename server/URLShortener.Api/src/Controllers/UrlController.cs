using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.DTOs;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/tinyurl")]
public class UrlController(IUrlService urlService) : ControllerBase
{   
    [HttpPost]
    [Route("shorten")]
    [Authorize]
    public async Task<ActionResult<UrlResponse>> ShortenAsync([FromBody] CreateUrlRequest request)
    {
        var ownerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(ownerUserId))
        {
            return Unauthorized(new { message = "Missing user identifier claim." });
        }

        var url = await urlService.ShortenAsync(request.LongUrl, ownerUserId);
        return ToUrlResponse(url);
    }

    [HttpGet]
    [Route("mine")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<UrlResponse>>> GetMineAsync()
    {
        var ownerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(ownerUserId))
        {
            return Unauthorized(new { message = "Missing user identifier claim." });
        }

        var urls = await urlService.GetMineAsync(ownerUserId);
        return Ok(urls.Select(ToUrlResponse).ToList());
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

    private static UrlResponse ToUrlResponse(URLShortener.Api.Entities.Url url)
    {
        return new UrlResponse
        {
            Id = url.Id,
            ShortUrl = url.ShortUrl,
            LongUrl = url.LongUrl,
            CreatedAt = url.CreatedAt
        };
    }
}
