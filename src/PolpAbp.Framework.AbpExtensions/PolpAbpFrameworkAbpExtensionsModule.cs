using System;
using Volo.Abp.Modularity;
using Volo.Abp.AutoMapper;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAutoMapperModule)
        )]
    public class PolpAbpFrameworkAbpExtensionsModule : AbpModule
    {
    }
}
