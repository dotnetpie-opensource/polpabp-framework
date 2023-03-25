using Volo.Abp.Modularity;
using Volo.Abp.Sms;

namespace PolpAbp.Framework
{
    [DependsOn(
      typeof(AbpSmsModule)
      )]
    public class PolpAbpFrameworkAbpExtensionsSmsModule : AbpModule
    {
    }
}
