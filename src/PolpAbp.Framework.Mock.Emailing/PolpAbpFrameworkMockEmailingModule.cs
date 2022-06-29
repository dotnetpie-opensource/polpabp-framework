using Volo.Abp.Emailing;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework.Mock.Emailing
{
    [DependsOn(
           typeof(AbpEmailingModule)
       )]
    public class PolpAbpFrameworkMockEmailingModule : AbpModule
    {
    }
}
