
using PolpAbp.Framework.Exceptions.Identity;
using Volo.Abp.Data;

namespace PolpAbp.Framework.Authorization.Users
{
    public class UserIdentityOpContext : IUserIdentityOpContext, IHasExtraProperties
    {
        public bool ShouldStop => OperationFailureCode > 0;

        public UserIdentityOpEnum OperationId { get; set; }

        public int OperationFailureCode { get; set; }

        public string OperationFailureDescription { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public UserIdentityOpContext() {
            ExtraProperties = new ExtraPropertyDictionary();
            OperationFailureCode = 0;
        }
    }
}
