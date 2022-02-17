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
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IIdentityUserRepositoryExt _identityUserRepositoryExt;

        public UserAppService(IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IObjectMapper objectMapper,
            IIdentityUserRepositoryExt identityUserRepositoryExt)
        {
            _identityUserRepository = identityUserRepository;
            _roleRepository = identityRoleRepository;
            _objectMapper = objectMapper;
            _identityUserRepositoryExt = identityUserRepositoryExt;
        }

        public async Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var roles = await _roleRepository.GetListAsync();
            var dict = roles.ToDictionary(x => x.Id, x => x.Name);
            var count = await _identityUserRepository.GetCountAsync(input.Filter);

            var list = await _identityUserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: true);
            var aList = list.Select(a =>
            {
                var b = new IdentityUserAdaptorDto();
                _objectMapper.Map<IdentityUser, IdentityUserDto>(a, b);
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
                b.IsEmailConfirmed = b.EmailConfirmed;
                b.EmailAddress = b.Email;

                return b;
            }).ToList();

            return new PagedResultDto<IdentityUserAdaptorDto>(count, aList);
        }

        public async Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(Guid orgUnitId, GetIdentityUsersInput input)
        {
            var roles = await _roleRepository.GetListAsync();
            var dict = roles.ToDictionary(x => x.Id, x => x.Name);
            var count = await _identityUserRepository.GetCountAsync(input.Filter);

            var list = await _identityUserRepositoryExt
                .GetUsersInOrganizationUnitAsync(orgUnitId, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: true);
            var aList = list.Select(a =>
            {
                var b = new IdentityUserAdaptorDto();
                _objectMapper.Map<IdentityUser, IdentityUserDto>(a, b);
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
                b.IsEmailConfirmed = b.EmailConfirmed;
                b.EmailAddress = b.Email;

                return b;
            }).ToList();

            return new PagedResultDto<IdentityUserAdaptorDto>(count, aList);
        }


    }
}
