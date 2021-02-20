using System.Net.Http;
using System.Threading.Tasks;
using Aiirh.Basic.Services.Http;

namespace Aiirh.Basic.Abstractions
{
    /// <summary>
    /// To be used only for manual logging
    /// </summary>
    public interface IMessageLogWriter
    {
        Task WriteMessageLog(HttpRequestDescription description, IMessageLogData data);
    }

    /// <summary>
    /// To be used only in cases where you have HttpResponseMessage object. For manual logging use IMessageLogWriter
    /// </summary>
    public interface IMessageLogSystemWriter
    {
        Task WriteMessageLog(HttpRequestDescription description, IMessageLogSystemData data);
    }

    public interface IMessageLogSystemData
    {
        byte[] Request { get; }
        HttpResponseMessage Response { get; }
        bool SkipIfSuccessful { get; }
        long ElapsedTime { get; }
    }

    public interface IMessageLogData
    {
        byte[] Request { get; }
        byte[] Response { get; }
        bool IsError { get; }
        bool SkipIfSuccessful { get; }
        long ElapsedTime { get; }
    }
}
