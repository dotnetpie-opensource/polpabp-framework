namespace Volo.Abp.Emailing;

public interface IAmbientEmailSender : IEmailSender
{
    public EmailSendingContext SendingContext { get; }

    Task BeforeSendingAsync();

    Task AfterSendingAsync();
}

