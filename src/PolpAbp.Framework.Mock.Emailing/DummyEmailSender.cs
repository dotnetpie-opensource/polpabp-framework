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
            SharedMemory.Data.EmailReceiver = to;
            return Task.CompletedTask;
        }

        public Task QueueAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.EmailReceiver = to;
            return Task.CompletedTask;
        }

        public Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.EmailReceiver = to;
            return Task.CompletedTask;
        }

        public Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            SharedMemory.Data.EmailReceiver = to;
            return Task.CompletedTask;
        }

        public Task SendAsync(MailMessage mail, bool normalize = true)
        {
            SharedMemory.Data.EmailReceiver = mail.To.ToString();
            return Task.CompletedTask;
        }
    }
}