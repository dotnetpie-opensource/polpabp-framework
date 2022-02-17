using PolpAbp.Framework.Identity.Dto;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace PolpAbp.Framework.Identity
{
    /// <summary>
    /// Provides the services of looking up users for an authenticated user.
    /// Note that we cannot use "AbpIdentity.Lookup" from the abp identity, 
    /// as that service is used for a (machine) client. We cannot use its permission 
    /// in this case. 
    /// (That permission is only used for a machine client.)
    /// </summary>
    public interface IUserLookupAppService : IApplicationService
    {
        Task<ListResultDto<IUserData>> ResolveAsync(ResolveUsersInputDto input);
        Task<PagedResultDto<IUserData>> SearchAsync(string sorting = null, string filter = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    }
}
