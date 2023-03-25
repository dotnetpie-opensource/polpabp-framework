namespace Volo.Abp.Sms;

public interface IAmbientSmsSender : ISmsSender
{
    public SmsSendingContext SendingContext { get; }

    Task BeforeSendingAsync(SmsSendingContext context);

    Task AfterSendingAsync(SmsSendingContext context);
}

