using Microsoft.AspNetCore.Http;

namespace PolpAbp.Framework.Mvc.Interceptors
{
	public interface ILogoutInterceptor
	{
		Task BeforeSignOutAsync(HttpContext httpContext);
		Task AfterSignOutAsync(HttpContext httpContext);
	}
}

