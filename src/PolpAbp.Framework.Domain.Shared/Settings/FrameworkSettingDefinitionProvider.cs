using System;
using Volo.Abp.Settings;

namespace PolpAbp.Framework.Settings
{
    public class FrameworkSettingDefinitionProvider : SettingDefinitionProvider
    {
        // todo: 
        public override void Define(ISettingDefinitionContext context)
        {
            // Tenant Account 
            context.Add(
                // Registration
                new SettingDefinition(FrameworkSettings.TenantAccountRegistrationApprovalType, "0")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                new SettingDefinition(FrameworkSettings.TenantAccountRegistrationDisabled, "false")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                // Password complexity
                new SettingDefinition(FrameworkSettings.TenantAccountPassComplexityRequireDigit, "true")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                new SettingDefinition(FrameworkSettings.TenantAccountPassComplexityRequireLowercase, "true")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                new SettingDefinition(FrameworkSettings.TenantAccountPassComplexityRequireUppercase, "true")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                new SettingDefinition(FrameworkSettings.TenantAccountPassComplexityRequireNonAlphanumeric, "true")
                .WithProviders(TenantSettingValueProvider.ProviderName),
                new SettingDefinition(FrameworkSettings.TenantAccountPassComplexityRequiredLength, "8")
                .WithProviders(TenantSettingValueProvider.ProviderName)
                );
        }
    }
}

