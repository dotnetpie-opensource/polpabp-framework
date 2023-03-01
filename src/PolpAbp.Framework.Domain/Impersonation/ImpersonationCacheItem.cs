using System;
using Volo.Abp.Caching;

namespace PolpAbp.Framework.Impersonation
{
    [Serializable]
    [CacheName("Impersonation")] // Explicitly
    public class ImpersonationCacheItem
    {

        public Guid? ImpersonatorTenantId { get; set; }

        public Guid ImpersonatorUserId { get; set; }

        public Guid? TargetTenantId { get; set; }

        public Guid TargetUserId { get; set; }

        public bool IsBackToImpersonator { get; set; }

        public ImpersonationCacheItem()
        {
        }

        public ImpersonationCacheItem(Guid? targetTenantId, Guid targetUserId, bool isBackToImpersonator)
        {
            TargetTenantId = targetTenantId;
            TargetUserId = targetUserId;
            IsBackToImpersonator = isBackToImpersonator;
        }
    }
}
