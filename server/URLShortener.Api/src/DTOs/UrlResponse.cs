namespace URLShortener.Api.DTOs;

public class UrlResponse
{
    public required string ShortUrl { get; set; }
    public required string LongUrl { get; set; }
}
