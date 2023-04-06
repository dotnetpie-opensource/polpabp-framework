using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Authorization.Users
{
    public class DummyUserIdentityOpInterceptor : IUserIdentityOpInterceptor, ITransientDependency
    {
        public UserIdentityOpContext Context { get; }

        public DummyUserIdentityOpInterceptor()
        {
            Context = new UserIdentityOpContext();
        }

        public Task BeforeCreateUserAsync()
        {
            return Task.CompletedTask;
        }

        public Task BeforeDeleteUserAsync()
        {
            return Task.CompletedTask;
        }

        public Task BeforeUpdateUserAsync()
        {
            return Task.CompletedTask;
        }

    }
}
