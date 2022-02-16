using System;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(FrameworkCoreSharedModule)
    )]
    public class FrameworkCoreModule : AbpModule
    {
    }
}