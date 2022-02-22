using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace PolpAbp.Framework.Identity
{
    public class TenantMemberIterator
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IIdentityUserRepository _identityUserRepository;

        public TenantMemberIterator(ICurrentTenant currentTenant,
            IIdentityUserRepository identityUserRepository)
        {
            _currentTenant = currentTenant;
            _identityUserRepository = identityUserRepository;
        }

        public async Task RunAsync(Guid? tenantId, Func<List<IdentityUser>, Task> func)
        {
            using (_currentTenant.Change(tenantId))
            {
                var count = await _identityUserRepository.GetCountAsync();

                var index = 0;
                while (index < count)
                {
                    var items = await _identityUserRepository.GetListAsync(maxResultCount: 100,
                        skipCount: index,
                        includeDetails: false);
                    index = index + items.Count;

                    await func(items);
                }
            }
        }
    }
}
