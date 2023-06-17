using System.Net.Mail;
using System.Threading.Tasks;
using Volo.Abp.Emailing;

namespace PolpAbp.Framework.Mock.Emailing
{
    public class DummyEmailSender : IEmailSender
    {
        public DummyEmailSender()
        {
        }

        public Task QueueAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.ExtraProperties.Add("email.to", to);
            SharedMemory.Data.ExtraProperties.Add("email.subject", subject);
            SharedMemory.Data.ExtraProperties.Add("email.body", body);
            return Task.CompletedTask;
        }

        public Task QueueAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.ExtraProperties.Add("email.from", from);
            SharedMemory.Data.ExtraProperties.Add("email.to", to);
            SharedMemory.Data.ExtraProperties.Add("email.subject", subject);
            SharedMemory.Data.ExtraProperties.Add("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.ExtraProperties.Add("email.to", to);
            SharedMemory.Data.ExtraProperties.Add("email.subject", subject);
            SharedMemory.Data.ExtraProperties.Add("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.ExtraProperties.Add("email.from", from);
            SharedMemory.Data.ExtraProperties.Add("email.to", to);
            SharedMemory.Data.ExtraProperties.Add("email.subject", subject);
            SharedMemory.Data.ExtraProperties.Add("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(MailMessage mail, bool normalize = true)
        {
            SharedMemory.Data.ExtraProperties.Add("email.from", mail.From);
            SharedMemory.Data.ExtraProperties.Add("email.to", mail.To);
            SharedMemory.Data.ExtraProperties.Add("email.subject", mail.Subject);
            SharedMemory.Data.ExtraProperties.Add("email.body", mail.Body);

            return Task.CompletedTask;
        }
    }
}