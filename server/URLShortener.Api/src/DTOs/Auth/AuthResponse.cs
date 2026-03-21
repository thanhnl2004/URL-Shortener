namespace URLShortener.Api.DTOs.Auth;

public class AuthResponse
{
    public required string AccessToken { get; set; }
    public required string TokenType { get; set; }
    public required DateTime ExpiresAtUtc { get; set; }
    public required UserProfileResponse User { get; set; }
}
