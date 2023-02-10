﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PolpAbp.Framework.Extensions;
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
                return _configuration.GetValue<bool>("PolpAbp:Framework:BackgroundEmailEnabled", false);
            }
        }

        protected string DefaultEmailSignature
        {
            get
            {
                return _configuration.GetValue<string>("PolpAbp:Framework:DefaultEmailSignature", string.Empty);
            }
        }

        public async Task SendEmailActivationLinkAsync(Guid userId, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                // Sends out an email regardless the email is confirmed or not.
                // Therefore, this is a tryout attempt.
                if (user == null)
                {
                    return;
                }

                var tenant = await _tenantRepository.FindAsync(user.TenantId.Value);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = await _appUrlProvider.GetUrlAsync("MVC", FrameworkUrlNames.EmailActivation);

                var link = $"{url}?userId={user.Id}&tenantId={user.TenantId}&confirmationCode={UrlEncoder.Default.Encode(token)}";

                var emailContent = await _templateRenderer.RenderAsync(
                    user.IsActive ? Templates.AccountEmailTemplates.EmailConfirmatiionLink : Templates.AccountEmailTemplates.EmailActivationtLink,
                    new
                    {
                        name = user.GetFirstOrLastName(),
                        signature = DefaultEmailSignature,
                        link = link,
                        tenancy = tenant.Name
                    }
                );

                // todo: Should we use the background ??
                // In that case, the email may not be sent instantly.
                var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                await _emailSender.SendAsync(
                    receipents,
                    StringLocalizer[user.IsActive ? "EmailConfirmation_Subject" : "EmailActivation_Subject"],
                    emailContent
                );
            }
        }

        public async Task SendPasswordChangeNotyAsync(Guid userId, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null || !user.EmailConfirmed)
                {
                    return;
                }

                var emailContent = await _templateRenderer.RenderAsync(
                    Templates.AccountEmailTemplates.NotyPasswordChange,
                    new
                    {
                        name = user.GetFirstOrLastName(),
                        signature = DefaultEmailSignature
                    }
                );

                var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                if (IsBackgroundEmailEnabled)
                {
                    await _emailSender.QueueAsync(
                        receipents,
                        StringLocalizer["NotyPasswordChange_Subject"],
                        emailContent
                    );
                }
                else
                {
                    await _emailSender.SendAsync(
                        receipents,
                        StringLocalizer["NotyPasswordChange_Subject"],
                        emailContent
                    );
                }
            }
        }

        public async Task SendTwoFactorCodeAsync(Guid userId, string code, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null || !user.EmailConfirmed)
                {
                    return;
                }

                var emailContent = await _templateRenderer.RenderAsync(
                    Templates.AccountEmailTemplates.TwoFactorCode,
                    new
                    {
                        name = user.GetFirstOrLastName(),
                        signature = DefaultEmailSignature,
                        code = code
                    }
                );

                var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                await _emailSender.SendAsync(
                    receipents,
                    StringLocalizer["TwoFactorCode_Subject"],
                    emailContent
                );
            }
        }

        public async Task SendMemberRegistrationNotyAsync(Guid userId, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var target = await _userManager.FindByIdAsync(userId.ToString());
                if (target == null)
                {
                    return;
                }

                // Find out the admin of the tenant.
                // todo: Is it ok to hardcode the admin.
                var admins = await _userManager.GetUsersInRoleAsync("ADMIN");
                foreach (var user in admins)
                {
                    var emailContent = await _templateRenderer.RenderAsync(
                        Templates.AccountEmailTemplates.NotyMembereRegistration,
                        new
                        {
                            name = user.GetFirstOrLastName(),
                            signature = DefaultEmailSignature,
                            member = target.GetFullName()
                        }
                    );

                    var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                    if (IsBackgroundEmailEnabled)
                    {
                        await _emailSender.QueueAsync(
                            receipents,
                            StringLocalizer["NotyMemberRegistration_Subject"],
                            emailContent
                        );
                    }
                    else
                    {
                        await _emailSender.SendAsync(
                            receipents,
                            StringLocalizer["NotyMemberRegistration_Subject"],
                            emailContent
                        );
                    }
                }
            }
        }

        public async Task SendMemberRegistrationApprovalAsync(Guid userId, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var target = await _userManager.FindByIdAsync(userId.ToString());
                if (target == null)
                {
                    return;
                }

                // todo: A dedicated page for approve the user.
                var url = await _appUrlProvider.GetUrlAsync("MVC", "main");

                var link = $"{url}";

                // Find out the admin of the tenant.
                // todo: Is it ok to hardcode the admin.
                var admins = await _userManager.GetUsersInRoleAsync("ADMIN");
                foreach (var user in admins)
                {
                    var emailContent = await _templateRenderer.RenderAsync(
                        Templates.AccountEmailTemplates.ApproveMembereRegistration,
                        new
                        {
                            name = user.GetFirstOrLastName(),
                            signature = DefaultEmailSignature,
                            member = target.GetFullName(),
                            link = link
                        }
                    );

                    var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                    if (IsBackgroundEmailEnabled)
                    {
                        await _emailSender.QueueAsync(
                            receipents,
                            StringLocalizer["ApproveMemberRegistration_Subject"],
                            emailContent
                        );
                    }
                    else
                    {
                        await _emailSender.SendAsync(
                            receipents,
                            StringLocalizer["ApproveMemberRegistration_Subject"],
                            emailContent
                        );
                    }
                }
            }
        }


        public async Task SendNewOrResetPasswordAsync(Guid userId, string password, string cc = null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return;
                }

                var emailContent = await _templateRenderer.RenderAsync(
                    Templates.AccountEmailTemplates.NewOrResetPassword,
                    new
                    {
                        name = user.GetFirstOrLastName(),
                        signature = DefaultEmailSignature,
                        password = password
                    }
                );

                var receipents = string.IsNullOrEmpty(cc) ? user.Email : $@"{user.Email},{cc}";
                await _emailSender.SendAsync(
                    receipents,
                    StringLocalizer["NewOrResetPassword_Subject"],
                    emailContent
                );
            }
        }


        public async Task SendFarewellToDeletedUserAsync(string email, string name, string cc = null)
        {
            var emailContent = await _templateRenderer.RenderAsync(
                Templates.AccountEmailTemplates.FarewellDeletedUser,
                new
                {
                    name = name,
                    signature = DefaultEmailSignature
                }
            );

            var receipents = string.IsNullOrEmpty(cc) ? email : $@"{email},{cc}";

            if (IsBackgroundEmailEnabled)
            {
                await _emailSender.QueueAsync(
                    receipents,
                    StringLocalizer["FarewellDeletedUser_Subject"],
                    emailContent
                );
            }
            else
            {
                await _emailSender.SendAsync(
                    receipents,
                    StringLocalizer["FarewellDeletedUser_Subject"],
                    emailContent
                );
            }
        }
    }
}
