using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Identity.Dto
{
    public class IdentityUserAdaptorDto : IdentityUserDto
    {
        public Guid? ProfilePictureId { get; set; }

        /// <summary>
        /// A redundancy item on purpose
        /// </summary>
        public string EmailAddress => Email;

        /// <summary>
        /// A redundancy item on purpose
        /// </summary>
        public bool IsEmailConfirmed => EmailConfirmed;

        public List<UserListRoleDto> Roles { get; set; }

        public List<UserListOrgUnitDto> OrgUnits { get; set; }

        public bool IsExternal { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public IdentityUserAdaptorDto()
        {
            Roles = new List<UserListRoleDto>();
            OrgUnits = new List<UserListOrgUnitDto>();
        }
    }

    public class UserListRoleDto
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }

    public class UserListOrgUnitDto
    {
        public Guid OrgUnitId { get;set; }
        public string OrgUnitName { get; set; }
    }
}
