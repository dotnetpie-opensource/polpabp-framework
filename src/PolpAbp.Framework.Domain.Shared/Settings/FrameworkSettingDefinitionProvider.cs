using System;
using PolpAbp.Framework.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace PolpAbp.Framework.Settings
{
    public class FrameworkSettingDefinitionProvider : SettingDefinitionProvider
    {
        // todo: 
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                // Account
                new SettingDefinition(
                    FrameworkSettings.RegistrationApprovalType,
                    "0",
                    L("DisplayName:PolpAbp.Framework.Account.RegistrationApprovalType"),
                    L("Description:PolpAbp.Framework.Account.RegistrationApprovalType"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsRegistrationDisabled,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Account.IsRegistrationDisabled"),
                    L("Description:PolpAbp.Framework.Account.IsRegistrationDisabled"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsUserNameEnabled,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Account.IsUserNameEnabled"),
                    L("Description:PolpAbp.Framework.Account.IsUserNameEnabled"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsNewRegistrationNotyEnabled,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Account.IsNewRegistrationNotyEnabled"),
                    L("Description:PolpAbp.Framework.Account.IsNewRegistrationNotyEnabled"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsSsoEnabled,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Account.Sso.Enabled"),
                    L("Description:PolpAbp.Framework.Account.Sso.Enabled"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsSsoEnforced,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Account.Sso.Enforced"),
                    L("Description:PolpAbp.Framework.Account.Sso.Enforced"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.SsoProviders,
                    "",
                    L("DisplayName:PolpAbp.Framework.Account.Sso.Providers"),
                    L("Description:PolpAbp.Framework.Account.Sso.Providers"),
                    isVisibleToClients: true
                    ),

                // Security
                new SettingDefinition(
                    FrameworkSettings.IsRecaptchaDisabledOnRegistration,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Security.IsRecaptchaDisabledOnRegistration"),
                    L("Description:PolpAbp.Framework.Security.IsRecaptchaDisabledOnRegistration"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsRecaptchaDisabledOnLogin,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Security.IsRecaptchaDisabledOnLogin"),
                    L("Description:PolpAbp.Framework.Security.IsRecaptchaDisabledOnLogin"),
                    isVisibleToClients: true
                    ),

                // Two factor 
                new SettingDefinition(
                    FrameworkSettings.IsTwoFactorEnabled,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Security.TwoFactor.Enabled"),
                    L("Description:PolpAbp.Framework.Security.TwoFactor.Enabled"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.IsTwoFactorEnforced,
                    "false",
                    L("DisplayName:PolpAbp.Framework.Security.TwoFactor.Enforced"),
                    L("Description:PolpAbp.Framework.Security.TwoFactor.Enforced"),
                    isVisibleToClients: true
                    )

                );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PolpAbpFrameworkResource>(name);
        }
    }
}

