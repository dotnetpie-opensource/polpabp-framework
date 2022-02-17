using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpTenantManagementApplicationContractsModule), // Tenant management
        typeof(AbpIdentityApplicationContractsModule), // identity
        typeof(AbpLocalizationModule), // localization
        typeof(AbpAccountApplicationContractsModule), // account
        typeof(PolpAbpFrameworkCoreSharedModule)
    )]
    public class PolpAbpFrameworkApplicationContractsModule : AbpModule
    {
    }
}