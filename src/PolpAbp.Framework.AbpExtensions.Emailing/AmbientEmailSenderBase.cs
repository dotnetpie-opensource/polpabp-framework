using System.Net.Mail;

namespace Volo.Abp.Emailing;

public abstract class AmbientEmailSenderBase : IAmbientEmailSender
{
    protected readonly IEmailSender EmailSender;
    
    public EmailSendingContext SendingContext { get; }

    public AmbientEmailSenderBase(IEmailSender emailSender)
    {
        EmailSender = emailSender;
        SendingContext = new EmailSendingContext();
    }

    public virtual Task AfterSendingAsync(EmailSendingContext context)
    {
        return Task.CompletedTask;
    }

    public virtual Task BeforeSendingAsync(EmailSendingContext context)
    {
        return Task.CompletedTask;
    }

    public virtual async Task QueueAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await EmailSender.QueueAsync(to, subject, body, isBodyHtml);
    }

    public virtual async Task QueueAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
    {
        await EmailSender.QueueAsync(from, to, subject, body, isBodyHtml);
    }

    public virtual async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        SendingContext.UpdateWith(to: to, subject: subject, body: body, isBodyHtml: isBodyHtml);

        await BeforeSendingAsync(SendingContext);
        if (SendingContext.ShouldStop)
        {
            return;
        }

        await EmailSender.SendAsync(to, subject, body, isBodyHtml);
        await AfterSendingAsync(SendingContext);
    }

    public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
    {
        SendingContext.UpdateWith(from: from, to: to, subject: subject, body: body, isBodyHtml: isBodyHtml);

        await BeforeSendingAsync(SendingContext);
        if (SendingContext.ShouldStop)
        {
            return;
        }

        await EmailSender.SendAsync(to, subject, body, isBodyHtml);
        await AfterSendingAsync(SendingContext);
    }

    public virtual async Task SendAsync(MailMessage mail, bool normalize = true)
    {
        SendingContext.UpdateWith(mail);

        await BeforeSendingAsync(SendingContext);
        if (SendingContext.ShouldStop)
        {
            return;
        }
        await EmailSender.SendAsync(mail, normalize);
        await AfterSendingAsync(SendingContext);
    }

}

