using Microsoft.AspNetCore.Identity;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace PolpAbp.Framework
{
    // The following is to fix the issue from the abp library: 
    // With the abp implementation, the lowercase of "admin" will be used in the permission store.
    // However, we want to use a normalized name.
    [ExposeServices(typeof(IPermissionDataSeeder))]
    public class PolpAbpPermissionDataSeeder : IPermissionDataSeeder, ITransientDependency
    {
        protected IPermissionGrantRepository PermissionGrantRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }

        protected ICurrentTenant CurrentTenant { get; }

        protected ILookupNormalizer LookupNormalizer { get; }

        public PolpAbpPermissionDataSeeder(
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ILookupNormalizer lookupNormalizer)
        {
            PermissionGrantRepository = permissionGrantRepository;
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
            LookupNormalizer = lookupNormalizer;
        }

        public virtual async Task SeedAsync(
            string providerName,
            string providerKey,
            IEnumerable<string> grantedPermissions,
            Guid? tenantId = null)
        {
            if (providerName == RolePermissionValueProvider.ProviderName)
            {
                providerKey = LookupNormalizer.NormalizeName(providerKey);
            }

            using (CurrentTenant.Change(tenantId))
            {
                var names = grantedPermissions.ToArray();
                var existsPermissionGrants = (await PermissionGrantRepository.GetListAsync(names, providerName, providerKey)).Select(x => x.Name).ToList();

                foreach (var permissionName in names.Except(existsPermissionGrants))
                {
                    await PermissionGrantRepository.InsertAsync(
                        new PermissionGrant(
                            GuidGenerator.Create(),
                            permissionName,
                            providerName,
                            providerKey,
                            tenantId
                        )
                    );
                }
            }
        }
    }
}
