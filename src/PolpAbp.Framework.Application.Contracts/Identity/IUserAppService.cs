using System;
using System.Threading;
using System.Threading.Tasks;
using PolpAbp.Framework.Identity.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Identity
{
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// Finds the list of users, across a tenant, with the given criteria.
        /// The result includes the roles for each user.
        /// The client must establish the tenant context.
        /// Note that this method does not enforce permission check; it is up to the 
        /// client to check the permission.
        /// </summary>
        /// <param name="input">Critiera</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of users</returns>
        Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(GetIdentityUsersInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds the list of user, across a department, with the given criteria.
        /// The client must establish the tenant context.
        /// Note that this method does not enforce permission check; it is up to the 
        /// client to check the permission.
        /// </summary>
        /// <param name="orgUnitId">Organization Unit ID</param>
        /// <param name="input">Criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of users</returns>
        Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(Guid orgUnitId, GetIdentityUsersInput input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the details for the given list of ids.
        /// This method can be used when a given list of ids can be figured out in other means.
        /// The client must establish the tenant context.
        /// Note that this method does not enforce permission check; it is up to the 
        /// client to check the permission.
        /// </summary>
        /// <param name="ids">List of ids</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of users</returns>
        Task<ListResultDto<IdentityUserAdaptorDto>> GetListAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}
