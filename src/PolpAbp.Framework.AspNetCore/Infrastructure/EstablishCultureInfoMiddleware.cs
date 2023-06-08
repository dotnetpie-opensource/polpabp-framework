using System;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Users;
using Volo.Abp.SettingManagement;

namespace PolpAbp.Framework.Infrastructure
{
	public class EstablishCultureInfoMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICurrentUser _currentUser;
        private readonly ISettingManager _settingManager;

        private const string CookieName = "Abp.Localization.CultureName";
        private const string HeaderName = "AspNetCore-Culture";


        public EstablishCultureInfoMiddleware(
            ICurrentUser currentUser,
            ISettingManager settingManager
            )
        {
            _currentUser = currentUser;
            _settingManager = settingManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_currentUser.Id.HasValue)
            {
                var lang = await _settingManager.GetOrNullForUserAsync(LocalizationSettingNames.DefaultLanguage, _currentUser.Id.Value);
                CultureInfo.CurrentUICulture = new CultureInfo(lang);
            }
            else
            {
                var header = context.Request.Headers[HeaderName].FirstOrDefault();
                if (!string.IsNullOrEmpty(header))
                {
                    // Two forms
                    if (header.Contains("uic="))
                    {
                        var index = header.IndexOf("uic=");
                        var lang = header.Substring(index + "uic=".Length);
                        CultureInfo.CurrentUICulture = new CultureInfo(lang);
                    }
                    else
                    {
                        CultureInfo.CurrentUICulture = new CultureInfo(header);
                    }
                }
                else
                {
                    var anotherHeader = context.Request.Headers["Accept-Language"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(anotherHeader))
                    {
                        var codes = anotherHeader.Split(';');
                        var first = codes.FirstOrDefault();
                        if (!string.IsNullOrEmpty(first))
                        {
                            if (first.Contains("zh"))
                            {
                                CultureInfo.CurrentUICulture = new CultureInfo("zh-Hans");
                            }
                        }
                    }

                }
            } // TODO: Cookie

            await next(context).ConfigureAwait(false);
        }
    }
}

