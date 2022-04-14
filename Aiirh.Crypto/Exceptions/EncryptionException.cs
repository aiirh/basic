using System;

namespace Aiirh.Crypto.Exceptions
{
    public class EncryptionException : Exception
    {
        public EncryptionException(string message) : base(message)
        {
        }
    }
}
