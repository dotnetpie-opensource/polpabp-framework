using PolpAbp.Framework.Globalization.Dto;

namespace PolpAbp.Framework.Globalization
{
    public interface IPhoneNumberService
    {
        PhoneNumberDetailDto Parse(string phoneNumber, string defaultCountry = "US");
    }
}
