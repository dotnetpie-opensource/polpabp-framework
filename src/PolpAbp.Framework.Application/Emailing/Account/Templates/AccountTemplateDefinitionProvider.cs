﻿using Volo.Abp.Account.Localization;
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
            context.Add(
                           new TemplateDefinition(
                               AccountEmailTemplates.EmailActivationtLink,
                               displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.EmailActivationtLink}"),
                               layout: StandardEmailTemplates.Layout,
                               localizationResource: typeof(AccountResource)
                           ).WithVirtualFilePath("/Emailing/Account/Templates/EmailActivationLink.tpl", true)
                           .WithScribanEngine()
                       );

            context.Add(
                                   new TemplateDefinition(
                                       AccountEmailTemplates.NotyPasswordChange,
                                       displayName: LocalizableString.Create<AccountResource>($"TextTemplate:{AccountEmailTemplates.NotyPasswordChange}"),
                                       layout: StandardEmailTemplates.Layout,
                                       localizationResource: typeof(AccountResource)
                                   ).WithVirtualFilePath("/Emailing/Account/Templates/NotyPasswordChange.tpl", true)
                                   .WithScribanEngine()
                               );
        }
    }
}
