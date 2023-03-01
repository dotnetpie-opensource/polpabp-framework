using Microsoft.AspNetCore.Http;
using PolpAbp.Framework.Impersonation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Infrastructure
{
    public class ImpersonationMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICurrentImpersonation _current;

        private const string CookieName = "PolpAbpImpersonatorUserId";
        private const string HeaderName = "PolpAbpImpersonatorUserId";
        private const string TenantCookieName = "PolpAbpImpersonatorTenantId";
        private const string TenantHeaderName = "PolpAbpImpersonatorTenantId";

        public ImpersonationMiddleware(
            ICurrentImpersonation current)
        {
            _current = current;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Guid? impersonatorUserId = null;
            Guid? impersonatorTenantId = null;
            // todo: Get from header or cookie
            var anyUserId = context.Request.Headers[HeaderName].FirstOrDefault();
            if (string.IsNullOrEmpty(anyUserId))
            {
                anyUserId = context.Request.Cookies[CookieName];
                if (anyUserId != null)
                {
                    // Set up the header
                    context.Request.Headers[HeaderName] = anyUserId;
                }
            }

            if (!string.IsNullOrEmpty(anyUserId))
            {
                if (Guid.TryParse(anyUserId, out Guid a))
                {
                    impersonatorUserId = a;
                }
            }

            var anyTenantId = context.Request.Headers[TenantHeaderName].FirstOrDefault();
            if (string.IsNullOrEmpty(anyTenantId))
            {
                anyTenantId = context.Request.Cookies[TenantCookieName];
                if (anyTenantId != null)
                {
                    // Set up the header
                    context.Request.Headers[TenantHeaderName] = anyTenantId;
                }
            }

            if (!string.IsNullOrEmpty(anyTenantId))
            {
                if (Guid.TryParse(anyTenantId, out Guid a))
                {
                    impersonatorTenantId = a;
                }
            }

            // todo: Resolve 
            using (_current.Change(impersonatorUserId, impersonatorTenantId))
            {
                await next(context);
            }
        }
    }
}
