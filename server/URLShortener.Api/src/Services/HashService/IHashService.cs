namespace URLShortener.Api.Services;

public interface IHashService
{
    string Encode(byte[] input);
    abstract byte[] Decode(string input);
}