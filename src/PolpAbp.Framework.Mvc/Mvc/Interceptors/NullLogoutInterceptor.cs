﻿using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mvc.Interceptors
{
    public class NullLogoutInterceptor : ILogoutInterceptor, ITransientDependency
    {
        public Task AfterSignOutAsync(HttpContext httpContext)
        {
            return Task.CompletedTask;
        }

        public Task BeforeSignOutAsync(HttpContext httpContext)
        {
            return Task.CompletedTask;
        }
    }
}

