using Microsoft.AspNetCore.Http;

namespace PolpAbp.Framework.Mvc.Cookies
{
    public interface IAppCookieManager
    {
        void ClearTenantCookie(HttpResponse response, string? domain = null);
        bool HasTenantCookie(HttpRequest request);
        string ReadTenantCookieValue(HttpRequest request);
        void SetTenantCookieValue(HttpResponse response, string value, string? domain = null, TimeSpan? span = null);
    }
}
