using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework.Mock.BackgroundJobs
{
    [DependsOn(
           typeof(AbpBackgroundJobsModule)
       )]
    public class PolpAbpFrameworkMockBackgroundJobsModule : AbpModule
    {

    }
}
