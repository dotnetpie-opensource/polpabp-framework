namespace PolpAbp.Framework.Settings
{
    public static class FrameworkSettings
    {
        public const string GroupName = "PolpAbp.Framework";

        public static class Account
        {
            // Account (user management)
            public const string AccountSubGroup = $"{GroupName}.Account";

            public const string IsTenantRegistrationEnabled = $"{AccountSubGroup}.{nameof(IsTenantRegistrationEnabled)}";
            // Email (0), Admin (1), Auto (2) ...
            public const string RegistrationApprovalType = $"{AccountSubGroup}.{nameof(RegistrationApprovalType)}";
            /// <summary>
            /// Decides if the user name is enabled
            ///   - For login
            ///   - For registraion (maybe)
            /// </summary>
            public const string IsUserNameEnabled = $"{AccountSubGroup}.{nameof(IsUserNameEnabled)}";

            public const string IsPhoneNumberEnabled = $"{AccountSubGroup}.{nameof(IsPhoneNumberEnabled)}";

            public const string IsNewRegistrationNotyEnabled = $"{AccountSubGroup}.{nameof(IsNewRegistrationNotyEnabled)}";

            public static class Sso
            {
                // SSO provider
                public const string IsEnabled = $"{AccountSubGroup}.Sso.{nameof(IsEnabled)}";
                public const string IsEnforced = $"{AccountSubGroup}.Sso.{nameof(IsEnforced)}";
                public const string Providers = $"{AccountSubGroup}.Sso.{nameof(Providers)}";
            }
        }

        public static class Security
        {

            // Security
            public const string SecuritySubGroup = $"{GroupName}.Security";

            // Decides if Recaptcha is enabled for the registraion.
            public const string UseCaptchaOnRegistration = $"{SecuritySubGroup}.{nameof(UseCaptchaOnRegistration)}";

            // Decides if Recaptcha is enabled for the login.
            public const string UseCaptchaOnLogin = $"{SecuritySubGroup}.{nameof(UseCaptchaOnLogin)}";

            // Decides if the tenant use the default settings.
            public const string UseDefaultPasswordComplexitySettings = $"{SecuritySubGroup}.{nameof(UseDefaultPasswordComplexitySettings)}";

            // Two factor code
            public static class TwoFactor
            {
                public const string Prefix = $"{SecuritySubGroup}.TwoFactor";

                public const string IsEnabled = $"{Prefix}.{nameof(IsEnabled)}";
                public const string IsEnforced = $"{Prefix}.{nameof(IsEnforced)}";
                public const string IsEmailProviderEnabled = $"{Prefix}.{nameof(IsEmailProviderEnabled)}";
                public const string IsSmsProviderEnabled = $"{Prefix}.{nameof(IsSmsProviderEnabled)}";
                public const string IsRememberBrowserEnabled = $"{Prefix}.{nameof(IsRememberBrowserEnabled)}";
                public const string IsGoogleAuthenticatorEnabled = $"{Prefix}.{nameof(IsGoogleAuthenticatorEnabled)}";
            }

            // Session timeout
            public static class SessionTimeOut
            {
                public const string Prefix = $"{SecuritySubGroup}.SessionTimeOut";

                public const string IsEnabled = $"{Prefix}.{nameof(IsEnabled)}";
                public const string TimeOutSecond = $"{Prefix}.{nameof(TimeOutSecond)}";
                public const string ShowTimeOutNotificationSecond = $"{Prefix}.{nameof(ShowTimeOutNotificationSecond)}";
                public const string ShowLockScreenWhenTimedOut = $"{Prefix}.{nameof(ShowLockScreenWhenTimedOut)}";
            }
        }

        public static class DataPrivacy
        {
            // Date privacy
            public const string DataPrivacySubGroup = $"{GroupName}.DataPrivacy";

            // Cookie consent
            public const string IsCookieConsentEnabled = $"{DataPrivacySubGroup}.{nameof(IsCookieConsentEnabled)}";

        }

        public static class TenantManagement
        {
            public const string TenantManagementSubGroup = $"{GroupName}.TenantManagement";

            public static class EmailSettings
            {
                public const string UseHostDefault = $"{TenantManagementSubGroup}.Email.{nameof(UseHostDefault)}";
            }
        }

    }
}