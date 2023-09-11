using System;
using PolpAbp.Framework.AuditLogging;

namespace PolpAbp.Framework.EntityFrameworkCore.AuditLogging
{
	public class AuditLogRepositoryExtTests : FrameworkEntityFrameworkCoreTestBase {

        private readonly IAuditLogRepositoryExt _repo;

        public AuditLogRepositoryExtTests()
        {
            _repo = GetRequiredService<IAuditLogRepositoryExt>();
        }

        public 
    }
}

