using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using URLShortener.Api.Configurations;
using URLShortener.Api.DTOs.Auth;
using URLShortener.Api.Entities;
using URLShortener.Api.Services;

namespace URLShortener.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserProfileResponse>> RegisterAsync([FromBody] RegisterRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            return Conflict(new { message = "Email is already registered." });
        }

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description).ToArray();
            return BadRequest(new { message = "Registration failed.", errors });
        }

        return Created(string.Empty, new UserProfileResponse
        {
            Id = user.Id,
            Email = user.Email ?? request.Email
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var validPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!validPassword)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var accessToken = await tokenService.CreateAccessTokenAsync(user);
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiryMinutes);

        return Ok(new AuthResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresAtUtc = expiresAtUtc,
            User = new UserProfileResponse
            {
                Id = user.Id,
                Email = user.Email ?? request.Email
            }
        });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserProfileResponse>> MeAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new { message = "Missing user identifier claim." });
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Unauthorized(new { message = "User not found." });
        }

        return Ok(new UserProfileResponse
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty
        });
    }
}
