using System;
using Volo.Abp.Modularity;
using Volo.Abp.BlobStoring;

namespace PolpAbp.Framework
{
    [DependsOn(
        typeof(AbpBlobStoringModule)
    )]
    public class PolpAbpFrameworkBlobStoringModule : AbpModule
    {
    }
}

