namespace URLShortener.Api.Entities;

public class Url
{
    public long Id { get; set; }

    public required string ShortUrl { get; set; }
    public required string LongUrl { get; set; }
    public string? OwnerUserId { get; set; }
    public AppUser? OwnerUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
