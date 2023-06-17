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
            SharedMemory.Instance.AddProperty("email.to", to);
            SharedMemory.Instance.AddProperty("email.subject", subject);
            SharedMemory.Instance.AddProperty("email.body", body);
            return Task.CompletedTask;
        }

        public Task QueueAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Instance.AddProperty("email.from", from);
            SharedMemory.Instance.AddProperty("email.to", to);
            SharedMemory.Instance.AddProperty("email.subject", subject);
            SharedMemory.Instance.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Instance.AddProperty("email.to", to);
            SharedMemory.Instance.AddProperty("email.subject", subject);
            SharedMemory.Instance.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Instance.AddProperty("email.from", from);
            SharedMemory.Instance.AddProperty("email.to", to);
            SharedMemory.Instance.AddProperty("email.subject", subject);
            SharedMemory.Instance.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(MailMessage mail, bool normalize = true)
        {
            SharedMemory.Instance.AddProperty("email.from", mail.From);
            SharedMemory.Instance.AddProperty("email.to", mail.To);
            SharedMemory.Instance.AddProperty("email.subject", mail.Subject);
            SharedMemory.Instance.AddProperty("email.body", mail.Body);
            SharedMemory.Instance.AddProperty("email.cc", mail.CC);

            return Task.CompletedTask;
        }
    }
}