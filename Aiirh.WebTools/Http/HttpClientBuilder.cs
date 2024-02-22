using Aiirh.WebTools.Http.RequestLogging;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Aiirh.WebTools.Logging;

namespace Aiirh.WebTools.Http
{
    /// <summary>
    /// Builds the client
    /// </summary>
    public interface IHttpClientBuilder
    {
        /// <summary>
        /// Builds the http client.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        ExtendedHttpClient BuildClient(HttpClientInitializationArgs args);
    }

    /// <inheritdoc />
    /// <summary>
    /// HttpClientBuilder
    /// </summary>
    /// <seealso cref="T:Aiirh.WebTools.Http.IHttpClientBuilder" />
    internal class HttpClientBuilder : IHttpClientBuilder
    {

        private readonly IRequestLogSystemWriter _requestLogWriter;
        private readonly ILogger _logger;

        public HttpClientBuilder(IRequestLogSystemWriter requestLogWriter, ILogger logger)
        {
            _requestLogWriter = requestLogWriter;
            _logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// Builds the http client.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public ExtendedHttpClient BuildClient(HttpClientInitializationArgs args)
        {
            var client = new ExtendedHttpClient(InitHandler(args), _requestLogWriter, _logger)
            {
                Timeout = args.TimeoutInSeconds != null ? new TimeSpan(0, 0, args.TimeoutInSeconds.Value) : new TimeSpan(0, 0, 8)
            };
            if (args.AcceptHeaderValues != null && args.AcceptHeaderValues.Any())
            {
                foreach (var headerValue in args.AcceptHeaderValues)
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerValue));
                }
            }
            if (args.RequestHeaderValues != null && args.RequestHeaderValues.Any())
            {
                foreach (var requestHeader in args.RequestHeaderValues)
                {
                    client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }
            return client;
        }

        private static HttpMessageHandler InitHandler(HttpClientInitializationArgs args)
        {
            var result = new HttpClientHandler();
            if (!string.IsNullOrWhiteSpace(args.ProxyUrl))
            {
                result.Proxy = new WebProxy() { Address = new Uri(args.ProxyUrl) };
                result.UseProxy = true;
            }
            if (args.CertificateForSigning != null)
            {
                result.ClientCertificates.Add(args.CertificateForSigning);
            }
            if (args.Credentials != null)
            {
                result.Credentials = args.Credentials;
            }

            if (args.NeedValidateCert)
            {
                result.ServerCertificateCustomValidationCallback = (sender, certificate, chain, errors) =>
                {
                    if (args.TrustAnyCertificate)
                    {
                        return true;
                    }
                    else
                    {
                        return certificate.GetCertHashString() == args.CertificateForValidatingOtherParty.GetCertHashString();
                    }
                };
            }
            return result;
        }
    }
}
