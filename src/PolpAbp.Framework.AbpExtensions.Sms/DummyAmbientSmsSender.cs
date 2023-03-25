using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Sms;

[ExposeServices(typeof(IAmbientSmsSender))]
public class DummyAmbientSmsSender : AmbientSmsSenderBase, IAmbientSmsSender, ITransientDependency
{
    public DummyAmbientSmsSender(ISmsSender smsSender) : base(smsSender)
    {
    }
}

