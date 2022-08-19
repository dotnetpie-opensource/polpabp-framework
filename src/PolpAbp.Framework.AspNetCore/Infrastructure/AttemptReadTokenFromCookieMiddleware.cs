using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Infrastructure
{
    public class AttemptReadTokenFromCookieMiddleware : IMiddleware, ITransientDependency
    {
        private const string CookieName = "Abp.AuthToken";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader == null)
            {
                var cookie = context.Request.Cookies[CookieName];
                if (cookie != null)
                {
                    // Set up the authorization header
                    context.Request.Headers["Authorization"] = "Bearer " + cookie;
                }
            }
            await next(context).ConfigureAwait(false);
        }
    }
}
