using PolpAbp.Framework.Identity.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Xunit;

namespace PolpAbp.Framework.Identity
{
    public class UserAppServiceTests : FrameworkApplicationTestBase4Admin
    {
        private readonly IUserAppService _userAppService;

        public UserAppServiceTests() : base()
        {
            _userAppService = GetRequiredService<IUserAppService>();
        }

        [Fact]
        public async Task CanListUsersAsync()
        {
            var users = await _userAppService.GetListAsync(new Volo.Abp.Identity.GetIdentityUsersInput
            {
                MaxResultCount = 100,
                SkipCount = 0
            });

            Assert.True(users.TotalCount > 0);
        }

        [Fact]
        public async Task CanListUsersByIdsAsync()
        {
            var users = await _userAppService.GetListAsync(new Guid[] { FrameworkTestConsts.AdminId });

            Assert.Single(users.Items);
            Assert.NotNull(users.Items[0].ExtraProperties);
            Assert.NotNull(users.Items[0].GetProperty<string>("Test"));

        }

    }
}
