namespace URLShortener.Api.Services.HashService;

public interface IHashService
{
    string Encode(byte[] input);
    abstract byte[] Decode(string input);
}