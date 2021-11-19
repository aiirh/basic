using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Aiirh.WebTools.Http
{
    public interface IClientInitializationArgs
    {
        string BaseUrl { get; set; }
        bool SkipRequestLog { get; set; }
        string ProxyUrl { get; set; }
        int? TimeoutInSeconds { get; set; }
    }

    public class HttpClientInitializationArgs : IClientInitializationArgs
    {
        public HttpClientInitializationArgs()
        {
            AcceptHeaderValues = new HashSet<string>();
        }

        public string BaseUrl { get; set; }
        public bool SkipRequestLog { get; set; }
        public string ProxyUrl { get; set; }

        public int? TimeoutInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the certificate for signing.
        /// </summary>
        /// <value>
        /// The certificate for signing.
        /// </value>
        public X509Certificate2 CertificateForSigning { get; set; }

        /// <summary>
        /// Gets or sets the certificate for validating other party. <seealso cref="TrustAnyCertificate"/>
        /// </summary>
        /// <value>
        /// The certificate for validating other party.
        /// </value>
        public X509Certificate2 CertificateForValidatingOtherParty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether any other party certificate should be trusted.
        /// USe it if <see cref="CertificateForValidatingOtherParty"/> cant be used
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trust any certificate]; otherwise, <c>false</c>.
        /// </value>
        public bool TrustAnyCertificate { get; set; }

        public ICredentials Credentials { get; set; }

        public HashSet<string> AcceptHeaderValues { get; set; }

        public Dictionary<string, string> RequestHeaderValues { get; set; }

        public bool NeedValidateCert => TrustAnyCertificate || CertificateForValidatingOtherParty != null;
    }
}
