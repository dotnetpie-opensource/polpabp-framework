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

    public virtual Task AfterSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task BeforeSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual async Task SendAsync(SmsMessage smsMessage)
    {
        SendingContext.UpdateWith(smsMessage);

        await BeforeSendingAsync();
        if (SendingContext.ShouldStop)
        {
            return;
        }

        await SmsSender.SendAsync(smsMessage);
        await AfterSendingAsync();
    }

}

