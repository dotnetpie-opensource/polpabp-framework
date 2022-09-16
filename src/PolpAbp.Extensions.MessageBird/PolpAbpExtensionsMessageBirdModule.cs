using PolpAbp.Framework;
using System;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;

namespace PolpAbp.Extensions.MessageBird
{
    // We use AbpJsonModule to try to eliminate the dependency conflict 
    // on NewtonJson.
    [DependsOn(
        typeof(AbpSmsModule),
        typeof(AbpJsonModule),
        typeof(PolpAbpFrameworkCoreSharedModule)
        )]
    public class PolpAbpExtensionsMessageBirdModule : AbpModule
    {
    }
}
