using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;
using PolpAbp.Framework.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Identity.Localization;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpTenantManagementApplicationContractsModule), // Tenant management
        typeof(AbpIdentityApplicationContractsModule), // identity
        typeof(AbpLocalizationModule), // localization
        typeof(AbpAccountApplicationContractsModule), // account
        typeof(PolpAbpFrameworkCoreSharedModule)
    )]
    public class PolpAbpFrameworkApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PolpAbpFrameworkApplicationContractsModule>("PolpApb.Framework");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<IdentityResource>()
                    .AddVirtualJson("/Localization/OrgUnits/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("PolpApb.Framework", typeof(FrameworkResource));

            });
        }
    }
}