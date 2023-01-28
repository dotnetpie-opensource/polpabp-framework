﻿<h3>{{L "ApproveMemberRegistration_Title"}}</h3>

<p>
    {{L "GeneralEmailGreetingFirstLine" model.name}}
    <br />

    {{L "GeneralEmailGreetingSecondLine"}}
</p>

<p>
    {{L "ApproveMemberRegistration_Body" model.member}}
</p>

<p>
    {{model.link}}
</p>

<p>
    {{L "GeneralEmailClosing"}}
    <br />
    {{L "GeneralEmailSignature" model.signature}}
</p>
