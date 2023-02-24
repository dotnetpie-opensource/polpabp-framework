using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class EntityChangeListDto : EntityDto<Guid>
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ChangeTime { get; set; }

        public string EntityTypeFullName { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public string ChangeTypeName => ChangeType.ToString();

        public Guid EntityChangeSetId { get; set; }
    }
}