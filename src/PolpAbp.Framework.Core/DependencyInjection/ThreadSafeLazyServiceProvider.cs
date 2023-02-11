using System;
using System.Collections.Concurrent;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Core.DependencyInjection
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpLazyServiceProvider))]
    public class ThreadSafeLazyServiceProvider : AbpLazyServiceProvider, IAbpLazyServiceProvider, ITransientDependency
    {
        public ThreadSafeLazyServiceProvider(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
            CachedServices = new ConcurrentDictionary<Type, object>();
        }
    }
}
