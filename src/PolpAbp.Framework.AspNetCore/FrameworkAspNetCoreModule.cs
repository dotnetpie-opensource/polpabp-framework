using Volo.Abp.AspNetCore;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpIdentityAspNetCoreModule), 
        typeof(AbpIdentityServerDomainModule)
        )]
    public class FrameworkAspNetCoreModule : AbpModule
    {
    }
}
