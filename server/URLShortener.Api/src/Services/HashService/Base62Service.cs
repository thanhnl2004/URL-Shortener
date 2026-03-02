using System.Numerics;
using System.Text;

namespace URLShortener.Api.Services.HashService;

public class Base62Service : IHashService
{
    private const string BASE62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public string Encode(byte[] input)
    {
        if (input.Length == 0)
        {
            return string.Empty;
        }

        var value = new BigInteger(input);
        var result = new StringBuilder();

        while (value > 0)
        {
            value = BigInteger.DivRem(value, 62, out var remainder);
            result.Append(BASE62[(int)remainder]);
        }

        foreach (var b in input)
        {
            if (b == 0)
            {
                result.Append(BASE62[0]);
            }
            else
            {
                break;
            }
        }

        var resultArray = result.ToString().ToCharArray();
        Array.Reverse(resultArray);
        return new string(resultArray);
    }

    public byte[] Decode(string input)
    {
        if (input.Length == 0)
        {
            return new byte[0];
        }

        var value = BigInteger.Zero;
        foreach (var c in input)
        {
            value = value * 62 + BASE62.IndexOf(c);
        }

        var bytes = value.ToByteArray();
        if (bytes[0] == 0)
        {
            bytes = bytes[1..];
        }

        var leadingZeroes = 0;
        foreach (var c in input)
        {
            if (c == BASE62[0])
            {
                leadingZeroes++;
            }
            else
            {
                break;
            }
        }

        var result = new byte[leadingZeroes + bytes.Length];
        Array.Copy(bytes, 0, result, leadingZeroes, bytes.Length);
        return result;
    }
}