using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Sms;

public interface IAmbientSmsSender : ISmsSender
{
    Task<bool> CanSendAsync();

    Task BeforeSendingAsync();

    Task AfterSendingAsync();
}

