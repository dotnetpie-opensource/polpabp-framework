using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace PolpAbp.Framework.Identity
{
    /// <summary>
    /// Provides a chance for additionally preparing data for a newly created tenant.
    /// Note that this class is not designed to be used in a data seeder contributor.
    /// 
    /// Therefore, it will not automatically discovered and invoked
    /// when application starts.
    /// 
    /// It includes the following logic:
    ///   - Create a user role, if there is no user role.
    /// </summary>
    public class RegisteredTenantDataSeeder: ITransientDependency, IRegisteredTenantDataSeeder
    {
        protected IGuidGenerator GuidGenerator { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }
        protected IdentityRoleManager RoleManager { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public RegisteredTenantDataSeeder(
            IGuidGenerator guidGenerator,
            IIdentityRoleRepository roleRepository,
            ILookupNormalizer lookupNormalizer,
            IdentityRoleManager roleManager,
            ICurrentTenant currentTenant)
        {
            GuidGenerator = guidGenerator;
            RoleRepository = roleRepository;
            LookupNormalizer = lookupNormalizer;
            RoleManager = roleManager;
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(Guid? tenantId = null)
        {
            using (CurrentTenant.Change(tenantId))
            {
                //"user" role
                const string userRoleName = "user";
                var userRole = await RoleRepository.FindByNormalizedNameAsync(LookupNormalizer.NormalizeName(userRoleName));
                if (userRole == null)
                {
                    userRole = new IdentityRole(
                        GuidGenerator.Create(),
                        userRoleName,
                        tenantId
                    )
                    {
                        IsStatic = true,
                        IsPublic = true
                    };

                    (await RoleManager.CreateAsync(userRole)).CheckErrors();
                }
            }
        }
    }
}
