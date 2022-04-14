using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Aiirh.WebTools.Security
{
    public interface IApiSignatureManager
    {
        string AppendSignature(string apiType, string baseUrl, params string[] additionalArgs);

        void ValidateKey(string apiType, string apiHash, string key, DateTime utcTime, params string[] additionalArgs);

        bool IsDisabled();
    }

    internal class ApiSignatureManager : IApiSignatureManager
    {
        protected static ApiSignatureOptions _apiSignatureOptions;

        internal static void Init(ApiSignatureOptions apiSignatureOptions)
        {
            _apiSignatureOptions = apiSignatureOptions;
        }

        private readonly IConfiguration _configuration;

        public ApiSignatureManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AppendSignature(string apiType, string baseUrl, params string[] additionalArgs)
        {
            var prefixAdded = false;
            if (baseUrl.IndexOf("?", StringComparison.OrdinalIgnoreCase) == -1)
            {
                baseUrl += "?";
                prefixAdded = true;
            }

            var privateKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPrivateKey"] ?? string.Empty;
            var publicKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPublicKey"] ?? string.Empty;
            var utcNow = DateTime.UtcNow;
            baseUrl += (prefixAdded ? string.Empty : "&") + "key=" + HttpUtility.UrlEncode(publicKey) + "&utcDate=" + HttpUtility.UrlEncode(utcNow.ToString("s")) + "&hash=" + HttpUtility.UrlEncode(GetHash(publicKey, privateKey, utcNow, additionalArgs));

            return baseUrl;
        }

        public void ValidateKey(string apiType, string apiHash, string key, DateTime utcTime, params string[] additionalArgs)
        {
            if (IsDisabled())
            {
                return;
            }

            var dateNow = DateTime.UtcNow;
            var privateKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPrivateKey"] ?? string.Empty;
            var publicKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPublicKey"] ?? string.Empty;

            if (publicKey != key)
            {
                throw new AccessViolationException("Registered api keys don't contain provided key");
            }

            if (Math.Abs((dateNow - utcTime).TotalSeconds) > _apiSignatureOptions.ApiSignatureValidityTimeInSeconds)
            {
                throw new AccessViolationException("Api signature has expired");
            }

            var hash = HttpUtility.UrlEncode(GetHash(publicKey, privateKey, utcTime, additionalArgs));
            if (hash != apiHash)
            {
                throw new AccessViolationException("Api signature is invalid");
            }

            return;
        }

        public bool IsDisabled()
        {
            return _apiSignatureOptions.Disabled;
        }

        private static string GetHash(string publicKey, string privateKey, DateTime utcTime, params string[] additionalArgs)
        {
            additionalArgs ??= new string[0];
            var digestText = publicKey + utcTime.ToString("s") + string.Join("|", additionalArgs);
            return ShaHash(digestText, privateKey);
        }

        private static string ShaHash(string value, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            return ByteToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        private static string ByteToString(IEnumerable<byte> data)
        {
            return string.Concat(data.Select(b => b.ToString("x2")));
        }
    }
}
