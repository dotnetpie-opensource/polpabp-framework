using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework.Mock.BackgroundJobs
{
    [DependsOn(
        typeof(AbpBackgroundJobsModule),
        typeof(PolpAbpFrameworkMockModule)
       )]
    public class PolpAbpFrameworkMockBackgroundJobsModule : AbpModule
    {

    }
}
