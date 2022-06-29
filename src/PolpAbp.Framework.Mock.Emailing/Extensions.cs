using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Emailing;

namespace PolpAbp.Framework.Mock.Emailing
{
    public static class Extensions
    {
        /// <summary>
        /// Building the service descriptor for dummy email sender. 
        /// It shoule be used for update the DI. 
        /// Note that we do not introduce the package 
        /// Microsoft.Extensions.DependencyInjection.Extensions to reduce the package dependency.
        /// </summary>
        /// <returns>Service descriptor</returns>
        public static ServiceDescriptor BuildEmailSenderDescriptor()
        {
            var descriptor =
              new ServiceDescriptor(
                  typeof(IEmailSender),
                  typeof(DummyEmailSender),
                  ServiceLifetime.Singleton);

            return descriptor;
        }
    }
}
