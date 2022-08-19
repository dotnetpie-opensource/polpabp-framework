﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpIdentityModelModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpTextTemplatingScribanModule),
        typeof(AbpEmailingModule),
        typeof(AbpUiNavigationModule),
        typeof(AbpIdentityApplicationModule), // required for automapper
        typeof(PolpAbpFrameworkApplicationContractsModule),
        typeof(PolpAbpFrameworkDomainModule)
    )]
    public class PolpAbpFrameworkApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FrameworkApplicationAutoMapperProfile>();
            });

            // Configure
            var configuration = context.Services.GetConfiguration();
            var activationPath = configuration.GetValue<string>("PolpAbpFramework:AccountEmailActivationPath");
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].Urls[FrameworkUrlNames.EmailActivation] = activationPath ?? "Account/EmailActivation";
            });

        }
    }
}
