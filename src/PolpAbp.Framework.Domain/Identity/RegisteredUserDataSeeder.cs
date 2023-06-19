using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace PolpAbp.Framework.Identity
{

    /// <summary>
    /// Provides a chance for preparing data for a newly registered user
    /// (excluding amdin).
    /// 
    /// Note that this class does not have any corresponding data seeder
    /// contributor, though it follows the data seeding paradigm
    /// of the abp framework.
    /// Therefore, it will not be automatically discovered by the abp framework.
    ///
    /// Its logic includes:
    ///   - Adding a user role if the user in question does not have;
    /// </summary>
    public class RegisteredUserDataSeeder : ITransientDependency, IRegisteredUserDataSeeder
    {
        protected IGuidGenerator GuidGenerator { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected ILookupNormalizer LookupNormalizer { get; }
        protected IdentityUserManager UserManager { get; }
        protected IdentityRoleManager RoleManager { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public RegisteredUserDataSeeder(
            IGuidGenerator guidGenerator,
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
            ILookupNormalizer lookupNormalizer,
            IdentityUserManager userManager,
            IdentityRoleManager roleManager,
            ICurrentTenant currentTenant)
        {
            GuidGenerator = guidGenerator;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            LookupNormalizer = lookupNormalizer;
            UserManager = userManager;
            RoleManager = roleManager;
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(
    string userEmail,
    Guid? tenantId = null)
        {
            Check.NotNullOrWhiteSpace(userEmail, nameof(userEmail));

            using (CurrentTenant.Change(tenantId))
            {
                //current user
                var currentUser = await UserRepository.FindByNormalizedEmailAsync(
                    LookupNormalizer.NormalizeName(userEmail)
                );

                if (currentUser == null)
                {
                    return;
                }

                //"user" role
                const string userRoleName = "member";
                var userRole = await RoleRepository.FindByNormalizedNameAsync(LookupNormalizer.NormalizeName(userRoleName));
                if (userRole == null)
                {
                    return;
                }

                // TODO: Verify if this method will create redundancy or not.
                (await UserManager.AddToRoleAsync(currentUser, userRoleName)).CheckErrors();
            }
        }
    }
}
