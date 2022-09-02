using System;
using Volo.Abp.Settings;

namespace PolpAbp.Framework.Settings
{
    public class FrameworkSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            // Account 
            context.Add(new SettingDefinition(FrameworkSettings.TenantAccountRegistrationApprovalType));
            context.Add(new SettingDefinition(FrameworkSettings.TenantAccountRegistrationDisabled));
        }
    }
}

