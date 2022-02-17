using Microsoft.AspNetCore.Identity;
using PolpAbp.Framework.Identity.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using PolpAbp.Framework.Identity;

namespace PolpAbp.Framework.Identity
{
    [RemoteService(false)]
    public class UserLookupAppService : FrameworkAppService, IUserLookupAppService
    {
        protected IIdentityUserRepository UserRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }

        private readonly IIdentityUserRepositoryExt _userRepositoryExt;

        public UserLookupAppService(
            IIdentityUserRepository userRepository,
            ILookupNormalizer lookupNormalizer,
            IIdentityUserRepositoryExt userRepositoryExt)
        {
            UserRepository = userRepository;
            LookupNormalizer = lookupNormalizer;
            _userRepositoryExt = userRepositoryExt;
        }

        public async Task<PagedResultDto<IUserData>> SearchAsync(
           string sorting = null,
           string filter = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {

            var count = await UserRepository.GetCountAsync(filter);

            var items = await UserRepository.GetListAsync(
                sorting: sorting,
                maxResultCount: maxResultCount,
                skipCount: skipCount,
                filter: filter,
                includeDetails: false,
                cancellationToken: cancellationToken
            );

            var payload = items.Select(x => x.ToAbpUserData()).ToList();

            return new PagedResultDto<IUserData>(count, payload);
        }
        
        public async Task<ListResultDto<IUserData>> ResolveAsync(ResolveUsersInputDto input)
        {
            var items = await _userRepositoryExt.GetListAsync(input.Ids);
            var data = items.Select(x => x.ToAbpUserData()).ToList();
            return new ListResultDto<IUserData>(data);
        }

    }
}
