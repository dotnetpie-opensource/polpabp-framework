using PolpAbp.Framework.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace PolpAbp.Framework.Settings
{
    public class FrameworkSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                // Account
                new SettingDefinition(
                    FrameworkSettings.Account.RegistrationApprovalType,
                    "0",
                    L($"DisplayName:{FrameworkSettings.Account.RegistrationApprovalType}"),
                    L($"Description:{FrameworkSettings.Account.RegistrationApprovalType}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.IsRegistrationDisabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.IsRegistrationDisabled}"),
                    L($"Description:{FrameworkSettings.Account.IsRegistrationDisabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.IsUserNameEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.IsUserNameEnabled}"), L($"Description:{FrameworkSettings.Account.IsUserNameEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.IsNewRegistrationNotyEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.IsNewRegistrationNotyEnabled}"), L($"Description:{FrameworkSettings.Account.IsNewRegistrationNotyEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.Sso.IsEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.Sso.IsEnabled}"),
                    L($"Description:{FrameworkSettings.Account.Sso.IsEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.Sso.IsEnforced,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.Sso.IsEnforced}"), L($"Description:{FrameworkSettings.Account.Sso.IsEnforced}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.Sso.Providers,
                    "",
                    L($"DisplayName:{FrameworkSettings.Account.Sso.Providers}"),
                    L($"Description:{FrameworkSettings.Account.Sso.Providers}"),
                    isVisibleToClients: true
                    ),

                // Security
                new SettingDefinition(
                    FrameworkSettings.Security.UseCaptchaOnRegistration,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Security.UseCaptchaOnRegistration}"), L($"Description:{FrameworkSettings.Security.UseCaptchaOnRegistration}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.UseCaptchaOnLogin,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Security.UseCaptchaOnLogin}"), L($"Description:{FrameworkSettings.Security.UseCaptchaOnLogin}"),
                    isVisibleToClients: true
                    ),

                // Two factor 
                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsEnabled}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsEnforced,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsEnforced}"), L($"Description:{FrameworkSettings.Security.TwoFactor.IsEnforced}"),
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

