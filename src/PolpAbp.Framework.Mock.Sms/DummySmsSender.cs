using System.Threading.Tasks;
using Volo.Abp.Sms;

namespace PolpAbp.Framework.Mock.Sms
{
    public class DummySmsSender : ISmsSender
    {
        private readonly IMockScopeContext _scopeContext;

        public DummySmsSender(IMockScopeContext scopeContext)
        {
            _scopeContext = scopeContext;
        }

        public Task SendAsync(SmsMessage smsMessage)
        {
            _scopeContext.AddProperty("sms.to", smsMessage.PhoneNumber);
            _scopeContext.AddProperty("sms.body", smsMessage.Text);

            return Task.CompletedTask;
        }
    }
}