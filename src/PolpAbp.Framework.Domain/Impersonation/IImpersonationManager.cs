using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Impersonation
{
    public interface IImpersonationManager
    {
        Task<string> GetBackToImpersonatorToken();
        Task<UserAndIdentity> GetImpersonatedUserAndIdentity(string impersonationToken);
        Task<string> GetImpersonationToken(Guid userId, Guid? tenantId);
    }
}
