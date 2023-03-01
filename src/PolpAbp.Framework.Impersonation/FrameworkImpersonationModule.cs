using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(PolpAbpFrameworkCoreSharedModule)
    )]
    public class FrameworkImpersonationModule : AbpModule
    {
    }
}