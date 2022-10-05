using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Framework.MultiTenancy
{
    /// <summary>
    /// Provides the information that a tenant needs.
    /// </summary>
    public interface ITenantFacetAppService : IApplicationService
    {
        Task<string> BuildTenantDomainAsync(Guid id);

        Task<bool> IsValidTenantNameAsync(string name);
    }
}

