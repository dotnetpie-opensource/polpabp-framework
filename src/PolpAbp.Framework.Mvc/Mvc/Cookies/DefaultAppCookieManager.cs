using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mvc.Cookies
{
    public class DefaultAppCookieManager : IAppCookieManager, ITransientDependency
    {
        protected readonly IConfiguration Configuration;
        protected readonly AbpAspNetCoreMultiTenancyOptions TenancyOptions;

        protected string TenantCookieName => TenancyOptions.TenantKey;
        protected string CrossDomainName => Configuration["App:CrossDomain"] ?? string.Empty;

        public DefaultAppCookieManager(IConfiguration configuration,
            IOptions<AbpAspNetCoreMultiTenancyOptions> options
            )
        {
            Configuration = configuration;
            TenancyOptions = options.Value;
        }

        public bool HasTenantCookie(HttpRequest request)
        {
            var c = request.Cookies[TenantCookieName];
            return c != null;
        }
        public string ReadTenantCookieValue(HttpRequest request)
        {
            var c = request.Cookies[TenantCookieName];
            return c ?? string.Empty;
        }

        public void SetTenantCookieValue(HttpResponse response, string value, string? domain= null,TimeSpan? span = null)
        {
            response.SetNamedCookie(TenantCookieName, value, domain ?? CrossDomainName, span);
        }

        public void ClearTenantCookie(HttpResponse response, string? domain = null)
        {
            response.ClearNamedCookie(TenantCookieName, domain ?? CrossDomainName);
        }
    }
}
