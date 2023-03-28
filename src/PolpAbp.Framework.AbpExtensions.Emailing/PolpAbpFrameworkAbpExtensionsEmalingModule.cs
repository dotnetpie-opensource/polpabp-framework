using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpEmailingModule)
        )]
    public class PolpAbpFrameworkAbpExtensionsEmalingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.AddJob<BackgroundAmbientEmailingSendingJob>();
            });
        }
    }
}
