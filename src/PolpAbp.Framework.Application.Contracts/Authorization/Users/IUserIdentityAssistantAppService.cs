﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PolpAbp.Framework.Authorization.Users
{
    public interface IUserIdentityAssistantAppService : IApplicationService
    {
        Task<string> CreateRandomPasswordAsync();

        /// <summary>
        /// Validates the given password according to the tenant-specific settings.
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        Task<IdentityResult> ValidatePasswordAsync(string password);
    }
}