using System;
using System.Runtime.Serialization;

namespace Chemiklani.BL.Exceptions
{
    public class AppLogicException : Exception
    {
        public AppLogicException(string message) : base(message)
        {
        }

        public AppLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}