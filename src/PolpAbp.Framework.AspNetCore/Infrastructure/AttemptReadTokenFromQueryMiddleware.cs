using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Infrastructure
{
    public class AttemptReadTokenFromQueryMiddleware : IMiddleware, ITransientDependency
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // The case-insensitive query parameter will normalize the token into a lowere case.
            var tokenValue = context.Request.Query["EncodedToken"].FirstOrDefault();

            if (tokenValue != null)
            {
                // Set up the authorization header
                context.Request.Headers["Authorization"] = "Bearer " + tokenValue;
            }
            await next(context).ConfigureAwait(false);
        }
    }
}
