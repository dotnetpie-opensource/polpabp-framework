using Microsoft.AspNetCore.Http;

namespace PolpAbp.Framework.Mvc.Interceptors
{
	public interface ILoginInterceptor
	{
		Task BeforeLoginAsync(HttpContext httpContext);
		Task AfterLoginAsync(HttpContext httpContext);
	}
}

