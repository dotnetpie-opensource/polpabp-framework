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
    }
}