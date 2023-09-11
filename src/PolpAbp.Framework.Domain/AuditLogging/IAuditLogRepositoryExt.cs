using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.AuditLogging;

namespace PolpAbp.Framework.AuditLogging
{
    public interface IAuditLogRepositoryExt
    {
        Task<List<Tuple<AuditLog, AuditLogAction>>> GetActionListsync(
                string sorting = null,
                int maxResultCount = 50,
                int skipCount = 0,
                DateTime? startTime = null,
                DateTime? endTime = null,
                string httpMethod = null,
                string url = null,
                Guid? userId = null,
                string userName = null,
                string applicationName = null,
                string clientIpAddress = null,
                string correlationId = null,
                int? maxExecutionDuration = null,
                int? minExecutionDuration = null,
                bool? hasException = null,
                HttpStatusCode? httpStatusCode = null,
                string serviceName = null,
                string methodName = null,
                CancellationToken cancellationToken = default);

        Task<long> GetActionCountAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            string httpMethod = null,
            string url = null,
            Guid? userId = null,
            string userName = null,
            string applicationName = null,
            string clientIpAddress = null,
            string correlationId = null,
            int? maxExecutionDuration = null,
            int? minExecutionDuration = null,
            bool? hasException = null,
            HttpStatusCode? httpStatusCode = null,
            string serviceName = null,
            string methodName = null,
            CancellationToken cancellationToken = default);
    }
}
