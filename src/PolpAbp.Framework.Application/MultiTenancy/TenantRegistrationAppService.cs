using PolpAbp.Framework.Identity;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace PolpAbp.Framework.MultiTenancy
{
    /// <summary>
    /// Provides more services for tenant management,
    /// particularly for tenant registration.
    /// This service is a supplement to the tenant management module form ABP.
    /// (That is why we base this service on TenantManagementAppServiceBase.)
    /// </summary>
    [RemoteService(false)]
    public class TenantRegistrationAppService : TenantManagementAppServiceBase, ITenantRegistrationAppService
    {
        protected IDataSeeder DataSeeder { get; }
        protected ITenantRepository TenantRepository { get; }
        protected ITenantManager TenantManager { get; }
        private readonly IRegisteredTenantDataSeeder _registeredTenantDataSeeder;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public TenantRegistrationAppService(
            ITenantRepository tenantRepository,
            ITenantManager tenantManager,
            IDataSeeder dataSeeder,
            IRegisteredTenantDataSeeder registeredTenantDataSeeder,
            IPermissionDataSeeder permissionDataSeeder
            )
        {
            DataSeeder = dataSeeder;
            TenantRepository = tenantRepository;
            TenantManager = tenantManager;
            _registeredTenantDataSeeder = registeredTenantDataSeeder;
            _permissionDataSeeder = permissionDataSeeder;
        }

        /// <summary>
        /// Performs the logic of tenant registration.
        ///
        /// It completes the following jobs:
        ///   - Creating a tenant
        ///   - Creating an admin
        ///   - Creating an admin role for the tenant
        ///   - Assigning the admin role to the admin
        ///   - Creating the user role
        ///   - Creating additional permissions for "user" and "admin" roles
        /// </summary>
        /// <param name="input">Input DTO</param>
        /// <returns>Tenant DTO</returns>
        public async Task<TenantDto> RegisterTenant(TenantCreateDto input)
        {
            var tenant = await TenantManager.CreateAsync(input.Name);
            input.MapExtraPropertiesTo(tenant);

            await TenantRepository.InsertAsync(tenant);

            await CurrentUnitOfWork.SaveChangesAsync();

            using (CurrentTenant.Change(tenant.Id, tenant.Name))
            {
                //TODO: Handle database creation?

                await DataSeeder.SeedAsync(
                                new DataSeedContext(tenant.Id)
                                    .WithProperty("AdminEmail", input.AdminEmailAddress)
                                    .WithProperty("AdminPassword", input.AdminPassword)
                                );
            }

            // More data init for tenant
            await _registeredTenantDataSeeder.SeedAsync(tenant.Id);

            return ObjectMapper.Map<Tenant, TenantDto>(tenant);
        }
    }
}
