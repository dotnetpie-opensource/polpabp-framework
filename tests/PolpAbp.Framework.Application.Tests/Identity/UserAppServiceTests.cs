using PolpAbp.Framework.Identity.Dto;
using System.Threading.Tasks;
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
    }
}
