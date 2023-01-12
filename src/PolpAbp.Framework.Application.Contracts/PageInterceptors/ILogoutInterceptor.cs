using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.PageInterceptors
{
	public interface ILogoutInterceptor
	{
		Task BeforeLogoutAsync();
		Task AfterLogoutAsync();
	}
}

