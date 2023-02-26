using Microsoft.Extensions.Configuration;
using PolpAbp.Framework.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.Localization;

namespace PolpAbp.Framework
{
    public class FrameworkPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        private readonly IConfiguration _configuration;

        public FrameworkPermissionDefinitionProvider(IConfiguration configuration)
            :base()
        { 
            _configuration = configuration; 
        }

        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(OrganizationUnitPermissions.GroupName, L("Permission:OrganizationUnits"));

            var rolesPermission = identityGroup.AddPermission(OrganizationUnitPermissions.Default, L("Permission:OrganizationUnits_Default"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageTree, L("Permission:OrganizationUnits_ManageOrganizationTree"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageMembers, L("Permission:OrganizationUnits_ManageMembers"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageRoles, L("Permission:OrganizationUnits_ManageRoles"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManagePermissions, L("Permission:OrganizationUnits_ManagePermissions"));


            var tenantSettingGroup = context.GetGroup(SettingManagementPermissions.GroupName);
            // Add other ...
            tenantSettingGroup.AddPermission(TenantManagementPermissions.UserManagement, 
                LL("Permission:SettingManagement_UserManagement"));
            tenantSettingGroup.AddPermission(TenantManagementPermissions.Security,
                LL("Permission:SettingManagement_Security"));
            tenantSettingGroup.AddPermission(TenantManagementPermissions.Billing,
                LL("Permission:SettingManagement_Billing"));
            tenantSettingGroup.AddPermission(TenantManagementPermissions.Appearance,
                LL("Permission:SettingManagement_Appearance"));

            var hasImpersonationFeature = _configuration.GetValue<bool>("PolpAbp:Framework:HasImpersonationFeature");
            if (hasImpersonationFeature)
            {
                var usersPermission = context.GetPermissionOrNull(IdentityPermissions.Users.Default);
                if (usersPermission != null)
                {
                    usersPermission.AddChild("AbpIdentity.Users.Impersonation", L("Permission:Users_Impersonation"));
                }
            }
            var hasAuditLoggingFeature = _configuration.GetValue<bool>("PolpAbp:Framework:HasAuditLoggingFeature");
            if (hasAuditLoggingFeature)
            {
                var group = context.AddGroup(AuditLoggingPermissions.GroupName, L("Permission:AuditLogging"));
                group.AddPermission(AuditLoggingPermissions.Default, L("Permission:AuditLogging_Default"));
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }

        private static LocalizableString LL(string name)
        {
            return LocalizableString.Create<AbpSettingManagementResource>(name);
        }

    }
}
