using System;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(PolpAbpFrameworkCoreSharedModule)
    )]
    public class PolpAbpFrameworkCoreModule : AbpModule
    {
    }
}