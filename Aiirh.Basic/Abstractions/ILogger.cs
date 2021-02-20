namespace Aiirh.Basic.Abstractions
{
    public interface ILogger
    {
        void Info(string category, string message, bool logEvent = false);
        void Warning(string category, string message);
        void Error(string category, string message, string stackTrace = null);
        void Debug(string message);
    }
}
