using System;

namespace PolpAbp.Framework.BackgroundJobs
{
    public abstract class IndividualRoleJobArgs
    {
        public Guid? TenantId { get; set; }

        public Guid RoleId { get; set; }

        public Guid? OperatorId { get; set; }

    }
}
