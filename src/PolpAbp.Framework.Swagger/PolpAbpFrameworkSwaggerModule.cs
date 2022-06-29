using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpSwashbuckleModule)
   )]
    public class PolpAbpFrameworkSwaggerModule : AbpModule
    {
    }
}
