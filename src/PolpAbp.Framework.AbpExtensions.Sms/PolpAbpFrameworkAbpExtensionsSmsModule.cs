using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpDataModule),
        typeof(AbpSmsModule)
      )]
    public class PolpAbpFrameworkAbpExtensionsSmsModule : AbpModule
    {
    }
}
