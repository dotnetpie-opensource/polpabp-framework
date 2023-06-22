using System;
using Volo.Abp.EventBus;

namespace PolpAbp.Framework.DistributedEvents.Account
{
    [EventName("PolpAbp.Framework.DistributedEvents.Account.StateChange")]
    public class AccountStateChangeEto
	{
        public Guid TenantId { get; set; }
        public Guid AccountId { get; set; }
        public AccountStateChangeEnum ChangeId { get; set; }
    }
}

