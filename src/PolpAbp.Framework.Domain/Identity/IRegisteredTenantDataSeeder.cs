using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Identity
{
    public interface IRegisteredTenantDataSeeder
    {
        Task SeedAsync(Guid? tenantId = null);
    }
}
