using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(PolpAbpFrameworkDomainSharedModule)
    )]
    public class PolpAbpFrameworkDomainModule : AbpModule
    {
    }
}
