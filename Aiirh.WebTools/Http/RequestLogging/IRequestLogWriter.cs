using System.Net.Http;
using System.Threading.Tasks;

namespace Aiirh.WebTools.Http.RequestLogging
{
    /// <summary>
    /// To be used only for manual logging
    /// </summary>
    public interface IRequestLogWriter
    {
        Task WriteRequestLog(HttpRequestDescription description, IRequestLogData data);
    }

    /// <summary>
    /// To be used only in cases where you have HttpResponseMessage object. For manual logging use IMessageLogWriter
    /// </summary>
    public interface IRequestLogSystemWriter
    {
        Task WriteRequestLog(HttpRequestDescription description, IRequestLogSystemData data);
    }

    public interface IRequestLogSystemData
    {
        byte[] Request { get; }
        HttpResponseMessage Response { get; }
        bool SkipIfSuccessful { get; }
        long ElapsedTime { get; }
    }

    public interface IRequestLogData
    {
        byte[] Request { get; }
        byte[] Response { get; }
        bool IsError { get; }
        bool SkipIfSuccessful { get; }
        long ElapsedTime { get; }
    }
}
