using Microsoft.AspNetCore.Identity;

namespace URLShortener.Api.Entities;

public class AppUser : IdentityUser
{
    public ICollection<Url> OwnedUrls { get; set; } = [];
}