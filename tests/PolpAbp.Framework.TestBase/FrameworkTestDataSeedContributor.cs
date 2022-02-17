using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
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
        private readonly ICurrentTenant _currentTenant;

        public FrameworkTestDataSeedContributor(IdentityUserManager userManager,
            ITenantManager tenantManager,
            ITenantRepository tenantRepository,
            OrganizationUnitManager organizationUnitManager,
            ICurrentTenant currentTenant)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _tenantRepository = tenantRepository;
            _organizationUnitManager = organizationUnitManager;
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
