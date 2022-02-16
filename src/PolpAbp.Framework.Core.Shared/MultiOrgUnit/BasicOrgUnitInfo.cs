using System;

namespace PolpAbp.Framework.MultiOrgUnit
{
    public class BasicOrgUnitInfo
    {
        public Guid? OrgUnitId { get; }
        public string Code { get; }
        public string Name { get; }

        public BasicOrgUnitInfo(Guid? orgUnitId, string code = null, string name = null)
        {
            OrgUnitId = orgUnitId;
            Code = code;
            Name = name;
        }
    }
}
