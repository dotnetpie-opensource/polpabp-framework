using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpIdentityDomainModule)
        )]
    public class PolpAbpFrameworkAbpExtensionsModule : AbpModule
    {
    }
}
