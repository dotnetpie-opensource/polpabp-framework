using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Modularity;
using Volo.Abp.AuditLogging;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpCachingModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(PolpAbpFrameworkDomainSharedModule)
    )]
    public class PolpAbpFrameworkDomainModule : AbpModule
    {
    }
}
