﻿<h3>{{L "EmailConfirmation_Title"}}</h3>
 <h4>{{L "EmailConfirmation_SubTitle"}}</h4>

<p>
    {{L "GeneralEmailGreetingFirstLine" model.name}}
    <br />

    {{L "GeneralEmailGreetingSecondLine"}},
</p>

<p>
    <b>{{L "TenancyName"}}</b>: <span>{{model.tenancy}}</span>
</p>

<div>
    <a href="{{model.link}}">{{L "EmailConfirmation_ClickTheLinkBelowToConfirmYourEmail"}}</a>
    <p>
        {{L "EmailMessage_CopyTheLinkBelowToYourBrowser"}} <span>{{model.link}}</span>
    </p>
</div>

<p>
    {{L "GeneralEmailClosing"}}
    <br />
    {{L "GeneralEmailSignature" model.signature}}
</p>
