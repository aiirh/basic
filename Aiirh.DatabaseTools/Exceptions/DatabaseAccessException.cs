using System;

namespace Aiirh.DatabaseTools.Exceptions
{
    internal class DatabaseAccessException : Exception
    {
        public DatabaseAccessException(string message) : base(message)
        {
        }
    }
}
