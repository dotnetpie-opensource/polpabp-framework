using System.Net.Mail;

namespace Volo.Abp.Emailing;

public abstract class AmbientEmailSenderBase : IAmbientEmailSender
{
    protected readonly IEmailSender EmailSender;

    public AmbientEmailSenderBase(IEmailSender emailSender)
    {
        EmailSender = emailSender;
    }

    public virtual Task AfterSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task BeforeSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task<bool> CanSendAsync()
    {
        return Task.FromResult(true);
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
        var shouldStop = await CanSendAsync();
        if (shouldStop)
        {
            return;
        }

        await BeforeSendingAsync();
        await EmailSender.SendAsync(to, subject, body, isBodyHtml);
        await AfterSendingAsync();
    }

    public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
    {
        var shouldStop = await CanSendAsync();
        if (shouldStop)
        {
            return;
        }

        await BeforeSendingAsync();
        await EmailSender.SendAsync(from, to, subject, body, isBodyHtml);
        await AfterSendingAsync();
    }

    public virtual async Task SendAsync(MailMessage mail, bool normalize = true)
    {
        var shouldStop = await CanSendAsync();
        if (shouldStop)
        {
            return;
        }

        await BeforeSendingAsync();
        await EmailSender.SendAsync(mail, normalize);
        await AfterSendingAsync();
    }
}

