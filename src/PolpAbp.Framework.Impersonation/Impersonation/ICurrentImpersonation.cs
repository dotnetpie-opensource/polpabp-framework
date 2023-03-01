using System;

namespace PolpAbp.Framework.Impersonation
{
    public interface ICurrentImpersonation
    {
        bool IsAvailable { get; }

        // Summary:
        //     UserId of the impersonator. This is filled if a user is performing actions behalf
        //     of the userid.
        Guid? UserId { get; }

        //
        // Summary:
        //     TenantId of the impersonator. T
        //     performing actions behalf of the userId.
        Guid? TenantId { get; }

        IDisposable Change(Guid? userId = null, Guid? tenantId = null);
    }
}
