using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class EntityChangeDto:EntityDto<Guid>
    {
        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public long EntityChangeSetId { get; set; }
        
        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public int? TenantId { get; set; }

        public object EntityEntry { get; set; }
    }
}