using System;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class AuditLogListDto : EntityDto<Guid>
    {
        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public Guid? ImpersonatorTenantId { get; set; }

        public Guid? ImpersonatorUserId { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string BrowserInfo { get; set; }

        public string Exception { get; set; }

        public string CustomData { get; set; }
    }
}