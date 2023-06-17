using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;

namespace PolpAbp.Framework.Mock.BackgroundJobs
{
    public class DummyBackgroundJobManager : IBackgroundJobManager
    {
        private readonly IMockScopeContext _scopeContext;

        public DummyBackgroundJobManager(IMockScopeContext scopeContext) {
            _scopeContext = scopeContext;
        }

        public async Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            _scopeContext.BackgroundJobs.Add(typeof(TArgs).ToString());

            if (_scopeContext.ServiceProvider != null) {
                var k = typeof(TArgs);
                if (_scopeContext.Arg2HandlerMappings.ContainsKey(k))
                {
                    var instance = _scopeContext.ServiceProvider.GetService(_scopeContext.Arg2HandlerMappings[k])
                        as AsyncBackgroundJob<TArgs>;
                    await instance.ExecuteAsync(args);
                }
            }
            return "dummy";
        }
    }
}
