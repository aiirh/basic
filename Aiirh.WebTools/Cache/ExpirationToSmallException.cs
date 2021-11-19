using System;

namespace Aiirh.WebTools.Cache
{
    public class ExpirationToSmallException : Exception
    {
        public ExpirationToSmallException(string message) : base(message)
        {
        }
    }
}
