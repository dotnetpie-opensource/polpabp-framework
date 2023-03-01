using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpIdentityAspNetCoreModule), 
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(FrameworkImpersonationModule)
        )]
    public class FrameworkAspNetCoreModule : AbpModule
    {
    }
}
