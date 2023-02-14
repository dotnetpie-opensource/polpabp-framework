using Volo.Abp.Account.Localization;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating.Scriban;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace PolpAbp.Framework.Emailing.Account.Templates
{
    public class AccountEmailTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(new TemplateDefinition(
                               AccountEmailTemplates.EmailActivationtLink,
                               displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.EmailActivationtLink}"),
                               layout: StandardEmailTemplates.Layout,
                               localizationResource: typeof(AccountResource)
                           ).WithVirtualFilePath("/Emailing/Account/Templates/EmailActivationLink.tpl", true)
                           .WithScribanEngine()
                       );

            context.Add(new TemplateDefinition(
                               AccountEmailTemplates.EmailConfirmationLink,
                               displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.EmailConfirmationLink}"),
                               layout: StandardEmailTemplates.Layout,
                               localizationResource: typeof(AccountResource)
                           ).WithVirtualFilePath("/Emailing/Account/Templates/EmailConfirmationLink.tpl", true)
                           .WithScribanEngine()
                       );

            context.Add(new TemplateDefinition(
                                       AccountEmailTemplates.NotyPasswordChange,
                                       displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.NotyPasswordChange}"),
                                       layout: StandardEmailTemplates.Layout,
                                       localizationResource: typeof(AccountResource)
                                   ).WithVirtualFilePath("/Emailing/Account/Templates/NotyPasswordChange.tpl", true)
                                   .WithScribanEngine()
                               );
            context.Add(new TemplateDefinition(
                                       AccountEmailTemplates.TwoFactorCode,
                                       displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.TwoFactorCode}"),
                                       layout: StandardEmailTemplates.Layout,
                                       localizationResource: typeof(AccountResource)
                                   ).WithVirtualFilePath("/Emailing/Account/Templates/TwoFactorCode.tpl", true)
                                   .WithScribanEngine()
                               );
            context.Add(new TemplateDefinition(
                                         AccountEmailTemplates.ApproveMembereRegistration,
                                         displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.ApproveMembereRegistration}"),
                                         layout: StandardEmailTemplates.Layout,
                                         localizationResource: typeof(AccountResource)
                                     ).WithVirtualFilePath("/Emailing/Account/Templates/ApproveMemberRegistration.tpl", true)
                                     .WithScribanEngine()
                                 );
            context.Add(new TemplateDefinition(
                                         AccountEmailTemplates.NotyMembereRegistration,
                                         displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.NotyMembereRegistration}"),
                                         layout: StandardEmailTemplates.Layout,
                                         localizationResource: typeof(AccountResource)
                                     ).WithVirtualFilePath("/Emailing/Account/Templates/NotyMemberRegistration.tpl", true)
                                     .WithScribanEngine()
                                 );
            context.Add(new TemplateDefinition(
                                         AccountEmailTemplates.NewOrResetPassword,
                                         displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.NewOrResetPassword}"),
                                         layout: StandardEmailTemplates.Layout,
                                         localizationResource: typeof(AccountResource)
                                     ).WithVirtualFilePath("/Emailing/Account/Templates/NewOrResetPassword.tpl", true)
                                     .WithScribanEngine()
                                 );
            context.Add(new TemplateDefinition(
                                         AccountEmailTemplates.FarewellDeletedUser,
                                         displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.FarewellDeletedUser}"),
                                         layout: StandardEmailTemplates.Layout,
                                         localizationResource: typeof(AccountResource)
                                     ).WithVirtualFilePath("/Emailing/Account/Templates/FarewellDeletedUser.tpl", true)
                                     .WithScribanEngine()
                                 );
        }
    }
}
