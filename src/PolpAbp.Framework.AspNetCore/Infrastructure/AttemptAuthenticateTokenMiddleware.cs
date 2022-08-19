using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace PolpAbp.Framework.Infrastructure
{
    public class AttemptAuthenticateTokenMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly ITokenValidator _validator;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
        private readonly ILogger _logger;


        public AttemptAuthenticateTokenMiddleware(ICurrentTenant currentTenant,
            ITokenValidator tokenValidator,
            ICurrentUser currentUser,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            ILogger<AttemptAuthenticateTokenMiddleware> logger
            )
        {
            _currentTenant = currentTenant;
            _validator = tokenValidator;
            _currentUser = currentUser;
            _currentPrincipalAccessor = currentPrincipalAccessor;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // TODO: Find any other scheme

            _logger.LogDebug($"Path is {context.Request.Path.Value}");
            _logger.LogDebug($"IsHttps is {context.Request.IsHttps}");
            _logger.LogDebug($"IsHttps is {string.Join(" ", context.Request.Headers.Keys)}");

            if (!_currentUser.Id.HasValue)
            {

                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                if (authorizationHeader != null)
                {
                    if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        var parameter = authorizationHeader.Substring("Bearer ".Length);

                        var ret = await _validator.ValidateAccessTokenAsync(parameter);
                        if (!ret.IsError)
                        {
                            var jwt = new JwtSecurityToken(parameter);

                            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                                    new Claim(AbpClaimTypes.UserId, jwt.Subject)
                             }));

                            using (_currentPrincipalAccessor.Change(principal))
                            {
                                await next(context).ConfigureAwait(false);
                            }

                            return;

                        }
                    }
                }

            }

            await next(context).ConfigureAwait(false);
        }
    }
}
