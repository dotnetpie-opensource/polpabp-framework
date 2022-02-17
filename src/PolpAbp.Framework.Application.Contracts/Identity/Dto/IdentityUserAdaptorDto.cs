using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Identity.Dto
{
    public class IdentityUserAdaptorDto : IdentityUserDto
    {
        public Guid? ProfilePictureId { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public List<UserListRoleDto> Roles { get; set; }

        public bool IsActive { get; set; }

        public IdentityUserAdaptorDto()
        {
            Roles = new List<UserListRoleDto>();
        }
    }

    public class UserListRoleDto
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
