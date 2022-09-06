using System;
namespace PolpAbp.Framework.Settings
{
    public static class FrameworkSettings
    {
        public const string GroupName = "PolpAbp.Framework";

        // Account 
        public const string AccountSubGroup = $"{GroupName}.Account";

        public const string TenantAccountRegistrationDisabled = $"{AccountSubGroup}.Registeration.Disabled";
        // Email (0), Admin (1), Auto (2) ...
        public const string TenantAccountRegistrationApprovalType = $"{AccountSubGroup}.Registeration.ApprovalType";

        // Password complexity settings 
        public const string TenantAccountPassComplexityRequireDigit = $"{AccountSubGroup}.PasswordComplexity.RequireDigit";
        public const string TenantAccountPassComplexityRequireLowercase = $"{AccountSubGroup}.PasswordComplexity.RequireLowercase";
        public const string TenantAccountPassComplexityRequireNonAlphanumeric = $"{AccountSubGroup}.PasswordComplexity.RequireNonAlphanumeric";
        public const string TenantAccountPassComplexityRequiredLength = $"{AccountSubGroup}.PasswordComplexity.RequiredLength";
        public const string TenantAccountPassComplexityRequireUppercase = $"{AccountSubGroup}.PasswordComplexity.RequireUppercase";
    }
}