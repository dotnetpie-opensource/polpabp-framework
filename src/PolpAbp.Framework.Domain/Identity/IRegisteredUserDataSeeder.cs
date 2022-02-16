using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace PolpAbp.Framework.Identity
{
    public interface IRegisteredUserDataSeeder
    {
        Task SeedAsync(
            [NotNull] string userEmail,
            Guid? tenantId = null);
    }
}
