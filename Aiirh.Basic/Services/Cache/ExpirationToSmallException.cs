using System;

namespace Aiirh.Basic.Services.Cache
{
    public class ExpirationToSmallException : Exception
    {
        public ExpirationToSmallException(string message) : base(message)
        {
        }
    }
}
