using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.TextTemplating;
using Volo.Abp.UI.Navigation.Urls;

namespace PolpAbp.Framework.Emailing.Account
{
    public class FrameworkAccountEmailer : IFrameworkAccountEmailer, ITransientDependency
    {
        private readonly ITemplateRenderer _templateRenderer;
        private readonly IEmailSender _emailSender;
        protected IStringLocalizer<AccountResource> StringLocalizer { get; }
        private readonly IAppUrlProvider _appUrlProvider;
        private readonly IdentityUserManager _userManager;
        private readonly IDataFilter _dataFilter;
        private readonly ITenantRepository _tenantRepository;
        private readonly IConfiguration _configuration;

        public FrameworkAccountEmailer(
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer,
            IStringLocalizer<AccountResource> stringLocalizer,
            IAppUrlProvider appUrlProvider,
            ICurrentTenant currentTenant,
            IdentityUserManager userManager,
            IDataFilter dataFilter,
            ITenantRepository tenantRepository,
            IConfiguration configuration)
        {
            _emailSender = emailSender;
            StringLocalizer = stringLocalizer;
            _appUrlProvider = appUrlProvider;
            _templateRenderer = templateRenderer;
            _userManager = userManager;
            _dataFilter = dataFilter;
            _tenantRepository = tenantRepository;
            _configuration = configuration;
        }

        protected bool IsBackgroundEmailEnabled
        {
            get
            {
                return _configuration.GetValue<bool>("PolpAbp:Framework:BackgroundEmailEnabled");
            }
        }

        public async Task SendEmailActivationLinkAsync(Guid userId)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return;
                }

                var tenant = await _tenantRepository.FindAsync(user.TenantId.Value);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = await _appUrlProvider.GetUrlAsync("MVC", FrameworkUrlNames.EmailActivation);

                var link = $"{url}?userId={user.Id}&tenantId={user.TenantId}&confirmationCode={UrlEncoder.Default.Encode(token)}";

                var emailContent = await _templateRenderer.RenderAsync(
                    Templates.AccountEmailTemplates.EmailActivationtLink,
                    new
                    {
                        link = link,
                        tenancy = tenant.Name
                    }
                );

                // todo: Should we use the background ??
                // In that case, the email may not be sent instantly.
                await _emailSender.SendAsync(
                    user.Email,
                    StringLocalizer["EmailActivation_Subject"],
                    emailContent
                );
            }
        }

        public async Task SendPasswordChangeNotyAsync(Guid userId)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return;
                }

                var emailContent = await _templateRenderer.RenderAsync(
                    Templates.AccountEmailTemplates.NotyPasswordChange,
                    new
                    {
                        name = user.Name
                    }
                );

                if (IsBackgroundEmailEnabled)
                {
                    await _emailSender.QueueAsync(
                        user.Email,
                        StringLocalizer["NotyPasswordChange_Subject"],
                        emailContent
                    );
                }
                else
                {
                    await _emailSender.SendAsync(
                        user.Email,
                        StringLocalizer["NotyPasswordChange_Subject"],
                        emailContent
                    );
                }
            }
        }
    }
}
