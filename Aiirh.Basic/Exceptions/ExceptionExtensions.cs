using System;
using System.Text;

namespace Aiirh.Basic.Exceptions;

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

    /// <summary>
    /// Checks if the exception itself or any of its inner exceptions are of or inherited from the provided type.
    /// </summary>
    /// <typeparam name="T">The type of the exception to check for.</typeparam>
    /// <param name="exception">The exception to check.</param>
    /// <returns>True if the exception or any of its inner exceptions are of or inherited from the specified type; otherwise, false.</returns>
    public static bool IsCausedBy<T>(this Exception exception) where T : Exception
    {
        return exception switch
        {
            null => false,
            T _ => true,
            _ => IsCausedBy<T>(exception.InnerException)
        };
    }
}