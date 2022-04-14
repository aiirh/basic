using System;

namespace Aiirh.Crypto.Exceptions
{
    public class DecryptionException : Exception
    {
        public DecryptionException(string message) : base(message)
        {
        }
    }
}
