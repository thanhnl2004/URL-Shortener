namespace URLShortener.Api.Services;

public interface IHashService
{
    string Encode(byte[] input);
    byte[] Decode(string input);
}