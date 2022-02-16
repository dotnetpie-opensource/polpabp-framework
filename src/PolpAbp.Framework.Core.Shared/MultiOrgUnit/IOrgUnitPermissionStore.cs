using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolpAbp.Framework.MultiOrgUnit
{
    public interface IOrgUnitPermissionStore
    {
        Task<List<string>> LoadPermissionsAsync(Guid userId, Guid orgUnitId);
    }
}