using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpEventBusModule)
    )]
    public class PolpAbpFrameworkEventsModule : AbpModule
    {
    }
}
