using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.ManyUsersUpdated")]
    public class ManyUsersUpdatedEto
    {
        public List<UserUpdatedEto> Items { get; set; }

        public ManyUsersUpdatedEto()
        {
            Items = new List<UserUpdatedEto>();
        }
    }
}
