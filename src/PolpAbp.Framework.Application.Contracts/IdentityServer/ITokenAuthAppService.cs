using System;
using System.Threading.Tasks;
using PolpAbp.Framework.IdentityServer.Dto;
using Volo.Abp.Application.Services;

namespace PolpAbp.Framework.IdentityServer
{
    public interface ITokenAuthAppService : IApplicationService
    {
        Task<TokenResponseDto> AuthenticateAsync(string username, string password);
    }
}
