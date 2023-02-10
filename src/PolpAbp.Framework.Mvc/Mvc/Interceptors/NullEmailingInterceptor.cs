using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mvc.Interceptors
{
    public class NullEmailingInterceptor : IEmailingInterceptor, ITransientDependency
    {
        public Task<string?> GetActivationLinkEmailCcAsync(Guid userId)
        {
            return Task.FromResult<string?>(null);
        }

        public Task<string?> GetForgotPasswordEmailCcAsync(Guid userId)
        {
            return Task.FromResult<string?>(null);
        }

        public Task<string?> GetMemberRegistrationEmailCcAsync(Guid userId)
        {
            return Task.FromResult<string?>(null);
        }

        public Task<string?> GetTenantRegistrationEmailCcAsync(Guid userId)
        {
            return Task.FromResult<string?>(null);
        }
    }
}
