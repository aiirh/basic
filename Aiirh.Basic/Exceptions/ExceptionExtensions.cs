using System;
using System.Text;

namespace Aiirh.Basic.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string LogException(this Exception ex)
        {
            var message = new StringBuilder(ex.Message);
            if (ex.InnerException != null)
            {
                message.Append($". INNER:{LogException(ex.InnerException)}");
            }
            return message.ToString();
        }

        public static string LogInnerExceptions(this Exception ex)
        {
            var message = new StringBuilder();
            if (ex.InnerException != null)
            {
                message.Append($". INNER:{LogException(ex.InnerException)}");
            }
            return message.ToString();
        }
    }
}
