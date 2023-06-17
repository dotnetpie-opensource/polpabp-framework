using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Sms;

namespace PolpAbp.Framework.Mock.Sms
{
    public class Extensions
    {
        /// <summary>
        /// Building the service descriptor for dummy sms sender. 
        /// It should be used for update the DI. 
        /// Note that we do not introduce the package 
        /// Microsoft.Extensions.DependencyInjection.Extensions to reduce the package dependency.
        /// </summary>
        /// <returns>Service descriptor</returns>
        public static ServiceDescriptor BuildEmailSenderDescriptor()
        {
            var descriptor =
              new ServiceDescriptor(
                  typeof(ISmsSender),
                  typeof(DummySmsSender),
                  ServiceLifetime.Singleton);

            return descriptor;
        }
    }
}
