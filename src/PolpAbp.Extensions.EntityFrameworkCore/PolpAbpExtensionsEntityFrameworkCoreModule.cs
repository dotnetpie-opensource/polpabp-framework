using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PolpAbp.Extensions.EntityFrameworkCore
{
    [DependsOn(
       typeof(AbpEntityFrameworkCoreModule)
       )]
    public class PolpAbpExtensionsEntityFrameworkCoreModule : AbpModule
    {
    }
}