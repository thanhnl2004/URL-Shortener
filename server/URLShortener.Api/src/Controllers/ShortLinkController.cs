using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.Entities;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortLinkController(IShortLinkService shortLinkService) : ControllerBase
{   
    [HttpPost]
    [Route("shorten")]
    public async Task<ActionResult<ShortLink>> CreateAsync([FromBody] ShortLink shortLink)
    {
        return await shortLinkService.CreateAsync(shortLink);
    }

    [HttpGet]
    [Route("{shortCode}")]
    public async Task<ActionResult<ShortLink>> GetAsync(string shortCode)
    {
        throw new InvalidOperationException();
    }
    
}