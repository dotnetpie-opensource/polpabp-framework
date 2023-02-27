using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace PolpAbp.Framework.Exceptions.IdentityServer
{
    public class PolpAbpTokenAuthException : AbpException
    {
        public PolpAbpTokenAuthException()
        {
        }

        public PolpAbpTokenAuthException(string message) : base(message)
        {
        }

        public PolpAbpTokenAuthException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PolpAbpTokenAuthException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
    }
}
