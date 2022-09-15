using PolpAbp.Framework;
using System;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;

namespace PolpAbp.Extensions.MessageBird
{
    [DependsOn(
      typeof(AbpSmsModule),
        typeof(PolpAbpFrameworkCoreSharedModule)
        )]
    public class PolpAbpExtensionsMessageBirdModule : AbpModule
    {
    }
}
