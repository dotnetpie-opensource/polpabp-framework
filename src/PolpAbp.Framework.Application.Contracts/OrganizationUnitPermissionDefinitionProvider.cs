﻿using PolpAbp.Framework.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace PolpAbp.Framework
{
    public class OrganizationUnitPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(OrganizationUnitPermissions.GroupName, L("Permission:OrganizationUnits"));

            var rolesPermission = identityGroup.AddPermission(OrganizationUnitPermissions.Default, L("Permission:OrganizationUnits_Default"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageTree, L("Permission:OrganizationUnits_ManageOrganizationTree"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageMembers, L("Permission:OrganizationUnits_ManageMembers"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManageRoles, L("Permission:OrganizationUnits_ManageRoles"));
            rolesPermission.AddChild(OrganizationUnitPermissions.ManagePermissions, L("Permission:OrganizationUnits_ManagePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
