using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Emailing;

[ExposeServices(typeof(IAmbientEmailSender))]
public class DummyAmbientEmailSender : AmbientEmailSenderBase, IAmbientEmailSender, ITransientDependency
{
    public DummyAmbientEmailSender(IEmailSender emailSender, ICurrentTenant currentTenant) 
        : base(emailSender, currentTenant)
    {
    }
}

