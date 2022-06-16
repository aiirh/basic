using System;

namespace Aiirh.DateAndTime.Exceptions
{
    public class InvalidTimeZoneException : Exception
    {
        public InvalidTimeZoneException(string message) : base(message)
        {
        }
    }
}
