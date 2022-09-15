using Microsoft.Extensions.Logging;
using PolpAbp.Framework.Net;
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
        protected readonly MessageBirdClient ProviderConfiguration;
        protected readonly ILogger Logger;

        public MessageBirdSmsSender(MessageBirdClient configuration, 
            ILogger<MessageBirdSmsSender> logger)
        {
            ProviderConfiguration = configuration;
            Logger = logger;
        }

        public Task SendAsync(SmsMessage smsMessage)
        {
            var codeStr = smsMessage.Properties.GetOrDefault("CountryCode") as string;
            if (!string.IsNullOrEmpty(codeStr))
            {
                if (int.TryParse(codeStr, out int countryCode))
                {
                    var orginator = ProviderConfiguration.GetOrginatorByCountry((CountryCodeEnum)countryCode);
                    if (orginator != null)
                    {
                        if (long.TryParse(smsMessage.PhoneNumber, out var targetNumber))
                        {
                            ProviderConfiguration.Client.SendMessage(orginator, smsMessage.Text, new long[] { targetNumber });
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
