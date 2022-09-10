using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.UserDeleted")]
    public class UserDeletedEto : EntityDeletedEtoBase
    {
    }
}
