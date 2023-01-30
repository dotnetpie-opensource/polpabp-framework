namespace PolpAbp.Framework.Authorization
{
    public static class TenantManagementPermissions
    {
        // We do not introduce the contract package this moment.
        public const string GroupName = "SettingManagement";

        public const string Billing = GroupName + ".Billing";

        public const string UserManagement = GroupName + ".UserManagement";

        public const string Security = GroupName + ".Security";

        public const string Appearance = GroupName + ".Appearance";
    }
}
