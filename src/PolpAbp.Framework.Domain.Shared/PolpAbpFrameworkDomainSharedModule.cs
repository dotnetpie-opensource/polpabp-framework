using PolpAbp.Framework.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpLocalizationModule), // localization
        typeof(AbpSettingsModule)
   )]
    public class PolpAbpFrameworkDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PolpAbpFrameworkDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PolpAbpFrameworkResource>("en")
                    .AddVirtualJson("/Localization/PolpAbp/Framework/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("PolpAbp.Framework", typeof(PolpAbpFrameworkResource));
            });
        }
    }
}
