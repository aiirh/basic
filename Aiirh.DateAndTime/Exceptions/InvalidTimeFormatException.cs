using System;

namespace Aiirh.DateAndTime.Exceptions
{
    public class InvalidTimeFormatException : Exception
    {
        public InvalidTimeFormatException(string message) : base(message)
        {
        }
    }
}
