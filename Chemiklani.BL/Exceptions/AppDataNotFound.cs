using System;
using System.Runtime.Serialization;

namespace Chemiklani.BL.Exceptions
{
    public class AppDataNotFound : AppLogicException
    {
        public AppDataNotFound(string message) : base(message)
        {
        }

        public AppDataNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AppDataNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}