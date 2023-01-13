using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Mvc.Interceptors
{
	public interface ILogoutInterceptor
	{
		Task BeforeLogoutAsync();
		Task AfterLogoutAsync();
	}
}

