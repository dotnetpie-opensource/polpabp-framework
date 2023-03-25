namespace Volo.Abp.Sms;

public abstract class AmbientSmsSenderBase : IAmbientSmsSender
{

    protected readonly ISmsSender SmsSender;

    public AmbientSmsSenderBase(ISmsSender smsSender)
    {
        SmsSender = smsSender;
    }

    public virtual Task AfterSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task BeforeSendingAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task<bool> CanSendAsync()
    {
        return Task.FromResult(true);
    }

    public virtual async Task SendAsync(SmsMessage smsMessage)
    {
        var shouldStop = await CanSendAsync();
        if (shouldStop)
        {
            return;
        }

        await BeforeSendingAsync();
        await SmsSender.SendAsync(smsMessage);
        await AfterSendingAsync();
    }

}

