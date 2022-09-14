﻿<h3>{{L "TwoFactorCode_Title"}}</h3>

<p>
    {{L "GeneralEmailGreetingFirstLine" model.name}}
    <br />

    {{L "GeneralEmailGreetingSecondLine"}},
</p>

<div>
    <p>
        {{L "TwoFactorCode_Body"}}
    </p>
    <p>
        <span>{{model.code}}</span>,
    </p>
</div>

<p>
    {{L "GeneralEmailClosing"}}
    <br />
    {{L "GeneralEmailSignature" model.sigature}}
</p>
