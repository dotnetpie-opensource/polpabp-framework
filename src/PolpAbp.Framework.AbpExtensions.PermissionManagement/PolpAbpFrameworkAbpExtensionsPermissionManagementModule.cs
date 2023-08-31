using System;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace PolpAbp.Framework.AbpExtensions.PermissionManagement
{
    [DependsOn(
  typeof(AbpPermissionManagementDomainModule)
  )]
    public class PolpAbpFrameworkAbpExtensionsPermissionManagementModule : AbpModule
    {
    }
}
