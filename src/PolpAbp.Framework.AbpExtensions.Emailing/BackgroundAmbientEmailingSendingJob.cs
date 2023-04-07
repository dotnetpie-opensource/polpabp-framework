using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace PolpAbp.Framework
{
    public class BackgroundAmbientEmailingSendingJob : AsyncBackgroundJob<BackgroundEmailSendingJobArgs>, ITransientDependency
    {
        protected IEmailSender EmailSender { get; }

        protected ICurrentTenant CurrentTenant { get; }

        public BackgroundAmbientEmailingSendingJob(IAmbientEmailSender emailSender,
            ICurrentTenant currentTenant)
        {
            EmailSender = emailSender;
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public override async Task ExecuteAsync(BackgroundEmailSendingJobArgs args)
        {
            Guid? tenantId = CurrentTenant.Id;

            var toAddress = args.To;

            var idx = toAddress.IndexOf("::::");
            if (idx >= 0)
            {
                var tennantStr = args.To.Substring(0, idx);
                if (Guid.TryParse(tennantStr, out Guid t)) {
                    tenantId = t;
                }
                toAddress = toAddress.Substring(idx + 4);
            }

            using (CurrentTenant.Change(tenantId))
            {
                if (args.From.IsNullOrWhiteSpace())
                {
                    await EmailSender.SendAsync(toAddress, args.Subject, args.Body, args.IsBodyHtml);
                }
                else
                {
                    await EmailSender.SendAsync(args.From, toAddress, args.Subject, args.Body, args.IsBodyHtml);
                }
            }
        }
    }
}
