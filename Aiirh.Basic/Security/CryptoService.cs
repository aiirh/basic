using Aiirh.Basic.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Aiirh.Basic.Security
{
    public static class CryptoService
    {
        private const int Iterations = 1000;
        private const int SecretKeySize = 128;
        private static string _passPhrase;

        internal static void Init(string passPhrase)
        {
            if (string.IsNullOrEmpty(passPhrase))
            {
                throw new SimpleException("Pass phrase can't be empty");
            }
            _passPhrase = passPhrase;
        }

        public static string MD5Hash(this string input)
        {
            var hash = new StringBuilder();
            var md5Provider = new MD5CryptoServiceProvider();
            var bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (var @byte in bytes)
            {
                hash.Append(@byte.ToString("x2"));
            }

            return hash.ToString();
        }

        public static string MD5Base64Encoded(this string input)
        {
            using var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(result);
        }

        public static string EncryptString(this string input)
        {
            return input.EncryptString(null);
        }

        public static string EncryptString(this string input, string customPassPhase)
        {
            var passPhase = string.IsNullOrEmpty(customPassPhase) ? _passPhrase : customPassPhase;
            if (string.IsNullOrEmpty(passPhase))
            {
                throw new SimpleException("CryptoService is not initialized");
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var saltStringBytes = Generate128BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(input);

            using var password = new Rfc2898DeriveBytes(passPhase, saltStringBytes, Iterations);
            var keyBytes = password.GetBytes(SecretKeySize / 8);

            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            memoryStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            var cipherTextBytes = saltStringBytes;
            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptString(this string cipherText)
        {
            return cipherText.DecryptString(null);
        }

        public static string DecryptString(this string cipherText, string customPassPhase)
        {
            var passPhase = string.IsNullOrEmpty(customPassPhase) ? _passPhrase : customPassPhase;
            if (string.IsNullOrEmpty(passPhase))
            {
                throw new SimpleException("CryptoService is not initialized");
            }

            if (string.IsNullOrWhiteSpace(cipherText))
            {
                return null;
            }

            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(SecretKeySize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(SecretKeySize / 8).Take(SecretKeySize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(SecretKeySize / 8 * 2)
                .Take(cipherTextBytesWithSaltAndIv.Length - (SecretKeySize / 8 * 2)).ToArray();

            using var password = new Rfc2898DeriveBytes(passPhase, saltStringBytes, Iterations);
            var keyBytes = password.GetBytes(SecretKeySize / 8);

            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16];
            using var rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(randomBytes);
            return randomBytes;
        }

        public static string GetSha1String(this string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
