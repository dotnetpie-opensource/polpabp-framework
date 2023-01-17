using EnumsNET;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Globalization
{
    public class CountryCodingService : ICountryCodingService, ISingletonDependency
    {
        public static readonly Dictionary<int, string> CountryAlphaDict = 
            Enum.GetValues(typeof(CountryAlphaEnum))
            .Cast<CountryAlphaEnum>()
            .ToDictionary(t => (int)t, t => t.ToString());

        private readonly Object _locker = new Object();

        private readonly Dictionary<string, int> _countryCodeDict = new Dictionary<string, int>();

        
        public CountryCodingService() { }

        public Dictionary<string, int> CountryCodeDict
        {
            get
            {
                EnsureCountryCodeDictBuilt();
                return _countryCodeDict;
            }
        }

        public int GetCountryAlphaIndex(string twoLetter)
        {
            twoLetter = twoLetter.ToUpper();
            var r = CountryAlphaDict.FirstOrDefault(a => a.Value == twoLetter);
            return r.Key;
        }

        public string GetCountryAlphaLetter(int index)
        {
            CountryAlphaDict.TryGetValue(index, out var r);
            return r;
        }

        public string GetCountryName(string twoLetter)
        {
            var idx = GetCountryAlphaIndex(twoLetter);
            var name = ((CountryAlphaEnum)idx).AsString(EnumFormat.Description);
            return name;
        }

        private void EnsureCountryCodeDictBuilt()
        {
            lock(_locker)
            {
                if (_countryCodeDict.Count > 0)
                {
                    return;
                }
                        
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                foreach (var cul in cinfo)
                {
                    char[] name = cul.Name.ToCharArray();
                    if (name.Length >= 2)
                    {
                        string twoLetterCode = "" + name[name.Length - 2] + name[name.Length - 1];
                        twoLetterCode = twoLetterCode.ToUpper();
                        var countryCode = phoneUtil.GetCountryCodeForRegion(twoLetterCode);
                        _countryCodeDict[twoLetterCode] = countryCode;
                    }

                }

            }
        }

    }
}
