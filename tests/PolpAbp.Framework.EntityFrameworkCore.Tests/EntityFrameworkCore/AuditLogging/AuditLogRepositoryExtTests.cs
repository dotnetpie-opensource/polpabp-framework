using System;
using System.Threading.Tasks;
using PolpAbp.Framework.AuditLogging;
using Shouldly;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace PolpAbp.Framework.EntityFrameworkCore.AuditLogging
{
	public class AuditLogRepositoryExtTests : FrameworkEntityFrameworkCoreTestBase {

        private readonly IAuditLogRepository _repo;
        private readonly IAuditLogRepositoryExt _repoExt;
        private readonly ICurrentTenant _currentTenant;
        private readonly IAuditingStore _auditingStore;

        public AuditLogRepositoryExtTests()
        {
            _repo = GetRequiredService<IAuditLogRepository>();
            _repoExt = GetRequiredService<IAuditLogRepositoryExt>();
            _currentTenant = GetRequiredService<ICurrentTenant>();
            _auditingStore = GetRequiredService<IAuditingStore>();
        }

        [Fact]
        public async Task CanGetActionListTestAsync()
        {
            using (_currentTenant.Change(FrameworkTestConsts.TenantId))
            {
                var l = await _repoExt.GetActionListsync();

                l.Count.ShouldBe(3);
            }
        }

        [Fact]
        public async Task CanGetActionListWithMethodNameTestAsync()
        {
            using (_currentTenant.Change(FrameworkTestConsts.TenantId))
            {
                var l = await _repoExt.GetActionListsync(methodName: "Role");

                l.Count.ShouldBe(1);

                var m = await _repoExt.GetActionListsync(methodName: "List");

                m.Count.ShouldBe(3);
            }
        }

    }
}

