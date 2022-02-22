using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace PolpAbp.Framework
{
    public class FrameworkTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IdentityUserManager _userManager;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly ILookupNormalizer _lookupNormalizer;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IdentityRoleManager _identityRoleManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly ICurrentTenant _currentTenant;

        public FrameworkTestDataSeedContributor(IdentityUserManager userManager,
            ITenantManager tenantManager,
            ITenantRepository tenantRepository,
            OrganizationUnitManager organizationUnitManager,
            IIdentityRoleRepository roleRepository,
            ILookupNormalizer lookupNormalizer,
            IGuidGenerator guidGenerator,
            IdentityRoleManager identityRoleManager,
            IPermissionManager permissionManager,
            IPermissionDataSeeder permissionDataSeeder,
            ICurrentTenant currentTenant)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _tenantRepository = tenantRepository;
            _organizationUnitManager = organizationUnitManager;
            _roleRepository = roleRepository;
            _lookupNormalizer = lookupNormalizer;
            _guidGenerator = guidGenerator;
            _identityRoleManager = identityRoleManager;
            _permissionManager = permissionManager;
            _permissionDataSeeder = permissionDataSeeder;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            //...

            var tenant = await _tenantManager.CreateAsync("Tenant1");
            var p = tenant.GetType().GetProperty("Id");
            p.SetValue(tenant, FrameworkTestConsts.TenantId);

            await _tenantRepository.InsertAsync(tenant);

            using (_currentTenant.Change(FrameworkTestConsts.TenantId))
            {
                var adminUser = new IdentityUser(FrameworkTestConsts.AdminId, "admin",
                    FrameworkTestConsts.AdminEmail, FrameworkTestConsts.TenantId);

                await _userManager.CreateAsync(adminUser);

                const string adminRoleName = "admin";
                var adminRole = await _roleRepository.FindByNormalizedNameAsync(_lookupNormalizer.NormalizeName("admin"));
                if (adminRole == null)
                {
                    adminRole = new IdentityRole(
                        _guidGenerator.Create(),
                        adminRoleName,
                        FrameworkTestConsts.TenantId
                    )
                    {
                        IsStatic = true,
                        IsPublic = true
                    };

                    (await _identityRoleManager.CreateAsync(adminRole)).CheckErrors();
                }

                (await _userManager.AddToRoleAsync(adminUser, adminRoleName)).CheckErrors();

                var permissionNames = IdentityPermissions.GetAll();

                // Add permission to roles 
                await _permissionDataSeeder.SeedAsync(
                RolePermissionValueProvider.ProviderName,
                "admin",
                permissionNames,
                FrameworkTestConsts.TenantId);

                // Look up the user 
                // var storedAdminUser = await _userManager.GetByIdAsync(adminUser.Id);
                // Set up the password 
                await _userManager.AddPasswordAsync(adminUser, FrameworkTestConsts.AdminPass);

                /* Seed additional test data... */
                await _userManager.CreateAsync(new IdentityUser(FrameworkTestConsts.MemberUserId1, FrameworkTestConsts.UserName1,
                    FrameworkTestConsts.User1Email, FrameworkTestConsts.TenantId));

                await _userManager.CreateAsync(new IdentityUser(Guid.NewGuid(), FrameworkTestConsts.UserName2,
                    FrameworkTestConsts.User2Email, FrameworkTestConsts.TenantId));


                await _organizationUnitManager.CreateAsync(new OrganizationUnit(FrameworkTestConsts.OrgUnitId,
                    FrameworkTestConsts.Group1DsiplayName, null, FrameworkTestConsts.TenantId));
            }
        }
    }
}
