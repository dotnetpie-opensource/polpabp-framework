﻿using System;

namespace PolpAbp.Framework.BackgroundJobs
{
    public abstract class IndividualTenantJobArgs
    {
        public Guid TenantId { get; set; }

        public Guid? OperatorId { get; set; }
    }
}
