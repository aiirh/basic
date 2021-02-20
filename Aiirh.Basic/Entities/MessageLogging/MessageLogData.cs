using System.Net.Http;
using Aiirh.Basic.Abstractions;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Entities.MessageLogging
{
    public class MessageLogData : IMessageLogData
    {

        public byte[] Request { get; }
        public byte[] Response { get; }
        public bool IsError { get; }
        public bool SkipIfSuccessful { get; }
        public long ElapsedTime { get; set; }

        public MessageLogData(byte[] request, string response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request, response.ToByteArray(), isError, skipIfSuccessful, elapsedTime) { }

        public MessageLogData(string request, byte[] response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request.ToByteArray(), response, isError, skipIfSuccessful, elapsedTime) { }

        public MessageLogData(string request, string response, bool isError, bool skipIfSuccessful, long elapsedTime)
            : this(request.ToByteArray(), response.ToByteArray(), isError, skipIfSuccessful, elapsedTime) { }

        public MessageLogData(byte[] request, byte[] response, bool isError, bool skipIfSuccessful, long elapsedTime)
        {
            Request = request;
            Response = response;
            IsError = isError;
            SkipIfSuccessful = skipIfSuccessful;
            ElapsedTime = elapsedTime;
        }
    }

    public class MessageLogSystemData : IMessageLogSystemData
    {
        public byte[] Request { get; }
        public HttpResponseMessage Response { get; set; }
        public bool SkipIfSuccessful { get; set; }
        public long ElapsedTime { get; set; }

        public MessageLogSystemData(string request)
        {
            Request = request.ToByteArray();
        }
    }
}
