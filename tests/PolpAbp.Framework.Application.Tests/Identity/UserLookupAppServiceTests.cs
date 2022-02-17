using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;


namespace PolpAbp.Framework.Identity
{
    public class UserLookupAppServiceTests : ApplicationldentityTestBase
    {
        private readonly IUserLookupAppService _appService;

        public UserLookupAppServiceTests()
        {
            _appService = GetRequiredService<IUserLookupAppService>();
        }


        [Fact]
        public void CanBeIntantiated()
        {
            _appService.ShouldNotBeNull();
        }
    }
}
