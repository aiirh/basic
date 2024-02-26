using Aiirh.Crypto.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Aiirh.Crypto;

public static class CryptographyTools
{
    private const int Iterations = 1000;
    private const int SecretKeySize = 128;
    private static string PassPhrase;

    internal static void Init(string passPhrase)
    {
        if (string.IsNullOrEmpty(passPhrase))
        {
            throw new InvalidConfigurationException("Pass phrase can't be empty");
        }

        PassPhrase = passPhrase;
    }

    public static string EncryptString(this string input)
    {
        return input.EncryptString(null);
    }

    public static string EncryptString(this string input, string customPassPhase)
    {
        try
        {
            var passPhase = string.IsNullOrEmpty(customPassPhase) ? PassPhrase : customPassPhase;
            if (string.IsNullOrEmpty(passPhase))
            {
                throw new InvalidConfigurationException("CryptoService is not initialized");
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var saltStringBytes = Generate128BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(input);

            using var password = GetRfc2898DeriveBytes(passPhase, saltStringBytes);
            var keyBytes = password.GetBytes(SecretKeySize / 8);

            using var symmetricKey = Aes.Create();
            symmetricKey.BlockSize = 128;
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            var cipherTextBytes = saltStringBytes;
            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }
        catch (Exception e)
        {
            throw new EncryptionException(e.Message);
        }
    }

    public static string DecryptString(this string cipherText)
    {
        return cipherText.DecryptString(null);
    }

    public static string DecryptString(this string cipherText, string customPassPhase)
    {
        try
        {
            var passPhase = string.IsNullOrEmpty(customPassPhase) ? PassPhrase : customPassPhase;
            if (string.IsNullOrEmpty(passPhase))
            {
                throw new InvalidConfigurationException("CryptoService is not initialized");
            }

            if (string.IsNullOrWhiteSpace(cipherText))
            {
                return null;
            }

            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(SecretKeySize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(SecretKeySize / 8).Take(SecretKeySize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv
                .Skip(SecretKeySize / 8 * 2)
                .Take(cipherTextBytesWithSaltAndIv.Length - (SecretKeySize / 8 * 2)).ToArray();

            using var password = GetRfc2898DeriveBytes(passPhase, saltStringBytes);
            var keyBytes = password.GetBytes(SecretKeySize / 8);

            using var symmetricKey = Aes.Create();
            symmetricKey.BlockSize = 128;
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;

            using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var plainTextReader = new StreamReader(cryptoStream);
            return plainTextReader.ReadToEnd();
        }
        catch (Exception e)
        {
            throw new DecryptionException(e.Message);
        }
    }

    private static byte[] Generate128BitsOfRandomEntropy()
    {
        var randomBytes = new byte[16];
        using var rngCsp = RandomNumberGenerator.Create();
        rngCsp.GetBytes(randomBytes);
        return randomBytes;
    }

#if NETSTANDARD2_0 || NETSTANDARD2_1
        private static Rfc2898DeriveBytes GetRfc2898DeriveBytes(string passPhase, byte[] saltStringBytes)
        {
            return new Rfc2898DeriveBytes(passPhase, saltStringBytes, Iterations);
        }
#else
    private static Rfc2898DeriveBytes GetRfc2898DeriveBytes(string passPhase, byte[] saltStringBytes)
    {
        return new Rfc2898DeriveBytes(passPhase, saltStringBytes, Iterations, HashAlgorithmName.SHA256);
    }
#endif
}