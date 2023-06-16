using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;

namespace PolpAbp.Framework.Mock.BackgroundJobs
{
    public class DummyBackgroundJobManager : IBackgroundJobManager
    {
        public static Dictionary<System.Type, System.Type> Arg2HandlerMappings =
            new Dictionary<Type, Type>();

        public async Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            if (SharedMemory.Data.ServiceProvider != null) {
                var k = typeof(TArgs);
                if (Arg2HandlerMappings.ContainsKey(k))
                {
                    var instance = SharedMemory.Data.ServiceProvider.GetService(Arg2HandlerMappings[k])
                        as AsyncBackgroundJob<TArgs>;
                    await instance.ExecuteAsync(args);
                }
            }

            SharedMemory.Data.BackgroundJobName = typeof(TArgs).ToString();
            return "dummy";
        }
    }
}
