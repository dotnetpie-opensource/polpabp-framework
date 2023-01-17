namespace PolpAbp.Framework.Globalization.Dto
{
    public class PhoneNumberDetailDto
    {
        public bool IsValid { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryAlpha { get; set; }
        /// <summary>
        /// A phone number
        /// </summary>
        public string E164PhoneNumber { get; set; }
    }
}
