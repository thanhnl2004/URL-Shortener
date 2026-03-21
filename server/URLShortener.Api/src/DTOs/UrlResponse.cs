namespace URLShortener.Api.DTOs;

public class UrlResponse
{
    public long Id { get; set; }
    public required string ShortUrl { get; set; }
    public required string LongUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
