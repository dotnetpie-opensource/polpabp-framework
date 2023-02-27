using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PolpAbp.Framework.Exceptions.IdentityServer;
using PolpAbp.Framework.IdentityServer.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.IdentityModel;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace PolpAbp.Framework.IdentityServer
{
    [RemoteService(false)]
    [ExposeServices(typeof(ITokenAuthAppService))]
    public class TokenAuthAppService : IdentityModelAuthenticationService, ITokenAuthAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public TokenAuthAppService(
            IOptions<AbpIdentityClientOptions> options,
            ICancellationTokenProvider cancellationTokenProvider,
            IHttpClientFactory httpClientFactory,
            ICurrentTenant currentTenant,
            IOptions<IdentityModelHttpRequestMessageOptions> identityModelHttpRequestMessageOptions,
            IDistributedCache<IdentityModelTokenCacheItem> tokenCache,
            IDistributedCache<IdentityModelDiscoveryDocumentCacheItem> discoveryDocumentCache,
            IClientRepository clientRepository,
            IConfiguration configuration,
            ILogger<TokenAuthAppService> logger
            )
            :base(options, cancellationTokenProvider, httpClientFactory,
                 currentTenant, identityModelHttpRequestMessageOptions,
                 tokenCache, discoveryDocumentCache)
        {
            _clientRepository = clientRepository;
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<TokenResponseDto> AuthenticateAsync(string username, string password)
        {
            
            // var cfg = GetClientConfiguration("FormPortal_App");
            /*
            if (cfg == null)
            {
                var client = await _clientRepository.FindByCliendIdAsync("FormPortal_Web");
                cfg = new IdentityClientConfiguration
                {
                    ClientId = client.ClientId,
                    ClientSecret = client.
                };
            } */

            var authority = _configuration["AuthServer:Authority"];
            var clientId = _configuration["AuthServer:ClientId"];
            var clientSecret = _configuration["AuthServer:ClientSecret"];
            var scopes = _configuration["AuthServer:Scopes"];
            // TODO: Read from database to get the client information ????
            var input = new IdentityClientConfiguration(
                authority,
                scopes,
                clientId, 
                clientSecret,
                OidcConstants.GrantTypes.Password,
                username,
                password
            );
            var tokenResponse = await GetTokenResponse(input);

            if (tokenResponse.IsError)
            {
                if (tokenResponse.ErrorDescription != null)
                {
                    throw new PolpAbpTokenAuthException($"Could not get token from the OpenId Connect server! ErrorType: {tokenResponse.ErrorType}. " +
                                           $"Error: {tokenResponse.Error}. ErrorDescription: {tokenResponse.ErrorDescription}. HttpStatusCode: {tokenResponse.HttpStatusCode}");
                }

                var rawError = tokenResponse.Raw;
                var withoutInnerException = rawError.Split(new string[] { "<eof/>" }, StringSplitOptions.RemoveEmptyEntries);
                throw new PolpAbpTokenAuthException(withoutInnerException[0]);
            }

            return ToDto(tokenResponse);
        }

        /*
        protected override async Task<TokenResponse> GetTokenResponse(IdentityClientConfiguration configuration)
        {
            using (var httpClient = HttpClientFactory.CreateClient(HttpClientName))
            {
                AddHeaders(httpClient);

                switch (configuration.GrantType)
                {
                    case OidcConstants.GrantTypes.ClientCredentials:
                        return await httpClient.RequestClientCredentialsTokenAsync(
                            await CreateClientCredentialsTokenRequestAsync(configuration),
                            CancellationTokenProvider.Token
                        );
                    case OidcConstants.GrantTypes.Password:
                        return await httpClient.RequestPasswordTokenAsync(
                            await CreatePasswordTokenRequestAsync(configuration),
                            CancellationTokenProvider.Token
                        );
                    default:
                        throw new AbpException("Grant type was not implemented: " + configuration.GrantType);
                }
            }
        } */

        protected override void AddHeaders(HttpClient client)
        {
            _logger.LogDebug($"Set up TenantId in Header");

            //tenantId
            if (CurrentTenant.Id.HasValue)
            {
                _logger.LogDebug($"Tenant Id is {CurrentTenant.Id.Value}");

                var tenantKey = _configuration["PolpAbp:Framework:TenantKey"];
                //TODO: Use AbpAspNetCoreMultiTenancyOptions to get the key
                client.DefaultRequestHeaders.Add(tenantKey, CurrentTenant.Id.Value.ToString());

            }
        }


        private IdentityClientConfiguration GetClientConfiguration(string identityClientName = null)
        {
            if (identityClientName.IsNullOrEmpty())
            {
                return ClientOptions.IdentityClients.Default;
            }

            return ClientOptions.IdentityClients.GetOrDefault(identityClientName) ??
                   ClientOptions.IdentityClients.Default;
        }

        private TokenResponseDto ToDto(TokenResponse response)
        {
            return new TokenResponseDto
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = response.ExpiresIn,
                ErrorDescription = response.ErrorDescription,
                Scope = response.Scope
            };
        }
    }
}
