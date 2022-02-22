using System;

namespace PolpAbp.Framework.BackgroundJobs
{
    public abstract class IndividualUserJobArgs
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }

    }
}
