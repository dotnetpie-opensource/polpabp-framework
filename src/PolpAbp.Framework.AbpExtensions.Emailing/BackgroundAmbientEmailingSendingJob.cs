using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace PolpAbp.Framework
{
    public class BackgroundAmbientEmailingSendingJob : AsyncBackgroundJob<BackgroundEmailSendingJobArgs>, ITransientDependency
    {
        protected IEmailSender EmailSender { get; }

        public BackgroundAmbientEmailingSendingJob(IAmbientEmailSender emailSender)
        {
            EmailSender = emailSender;
        }

        public override async Task ExecuteAsync(BackgroundEmailSendingJobArgs args)
        {
            if (args.From.IsNullOrWhiteSpace())
            {
                await EmailSender.SendAsync(args.To, args.Subject, args.Body, args.IsBodyHtml);
            }
            else
            {
                await EmailSender.SendAsync(args.From, args.To, args.Subject, args.Body, args.IsBodyHtml);
            }
        }
    }
}
