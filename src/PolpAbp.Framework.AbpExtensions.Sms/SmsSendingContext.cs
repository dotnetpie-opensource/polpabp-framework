﻿using Volo.Abp.Data;

namespace Volo.Abp.Sms;

public class SmsSendingContext : IHasExtraProperties
{
    public bool ShouldStop { get; set; }

    public SmsMessage? SmsMessage { get; protected set; }

    public ExtraPropertyDictionary ExtraProperties { get; }

    public SmsSendingContext()
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }

    public void UpdateWith(SmsMessage smsMessage)
    {
        SmsMessage = smsMessage;
    }
}

