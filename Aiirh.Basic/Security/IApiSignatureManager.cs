using System;
using System.Threading.Tasks;

namespace Aiirh.Basic.Security
{
    public interface IApiSignatureManager<in TApiType> where TApiType : Enum
    {
        string AppendSignature(TApiType apiType, string baseUrl, params string[] additionalArgs);

        Task ValidateKey(TApiType apiType, string apiHash, string key, DateTime utcTime, params string[] additionalArgs);
    }
}
