using PolpAbp.Framework.Auditing.Dto;
using PolpAbp.Framework.AuditLogging;
using PolpAbp.Framework.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace PolpAbp.Framework.Auditing
{
    [RemoteService(false)]
    public class AuditLogAppService : FrameworkAppService, IAuditLogAppService
    {
        protected readonly IAuditLogRepository AuditLogRepository;
        protected readonly IAuditLogRepositoryExt AuditLogRepositoryExt;
        protected readonly IIdentityUserRepositoryExt IdentityUserRepositoryExt;

		public AuditLogAppService(IAuditLogRepository auditLogRepository,
            IAuditLogRepositoryExt auditLogRepositoryExt,
            IIdentityUserRepositoryExt identityUserRepositoryExt)
		{
            AuditLogRepository = auditLogRepository;
            AuditLogRepositoryExt = auditLogRepositoryExt;
            IdentityUserRepositoryExt = identityUserRepositoryExt;
		}

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogsAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default)
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

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogActionsAsync(GetAuditLogsInput input, CancellationToken cancellationToken = default)
        {
            var total = await AuditLogRepositoryExt.GetActionCountAsync(
                startTime: input.StartDate,
                endTime: input.EndDate,
                httpMethod: input.MethodName,
                userName: input.UserName,
                applicationName: input.ServiceName,
                minExecutionDuration: input.MinExecutionDuration,
                maxExecutionDuration: input.MaxExecutionDuration,
                hasException: input.HasException,
                methodName: input.MethodName,
                serviceName: input.ServiceName,
                cancellationToken: cancellationToken);

            var data = await AuditLogRepositoryExt.GetActionListAsync(
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
                methodName: input.MethodName,
                serviceName: input.ServiceName,
                cancellationToken: cancellationToken);

            // Get the user Ids 
            var userIds = data.Where(a => a.Item1.UserId.HasValue).Select(b => b.Item1.UserId.Value).Distinct().ToArray();
            var users = await IdentityUserRepositoryExt.GetListAsync(userIds, false, cancellationToken);

            var items = data.Select(x =>
            {
                var y = ObjectMapper.Map<AuditLog, AuditLogListDto>(x.Item1);

                if (y.UserId.HasValue)
                {
                    var z = users.FirstOrDefault(m => y.UserId == m.Id);
                    if (z != null)
                    {
                        y.UserName = z.UserName;
                    }
                }

                y.MethodName = x.Item2.MethodName;

                var idx = x.Item2.ServiceName.LastIndexOf(".");
                if (idx >= 0)
                {
                    y.ServiceName = x.Item2.ServiceName.Substring(idx + 1);
                } 
                else
                {
                    y.ServiceName = x.Item2.ServiceName;
                }

                y.ExecutionTime = x.Item2.ExecutionTime;
                y.ExecutionDuration = x.Item2.ExecutionDuration;
                y.Parameters = x.Item2.Parameters;

                return y;

            }).ToList();


            return new PagedResultDto<AuditLogListDto>(total, items);
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityChangesAsync(GetEntityChangeInput input, CancellationToken cancellationToken = default)
        {
            var total = await AuditLogRepositoryExt.GetEntityChangeCountAsync(
                startTime: input.StartDate,
                endTime: input.EndDate,
                entityTypeFullName: input.EntityTypeFullName,
                userName: input.UserName,
                cancellationToken: cancellationToken);

            var data = await AuditLogRepositoryExt.GetEntityChangeListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                startTime: input.StartDate,
                endTime: input.EndDate,
                entityTypeFullName: input.EntityTypeFullName,
                userName: input.UserName,
                cancellationToken: cancellationToken);

            // Get the user Ids 
            var userIds = data.Where(a => a.Item1.UserId.HasValue).Select(b => b.Item1.UserId.Value).Distinct().ToArray();
            var users = await IdentityUserRepositoryExt.GetListAsync(userIds, false, cancellationToken);

            var items = data.Select(x =>
            {
                var y = ObjectMapper.Map<EntityChange, EntityChangeListDto>(x.Item2);
                if (y.UserId.HasValue)
                {
                    var z = users.FirstOrDefault(m => y.UserId == m.Id);
                    if (z != null)
                    {
                        y.UserName = z.UserName;
                    }
                }
                return y;
            }).ToList();

            return new PagedResultDto<EntityChangeListDto>(total, items);
        }

        public async Task<List<EntityPropertyChangeDto>> GetEntityPropertyChangesAsync(Guid entityChangeId, CancellationToken cancellationToken = default)
        {
            var data = await AuditLogRepository.GetEntityChange(entityChangeId, cancellationToken);

            return data.PropertyChanges.Select(x => ObjectMapper.Map<EntityPropertyChange, EntityPropertyChangeDto>(x)).ToList();
        }

        public async Task<PagedResultDto<EntityChangeListDto>> GetEntityTypeChangesAsync(GetEntityTypeChangeInput input, CancellationToken cancellationToken = default)
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

        public Task<List<NameValueDto<string>>> GetEntityHistoryObjectTypesAsync()
        {
            return Task.FromResult(new List<NameValueDto<string>>());
        }

    }
}

