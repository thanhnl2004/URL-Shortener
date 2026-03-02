using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.Entities;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController(IUrlService urlService) : ControllerBase
{   
    [HttpPost]
    [Route("shorten")]
    public async Task<ActionResult<Url>> CreateAsync([FromBody] Url url)
    {
        return await urlService.CreateAsync(url);
    }

    [HttpGet]
    [Route("{shortUrl}")]
    public async Task<ActionResult<Url>> GetAsync(string shortUrl)
    {
        throw new InvalidOperationException();
    }
}
