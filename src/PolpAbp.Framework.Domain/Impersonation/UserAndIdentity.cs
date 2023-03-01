using System.Security.Claims;
using Volo.Abp.Identity;

namespace PolpAbp.Framework.Impersonation
{
    public class UserAndIdentity
    {
        public IdentityUser User { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public UserAndIdentity(IdentityUser user, ClaimsIdentity identity)
        {
            User = user;
            Identity = identity;
        }
    }
}