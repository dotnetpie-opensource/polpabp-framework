using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PolpAbp.Framework.CustomServices;
using Volo.Abp.Users;

namespace PolpAbp.Framework.AspNetCore
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Use cutomized one to have extra featurs.
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomMultiTenancy(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<CustomMultiTenancyMiddleware>();
        }


        /// <summary>
        /// Building the service descriptor for the custom current user. 
        /// It should be used to update the DI. 
        /// </summary>
        /// <returns>Service descriptor</returns>
        public static ServiceDescriptor GetCustomCurrentUserDescriptor()
        {
            var descriptor =
 new ServiceDescriptor(
     typeof(ICurrentUser),
     typeof(CustomCurrentUser),
     ServiceLifetime.Transient);

            return descriptor;
        }

    }
}
