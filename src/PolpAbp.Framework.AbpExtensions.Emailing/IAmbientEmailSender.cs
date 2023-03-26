namespace Volo.Abp.Emailing;

public interface IAmbientEmailSender : IEmailSender
{
    EmailSendingContext SendingContext { get; }

    Task BeforeSendingAsync();

    Task AfterSendingAsync();
}

