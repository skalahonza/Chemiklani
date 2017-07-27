using System;
using System.Runtime.Serialization;

namespace Chemiklani.BL.Exceptions
{
    public class AppAuthenticationException : AppLogicException
    {
        public AppAuthenticationException(string message) : base(message)
        {
        }

        public AppAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AppAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}