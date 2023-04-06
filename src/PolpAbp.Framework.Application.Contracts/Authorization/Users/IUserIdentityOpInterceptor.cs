using System.Threading.Tasks;

namespace PolpAbp.Framework.Authorization.Users
{
    public interface IUserIdentityOpInterceptor
    {
        UserIdentityOpContext Context { get; } 

        Task BeforeCreateUserAsync();
        Task BeforeDeleteUserAsync();
        Task BeforeUpdateUserAsync();
    }
}
