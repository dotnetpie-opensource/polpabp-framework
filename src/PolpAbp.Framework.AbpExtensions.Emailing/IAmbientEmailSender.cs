namespace Volo.Abp.Emailing;

public interface IAmbientEmailSender : IEmailSender
{
    Task<bool> CanSendAsync();

    Task BeforeSendingAsync();

    Task AfterSendingAsync();
}

