using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace PolpAbp.Framework
{
    public abstract class IdentityUserRepositoryTestsBase<TStartupModule> : FrameworkTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
    }
}
