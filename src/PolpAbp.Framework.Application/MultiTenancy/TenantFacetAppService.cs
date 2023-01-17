using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.TenantManagement;

namespace PolpAbp.Framework.MultiTenancy
{
    [RemoteService(false)]
    public class TenantFacetAppService : FrameworkAppService, ITenantFacetAppService
    {
        protected readonly IConfiguration Configuration;
        protected readonly ITenantRepository TenantRepository;

        public TenantFacetAppService(IConfiguration configuration,
            ITenantRepository tenantRepository)
        {
            Configuration = configuration;
            TenantRepository = tenantRepository;
        }

        public async Task<string> BuildTenantDomainAsync(Guid id)
        {
            var tenant = await TenantRepository.GetAsync(id, false);

            var primaryDomain = Configuration["PolpAbp:Framework:TenantPrimaryDomain"] ?? string.Empty;
            // Convert the given tenant name into a subdomain
            var subDomain = tenant.Name.MakeSubdomain(primaryDomain);
            return subDomain;
        }

        public Task<bool> IsValidTenantNameAsync(string name)
        {
            var a = name.IsValidIdentifier();
            return Task.FromResult(a);
        }

    }
}

