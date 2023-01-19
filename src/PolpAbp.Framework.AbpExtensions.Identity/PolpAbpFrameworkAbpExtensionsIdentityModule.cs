using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework.AbpExtensions.Identity
{
    [DependsOn(
      typeof(AbpIdentityDomainModule)
      )]
    public class PolpAbpFrameworkAbpExtensionsIdentityModule : AbpModule
    {
    }
}