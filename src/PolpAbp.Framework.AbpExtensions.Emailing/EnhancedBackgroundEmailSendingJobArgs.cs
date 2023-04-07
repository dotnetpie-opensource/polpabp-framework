using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Emailing;

[Serializable]
public class EnhancedBackgroundEmailSendingJobArgs : BackgroundEmailSendingJobArgs, IMultiTenant
{
    public Guid? TenantId { get; set; }
}
