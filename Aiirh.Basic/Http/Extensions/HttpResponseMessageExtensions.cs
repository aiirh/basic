using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aiirh.Basic.Utilities;
using Newtonsoft.Json;

namespace Aiirh.Basic.Http.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TResult> TryParseResultAsync<TResult>(this HttpResponseMessage responseMsg, ResponseFormat responseFormat)
        {
            var responseStr = await responseMsg.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                switch (responseFormat)
                {
                    case ResponseFormat.Json:
                        return JsonConvert.DeserializeObject<TResult>(responseStr);
                    case ResponseFormat.Xml:
                        return XmlUtility.Deserialize<TResult>(responseStr);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(responseFormat), responseFormat, null);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to deserialize response. Message: {responseStr.Truncate(500)}", e);
            }
        }

        public static async Task<TResponse> ReadAsSpecifiedFormatAsync<TResponse>(this HttpContent content, ResponseFormat responseFormat)
        {
            switch (responseFormat)
            {
                case ResponseFormat.Json:
                    return await content.ReadAsJsonAsync<TResponse>().ConfigureAwait(false);
                case ResponseFormat.Xml:
                    var responseStr = await ReadAsStringAsync(content);
                    return XmlUtility.Deserialize<TResponse>(responseStr);
                default:
                    throw new ArgumentOutOfRangeException(nameof(responseFormat), responseFormat, null);
            }
        }

        public static async Task<string> ReadAsStringAsync(this HttpContent content)
        {
            var responseStr = await content.ReadAsStringAsync().ConfigureAwait(false);
            return responseStr;
        }

        public static HttpMethod ToHttpMethod(this RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }
    }
}
