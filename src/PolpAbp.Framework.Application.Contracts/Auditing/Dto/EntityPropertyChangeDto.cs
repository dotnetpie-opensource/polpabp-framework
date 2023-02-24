using System;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class EntityPropertyChangeDto : EntityDto<Guid>
    {
        public Guid EntityChangeId { get; set; }

        public string NewValue { get; set; }

        public string OriginalValue { get; set; }

        public string PropertyName { get; set; }

        public string PropertyTypeFullName { get; set; }

        public Guid? TenantId { get; set; }
    }
}