using System;
using Volo.Abp.Identity;

// Note that we on purpose use the extensions,
// so that we can hide these extensions normally.
// The client will not have any surprising effects
// unless the client explicity uses this namespace.
//
// Also we have the local extensions to reduce
// the dependency on some packages if these extensions
// are included in other modules.
namespace PolpAbp.Framework.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFullName(this IdentityUser user)
        {
            return UtitlityExtensions.ComposeFullName(user.Name, user.Surname);
        }
    }
}

