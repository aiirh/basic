using System;

namespace Aiirh.Basic.Cache
{
    public class ExpirationToSmallException : Exception
    {
        public ExpirationToSmallException(string message) : base(message)
        {
        }
    }
}
