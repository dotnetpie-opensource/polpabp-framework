using Microsoft.Extensions.Logging;
using PolpAbp.Framework.Globalization;
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
        protected readonly IPhoneNumberService PhoneNumberService;

        public MessageBirdSmsSender(MessageBirdClientWrapper client, 
            ILogger<MessageBirdSmsSender> logger,
            IPhoneNumberService phoneNumberService)
        {
            ClientWrapper = client;
            Logger = logger;
            PhoneNumberService = phoneNumberService;
        }

        public Task SendAsync(SmsMessage smsMessage)
        {
            var errorCode = SmsErrorCodeEnum.PhoneNumberInvalid;

            var phoneNumberDetail = PhoneNumberService.Parse(smsMessage.PhoneNumber);
            if (phoneNumberDetail.IsValid)
            {
                if (long.TryParse(phoneNumberDetail.E164PhoneNumber, out var targetNumber))
                {
                    var orginator = ClientWrapper.GetOrginatorByCountry(phoneNumberDetail.CountryAlpha);
                    if (orginator != null)
                    {

                        ClientWrapper.Client.SendMessage(orginator, smsMessage.Text, new long[] { targetNumber });
                        errorCode = SmsErrorCodeEnum.None;
                    }
                    else
                    {
                        errorCode = SmsErrorCodeEnum.OriginatorNotConfigured;
                    }
                }
            } 
                    
            smsMessage.Properties.Add("ErrorCode", errorCode);

            return Task.CompletedTask;
        }
    }
}
