using System.Threading.Tasks;
using Volo.Abp.Sms;

namespace PolpAbp.Framework.Mock.Sms
{
    public class DummySmsSender : ISmsSender
    {
        public Task SendAsync(SmsMessage smsMessage)
        {
            SharedMemory.Instance.AddProperty("sms.to", smsMessage.PhoneNumber);
            SharedMemory.Instance.AddProperty("sms.body", smsMessage.Text);

            return Task.CompletedTask;
        }
    }
}