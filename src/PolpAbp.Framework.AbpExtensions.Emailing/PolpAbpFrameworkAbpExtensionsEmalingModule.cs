using Volo.Abp.Emailing;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpEmailingModule)
        )]
    public class PolpAbpFrameworkAbpExtensionsEmalingModule : AbpModule
    {
    }
}
