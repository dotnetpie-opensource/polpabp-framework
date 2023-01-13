using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework.Mvc;

[DependsOn(
    typeof(AbpAspNetCoreModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class PolpAbpFrameworkMvcModule : AbpModule
{
}