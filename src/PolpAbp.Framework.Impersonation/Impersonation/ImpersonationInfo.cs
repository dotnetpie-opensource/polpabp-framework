using System;

namespace PolpAbp.Framework.Impersonation
{
    public class ImpersonationInfo
    {
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
