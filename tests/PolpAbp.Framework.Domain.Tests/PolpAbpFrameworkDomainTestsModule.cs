using System;
using Volo.Abp.Modularity;
using Xunit;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(EntityFrameworkCore.PolpAbpFrameworkEntityFrameworkCoreTestsModule),
        typeof(PolpAbpFrameworkTestBaseModule))]
    public class PolpAbpFrameworkDomainTestsModule : AbpModule
    {
    }
}
