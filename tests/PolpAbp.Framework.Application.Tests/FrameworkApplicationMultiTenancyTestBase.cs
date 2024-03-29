﻿using System;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace PolpAbp.Framework
{
    public abstract class FrameworkApplicationMultiTenancyTestBase : FrameworkTestBase<FrameworkApplicationTestsModule>
    {
        protected virtual void UsingDbContext(Action<ITenantManagementDbContext> action)
        {
            using (var dbContext = GetRequiredService<ITenantManagementDbContext>())
            {
                action.Invoke(dbContext);
            }
        }

        protected virtual T UsingDbContext<T>(Func<ITenantManagementDbContext, T> action)
        {
            using (var dbContext = GetRequiredService<ITenantManagementDbContext>())
            {
                return action.Invoke(dbContext);
            }
        }

    }
}
