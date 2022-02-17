using System;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Identity.Dto
{
    public class IdentityRoleAdaptorDto : IdentityRoleDto
    {
        public string DisplayName { get; set; }
    }
}
