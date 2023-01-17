using PolpAbp.Framework.Globalization.Dto;
using System;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Globalization
{
    public class PhoneNumberService : IPhoneNumberService, ITransientDependency
    {
        private readonly ICountryCodingService _countryCodingService;

        public PhoneNumberService(ICountryCodingService countryCodingService)
        {
            _countryCodingService = countryCodingService;
        }

        public PhoneNumberDetailDto Parse(string phoneNumber, string defaultCountry = "US")
        {
            var ret = new PhoneNumberDetailDto();

            try
            {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

                var a = phoneNumberUtil.Parse(phoneNumber, defaultCountry);

                var b = phoneNumberUtil.Format(a, PhoneNumbers.PhoneNumberFormat.E164);

                ret.E164PhoneNumber= b;
                ret.CountryCode = a.CountryCode;
                ret.CountryAlpha = phoneNumberUtil.GetRegionCodeForCountryCode(a.CountryCode);
                ret.CountryName = _countryCodingService.GetCountryName(ret.CountryAlpha);
                ret.IsValid = true;
            }
            catch (Exception e) { 
            }

            return ret;
        }
    }
}
