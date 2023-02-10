namespace PolpAbp.Framework.Mvc.Interceptors
{
	public interface IEmailingInterceptor
	{
		Task<string?> GetActivationLinkEmailCcAsync(Guid userId);

        Task<string?> GetForgotPasswordEmailCcAsync(Guid userId);

        Task<string?> GetTenantRegistrationEmailCcAsync(Guid userId);

		Task<string?> GetMemberRegistrationEmailCcAsync(Guid userId);
	}
}

