namespace Volo.Abp.Sms;

public interface IAmbientSmsSender : ISmsSender
{
    SmsSendingContext SendingContext { get; }

    Task BeforeSendingAsync();

    Task AfterSendingAsync();
}

