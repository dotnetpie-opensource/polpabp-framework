using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.Framework.DistributedEvents
{
    public class EntityCreatedEtoBase
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
