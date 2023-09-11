using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PolpAbp.Framework.AuditLogging
{
    public class AuditLogRepositoryExt : EfCoreRepository<IAuditLoggingDbContext, AuditLog, Guid>, IAuditLogRepositoryExt
    {
        public AuditLogRepositoryExt(IDbContextProvider<IAuditLoggingDbContext> dbContextProvider)
       : base(dbContextProvider)
        {

        }

        public async Task<long> GetActionCountAsync(DateTime? startTime = null, DateTime? endTime = null, 
            string httpMethod = null, string url = null, Guid? userId = null, string userName = null, string applicationName = null, string clientIpAddress = null, 
            string correlationId = null, int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null, HttpStatusCode? httpStatusCode = null, 
            string serviceName = null, string methodName = null, CancellationToken cancellationToken = default)
        {
            var nHttpStatusCode = (int?)httpStatusCode;

            var dbContext = await GetDbContextAsync();

            var query = from action in dbContext.Set<AuditLogAction>()
                        join log in dbContext.AuditLogs on action.AuditLogId equals log.Id
                        select new
                        {
                            action = action,
                            auditLog = log
                        };

            query = query
            .WhereIf(startTime.HasValue, a => a.auditLog.ExecutionTime >= startTime)
            .WhereIf(endTime.HasValue, a => a.auditLog.ExecutionTime <= endTime)
            .WhereIf(hasException.HasValue && hasException.Value, a => a.auditLog.Exceptions != null && a.auditLog.Exceptions != "")
            .WhereIf(hasException.HasValue && !hasException.Value, a => a.auditLog.Exceptions == null || a.auditLog.Exceptions == "")
            .WhereIf(httpMethod != null, a => a.auditLog.HttpMethod == httpMethod)
            .WhereIf(url != null, a => a.auditLog.Url != null && a.auditLog.Url.Contains(url))
            .WhereIf(userId != null, a => a.auditLog.UserId == userId)
            .WhereIf(userName != null, a => a.auditLog.UserName == userName)
            .WhereIf(applicationName != null, a => a.auditLog.ApplicationName == applicationName)
            .WhereIf(clientIpAddress != null, a => a.auditLog.ClientIpAddress != null && a.auditLog.ClientIpAddress == clientIpAddress)
            .WhereIf(correlationId != null, a => a.auditLog.CorrelationId == correlationId)
            .WhereIf(httpStatusCode != null && httpStatusCode > 0, a => a.auditLog.HttpStatusCode == nHttpStatusCode)
            .WhereIf(maxExecutionDuration != null && maxExecutionDuration.Value > 0, a => a.auditLog.ExecutionDuration <= maxExecutionDuration)
            .WhereIf(minExecutionDuration != null && minExecutionDuration.Value > 0, a => a.auditLog.ExecutionDuration >= minExecutionDuration)
            .WhereIf(serviceName != null, a => a.action.ServiceName.Contains(serviceName))
            .WhereIf(methodName != null, a => a.action.MethodName.Contains(methodName));

            return await query.CountAsync();
        }

        public async Task<List<Tuple<AuditLog, AuditLogAction>>> GetActionListAsync(string sorting = null, int maxResultCount = 50, int skipCount = 0, 
            DateTime? startTime = null, DateTime? endTime = null, string httpMethod = null, string url = null, 
            Guid? userId = null, string userName = null, string applicationName = null, string clientIpAddress = null, string correlationId = null, 
            int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null, HttpStatusCode? httpStatusCode = null, 
            string serviceName = null, string methodName = null, CancellationToken cancellationToken = default)
        {
            var nHttpStatusCode = (int?)httpStatusCode;

            var dbContext = await GetDbContextAsync();

            var query = from action in dbContext.Set<AuditLogAction>()
                        join log in dbContext.AuditLogs on action.AuditLogId equals log.Id
                        select new
                        {
                            action = action,
                            auditLog = log
                        };

            query = query
            .WhereIf(startTime.HasValue, a => a.auditLog.ExecutionTime >= startTime)
            .WhereIf(endTime.HasValue, a => a.auditLog.ExecutionTime <= endTime)
            .WhereIf(hasException.HasValue && hasException.Value, a => a.auditLog.Exceptions != null && a.auditLog.Exceptions != "")
            .WhereIf(hasException.HasValue && !hasException.Value, a => a.auditLog.Exceptions == null || a.auditLog.Exceptions == "")
            .WhereIf(httpMethod != null, a => a.auditLog.HttpMethod == httpMethod)
            .WhereIf(url != null, a => a.auditLog.Url != null && a.auditLog.Url.Contains(url))
            .WhereIf(userId != null, a => a.auditLog.UserId == userId)
            .WhereIf(userName != null, a => a.auditLog.UserName == userName)
            .WhereIf(applicationName != null, a => a.auditLog.ApplicationName == applicationName)
            .WhereIf(clientIpAddress != null, a => a.auditLog.ClientIpAddress != null && a.auditLog.ClientIpAddress == clientIpAddress)
            .WhereIf(correlationId != null, a => a.auditLog.CorrelationId == correlationId)
            .WhereIf(httpStatusCode != null && httpStatusCode > 0, a => a.auditLog.HttpStatusCode == nHttpStatusCode)
            .WhereIf(maxExecutionDuration != null && maxExecutionDuration.Value > 0, a => a.auditLog.ExecutionDuration <= maxExecutionDuration)
            .WhereIf(minExecutionDuration != null && minExecutionDuration.Value > 0, a => a.auditLog.ExecutionDuration >= minExecutionDuration)
            .WhereIf(serviceName != null, a => a.action.ServiceName.Contains(serviceName))
            .WhereIf(methodName != null, a => a.action.MethodName.Contains(methodName))
            .OrderBy(a => a.auditLog.ExecutionTime)
            .PageBy(skipCount, maxResultCount);

            var ret = await query.ToListAsync(GetCancellationToken(cancellationToken));

            return ret.Select(a => new Tuple<AuditLog, AuditLogAction>(a.auditLog, a.action)).ToList();
        }
    }
}
