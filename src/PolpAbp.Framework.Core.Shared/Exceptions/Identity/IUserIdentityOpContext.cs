namespace PolpAbp.Framework.Exceptions.Identity
{
    public interface IUserIdentityOpContext
    {
        UserIdentityOpEnum OperationId { get; }
        int OperationFailureCode { get; }
        string OperationFailureDescription { get; }
    }
}
