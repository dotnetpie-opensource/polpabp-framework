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
            if (args is EnhancedBackgroundEmailSendingJobArgs enhancedBackgroundEmailSendingJobArgs)
            {
                tenantId = enhancedBackgroundEmailSendingJobArgs.TenantId;
            }

            using (CurrentTenant.Change(tenantId))
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
}
