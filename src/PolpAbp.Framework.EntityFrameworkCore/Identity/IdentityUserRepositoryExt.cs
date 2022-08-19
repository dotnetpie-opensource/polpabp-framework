using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace PolpAbp.Framework.Identity
{
    public class IdentityUserRepositoryExt : EfCoreRepository<IIdentityDbContext, IdentityUser, Guid>, IIdentityUserRepositoryExt
    {
        public IdentityUserRepositoryExt(IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<IdentityUser>> FindUsersByEmailAsync(string email,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var ret = await dbSet
                .IncludeDetails(includeDetails)
                .Where(x => x.Email == email)
                .ToListAsync(GetCancellationToken(cancellationToken));

            return ret;
        }

        public virtual async Task<List<IdentityUser>> GetListAsync( 
            Guid[] ids,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var ret = await dbSet
                .IncludeDetails(includeDetails)
                .Where( x => ids.Contains(x.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));

            return ids.Select(a => ret.Find(b => b.Id == a)).Where(c => c != null).ToList();
        }

        public async Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(
            Guid organizationUnitId,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                            .IncludeDetails(includeDetails)
                            .Where(x => x.OrganizationUnits.Any(y => y.OrganizationUnitId == organizationUnitId))
                            .WhereIf(
                                !filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.UserName.Contains(filter) ||
                                    u.Email.Contains(filter) ||
                                    (u.Name != null && u.Name.Contains(filter)) ||
                                    (u.Surname != null && u.Surname.Contains(filter)) ||
                                    (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                            )
                            .OrderBy(sorting ?? nameof(IdentityUser.Name))
                            .PageBy(skipCount, maxResultCount)
                            .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> CountUsersInOrganizationUnitAsync(
            Guid organizationUnitId,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();

            return  await queryable.Where(x => x.OrganizationUnits.Any(y => y.OrganizationUnitId == organizationUnitId))
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<IdentityUser>> GetUsersNotInOrganizationUnitAsync(
            Guid organizationUnitId,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                            .IncludeDetails(includeDetails)
                            .Where(x => !x.OrganizationUnits.Any(y => y.OrganizationUnitId == organizationUnitId))
                            .WhereIf(
                                !filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.UserName.Contains(filter) ||
                                    u.Email.Contains(filter) ||
                                    (u.Name != null && u.Name.Contains(filter)) ||
                                    (u.Surname != null && u.Surname.Contains(filter)) ||
                                    (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                            )
                            .OrderBy(sorting ?? nameof(IdentityUser.Name))
                            .PageBy(skipCount, maxResultCount)
                            .ToListAsync(GetCancellationToken(cancellationToken));
        }


        public virtual async Task<long> CountUsersNotInOrganizationUnitAsync(
                Guid organizationUnitId,
                string filter = null,
                CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();

            return await queryable.Where(x => !x.OrganizationUnits.Any(y => y.OrganizationUnitId == organizationUnitId))
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<List<IdentityUser>> GetUsersInRoleAsync(
        Guid RoleId,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        string filter = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                            .IncludeDetails(includeDetails)
                            .Where(x => x.Roles.Any(y => y.RoleId == RoleId))
                            .WhereIf(
                                !filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.UserName.Contains(filter) ||
                                    u.Email.Contains(filter) ||
                                    (u.Name != null && u.Name.Contains(filter)) ||
                                    (u.Surname != null && u.Surname.Contains(filter)) ||
                                    (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                            )
                            .OrderBy(sorting ?? nameof(IdentityUser.Name))
                            .PageBy(skipCount, maxResultCount)
                            .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> CountUsersInRoleAsync(
                 Guid RoleId,
                 string filter = null,
                 CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();

            return await queryable.Where(x => x.Roles.Any(y => y.RoleId == RoleId))
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<IdentityUser>> GetUsersNotInRoleAsync(
             Guid RoleId,
             string sorting = null,
             int maxResultCount = int.MaxValue,
             int skipCount = 0,
             string filter = null,
             bool includeDetails = false,
             CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                            .IncludeDetails(includeDetails)
                            .Where(x => x.Roles.All(y => y.RoleId != RoleId))
                            .WhereIf(
                                !filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.UserName.Contains(filter) ||
                                    u.Email.Contains(filter) ||
                                    (u.Name != null && u.Name.Contains(filter)) ||
                                    (u.Surname != null && u.Surname.Contains(filter)) ||
                                    (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                            )
                            .OrderBy(sorting ?? nameof(IdentityUser.Name))
                            .PageBy(skipCount, maxResultCount)
                            .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> CountUsersNotInRoleAsync(
                 Guid RoleId,
                 string filter = null,
                 CancellationToken cancellationToken = default)
        {
            var queryable = await GetQueryableAsync();

            return await queryable.Where(x => x.Roles.All(y => y.RoleId != RoleId))
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(filter) ||
                        u.Email.Contains(filter) ||
                        (u.Name != null && u.Name.Contains(filter)) ||
                        (u.Surname != null && u.Surname.Contains(filter)) ||
                        (u.PhoneNumber != null && u.PhoneNumber.Contains(filter))
                )
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}
