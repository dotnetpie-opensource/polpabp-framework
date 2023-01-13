using Microsoft.AspNetCore.Http;

namespace PolpAbp.Framework.Mvc.Cookies
{
    public static class CookieExtensions
    {
        public static void SetNamedCookie(this HttpResponse response, string name, string value, string? domain = null, TimeSpan? span = null)
        {
            var options = new CookieOptions();
            if (!string.IsNullOrWhiteSpace(domain))
            {
                options.Domain = domain;
            }
            if (span.HasValue)
            {
                var now = DateTimeOffset.UtcNow;
                var expired = now + span;
                options.Expires = expired;
            }
            // Remember 
            response.Cookies.Append(name, value, options);
        }

        public static void ClearNamedCookie(this HttpResponse response, string name, string? domain)
        {
            var options = new CookieOptions();
            if (!string.IsNullOrWhiteSpace(domain))
            {
                options.Domain = domain;
            }

            response.Cookies.Delete(name, options);
        }
    }
}
