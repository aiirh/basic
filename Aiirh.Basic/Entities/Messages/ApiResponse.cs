using System.Collections.Generic;
using System.Linq;
using System.Net;
using Aiirh.Basic.Extensions;
using Newtonsoft.Json;

namespace Aiirh.Basic.Entities.Messages
{
    public class ApiResponse<T> : ApiResponse
    {
        [JsonProperty("data", Order = 4)]
        public new virtual T Data { get; private set; }

        public static ApiResponse<T> CreateSuccess(T data, string message, string internalReference = null)
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK,
                Status = "OK",
                Messages = WebMessage.Simple(message, null).MakeCollection(),
                InternalReference = internalReference
            };
        }
    }

    public class ApiResponse
    {
        [JsonProperty("statusCode", Order = 1)]
        public int StatusCode { get; protected set; }

        [JsonProperty("status", Order = 2)]
        public string Status { get; protected set; }

        [JsonProperty("message", Order = 3)]
        public string Message => string.Join("; ", Messages?.Select(x => x.ToString()) ?? Enumerable.Empty<string>());

        [JsonProperty("data", Order = 4)]
        public object Data => new object();

        [JsonIgnore]
        public IEnumerable<WebMessage> Messages { get; protected set; }

        [JsonProperty("internalReference", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalReference { get; set; }

        public static ApiResponse CreateError(HttpStatusCode code, string statusMessage, IEnumerable<WebMessage> messages, string internalReference = null)
        {
            return new ApiResponse
            {
                StatusCode = (int)code,
                Status = statusMessage,
                Messages = messages,
                InternalReference = internalReference
            };
        }

        public static ApiResponse CreateError(HttpStatusCode code, string statusMessage, string message, string internalReference = null)
        {
            return new ApiResponse
            {
                StatusCode = (int)code,
                Status = statusMessage,
                Messages = WebMessage.Simple(message, null).MakeCollection(),
                InternalReference = internalReference
            };
        }

        public static ApiResponse CreateError(HttpStatusCode code, string statusMessage, IEnumerable<string> messages, string internalReference = null)
        {
            return new ApiResponse
            {
                StatusCode = (int)code,
                Status = statusMessage,
                Messages = messages.Select(x => WebMessage.Simple(x, null)),
                InternalReference = internalReference
            };
        }

        public static ApiResponse CreateSuccess(string message, string internalReference = null)
        {
            return new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Status = "OK",
                Messages = WebMessage.Simple(message, null).MakeCollection(),
                InternalReference = internalReference
            };
        }
    }
}
