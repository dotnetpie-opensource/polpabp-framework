using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;

namespace PolpAbp.Framework.Mock.BackgroundJobs
{
    public static class Extensions
    {
        /// <summary>
        /// Building the service descriptor for dummy background job manager. 
        /// It shoule be used for update the DI. 
        /// Note that we do not introduce the package 
        /// Microsoft.Extensions.DependencyInjection.Extensions to reduce the package dependency.
        /// </summary>
        /// <returns>Service descriptor</returns>
        public static ServiceDescriptor GetBackgroundJobManagerServiceDescriptor()
        {
            var descriptor =
 new ServiceDescriptor(
     typeof(IBackgroundJobManager),
     typeof(DummyBackgroundJobManager),
     ServiceLifetime.Transient);

            return descriptor;
        }
    }
}
