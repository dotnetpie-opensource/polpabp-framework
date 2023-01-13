using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpTenantManagementApplicationModule),
        typeof(PolpAbpFrameworkApplicationModule),
        typeof(PolpAbpFrameworkDomainTestsModule)
        )]
    public class FrameworkApplicationTestsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpIdentityServerBuilderOptions>(options =>
            {
                options.AddDeveloperSigningCredential = false;
            });

            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                identityServerBuilder.AddDeveloperSigningCredential(persistKey: false, filename: Guid.NewGuid().ToString());
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
        }
    }
}
