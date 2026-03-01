using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.Entities;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortLinkController(ShortLinkService shortLinkService) : ControllerBase
{   
    public async Task<ActionResult<ShortLink>> CreateAsync([FromBody] ShortLink shortLink)
    {
        return await shortLinkService.CreateAsync(shortLink);
    }
}