using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;

namespace PolpAbp.Framework.Mock
{
    public class DummyBackgroundJobManager : IBackgroundJobManager
    {

        public Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            SharedMemory.Data.BackgroundJobName = typeof(TArgs).ToString();

            var x = "dummy";
            return Task.FromResult(x);
        }
    }
}
