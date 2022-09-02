using Microsoft.AspNetCore.Builder;
using PolpAbp.Framework.CustomServices;

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

    }
}
