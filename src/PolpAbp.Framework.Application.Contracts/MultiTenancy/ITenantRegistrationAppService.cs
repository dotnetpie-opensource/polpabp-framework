using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.TenantManagement;

namespace PolpAbp.Framework.MultiTenancy
{
    /// <summary>
    /// Provides the service for tenant registration.
    /// Note that the multi tenancy module provides the similar functionalties,
    /// but the module specifies specific authorization requirements.
    /// As a result, we cannot use those services from that module
    /// when a user wants to register as a tenant.
    /// The services from that module are useful for a host, anyway.
    /// </summary>
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<TenantDto> RegisterTenant(TenantCreateDto input);

        //Task<EditionsSelectOutput> GetEditionsForSelect();

        //Task<EditionSelectDto> GetEdition(int editionId);
    }
}