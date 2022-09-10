using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.ManyUsersDeleted")]
    public class ManyUsersDeletedEto
    {
        public List<UserDeletedEto> Items { get; set; }

        public ManyUsersDeletedEto()
        {
            Items = new List<UserDeletedEto>();
        }
    }
}
