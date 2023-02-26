using Volo.Abp.Account;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpTenantManagementApplicationContractsModule), // Tenant management
        typeof(AbpSettingManagementApplicationContractsModule), // setting management
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
                options.FileSets.AddEmbedded<PolpAbpFrameworkApplicationContractsModule>("PolpAbp.Framework");
            });


            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AccountResource>()
                    .AddVirtualJson("/Localization/PolpAbp/Framework/Account");

                options.Resources
                    .Get<IdentityResource>()
                    .AddVirtualJson("/Localization/PolpAbp/Framework/Identity");

                options.Resources
                    .Get<AbpSettingManagementResource>()
                    .AddVirtualJson("/Localization/PolpAbp/Framework/SettingManagement");
            });
        }
    }
}