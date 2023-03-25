using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Emailing;

[ExposeServices(typeof(IAmbientEmailSender))]
public class DummyAmbientEmailSender : AmbientEmailSenderBase, IAmbientEmailSender, ITransientDependency
{
    public DummyAmbientEmailSender(IEmailSender emailSender) : base(emailSender)
    {
    }
}

