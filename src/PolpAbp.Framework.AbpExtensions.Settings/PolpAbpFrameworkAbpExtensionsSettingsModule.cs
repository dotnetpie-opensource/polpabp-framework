using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace PolpAbp.Framework
{
    [DependsOn(
      typeof(AbpSettingManagementDomainModule)
      )]
    public class PolpAbpFrameworkAbpExtensionsSettingsModule : AbpModule
    {
    }
}