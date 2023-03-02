using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Uow;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace PolpAbp.Framework.DataSeeders;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IApiResourceRepository _apiResourceRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IConfiguration _configuration;

    string[] CommonScopes = new[]
          {
            "openid",
            "email",
            "phone",
            "profile",
            "role",
            "address",
            "GoGoCite",
            "offline_access"
        };

    public IdentityServerDataSeedContributor(
        IClientRepository clientRepository,
        IApiResourceRepository apiResourceRepository,
        IIdentityResourceDataSeeder identityResourceDataSeeder,
        IGuidGenerator guidGenerator,
        IConfiguration configuration)
    {
        _clientRepository = clientRepository;
        _apiResourceRepository = apiResourceRepository;
        _identityResourceDataSeeder = identityResourceDataSeeder;
        _guidGenerator = guidGenerator;
        _configuration = configuration;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await _identityResourceDataSeeder.CreateStandardResourcesAsync();
        await UpdateClientsAsync();
    }

    private async Task UpdateClientsAsync()
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
                RequireClientSecret = clientModel.RequireClientSecret
            },
            autoSave: true
        );

        foreach (var scope in CommonScopes)
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
        public bool RequireConsent { get; set; }
        public bool RequireClientSecret { get; set; }

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
            RequireConsent = false;
            RequireClientSecret = false;
            IdentityTokenLifetime = 300; // 5 min
            AccessTokenLifetime = 3600; // 1 hour
            AuthorizationCodeLifetime = 300; // 5 min
            AbsoluteRefreshTokenLifetime = 2592000; // 30 days
            SlidingRefreshTokenLifetime = 1296000; // 15 days
        }

    }
}
