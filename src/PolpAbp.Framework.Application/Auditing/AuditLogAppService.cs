using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.Framework.Auditing.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace PolpAbp.Framework.Auditing
{
    [RemoteService(false)]
    public class AuditLogAppService : FrameworkAppService, IAuditLogAppService
    {
        protected readonly IAuditLogRepository AuditLogRepository;

		public AuditLogAppService(IAuditLogRepository auditLogRepository)
		{
            AuditLogRepository = auditLogRepository;
		}

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input, CancellationToken cancellationToken = default)
        {
            var total = await AuditLogRepository.GetCountAsync(
                startTime: input.StartDate,
                endTime: input.EndDate,
                httpMethod: input.MethodName,
                userName: input.UserName,
                applicationName: input.ServiceName,
                minExecutionDuration: input.MinExecutionDuration,
                maxExecutionDuration: input.MaxExecutionDuration,
                hasException: input.HasException,
                cancellationToken: cancellationToken);

            var data = await AuditLogRepository.GetListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                startTime: input.StartDate,
                endTime: input.EndDate,
                httpMethod: input.MethodName,
                userName: input.UserName,
                applicationName: input.ServiceName,
                minExecutionDuration: input.MinExecutionDuration,
                maxExecutionDuration: input.MaxExecutionDuration,
                hasException: input.HasException,
                cancellationToken: cancellationToken);

            var items = data.Select(x => ObjectMapper.Map<AuditLog, AuditLogListDto>(x)).ToList();

            return new PagedResultDto<AuditLogListDto>(total, items);
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityChanges(GetEntityChangeInput input, CancellationToken cancellationToken = default)
        {
            var total = await AuditLogRepository.GetEntityChangeCountAsync(
                startTime: input.StartDate,
                endTime: input.EndDate,
                entityTypeFullName: input.EntityTypeFullName,
                cancellationToken: cancellationToken);

            var data = await AuditLogRepository.GetEntityChangeListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                startTime: input.StartDate,
                endTime: input.EndDate,
                entityTypeFullName: input.EntityTypeFullName,
                cancellationToken: cancellationToken);

            var items = data.Select(x => ObjectMapper.Map<EntityChange, EntityChangeListDto>(x)).ToList();

            return new PagedResultDto<EntityChangeListDto>(total, items);
        }

        public async Task<List<EntityPropertyChangeDto>> GetEntityPropertyChanges(Guid entityChangeId, CancellationToken cancellationToken = default)
        {
            var data = await AuditLogRepository.GetEntityChange(entityChangeId, cancellationToken);

            return data.PropertyChanges.Select(x => ObjectMapper.Map<EntityPropertyChange, EntityPropertyChangeDto>(x)).ToList();
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityTypeChanges(GetEntityTypeChangeInput input, CancellationToken cancellationToken = default)
        {
            var total = await AuditLogRepository.GetEntityChangeCountAsync(
                entityId: input.EntityId,
                entityTypeFullName: input.EntityTypeFullName,
                cancellationToken: cancellationToken);

            var data = await AuditLogRepository.GetEntityChangeListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                entityId: input.EntityId,
                entityTypeFullName: input.EntityTypeFullName,
                cancellationToken: cancellationToken);

            var items = data.Select(x => ObjectMapper.Map<EntityChange, EntityChangeListDto>(x)).ToList();

            return new PagedResultDto<EntityChangeListDto>(total, items);
        }
    }
}

