namespace PolpAbp.Framework.Security
{
    /// <summary>
    /// Password complexity settings. 
    /// Note that we do not need any default value. 
    /// Because the abp identity has the default value 
    /// for each setting.
    /// </summary>
    public class PasswordComplexitySetting
    {

        public bool Equals(PasswordComplexitySetting other)
        {
            if (other == null)
            {
                return false;
            }

            return
                RequireDigit == other.RequireDigit &&
                RequireLowercase == other.RequireLowercase &&
                RequireNonAlphanumeric == other.RequireNonAlphanumeric &&
                RequireUppercase == other.RequireUppercase &&
                RequiredLength == other.RequiredLength &&
                RequiredUniqueChars == other.RequiredUniqueChars;
        }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public int RequiredLength { get; set; }

        public int RequiredUniqueChars { get; set; }
    }
}
