using System.Collections.Generic;

namespace PolpAbp.Framework.Globalization
{
    public interface ICountryCodingService
    {
        Dictionary<string, int> CountryCodeDict { get; }

        int GetCountryAlphaIndex(string twoLetter);
        string GetCountryAlphaLetter(int index);
        string GetCountryName(string twoLetter);
    }
}
