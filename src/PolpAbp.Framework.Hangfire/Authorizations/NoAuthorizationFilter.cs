using Hangfire.Dashboard;

namespace PolpAbp.Framework.Hangfire.Authorizations
{
    public class NoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
