using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mvc.Interceptors
{
    public class NullLogoutInterceptor : ILogoutInterceptor, ITransientDependency
    {
        public Task AfterLogoutAsync()
        {
            return Task.CompletedTask;
        }

        public Task BeforeLogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}

