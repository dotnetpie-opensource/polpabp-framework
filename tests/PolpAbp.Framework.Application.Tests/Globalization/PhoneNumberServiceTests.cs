using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PolpAbp.Framework.Globalization
{
    public class PhoneNumberServiceTests : FrameworkApplicationTestBase
    {
        private readonly IPhoneNumberService _service;

        public PhoneNumberServiceTests()
        {
            _service = GetRequiredService<IPhoneNumberService>();
        }

        [Fact]
        public void ParseUSNumberTest()
        {
            var input = _service.Parse("5026578157");
            Assert.Equal("US", input.CountryAlpha);
        }
    }
}
