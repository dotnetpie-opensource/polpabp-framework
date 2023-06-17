using System.Net.Mail;
using System.Threading.Tasks;
using Volo.Abp.Emailing;

namespace PolpAbp.Framework.Mock.Emailing
{
    public class DummyEmailSender : IEmailSender
    {
        private readonly IMockScopeContext _scopeContext;

        public DummyEmailSender(IMockScopeContext scopeContext)
        {
            _scopeContext = scopeContext;
        }

        public Task QueueAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            _scopeContext.AddProperty("email.to", to);
            _scopeContext.AddProperty("email.subject", subject);
            _scopeContext.AddProperty("email.body", body);
            return Task.CompletedTask;
        }

        public Task QueueAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            _scopeContext.AddProperty("email.from", from);
            _scopeContext.AddProperty("email.to", to);
            _scopeContext.AddProperty("email.subject", subject);
            _scopeContext.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            _scopeContext.AddProperty("email.to", to);
            _scopeContext.AddProperty("email.subject", subject);
            _scopeContext.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            _scopeContext.AddProperty("email.from", from);
            _scopeContext.AddProperty("email.to", to);
            _scopeContext.AddProperty("email.subject", subject);
            _scopeContext.AddProperty("email.body", body);

            return Task.CompletedTask;
        }

        public Task SendAsync(MailMessage mail, bool normalize = true)
        {
            _scopeContext.AddProperty("email.from", mail.From);
            _scopeContext.AddProperty("email.to", mail.To);
            _scopeContext.AddProperty("email.subject", mail.Subject);
            _scopeContext.AddProperty("email.body", mail.Body);
            _scopeContext.AddProperty("email.cc", mail.CC);

            return Task.CompletedTask;
        }
    }
}