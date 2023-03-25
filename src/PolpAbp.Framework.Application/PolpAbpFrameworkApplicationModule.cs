using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.IdentityModel;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.TextTemplating.Scriban;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace PolpAbp.Framework
{
    [DependsOn(
        // Abp domain modules
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpIdentityModelModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpAuditLoggingDomainModule),
        // Abp application modules
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpIdentityApplicationModule), // required for automapper
        // Abp features
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpTextTemplatingScribanModule),
        typeof(AbpEmailingModule),
        typeof(AbpUiNavigationModule),
        // Framework overriden features
        typeof(PolpAbpFrameworkAbpExtensionsEmalingModule),
        typeof(PolpAbpFrameworkAbpExtensionsSmsModule),
        // Contracts 
        typeof(PolpAbpFrameworkDomainModule),
        typeof(PolpAbpFrameworkApplicationContractsModule)
    )]
    public class PolpAbpFrameworkApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FrameworkApplicationAutoMapperProfile>();
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PolpAbpFrameworkApplicationModule>("PolpAbp.Framework");
            });

            // Configure
            var configuration = context.Services.GetConfiguration();
            var activationPath = configuration.GetValue<string>("PolpAbp:Account:EmailActivationPath");
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].Urls[FrameworkUrlNames.EmailActivation] = activationPath ?? "Account/EmailActivation";
            });


        }
    }
}
