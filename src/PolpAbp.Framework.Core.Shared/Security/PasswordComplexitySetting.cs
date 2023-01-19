﻿namespace PolpAbp.Framework.Security
{
    public class PasswordComplexitySetting
    {
        public PasswordComplexitySetting(bool withDefaults) 
        {
            if (withDefaults)
            {
                RequireDigit = true;
                RequireLowercase = true;
                RequiredLength = 8;
                RequireNonAlphanumeric = true;
                RequireUppercase = true;
            }
        }

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
                RequiredLength == other.RequiredLength;
        }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public int RequiredLength { get; set; }
    }
}
