using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Authorization.Permissions;

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
        protected IConfiguration Configuration { get; }
        protected IPermissionDataSeeder PermissionDataSeeder { get; }

        public RegisteredTenantDataSeeder(
            IGuidGenerator guidGenerator,
            IIdentityRoleRepository roleRepository,
            ILookupNormalizer lookupNormalizer,
            IdentityRoleManager roleManager,
            ICurrentTenant currentTenant,
            IConfiguration configuration,
            IPermissionDataSeeder permissionDataSeeder)
        {
            GuidGenerator = guidGenerator;
            RoleRepository = roleRepository;
            LookupNormalizer = lookupNormalizer;
            RoleManager = roleManager;
            CurrentTenant = currentTenant;
            Configuration = configuration;
            PermissionDataSeeder = permissionDataSeeder;   
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(Guid? tenantId = null)
        {
            if (!tenantId.HasValue)
            {
                return;
            }


            using (CurrentTenant.Change(tenantId))
            {
                //"user" role
                const string userRoleName = "member";
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

            // Predefined roles
            var rolesCfg = Configuration.GetSection("PolpAbp:Framework:PredefinedRoles");
            var roleDefs = new List<PredefinedRole>();
            rolesCfg.Bind(roleDefs);
            foreach (var d in roleDefs)
            {
                await EnsureRoleAsync(d.Name, d.GrantedPermissions, tenantId.Value);
            }
        }

        protected async Task EnsureRoleAsync(string roleName, List<string> grantedPermissions, Guid tenantId)
        {
            var role = await RoleRepository.FindByNormalizedNameAsync(LookupNormalizer.NormalizeName(roleName));
            if (role == null)
            {
                role = new IdentityRole(
                    GuidGenerator.Create(),
                    roleName,
                    tenantId
                )
                {
                    IsStatic = true,
                    IsPublic = true
                };

                (await RoleManager.CreateAsync(role)).CheckErrors();

                await PermissionDataSeeder.SeedAsync(RolePermissionValueProvider.ProviderName,
                    roleName, grantedPermissions, tenantId);
            }
        }

        public class PredefinedRole
        {
            public string Name { get; set; }
            public List<string> GrantedPermissions { get; set; }
        }
    }
}
