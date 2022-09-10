using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.UserUpdated")]
    public class UserUpdatedEto  : EntityUpdatedEtoBase
    {
        public string Surname { get; set; }
        public string[] OrgUnitCodes { get; set; }
    }
}
