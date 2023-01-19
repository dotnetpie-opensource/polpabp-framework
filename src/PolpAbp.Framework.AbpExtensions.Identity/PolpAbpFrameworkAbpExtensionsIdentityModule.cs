using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
      typeof(AbpIdentityDomainModule)
      )]
    public class PolpAbpFrameworkAbpExtensionsIdentityModule : AbpModule
    {
    }
}