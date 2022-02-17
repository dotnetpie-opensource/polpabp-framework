using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(PolpAbpFrameworkDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule)
    )]
    public class PolpAbpFrameworkEntityFrameworkCoreModule: AbpModule
    {
        // We do need to build our own db context here, 
        // bc we reuse the db context from our dependent modules 
        // - AbpIdentityEntityFrameworkCoreModule
    }
}
