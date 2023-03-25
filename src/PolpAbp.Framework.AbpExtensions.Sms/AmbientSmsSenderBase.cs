namespace Volo.Abp.Sms;

public abstract class AmbientSmsSenderBase : IAmbientSmsSender
{

    protected readonly ISmsSender SmsSender;
    
    public SmsSendingContext SendingContext { get; } 

    public AmbientSmsSenderBase(ISmsSender smsSender)
    {
        SmsSender = smsSender;
        SendingContext = new SmsSendingContext();
    }

    public virtual Task AfterSendingAsync(SmsSendingContext context)
    {
        return Task.CompletedTask;
    }

    public virtual Task BeforeSendingAsync(SmsSendingContext context)
    {
        return Task.CompletedTask;
    }

    public virtual async Task SendAsync(SmsMessage smsMessage)
    {
        SendingContext.UpdateWith(smsMessage);

        await BeforeSendingAsync(SendingContext);
        if (SendingContext.ShouldStop)
        {
            return;
        }

        await SmsSender.SendAsync(smsMessage);
        await AfterSendingAsync(SendingContext);
    }

}

