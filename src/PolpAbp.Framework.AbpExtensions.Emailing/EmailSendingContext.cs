using System.Net.Mail;
using Volo.Abp.Data;

namespace Volo.Abp.Emailing;

public class EmailSendingContext : IHasExtraProperties
{
    public bool ShouldStop { get; set; }
    public string? From { get; protected set; }
    public string? To { get; protected set; }
    public string? Subject { get; protected set; }
    public string? Body { get; protected set; }
    public bool IsBodyHtml { get; protected set; }

    public MailMessage? MailMessage { get; protected set; }

    public string? Intension { get; set; }
    public bool IsExempt { get; set; }
    public string? ExemptionReason { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; }

    public EmailSendingContext()
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }

    public void UpdateWith(string from = "", 
        string to = "", 
        string subject = "", 
        string body = "", 
        bool isBodyHtml = true)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
        IsBodyHtml = isBodyHtml;
    }

    public void UpdateWith(MailMessage mailMessage)
    {
        MailMessage = mailMessage;
    }
}

