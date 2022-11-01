using System;
using System.Linq;
using System.Security.Claims;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace PolpAbp.Framework.CustomServices
{
    public class CustomCurrentUser : CurrentUser, ICurrentUser
    {
        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CustomCurrentUser(ICurrentPrincipalAccessor principalAccessor)
            : base(principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public override Guid? Id
        {
            get
            {
                var baseId = base.Id;
                if (baseId.HasValue)
                {
                    return baseId.Value;
                }

                var userIdOrNull = _principalAccessor?.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdOrNull != null)
                {
                    if (Guid.TryParse(userIdOrNull.Value, out Guid result))
                    {
                        return result;
                    }
                    return null;
                }
                return null;

            }
        }
    }
}

