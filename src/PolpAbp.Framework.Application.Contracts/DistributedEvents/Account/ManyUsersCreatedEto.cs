using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.ManyUsersCreated")]
    public class ManyUsersCreatedEto
    {
        public List<UserCreatedEto> Items { get; set; }

        public ManyUsersCreatedEto()
        {
            Items = new List<UserCreatedEto>();
        }
    }
}
