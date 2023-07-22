using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Identity
{
    public interface IIdentityUserRepositoryExt : IBasicRepository<IdentityUser, Guid>
    {
        Task<long> CountUsersInOrganizationUnitAsync(Guid organizationUnitId, string filter = null, CancellationToken cancellationToken = default);
        Task<long> CountUsersInRoleAsync(Guid RoleId, string filter = null, CancellationToken cancellationToken = default);
        Task<long> CountUsersNotInOrganizationUnitAsync(Guid organizationUnitId, string filter = null, CancellationToken cancellationToken = default);
        Task<long> CountUsersNotInRoleAsync(Guid RoleId, string filter = null, CancellationToken cancellationToken = default);
        /// <summary>
        ///  Finds the list of users with the given email.
        ///  This method will report all users across tenant. 
        ///  It must be used in the context of IMultiTenancy being disabled.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="includeDetails">Details</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of users</returns>
        Task<List<IdentityUser>> FindUsersByEmailAsync(string email, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetListAsync(Guid[] ids, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<int> GetRolesCountInOrganizationUnitAsync(Guid organizationUnitId, CancellationToken cancellationToken = default);
        Task<List<Tuple<IdentityRole, DateTime>>> GetRolesInOrganizationUnitAsync(Guid organizationUnitId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(Guid organizationUnitId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersInRoleAsync(Guid RoleId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersNotInOrganizationUnitAsync(Guid organizationUnitId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersNotInRoleAsync(Guid RoleId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
    }
}
