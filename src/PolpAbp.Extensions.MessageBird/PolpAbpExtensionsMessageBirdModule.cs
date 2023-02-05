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
        typeof(PolpAbpFrameworkCoreSharedModule),
        // We on purpose do not refer to the application module, 
        // in order to reduce the dependency. 
        // Though doing so can impact the test modularity, 
        // the test host module has to import the application module.
        typeof(PolpAbpFrameworkApplicationContractsModule) 
        )]
    public class PolpAbpExtensionsMessageBirdModule : AbpModule
    {
    }
}
