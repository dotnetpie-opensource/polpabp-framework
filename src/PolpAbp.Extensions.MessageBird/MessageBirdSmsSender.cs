using Microsoft.Extensions.Logging;
using PolpAbp.Framework.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace PolpAbp.Extensions.MessageBird
{
    [RemoteService(false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ISmsSender))]
    public class MessageBirdSmsSender : ISmsSender, ITransientDependency
    {
        protected readonly MessageBirdClientWrapper ClientWrapper;
        protected readonly ILogger Logger;

        public MessageBirdSmsSender(MessageBirdClientWrapper client, 
            ILogger<MessageBirdSmsSender> logger)
        {
            ClientWrapper = client;
            Logger = logger;
        }

        public Task SendAsync(SmsMessage smsMessage)
        {
            var alpha = smsMessage.Properties.GetOrDefault("CountryCode");
            if (alpha != null)
            {
                var orginator = ClientWrapper.GetOrginatorByCountry(alpha.ToString());
                if (orginator != null)
                {
                    if (long.TryParse(smsMessage.PhoneNumber, out var targetNumber))
                    {
                        ClientWrapper.Client.SendMessage(orginator, smsMessage.Text, new long[] { targetNumber });
                        smsMessage.Properties.Add("ErrorCode", SmsErrorCodeEnum.None);
                    }
                    else
                    {
                        smsMessage.Properties.Add("ErrorCode", SmsErrorCodeEnum.PhoneNumberInvalid);
                    }
                }
                else
                {
                    smsMessage.Properties.Add("ErrorCode", SmsErrorCodeEnum.OriginatorNotConfigured);
                }
            } 
            else
            {
                smsMessage.Properties.Add("ErrorCode", SmsErrorCodeEnum.CountryCodeMissing);
            }

            return Task.CompletedTask;
        }
    }
}
