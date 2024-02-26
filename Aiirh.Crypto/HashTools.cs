using System.Security.Cryptography;
using System.Text;

namespace Aiirh.Crypto;

public static class HashTools
{
    public static string Md5(this string input)
    {
        var hash = new StringBuilder();
        var md5Provider = MD5.Create();
        var bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        foreach (var @byte in bytes)
        {
            hash.Append(@byte.ToString("x2"));
        }

        return hash.ToString();
    }
}