using System;
using System.Runtime.Serialization;

namespace Chemiklani.BL.Exceptions
{
    public class InvalidAppData : AppLogicException
    {
        public InvalidAppData(string message) : base(message)
        {
        }

        public InvalidAppData(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidAppData(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}