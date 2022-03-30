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
        Task<long> CountUsersNotInRoleAsync(Guid RoleId, string filter = null, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetListAsync(Guid[] ids, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(Guid organizationUnitId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersInRolAsync(Guid RoleId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<List<IdentityUser>> GetUsersNotInRolAsync(Guid RoleId, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default);
    }
}
