using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.TenantManagement;
using Xunit;

namespace PolpAbp.Framework.MultiTenancy
{
    public class TenantRegistrationAppServiceTests : ApplicationMultiTenancyTestBase
    {
        private readonly ITenantRegistrationAppService _appService;

        public TenantRegistrationAppServiceTests()
        {
            _appService = GetRequiredService<ITenantRegistrationAppService>();
        }

        [Fact]
        public async Task CannotRegisterDuplicateTenantAsync()
        {
            var tenancyName = "Tenant1";
            await Assert.ThrowsAsync<Volo.Abp.UserFriendlyException>(async () =>
            {
                await _appService.RegisterTenant(new TenantCreateDto { Name = tenancyName, AdminEmailAddress = "xx@test.com", AdminPassword = "123456" });
            });
        }

        [Fact]
        public async Task CanRegisterFreshTenantAsync()
        {
            var tenancyName = Guid.NewGuid().ToString("N").ToLowerInvariant();
            var tenant = await _appService.RegisterTenant(new TenantCreateDto { Name = tenancyName, AdminEmailAddress = "xx@test.com", AdminPassword = "123456" });
            tenant.Name.ShouldBe(tenancyName);
            tenant.Id.ShouldNotBe(default(Guid));

        }
    }
}
