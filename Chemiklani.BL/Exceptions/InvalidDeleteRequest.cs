using System;
using System.Runtime.Serialization;

namespace Chemiklani.BL.Exceptions
{
    public class InvalidDeleteRequest : AppLogicException {
        public InvalidDeleteRequest(string message) : base(message)
        {
        }

        public InvalidDeleteRequest(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidDeleteRequest(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}