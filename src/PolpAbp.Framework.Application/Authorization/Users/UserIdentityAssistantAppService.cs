using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PolpAbp.Framework.Security;
using PolpAbp.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Settings;

namespace PolpAbp.Framework.Authorization.Users
{
    [RemoteService(false)]
    public class UserIdentityAssistantAppService : FrameworkAppService, IUserIdentityAssistantAppService
    {
        protected readonly IConfiguration Configuration;

        public UserIdentityAssistantAppService(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IdentityResult> ValidatePasswordAsync(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.PasswordRequired
                });
            }

            var settings = await ReadInPasswordComplexityAsync();

            if (password.Length < settings.RequiredLength)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.InvalidMinLength
                });
            }

            if (settings.RequireUppercase && !password.Any(char.IsUpper))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.UppercaseRequired
                });
            }

            if (settings.RequireLowercase && !password.Any(char.IsLower))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.LowercaseRequired
                });
            }

            if (settings.RequireDigit && !password.Any(char.IsNumber))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.DigitRequired
                });
            }

            if (settings.RequireNonAlphanumeric && password.All(char.IsLetterOrDigit))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = PasswordValidationErrors.NonAlphaRequired
                });
            }

            return IdentityResult.Success;
        }

        public async Task<string> CreateRandomPasswordAsync()
        {
            // todo: Read from settings
            var passwordComplexitySetting = await ReadInPasswordComplexityAsync();

            var upperCaseLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            var lowerCaseLetters = "abcdefghijkmnopqrstuvwxyz";
            var digits = "0123456789";
            var nonAlphanumerics = "!@$?_-";

            string[] randomChars = {
                upperCaseLetters,
                lowerCaseLetters,
                digits,
                nonAlphanumerics
            };

            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            if (passwordComplexitySetting.RequireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    upperCaseLetters[rand.Next(0, upperCaseLetters.Length)]);
            }

            if (passwordComplexitySetting.RequireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    lowerCaseLetters[rand.Next(0, lowerCaseLetters.Length)]);
            }

            if (passwordComplexitySetting.RequireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    digits[rand.Next(0, digits.Length)]);
            }

            if (passwordComplexitySetting.RequireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    nonAlphanumerics[rand.Next(0, nonAlphanumerics.Length)]);
            }

            for (var i = chars.Count; i < passwordComplexitySetting.RequiredLength; i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            var ret = new string(chars.ToArray());

            return ret;
        }

        public async Task<PasswordComplexitySetting> ReadInPasswordComplexityAsync()
        {
            var complexity = new PasswordComplexitySetting(true);
            Configuration.GetSection("PolpAbp:Account:PasswordComplexity").Bind(complexity);

            complexity.RequireDigit = await SettingProvider.GetAsync<bool>(FrameworkSettings.AccountPassComplexityRequireDigit, complexity.RequireDigit);
            complexity.RequireLowercase = await SettingProvider.GetAsync<bool>(FrameworkSettings.AccountPassComplexityRequireLowercase, complexity.RequireLowercase);
            complexity.RequireUppercase = await SettingProvider.GetAsync<bool>(FrameworkSettings.AccountPassComplexityRequireUppercase, complexity.RequireUppercase);
            complexity.RequireNonAlphanumeric = await SettingProvider.GetAsync<bool>(FrameworkSettings.AccountPassComplexityRequireNonAlphanumeric, complexity.RequireNonAlphanumeric);
            complexity.RequiredLength = await SettingProvider.GetAsync<int>(FrameworkSettings.AccountPassComplexityRequiredLength, complexity.RequiredLength);

            return complexity;
        }
    }
}
