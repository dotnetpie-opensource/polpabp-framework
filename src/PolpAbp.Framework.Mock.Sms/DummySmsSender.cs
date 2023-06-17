using System.Threading.Tasks;
using Volo.Abp.Sms;

namespace PolpAbp.Framework.Mock.Sms
{
    public class DummySmsSender : ISmsSender
    {
        public Task SendAsync(SmsMessage smsMessage)
        {
            SharedMemory.Data.ExtraProperties.Add("sms.to", smsMessage.PhoneNumber);
            SharedMemory.Data.ExtraProperties.Add("sms.body", smsMessage.Text);

            return Task.CompletedTask;
        }
    }
}