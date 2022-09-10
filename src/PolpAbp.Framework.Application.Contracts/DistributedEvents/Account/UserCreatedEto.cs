using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.UserCreated")]
    public class UserCreatedEto : EntityCreatedEtoBase
    {
        public string Surname { get; set; }
        public string[] OrgUnitCodes { get; set; }
    }
}
