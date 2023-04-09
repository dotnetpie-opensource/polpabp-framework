using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Uow;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace PolpAbp.Framework.DataSeeders;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IApiScopeRepository _apiScopeRepository;
    private readonly IApiResourceRepository _apiResourceRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IConfiguration _configuration;

    public IdentityServerDataSeedContributor(
        IClientRepository clientRepository,
        IApiScopeRepository apiScopeRepository,
        IApiResourceRepository apiResourceRepository,
        IIdentityResourceDataSeeder identityResourceDataSeeder,
        IGuidGenerator guidGenerator,
        IConfiguration configuration)
    {
        _clientRepository = clientRepository;
        _apiScopeRepository = apiScopeRepository;
        _apiResourceRepository = apiResourceRepository;
        _identityResourceDataSeeder = identityResourceDataSeeder;
        _guidGenerator = guidGenerator;
        _configuration = configuration;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await _identityResourceDataSeeder.CreateStandardResourcesAsync();
        await PopulateApiScopesAsync();
        await PopulateApiResourcesAsync();
        await PopulateClientsAsync();
    }

    #region ApiScopes

    private async Task PopulateApiScopesAsync()
    {
        var configurationSection = _configuration.GetSection("IdentityServer:ApiScopes");
        var mappings = new Dictionary<string, ApiScopeModel>();
        configurationSection.Bind(mappings);

        foreach (var mapping in mappings)
        {
            if (mapping.Value.IsDeleted)
            {
                await DeleteApiScopeAsync(mapping.Key);
            }
            else
            {
                await CreateApiScopeAsync(mapping.Key, mapping.Value);
            }
        }
    }

    private async Task<ApiScope> CreateApiScopeAsync(string name, ApiScopeModel model)
    {
        var apiScope = await _apiScopeRepository.FindByNameAsync(name);
        if (apiScope != null)
        {
            return apiScope;
        }

        if (apiScope == null)
        {
            apiScope = await _apiScopeRepository.InsertAsync(
                new ApiScope(
                    _guidGenerator.Create(),
                    name,
                    model.DisplayName ?? name
                ),
                autoSave: true
            );
        }

        foreach (var claim in model.UserClaims)
        {
            if (apiScope.FindClaim(claim) == null)
            {
                apiScope.AddUserClaim(claim);
            }
        }

        return await _apiScopeRepository.UpdateAsync(apiScope);
    }

    private async Task DeleteApiScopeAsync(string name)
    {
        var apiScope = await _apiScopeRepository.FindByNameAsync(name);
        if (apiScope != null)
        {
            await _apiScopeRepository.DeleteAsync(apiScope, true);
        }
    }
    #endregion

    #region ApiResources
    private async Task PopulateApiResourcesAsync()
    {
        var configurationSection = _configuration.GetSection("IdentityServer:ApiResources");
        var mappings = new Dictionary<string, ApiResourceModel>();
        configurationSection.Bind(mappings);

        foreach (var mapping in mappings)
        {
            if (mapping.Value.IsDeleted)
            {
                await DeleteApiResourceAsync(mapping.Key);
            }
            else
            {
                await CreateApiResourceAsync(mapping.Key, mapping.Value);
            }
        }
    }

    private async Task<ApiResource> CreateApiResourceAsync(string name, ApiResourceModel model)
    {
        var apiResource = await _apiResourceRepository.FindByNameAsync(name);
        if (apiResource != null) {
            return apiResource;
        }

        if (apiResource == null)
        {
            apiResource = await _apiResourceRepository.InsertAsync(
                new ApiResource(
                    _guidGenerator.Create(),
                    name,
                    model.DisplayName ?? name + " API"
                ),
                autoSave: true
            );
        }

        foreach (var claim in model.UserClaims)
        {
            if (apiResource.FindClaim(claim) == null)
            {
                apiResource.AddUserClaim(claim);
            }
        }

        foreach(var s in model.Scopes)
        {
            if (apiResource.FindScope(s) == null)
            {
                apiResource.AddScope(s);
            }
        }

        return await _apiResourceRepository.UpdateAsync(apiResource);
    }

    private async Task DeleteApiResourceAsync(string name)
    {
        var apiResource = await _apiResourceRepository.FindByNameAsync(name);
        if (apiResource != null)
        {
            await _apiResourceRepository.DeleteAsync(apiResource, true);
        }
    }
    #endregion

    #region Clients
    private async Task PopulateClientsAsync()
    {
        var configurationSection = _configuration.GetSection("IdentityServer:Clients");
        var mappings = new Dictionary<string, ClientModel>();
        configurationSection.Bind(mappings);

        foreach (var mapping in mappings)
        {
            if (mapping.Value.IsDeleted)
            {
                await DeleteClientAsync(mapping.Key);
            }
            else
            {
                await CreateClientAsync(mapping.Key, mapping.Value);
            }
        }
    }
    private async Task DeleteClientAsync(string name)
    {
        var client = await _clientRepository.FindByClientIdAsync(name);
        if (client != null)
        {
            await _clientRepository.DeleteAsync(client, true);
        }
    }


    private async Task<Client> CreateClientAsync(string name, ClientModel clientModel)
    {
        var client = await _clientRepository.FindByClientIdAsync(name);
        if (client != null)
        {
            return client;
        }
        client = await _clientRepository.InsertAsync(
            new Client(
                _guidGenerator.Create(),
                name
            )
            {
                ClientName = name,
                ClientUri = clientModel.ClientUri,
                ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                Description = name,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                AbsoluteRefreshTokenLifetime = clientModel.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = clientModel.AccessTokenLifetime,
                AuthorizationCodeLifetime = clientModel.AuthorizationCodeLifetime,
                IdentityTokenLifetime = clientModel.IdentityTokenLifetime,
                SlidingRefreshTokenLifetime = clientModel.SlidingRefreshTokenLifetime,
                RequireConsent = clientModel.RequireConsent,
                RequireClientSecret = clientModel.RequireClientSecret,
                RequirePkce = clientModel.RequirePkce
            },
            autoSave: true
        );

        foreach (var scope in clientModel.Scopes)
        {
            if (client.FindScope(scope) == null)
            {
                client.AddScope(scope);
            }
        }

        foreach (var grantType in clientModel.GrantTypes)
        {
            if (client.FindGrantType(grantType) == null)
            {
                client.AddGrantType(grantType);
            }
        }

        var secret = clientModel.ClientSecret.Sha256();
        if (client.FindSecret(secret) == null)
        {
            client.AddSecret(secret);
        }

        foreach (var redirectUri in clientModel.RedirectUris)
        {
            if (client.FindRedirectUri(redirectUri) == null)
            {
                client.AddRedirectUri(redirectUri);
            }
        }

        foreach (var postLogoutRedirectUri in clientModel.PostLogoutRedirectUris)
        {
            if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
            {
                client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
            }
        }

        foreach (var corsOrigin in clientModel.CorsOrigins)
        {
            if (client.FindCorsOrigin(corsOrigin) == null)
            {
                client.AddCorsOrigin(corsOrigin);
            }
        }

        return await _clientRepository.UpdateAsync(client);
    }
    #endregion

    class ApiScopeModel
    {
        public bool IsDeleted { get; set; }
        public string DisplayName { get; set; }
        public List<string> UserClaims { get; set; }

        public ApiScopeModel()
        {
            UserClaims = new List<string>();
        }
    }

    class ApiResourceModel {
        public bool IsDeleted { get; set; }
        public string DisplayName { get; set; }
        public List<string> UserClaims { get; set; }
        public List<string> Scopes { get; set; }

        public ApiResourceModel() {
            UserClaims = new List<string>();
            Scopes = new List<string>();
        }
    }

    class ClientModel
    {
        public bool IsDeleted { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientUri { get; set; }
        public List<string> GrantTypes { get; set; }
        public List<string> CorsOrigins { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> Scopes { get;set; }
        public bool RequireConsent { get; set; }
        public bool RequireClientSecret { get; set; }
        public bool RequirePkce { get; set; }

        public int IdentityTokenLifetime {get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }

        public ClientModel()
        {
            GrantTypes = new List<string>();
            CorsOrigins = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            RedirectUris = new List<string>();
            Scopes = new List<string>();
            RequireConsent = false;
            RequireClientSecret = false;
            RequirePkce = false;
            IdentityTokenLifetime = 300; // 5 min
            AccessTokenLifetime = 3600; // 1 hour
            AuthorizationCodeLifetime = 300; // 5 min
            AbsoluteRefreshTokenLifetime = 2592000; // 30 days
            SlidingRefreshTokenLifetime = 1296000; // 15 days
        }

    }
}
