using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.MultiOrgUnit
{
    public interface IOrgUnitStore
    {
        Task<BasicOrgUnitInfo> FindAsync(Guid orgId);
    }
}
