using System;
using System.Security.Claims;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace PolpAbp.Framework
{
    public class FrameworkApplicationTestBase4Admin : FrameworkApplicationTestBase, IDisposable
    {
        protected readonly ICurrentTenant CurrentTenant;
        protected readonly ICurrentUser CurrentUser;
        protected readonly ICurrentPrincipalAccessor CurrentPrincipalAccessor;

        public FrameworkApplicationTestBase4Admin()
        {
            CurrentTenant = GetRequiredService<ICurrentTenant>();
            CurrentUser = GetRequiredService<ICurrentUser>();
            CurrentPrincipalAccessor = GetRequiredService<ICurrentPrincipalAccessor>();

            var newPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(AbpClaimTypes.UserId, FrameworkTestConsts.AdminId.ToString()),
                    new Claim(AbpClaimTypes.TenantId, FrameworkTestConsts.TenantId.ToString()),
                    new Claim(AbpClaimTypes.UserName, "admin")
                }
            ));

            CurrentTenant.Change(FrameworkTestConsts.TenantId);
            CurrentPrincipalAccessor.Change(newPrincipal);
        }
    }
}
