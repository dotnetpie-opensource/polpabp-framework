using System;
namespace PolpAbp.Framework.DistributedEvents.Account
{
	public enum AccountStateChangeEnum
    {
       RegisteredOnItsOwn = 10,
       ActivatedOnItsOwn = 20,
       ApprovedByAdmin = 30,
       DeactivatedByAdmin = 40
	}
}

