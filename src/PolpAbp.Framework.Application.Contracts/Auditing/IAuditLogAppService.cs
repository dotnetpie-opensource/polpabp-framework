using PolpAbp.Framework.Auditing.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace PolpAbp.Framework.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogsAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default);

        Task<PagedResultDto<EntityChangeListDto>> GetEntityChangesAsync(GetEntityChangeInput input, CancellationToken cancellationToken = default);

        Task<PagedResultDto<EntityChangeListDto>> GetEntityTypeChangesAsync(GetEntityTypeChangeInput input, CancellationToken cancellationToken = default);

        Task<List<EntityPropertyChangeDto>> GetEntityPropertyChangesAsync(Guid entityChangeId, CancellationToken cancellationToken = default);
    }
}