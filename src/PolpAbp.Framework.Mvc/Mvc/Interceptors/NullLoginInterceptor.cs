using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mvc.Interceptors
{
    public class NullLoginInterceptor : ILoginInterceptor, ITransientDependency
    {
        public Task AfterLoginAsync(HttpContext httpContext)
        {
            return Task.CompletedTask;
        }

        public Task BeforeLoginAsync(HttpContext httpContext)
        {
            return Task.CompletedTask;
        }
    }
}

