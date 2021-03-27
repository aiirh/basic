using System.Net.Http;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Http.RequestLogging
{
    public class RequestLogData : IRequestLogData
    {
        public byte[] Request { get; }
        public byte[] Response { get; }
        public bool IsError { get; }
        public bool SkipIfSuccessful { get; }
        public long ElapsedTime { get; set; }

        public RequestLogData(byte[] request, string response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request, response.ToByteArray(), isError, skipIfSuccessful, elapsedTime) { }

        public RequestLogData(string request, byte[] response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request.ToByteArray(), response, isError, skipIfSuccessful, elapsedTime) { }

        public RequestLogData(string request, string response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request.ToByteArray(), response.ToByteArray(), isError, skipIfSuccessful, elapsedTime) { }

        public RequestLogData(byte[] request, byte[] response, bool isError, bool skipIfSuccessful, long elapsedTime)
        {
            Request = request;
            Response = response;
            IsError = isError;
            SkipIfSuccessful = skipIfSuccessful;
            ElapsedTime = elapsedTime;
        }
    }

    public class RequestLogSystemData : IRequestLogSystemData
    {
        public byte[] Request { get; }
        public HttpResponseMessage Response { get; set; }
        public bool SkipIfSuccessful { get; set; }
        public long ElapsedTime { get; set; }

        public RequestLogSystemData(string request)
        {
            Request = request.ToByteArray();
        }
    }
}
