using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule)
        )]
    public class FrameworkDbMigratorModule : AbpModule
    {

    }
}