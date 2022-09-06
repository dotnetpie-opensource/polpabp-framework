using System;
namespace PolpAbp.Framework.Identity
{
    public class PasswordComplexity
    {
        public static PasswordComplexity Default = new PasswordComplexity
        {
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequiredLength = 8,
            RequireUppercase = true
        };

        public bool Equals(PasswordComplexity other)
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
                RequiredLength == other.RequiredLength;
        }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public int RequiredLength { get; set; }
    }
}
