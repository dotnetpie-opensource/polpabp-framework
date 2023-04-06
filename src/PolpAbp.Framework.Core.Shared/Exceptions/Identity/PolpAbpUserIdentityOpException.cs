using System;
using Volo.Abp;

namespace PolpAbp.Framework.Exceptions.Identity
{
    public class PolpAbpUserIdentityOpException : AbpException
    {
        public IUserIdentityOpContext OpContext { get; protected set; }  

        public PolpAbpUserIdentityOpException(IUserIdentityOpContext context) : base(context.OperationFailureDescription)
        {
            OpContext = context;
        }

        public PolpAbpUserIdentityOpException(IUserIdentityOpContext context, Exception innerException) : base(context.OperationFailureDescription, innerException)
        {
            OpContext = context;
        }
    }
}
