using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace PolpAbp.Framework.Impersonation
{
    public class ImpersonationManager : IDomainService, IImpersonationManager
    {
        private readonly IDistributedCache<ImpersonationCacheItem> _cacheManager;
        private readonly IdentityUserManager _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _principalFactory;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;

        public ImpersonationManager(
            IDistributedCache<ImpersonationCacheItem> cacheManager,
            IdentityUserManager userManager,
            IUserClaimsPrincipalFactory<IdentityUser> principalFactory,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser)
        {
            _cacheManager = cacheManager;
            _userManager = userManager;
            _principalFactory = principalFactory;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
        }

        public async Task<UserAndIdentity> GetImpersonatedUserAndIdentity(string impersonationToken)
        {
            var cacheItem = await _cacheManager.GetAsync(impersonationToken);
            if (cacheItem == null)
            {
                throw new UserFriendlyException("ImpersonationTokenErrorMessage");
            }

            CheckCurrentTenant(cacheItem.TargetTenantId);

            //Get the user from tenant
            var user = await _userManager.FindByIdAsync(cacheItem.TargetUserId.ToString());

            //Create identity
            var identity = await GetClaimsIdentityFromCache(user, cacheItem);

            //Remove the cache item to prevent re-use
            await _cacheManager.RemoveAsync(impersonationToken);

            return new UserAndIdentity(user, identity);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityFromCache(IdentityUser user, ImpersonationCacheItem cacheItem)
        {
            var identity = (ClaimsIdentity) (await _principalFactory.CreateAsync(user)).Identity;

            if (!cacheItem.IsBackToImpersonator)
            {
                //Add claims for audit logging
                if (cacheItem.ImpersonatorTenantId.HasValue)
                {
                    identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId,
                        cacheItem.ImpersonatorTenantId.Value.ToString()));
                }

                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId,
                    cacheItem.ImpersonatorUserId.ToString()));
            }

            return identity;
        }

        public Task<string> GetImpersonationToken(Guid userId, Guid? tenantId)
        {
            if (_currentUser.FindImpersonatorUserId().HasValue)
            {
                throw new UserFriendlyException("CascadeImpersonationErrorMessage");
            }

            if (_currentTenant.IsAvailable)
            {
                if (!tenantId.HasValue)
                {
                    throw new UserFriendlyException("FromTenantToHostImpersonationErrorMessage");
                }

                if (tenantId.Value != _currentTenant.Id.Value)
                {
                    throw new UserFriendlyException("DifferentTenantImpersonationErrorMessage");
                }
            }

            return GenerateImpersonationTokenAsync(tenantId, userId, false);
        }

        public Task<string> GetBackToImpersonatorToken()
        {
            if (!_currentUser.FindImpersonatorUserId().HasValue)
            {
                throw new UserFriendlyException("NotImpersonatedLoginErrorMessage");
            }

            return GenerateImpersonationTokenAsync(_currentUser.FindImpersonatorTenantId(), _currentUser.FindImpersonatorUserId().Value, true);
        }

        private void CheckCurrentTenant(Guid? tenantId)
        {
            if (_currentTenant.Id != tenantId)
            {
                throw new Exception($"Current tenant is different than given tenant. Current TenantId: {_currentTenant.Id}, given tenantId: {tenantId}");
            }
        }

        private async Task<string> GenerateImpersonationTokenAsync(Guid? tenantId, Guid userId, bool isBackToImpersonator)
        {
            //Create a cache item
            var cacheItem = new ImpersonationCacheItem(
                tenantId,
                userId,
                isBackToImpersonator
            );

            if (!isBackToImpersonator)
            {
                cacheItem.ImpersonatorTenantId = _currentTenant.Id;
                cacheItem.ImpersonatorUserId = _currentUser.Id.Value;
            }

            //Create a random token and save to the cache
            var token = Guid.NewGuid().ToString();

            await _cacheManager
                .SetAsync(token, cacheItem, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)});

            return token;
        }
    }
}
