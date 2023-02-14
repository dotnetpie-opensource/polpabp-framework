using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Infrastructure
{
    public class AuditingPreMiddleware : IMiddleware, ITransientDependency
    {
        private readonly AbpAuditingOptions _auditOptions;

        public AuditingPreMiddleware(IOptions<AbpAuditingOptions> options
            )
        {
            _auditOptions = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _auditOptions.IsEnabled = true;

            var urlPath = context.Request.Path.Value;
            if (!string.IsNullOrEmpty(urlPath))
            {
                urlPath = urlPath.ToLower();
                if (urlPath.Contains("csp-report"))
                {
                    _auditOptions.IsEnabled = false;
                }
            }

            await next(context).ConfigureAwait(false);
        }
    }
}
