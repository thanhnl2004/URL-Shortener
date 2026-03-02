using System.ComponentModel.DataAnnotations;

namespace URLShortener.Api.DTOs;

public class CreateUrlRequest
{
    [Required]
    [Url(ErrorMessage = "LongUrl must be a valid URL (e.g. https://example.com)")]
    public required string LongUrl { get; set; }
}
