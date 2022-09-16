using System;
namespace PolpAbp.Framework.Settings
{
    public static class FrameworkSettings
    {
        public const string GroupName = "PolpAbp.Framework";

        // Account (user management)
        public const string AccountSubGroup = $"{GroupName}.Account";

        public const string IsRegistrationDisabled = $"{AccountSubGroup}.RegistrationDisabled";
        // Email (0), Admin (1), Auto (2) ...
        public const string RegistrationApprovalType = $"{AccountSubGroup}.RegistrationApprovalType";
        /// <summary>
        /// Decides if the user name is enabled
        ///   - For login
        ///   - For registraion (maybe)
        /// </summary>
        public const string IsUserNameEnabled = $"{AccountSubGroup}.IsUserNameEnabled";

        public const string IsNewRegistrationNotyEnabled = $"{AccountSubGroup}.IsNewRegistrationNotyEnabled";


        // Security
        public const string SecuritySubGroup = $"{GroupName}.Security";

        // Decides if Recaptcha is enabled for the registraion.
        public const string IsRecaptchaDisabledOnRegistration = $"{SecuritySubGroup}.IsRecaptchaDisabledOnRegistration";
        // Decides if Recaptcha is enabled for the login.
        public const string IsRecaptchaDisabledOnLogin = $"{SecuritySubGroup}.Login.IsRecaptchaDisabledOnLogin";

        // Password complexity settings (security)
        public const string AccountPassComplexityRequireDigit = $"{SecuritySubGroup}.PasswordComplexity.RequireDigit";
        public const string AccountPassComplexityRequireLowercase = $"{SecuritySubGroup}.PasswordComplexity.RequireLowercase";
        public const string AccountPassComplexityRequireNonAlphanumeric = $"{SecuritySubGroup}.PasswordComplexity.RequireNonAlphanumeric";
        public const string AccountPassComplexityRequiredLength = $"{SecuritySubGroup}.PasswordComplexity.RequiredLength";
        public const string AccountPassComplexityRequireUppercase = $"{SecuritySubGroup}.PasswordComplexity.RequireUppercase";


        // Session timeout
        public const string SecuritySessionSubGroup = $"{SecuritySubGroup}.SessionTimeout";

        // Date privacy
        public const string DataPrivacySubGroup = $"{GroupName}.DataPrivacy";

        // Cookie consent
        public const string IsCookieConsentRequired = $"{DataPrivacySubGroup}.IsCookieConsentRequired"; 

    }
}