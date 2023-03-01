namespace PolpAbp.Framework.Impersonation
{
    public interface ICurrentImpersonationAccessor
    {
        ImpersonationInfo Current { get; set; }
    }
}
