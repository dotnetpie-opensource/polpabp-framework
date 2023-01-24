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
                    FrameworkSettings.Account.IsTenantRegistrationEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Account.IsTenantRegistrationEnabled}"),
                    L($"Description:{FrameworkSettings.Account.IsTenantRegistrationEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.IsUserNameEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Account.IsUserNameEnabled}"), L($"Description:{FrameworkSettings.Account.IsUserNameEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Account.IsPhoneNumberEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Account.IsPhoneNumberEnabled}"), L($"Description:{FrameworkSettings.Account.IsPhoneNumberEnabled}"),
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
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsEnforced}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsEnforced}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsEmailProviderEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsEmailProviderEnabled}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsEmailProviderEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsSmsProviderEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsSmsProviderEnabled}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsSmsProviderEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsRememberBrowserEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsRememberBrowserEnabled}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsRememberBrowserEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.TwoFactor.IsGoogleAuthenticatorEnabled,
                    "false",
                    L($"DisplayName:{FrameworkSettings.Security.TwoFactor.IsGoogleAuthenticatorEnabled}"),
                    L($"Description:{FrameworkSettings.Security.TwoFactor.IsGoogleAuthenticatorEnabled}"),
                    isVisibleToClients: true
                    ),


                // Session timeout
                new SettingDefinition(
                    FrameworkSettings.Security.SessionTimeOut.IsEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Security.SessionTimeOut.IsEnabled}"), 
                    L($"Description:{FrameworkSettings.Security.SessionTimeOut.IsEnabled}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.SessionTimeOut.TimeOutSecond,
                    "120",
                    L($"DisplayName:{FrameworkSettings.Security.SessionTimeOut.TimeOutSecond}"),
                    L($"Description:{FrameworkSettings.Security.SessionTimeOut.TimeOutSecond}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.SessionTimeOut.ShowTimeOutNotificationSecond,
                    "60",
                    L($"DisplayName:{FrameworkSettings.Security.SessionTimeOut.ShowTimeOutNotificationSecond}"),
                    L($"Description:{FrameworkSettings.Security.SessionTimeOut.ShowTimeOutNotificationSecond}"),
                    isVisibleToClients: true
                    ),

                new SettingDefinition(
                    FrameworkSettings.Security.SessionTimeOut.ShowLockScreenWhenTimedOut,
                    "true",
                    L($"DisplayName:{FrameworkSettings.Security.SessionTimeOut.ShowLockScreenWhenTimedOut}"),
                    L($"Description:{FrameworkSettings.Security.SessionTimeOut.ShowLockScreenWhenTimedOut}"),
                    isVisibleToClients: true
                    ),

                // tenant management
                // email settings
                new SettingDefinition(
                    FrameworkSettings.TenantManagement.EmailSettings.UseHostDefault,
                    "true",
                    L($"DisplayName:{FrameworkSettings.TenantManagement.EmailSettings.UseHostDefault}"),
                    L($"Description:{FrameworkSettings.TenantManagement.EmailSettings.UseHostDefault}"),
                    isVisibleToClients: true
                    ),

                // Data privacy
                new SettingDefinition(
                    FrameworkSettings.DataPrivacy.IsCookieConsentEnabled,
                    "true",
                    L($"DisplayName:{FrameworkSettings.DataPrivacy.IsCookieConsentEnabled}"),
                    L($"Description:{FrameworkSettings.DataPrivacy.IsCookieConsentEnabled}"),
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

