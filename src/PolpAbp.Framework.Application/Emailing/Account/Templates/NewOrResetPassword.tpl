<h3>{{L "NewOrResetPassword_Title"}}</h3>

<p>
    {{L "GeneralEmailGreetingFirstLine" model.name}}
    <br />

    {{L "GeneralEmailGreetingSecondLine"}},
</p>

<div>
    <p>
        {{L "NewOrResetPassword_Body"}}
    </p>
    <p>
        <span>{{model.password}}</span>,
    </p>
</div>

<p>
    {{L "GeneralEmailClosing"}}
    <br />
    {{L "GeneralEmailSignature" model.signature}}
</p>