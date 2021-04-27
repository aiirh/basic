using Aiirh.Basic.Time;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aiirh.Basic.Security
{
    public interface IApiSignatureManager<in TApiType> where TApiType : Enum
    {
        string AppendSignature(TApiType apiType, string baseUrl, params string[] additionalArgs);

        Task ValidateKey(TApiType apiType, string apiHash, string key, DateTime utcTime, params string[] additionalArgs);
    }

    internal class ApiSignatureManager
    {
        protected static ApiSignatureOptions _apiSignatureOptions;

        internal static void Init(ApiSignatureOptions apiSignatureOptions)
        {
            _apiSignatureOptions = apiSignatureOptions;
        }
    }

    internal class ApiSignatureManager<TApiType> : ApiSignatureManager, IApiSignatureManager<TApiType> where TApiType : Enum
    {
        private readonly IConfiguration _configuration;

        public ApiSignatureManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AppendSignature(TApiType apiType, string baseUrl, params string[] additionalArgs)
        {
            var prefixAdded = false;
            if (baseUrl.IndexOf("?", StringComparison.OrdinalIgnoreCase) == -1)
            {
                baseUrl += "?";
                prefixAdded = true;
            }

            var privateKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPrivateKey"] ?? string.Empty;
            var publicKey = _configuration[$"ApiSecurityKeys:{apiType}ApiPublicKey"] ?? string.Empty;
            var utcNow = SystemClock.Now;
            baseUrl += (prefixAdded ? string.Empty : "&") + "key=" + HttpUtility.UrlEncode(publicKey) + "&utcDate=" + HttpUtility.UrlEncode(utcNow.ToString("s")) + "&hash=" + HttpUtility.UrlEncode(GetHash(publicKey, privateKey, utcNow, additionalArgs));

            return baseUrl;
        }

        public Task ValidateKey(TApiType apiType, string apiHash, string key, DateTime utcTime, params string[] additionalArgs)
        {
            if (_apiSignatureOptions.Disabled)
            {
                return Task.CompletedTask;
            }

            var dateNow = SystemClock.Now;
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

            return Task.CompletedTask;
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
