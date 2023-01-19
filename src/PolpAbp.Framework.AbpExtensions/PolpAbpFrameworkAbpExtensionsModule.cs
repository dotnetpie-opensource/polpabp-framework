using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

// Do not include Volo.Abp.Identity.
// It may break the test modularity for the client module.
namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAutoMapperModule)
        )]
    public class PolpAbpFrameworkAbpExtensionsModule : AbpModule
    {
    }
}
