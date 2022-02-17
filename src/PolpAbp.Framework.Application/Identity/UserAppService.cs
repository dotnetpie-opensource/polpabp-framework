using PolpAbp.Framework.Identity.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace PolpAbp.Framework.Identity
{
    [RemoteService(false)]
    public class UserAppService : FrameworkAppService, IUserAppService
    {
        protected readonly IIdentityUserRepository IdentityUserRepository;
        protected readonly IIdentityRoleRepository RoleRepository;
        protected readonly IIdentityUserRepositoryExt IdentityUserRepositoryExt;
        protected readonly IOrganizationUnitRepository OrganizationUnitRepository;

        public UserAppService(IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IIdentityUserRepositoryExt identityUserRepositoryExt,
            IOrganizationUnitRepository organizationUnitRepository)
        {
            IdentityUserRepository = identityUserRepository;
            RoleRepository = identityRoleRepository;
            IdentityUserRepositoryExt = identityUserRepositoryExt;
            OrganizationUnitRepository = organizationUnitRepository;
        }

        public async Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(GetIdentityUsersInput input) 
        {
            var roles = await RoleRepository.GetListAsync();
            var rolesDict = roles.ToDictionary(x => x.Id, x => x.Name);

            var orgUnits = await OrganizationUnitRepository.GetListAsync();
            var orgUnitsDict = orgUnits.ToDictionary(x => x.Id, x => x.DisplayName);

            var count = await IdentityUserRepository.GetCountAsync(input.Filter);

            var list = await IdentityUserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: true);
            var aList = list.Select(a =>
            {
                var b = new IdentityUserAdaptorDto();
                ObjectMapper.Map<IdentityUser, IdentityUserDto>(a, b);
                if (a.Roles != null)
                {
                    b.Roles = a.Roles.Select(m => {
                        rolesDict.TryGetValue(m.RoleId, out var name);
                        return new UserListRoleDto
                        {
                            RoleId = m.RoleId,
                            RoleName = name // todo
                        };
                    }).ToList();
                }
                if (a.OrganizationUnits != null)
                {
                    b.OrgUnits = a.OrganizationUnits.Select(m =>
                    {
                        orgUnitsDict.TryGetValue(m.OrganizationUnitId, out var name);
                        return new UserListOrgUnitDto
                        {
                            OrgUnitId = m.OrganizationUnitId,
                            OrgUnitName = name 
                        };
                    }).ToList();
                }

                return b;
            }).ToList();

            return new PagedResultDto<IdentityUserAdaptorDto>(count, aList);
        }

        public async Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(Guid orgUnitId, GetIdentityUsersInput input)
        {
            var roles = await RoleRepository.GetListAsync();
            var dict = roles.ToDictionary(x => x.Id, x => x.Name);
            var count = await IdentityUserRepository.GetCountAsync(input.Filter);

            var list = await IdentityUserRepositoryExt
                .GetUsersInOrganizationUnitAsync(orgUnitId, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: true);
            var aList = list.Select(a =>
            {
                var b = new IdentityUserAdaptorDto();
                ObjectMapper.Map<IdentityUser, IdentityUserDto>(a, b);
                if (a.Roles != null)
                {
                    b.Roles = a.Roles.Select(m => {
                        dict.TryGetValue(m.RoleId, out var name);
                        return new UserListRoleDto
                        {
                            RoleId = m.RoleId,
                            RoleName = name // todo
                        };
                    }).ToList();
                }

                return b;
            }).ToList();

            return new PagedResultDto<IdentityUserAdaptorDto>(count, aList);
        }


    }
}
