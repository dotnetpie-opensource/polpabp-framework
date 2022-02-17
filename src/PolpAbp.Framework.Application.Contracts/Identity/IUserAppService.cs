using System;
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
        /// <returns>A list of users</returns>
        Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(GetIdentityUsersInput input);

        /// <summary>
        /// Finds the list of user, across a department, with the given criteria.
        /// The client must establish the tenant context.
        /// Note that this method does not enforce permission check; it is up to the 
        /// client to check the permission.
        /// </summary>
        /// <param name="orgUnitId">Organization Unit ID</param>
        /// <param name="input">Criteria</param>
        /// <returns>A list of users</returns>
        Task<PagedResultDto<IdentityUserAdaptorDto>> GetListAsync(Guid orgUnitId, GetIdentityUsersInput input);
    }
}
