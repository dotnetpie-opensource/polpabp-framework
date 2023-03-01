using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(AbpIdentityDomainModule),
        typeof(PolpAbpFrameworkDomainSharedModule)
    )]
    public class PolpAbpFrameworkDomainModule : AbpModule
    {
    }
}
