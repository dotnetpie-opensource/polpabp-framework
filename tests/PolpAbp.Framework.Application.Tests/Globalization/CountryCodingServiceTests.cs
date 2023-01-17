using Xunit;

namespace PolpAbp.Framework.Globalization
{
    public class CountryCodingServiceTests : FrameworkApplicationTestBase
    {
        private readonly ICountryCodingService _service;

        public CountryCodingServiceTests()
        {
            _service = GetRequiredService<ICountryCodingService>();
        }

        [Fact]
        public void GetCountryAlphaIndexTest()
        {
            var index = _service.GetCountryAlphaIndex("US");
            Assert.Equal((int)CountryAlphaEnum.US, index);
        }

        [Fact]
        public void GetCountryAlphaLetterTest()
        {
            var code = _service.GetCountryAlphaLetter((int)CountryAlphaEnum.US);
            Assert.Equal("US", code);
        }

        [Fact]
        public void GetCountryNameTest()
        {
            var name = _service.GetCountryName("US");
            Assert.Contains("united states", name.ToLower());
        }
    }
}
