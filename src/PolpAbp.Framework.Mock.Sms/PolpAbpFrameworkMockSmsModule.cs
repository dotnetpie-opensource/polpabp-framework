﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;

namespace PolpAbp.Framework.Mock.Sms
{
    [DependsOn(
       typeof(AbpSmsModule),
        typeof(PolpAbpFrameworkMockModule)
   )]
    public class PolpAbpFrameworkMockSmsModule : AbpModule
    {
    }
}
